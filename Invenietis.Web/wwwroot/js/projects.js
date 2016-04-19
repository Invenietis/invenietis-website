var Projects = function() {
    var selectors = [];
    var currentSelector;

    function getSelectors() {
        var els = $("[data-role='selector']").each(function(i) {
            selectors.push($(this).data('value'));

            $(this).click(function() {
                setCurrentSelector($(this));
            });
        });

        currentSelector = 'all';
    }

    function resetSelectedSelectors() {
        var els = $("[data-role='selector']").each(function(i) {
            $(this).removeClass('selected');
        });
    }

    function setCurrentSelector(el) {
        resetSelectedSelectors();
        el.addClass('selected');
        currentSelector = el.data('value');
        filter();
    }

    function filter() {
        var thumbnails = $("[data-role='thumbnail']");

        if (currentSelector == 'all') {
            thumbnails.each(function() {
                $(this).show(400);
            });
        }
        else {
            thumbnails.each(function(i) {
                var el = $(this);
                var type = el.data('type');

                if (type != currentSelector) el.hide(400);
            });

            thumbnails.each(function(i) {
                var el = $(this);
                var type = el.data('type');

                if (type == currentSelector) el.show(400);
            });
        }        
    }

    $(function() {
        getSelectors();
    });
}

Invenietis.Projects = new Projects();