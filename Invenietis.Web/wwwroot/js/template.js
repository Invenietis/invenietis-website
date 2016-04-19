var Invenietis = {};

Invenietis.ShaderConfigFactory = function() {
    return {
        static: true,
        background: {
            enabled: true,

            RENDER: {
                // Takes all the information in a Scene and renders it to a context.
                // A Scene sits at the very top of the stack. It simply manages arrays of Mesh & Light objects.
                renderer: 'canvas'
            },

            MESH: {
                ambient: '#555555', // Default 
                diffuse: '#FFFFFF', // Default
                baseWidth: 1.2, // Triangle Width
                widthRatio: 1920,
                baseHeight: 1.2, // Triangle Height
                heightRadio: 960,
                depth: 10, // Transparency of the triangles
                segments: 16, // Number of triangles to display in 1 row
                slices: 8, // Number of triangles to display in 1 column
                xRange: 0.8, // Wideness of the triangles in X Position
                yRange: 0.1, // Wideness of the triangles in Y Position
                zRange: 1.0, // Wideness of the triangles in Z Position
                speed: 0.0005 // Speed of the moving traingles
            },

            LIGHT: {
                autopilot: false, // Set this to true if you want the light to follow your mouse cursor
                ambient: '#880066',
                diffuse: '#ff8800',
                count: 2, // Contrast 
                zOffset: 100,

                xyScalar: 1,
                speed: 0.001,
                gravity: 1200,
                dampening: 0.15,
                minLimit: 8,
                maxLimit: null,
                minDistance: 20,
                maxDistance: 400
            }
        }
    }
}

// Prevent anchor from jumping to window top
function preventJumpingHash() {
    $('a[href="#"]').click(function(e) {
        e.preventDefault();
    });
}

function chronoLayout() {
    var els = $("[data-role='chrono-layout']");

    els.each(function(i) {
        var layout = $(this);
        var points = layout.find("[data-role='point']");
        var links = layout.find("[data-role='steplink']");

        // Reset existing margins when resize
        links.each(function(i) {
            $(this).css({
                'margin-top': 0 + 'px',
                'margin-bottom': 0 + 'px'
            });
        });

        for (var i = 0; i < links.length; i++) {
            var link = $(links[i]);
            var prev = $(points[i]);
            var next = $(points[i + 1]);

            var prevY = prev.position().top + prev.height();
            var nextY = next.position().top;

            // Good size for the separator
            var diffPrevNext = nextY - prevY;
            var linkHeight = link.height();
            link.height((diffPrevNext * 50) / 100);

            prevY = prev.position().top + prev.height();
            nextY = next.position().top;

            var linkYTop = link.position().top;
            var linkYBottom = link.position().top + link.height();

            // Centering
            var diffPrev = 25 - (linkYTop - prevY);
            var diffNext = 25 - (nextY - linkYBottom);

            link.css({ 'margin-top': diffPrev + "px" });
            link.css({ 'margin-bottom': diffNext + "px" });
        }
    });
}

function equalHeight() {
    var containers = $("[data-role='equal-height']");

    containers.each(function(i) {
        var container = $(this);

        var els = container.find("[data-role='equal-element']");

        var maxHeight = 0;

        els.each(function(i2) {
            var h = $(this).height();
            if (h > maxHeight) maxHeight = h;
        });

        els.each(function(i2) {
            $(this).height(maxHeight);
        });
    });
}

$(function() {
    preventJumpingHash();
    chronoLayout();
    equalHeight();
    
    $(window).on('resize', function() {
        chronoLayout();
        equalHeight();
    });
});