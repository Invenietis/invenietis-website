var Scroll = function () {
    // left: 37, up: 38, right: 39, down: 40,
    // spacebar: 32, pageup: 33, pagedown: 34, end: 35, home: 36
    var _upKeys = [38, 36, 33];
    var _downKeys = [40, 32, 34, 35];
    var _onScrollUp;
    var _onScrollDown;
    var _touchStartY = 0;

    function addListeners() {
        if (window.addEventListener) { // All browsers except IE before version 9
            window.addEventListener("mousewheel", onMouseWheel, false);
            window.addEventListener("DOMMouseScroll", onDOMMouseScroll, false);
            document.addEventListener("keydown", onKeyDown, false);
            window.addEventListener("touchstart", onTouchStart, false);
            window.addEventListener("touchmove", onTouchMove, false);
        }
        else {
            if (window.attachEvent) { // IE before version 9
                window.attachEvent("onmousewheel", onMouseWheel);
                document.attachEvent("onkeydown", onKeyDown);
            }
        }
    }

    function removeListeners() {
        if (window.removeEventListener) {
            window.removeEventListener("mousewheel", onMouseWheel, false);
            window.removeEventListener("DOMMouseScroll", onDOMMouseScroll, false);
            document.removeEventListener("keydown", onKeyDown, false);
            window.removeEventListener("touchstart", onTouchStart, false);
            window.removeEventListener("touchmove", onTouchMove, false);
        }
        else {
            if (window.detachEvent) {
                window.detachEvent("onmousewheel", onMouseWheel);
                document.detachEvent("onkeydown", onKeyDown);
            }
        }
    }

    function onDOMMouseScroll(e) {
        var wheelDeltaY = -40 * e.detail;

        if (wheelDeltaY > 0) _onScrollUp();
        else _onScrollDown();
    }

    function onMouseWheel(e) {
        if (e.wheelDeltaY > 0 || e.wheelDelta > 0) _onScrollUp();
        else _onScrollDown();
    }

    function onTouchStart(e) {
        _touchStartY = e.originalEvent.touches[0].clientY;
    }

    function onTouchMove(e) {
        var moved = e.originalEvent.changedTouches[0].clientY;

        if (_touchStartY > moved) _onScrollDown();
        else _onScrollUp();

        _touchStartY = 0;
    }

    function onKeyDown(e) {
        if (_upKeys.indexOf(e.keyCode) > -1) _onScrollUp();
        if (_downKeys.indexOf(e.keyCode) > -1) _onScrollDown();
    }

    this.enable = function () {
        addListeners();
    }

    this.disable = function () {
        removeListeners();
    }

    this.onScrollUp = function (callback) {
        _onScrollUp = callback;
    }

    this.onScrollDown = function (callback) {
        _onScrollDown = callback;
    }
}