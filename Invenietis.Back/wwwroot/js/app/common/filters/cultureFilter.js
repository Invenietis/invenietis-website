angular.module('invenietis.app.common').filter('cultured', function () {
    return function (input) {
        return input ? CultureResolver(input, String.IsNotNullOrEmpty) : '';
    };
});