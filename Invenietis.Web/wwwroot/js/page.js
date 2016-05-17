var Page = function () {
    function enableBannerPolygons() {
        var containers = $("[data-role='polygons-container']");

        if (containers.length) containers.each(function (i) {
            var container = $(this);
            var output = container.find("[data-role='polygons-output']");

            if (output.length) {
                var containerId = container.attr('id');
                var outputId = output.attr('id');

                if (containerId && outputId) {
                    var config = Invenietis.ShaderConfigFactory();

                    config.background.MESH.ambient = container.data('meshAmbient') || config.background.MESH.ambient;
                    config.background.MESH.diffuse = container.data('meshDiffuse') || config.background.MESH.diffuse;

                    config.background.LIGHT.ambient = container.data('lightAmbient') || config.background.LIGHT.ambient;
                    config.background.LIGHT.diffuse = container.data('lightDiffuse') || config.background.LIGHT.diffuse;
                    config.background.LIGHT.zOffset = container.data('lightZoffset') || config.background.LIGHT.zOffset;

                    FSS.Helpers.initShader(config, containerId, outputId);
                }
                else {
                    console.error("Container and Output must have an Id");
                }
            }
            else {
                console.error('A polygon container must contain a polygon output');
            }
        });
    }

    function enableBackgroundImages() {
        var els = $("[data-bg-img]");
        if (els.length) els.each(function (i) {
            var el = $(this);
            var path = el.data('bgImg');
            var align = el.data('bgAlign');

            if (align) el.css('background-position', align);
            el.css('background-image', "url('" + path + "')");
        });
    }

    function enableLightbox() {
        lightbox.init();

        lightbox.option({
            'resizeDuration': 300,
            'fadeDuration': 300
        })
    }

    $(function () {
        enableBannerPolygons();
        enableBackgroundImages();
        enableLightbox();
    });
}

Invenietis.Page = new Page();