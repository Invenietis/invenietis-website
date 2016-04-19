//============================================================   
//
// Flat Surface Shader
//
// Copyright (C) 2013 Matthew Wagerfield
//
// Twitter: https://twitter.com/mwagerfield
//
// Permission is hereby granted, free of charge, to any
// person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the
// Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice
// shall be included in all copies or substantial portions
// of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY
// OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO
// EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN
// AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE
// OR OTHER DEALINGS IN THE SOFTWARE.
//
//============================================================

FSS.Helpers = {
    initShader: function (config, containerId, outputId) {
        var container = document.getElementById(containerId);
        var output = document.getElementById(outputId);

        if (!config.background.enabled) {
            return;
        }

        config.background.LIGHT.bounds = FSS.Vector3.create(),
        config.background.LIGHT.step = FSS.Vector3.create(
            Math.randomInRange(0.2, 1.0),
            Math.randomInRange(0.2, 1.0),
            Math.randomInRange(0.2, 1.0)
            )

        //------------------------------
        // Global Properties
        //------------------------------
        var now, start = Date.now();
        var center = FSS.Vector3.create();
        var attractor = FSS.Vector3.create();
        var renderer, scene, mesh, geometry, material;
        var webglRenderer, canvasRenderer, svgRenderer;

        //------------------------------
        // Methods
        //------------------------------
        function initialise() {
            applyRatios(container.offsetWidth, container.offsetHeight);
            createRenderer();
            createScene();
            createMesh();
            createLights();
            addEventListeners();
            resize(container.offsetWidth, container.offsetHeight);
            animate();
        }

        function createRenderer() {
            webglRenderer = new FSS.WebGLRenderer();
            canvasRenderer = new FSS.CanvasRenderer();
            svgRenderer = new FSS.SVGRenderer();
            setRenderer(config.background.RENDER.renderer);
        }

        function setRenderer(index) {
            if (renderer) {
                output.removeChild(renderer.element);
            }

            renderer = canvasRenderer;
            renderer.setSize(container.offsetWidth, container.offsetHeight);
            output.appendChild(renderer.element);
        }

        function createScene() {
            scene = new FSS.Scene();
        }

        function createMesh() {
            scene.remove(mesh);
            renderer.clear();
            geometry = new FSS.Plane(config.background.MESH.width * renderer.width, config.background.MESH.height * renderer.height, config.background.MESH.segments, config.background.MESH.slices);
            material = new FSS.Material(config.background.MESH.ambient, config.background.MESH.diffuse);
            mesh = new FSS.Mesh(geometry, material);
            scene.add(mesh);

            // Augment vertices for animation
            var v, vertex;
            for (v = geometry.vertices.length - 1; v >= 0; v--) {
                vertex = geometry.vertices[v];
                vertex.anchor = FSS.Vector3.clone(vertex.position);
                vertex.step = FSS.Vector3.create(
                    Math.randomInRange(0.2, 1.0),
                    Math.randomInRange(0.2, 1.0),
                    Math.randomInRange(0.2, 1.0)
                    );
                vertex.time = Math.randomInRange(0, Math.PIM2);
            }
        }

        function createLights() {
            var l, light;
            for (l = scene.lights.length - 1; l >= 0; l--) {
                light = scene.lights[l];
                scene.remove(light);
            }
            renderer.clear();
            for (l = 0; l < config.background.LIGHT.count; l++) {
                light = new FSS.Light(config.background.LIGHT.ambient, config.background.LIGHT.diffuse);
                light.ambientHex = light.ambient.format();
                light.diffuseHex = light.diffuse.format();
                scene.add(light);

                // Augment light for animation
                light.mass = Math.randomInRange(0.5, 1);
                light.velocity = FSS.Vector3.create();
                light.acceleration = FSS.Vector3.create();
                light.force = FSS.Vector3.create();

                // Ring SVG Circle
                light.ring = document.createElementNS(FSS.SVGNS, 'circle');
                light.ring.setAttributeNS(null, 'stroke', light.ambientHex);
                light.ring.setAttributeNS(null, 'stroke-width', '0.5');
                light.ring.setAttributeNS(null, 'fill', 'none');
                light.ring.setAttributeNS(null, 'r', '10');

                // Core SVG Circle
                light.core = document.createElementNS(FSS.SVGNS, 'circle');
                light.core.setAttributeNS(null, 'fill', light.diffuseHex);
                light.core.setAttributeNS(null, 'r', '4');
            }
        }
        
        function applyRatios(width, height) {
            config.background.MESH.width = (config.background.MESH.widthRatio / width) * config.background.MESH.baseWidth;   
            config.background.MESH.height = (config.background.MESH.heightRadio / height) * config.background.MESH.baseHeight;   
        }

        function resize(width, height) {
            renderer.setSize(width, height);
            FSS.Vector3.set(center, renderer.halfWidth, renderer.halfHeight);
            createMesh();
        }

        function animate() {
            now = Date.now() - start;
            update();
            render();          
            if(!config.static) requestAnimationFrame(animate);      
        }

        function update() {
            var ox, oy, oz, l, light, v, vertex, offset = config.background.MESH.depth / 2;

            // Update Bounds
            FSS.Vector3.copy(config.background.LIGHT.bounds, center);
            FSS.Vector3.multiplyScalar(config.background.LIGHT.bounds, config.background.LIGHT.xyScalar);

            // Update Attractor
            FSS.Vector3.setZ(attractor, config.background.LIGHT.zOffset);

            // Overwrite the Attractor position
            if (config.background.LIGHT.autopilot) {
                ox = Math.sin(config.background.LIGHT.step[0] * now * config.background.LIGHT.speed);
                oy = Math.cos(config.background.LIGHT.step[1] * now * config.background.LIGHT.speed);
                FSS.Vector3.set(attractor,
                    config.background.LIGHT.bounds[0] * ox,
                    config.background.LIGHT.bounds[1] * oy,
                    config.background.LIGHT.zOffset);
            }

            // Animate Lights
            for (l = scene.lights.length - 1; l >= 0; l--) {
                light = scene.lights[l];

                // Reset the z position of the light
                FSS.Vector3.setZ(light.position, config.background.LIGHT.zOffset);

                // Calculate the force Luke!
                var D = Math.clamp(FSS.Vector3.distanceSquared(light.position, attractor), config.background.LIGHT.minDistance, config.background.LIGHT.maxDistance);
                var F = config.background.LIGHT.gravity * light.mass / D;
                FSS.Vector3.subtractVectors(light.force, attractor, light.position);
                FSS.Vector3.normalise(light.force);
                FSS.Vector3.multiplyScalar(light.force, F);

                // Update the light position
                FSS.Vector3.set(light.acceleration);
                FSS.Vector3.add(light.acceleration, light.force);
                FSS.Vector3.add(light.velocity, light.acceleration);
                FSS.Vector3.multiplyScalar(light.velocity, config.background.LIGHT.dampening);
                FSS.Vector3.limit(light.velocity, config.background.LIGHT.minLimit, config.background.LIGHT.maxLimit);
                FSS.Vector3.add(light.position, light.velocity);
            }

            // Animate Vertices
            for (v = geometry.vertices.length - 1; v >= 0; v--) {
                vertex = geometry.vertices[v];
                ox = Math.sin(vertex.time + vertex.step[0] * now * config.background.MESH.speed);
                oy = Math.cos(vertex.time + vertex.step[1] * now * config.background.MESH.speed);
                oz = Math.sin(vertex.time + vertex.step[2] * now * config.background.MESH.speed);
                FSS.Vector3.set(vertex.position,
                    config.background.MESH.xRange * geometry.segmentWidth * ox,
                    config.background.MESH.yRange * geometry.sliceHeight * oy,
                    config.background.MESH.zRange * offset * oz - offset);
                FSS.Vector3.add(vertex.position, vertex.anchor);
            }

            // Set the Geometry to dirty
            geometry.dirty = true;
        }

        function render() {
            renderer.render(scene);
        }

        function addEventListeners() {
            window.addEventListener('resize', onWindowResize);
            container.addEventListener('click', onMouseClick);
            container.addEventListener('mousemove', onMouseMove);
        }

        //------------------------------
        // Callbacks
        //------------------------------

        function onMouseClick(event) {
            if (config.background.LIGHT.followMouseClicks) {
                FSS.Vector3.set(attractor, event.x, renderer.height - event.y);
                FSS.Vector3.subtract(attractor, center);
                config.background.LIGHT.autopilot = !config.background.LIGHT.autopilot;
            }
        }

        function onMouseMove(event) {
            FSS.Vector3.set(attractor, event.x, renderer.height - event.y);
            FSS.Vector3.subtract(attractor, center);
        }

        function onWindowResize(event) {
            initialise();
        }

        // Let there be light!
        initialise();
    }
};