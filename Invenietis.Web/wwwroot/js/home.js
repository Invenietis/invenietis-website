var Home = function() {
    var _jNavbar;
    var _jBottomArrow;
    var _jHtmlBody;
    var _jWindow;
    var _jFirstPage;
    var _jSecondPage;
    var _scrollHandler;

    var _currentPage = 1;
    var _windowCurrentSize;
    var _isChangingPage = false;

    /* When DOM is ready */
    $(function() {
        _jBottomArrow = $("#bottom-arrow");
        _jHtmlBody = $('html, body');
        _jNavbar = $('#navbar');
        _jWindow = $(window);
        _jFirstPage = $("#full-banner");
        _jSecondPage = $("#page");
        _scrollHandler = new Scroll();

        onWindowSizeChanged();

        window.addEventListener('resize', function() {
            onWindowSizeChanged();
        });

        _scrollHandler.onScrollUp(onScrollUp);
        _scrollHandler.onScrollDown(onScrollDown);

        enableWhatWeDoBackground();
        enableFullBannerBackground();
        enableBottomArrowScroll();
    });

    function setPage() {
        _currentPage = getCurrentPage();

        setNavbarDisplay();
    }

    function onScrollUp() {
        if (!_isChangingPage && _currentPage == 2 && _jSecondPage.scrollTop() == 0) {
            scrollToFirstPage();
        }
    }

    function scrollToFirstPage() {
        _isChangingPage = true;

        hideNavbar();
        hideSecondPage();
        showFirstPage();

        _jHtmlBody.animate({
            scrollTop: 0
        }, 500, "swing", function() {
            showNavbar();
            setPage();
            _isChangingPage = false;
        });
    }

    function onScrollDown() {
        if (!_isChangingPage && _currentPage == 1) {
            scrollToSecondPage();
        }
    }

    function scrollToSecondPage() {
        _isChangingPage = true;

        hideNavbar();
        showSecondPage();

        _jHtmlBody.animate({
            scrollTop: window.innerHeight
        }, 500, "swing", function() {
            setPage();
            showNavbar();
            hideFirstPage();
            _isChangingPage = false;
        });
    }

    function getPageOffsetTop() {
        return window.innerHeight - _jNavbar.height();
    }

    function getCurrentPage() {       
        // Page 1 only exists on screens > XS
        if (ResponsiveUtilities.getSize() > 0) {
            return (document.documentElement.scrollTop || document.body.scrollTop) <= getPageOffsetTop() ? 1 : 2;
        }

        return 2;
    }

    function onWindowSizeChanged() {
        var oldSize = _windowCurrentSize;
        _windowCurrentSize = ResponsiveUtilities.getSize();

        if (oldSize != _windowCurrentSize) {
            if (_windowCurrentSize > 0) {
                _scrollHandler.enable();
            }
            else {
                _scrollHandler.disable();
            }
        }

        setPage();
    }

    function showNavbar() {
        _jNavbar.animate({ opacity: 1 }, 150, 'linear');

    }

    function hideNavbar() {
        _jNavbar.animate({ opacity: 0 }, 200, 'linear');
    }

    function showFirstPage() {
        _jFirstPage.show();

    }

    function hideFirstPage() {
        _jFirstPage.hide();
    }

    function showSecondPage() {
        _jSecondPage.fadeIn(500);

    }

    function hideSecondPage() {
        _jSecondPage.fadeOut(500);
    }

    function setNavbarDisplay() {
        if (_currentPage == 1) {
            if (_jNavbar.hasClass("white")) _jNavbar.removeClass('white');
        }
        else {
            if (!_jNavbar.hasClass("white")) _jNavbar.addClass("white");
        }
    }

    /* Full banner polygons background */
    function enableFullBannerBackground() {
        var config = Invenietis.ShaderConfigFactory();

        config.background.LIGHT.ambient = "#0c0c0c";
        config.background.LIGHT.diffuse = "#141414";

        FSS.Helpers.initShader(config, "bg-full-banner", "bg-full-banner-output");
    }

    /* What We Do polygons background */
    function enableWhatWeDoBackground() {
        var config = Invenietis.ShaderConfigFactory();

        config.background.MESH.ambient = "#555555";
        config.background.MESH.diffuse = "#ffffff";

        config.background.LIGHT.ambient = "#880066";
        config.background.LIGHT.diffuse = "#ff8800";
        config.background.LIGHT.zOffset = 20;

        FSS.Helpers.initShader(config, "bg-what-we-do", "bg-what-we-do-output");
    }

    /* Click handler for scroll arrow */
    function enableBottomArrowScroll() {
        _jBottomArrow.click(function() {
            scrollToSecondPage();
        });
    }
}

Invenietis.Home = new Home();