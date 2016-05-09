angular.module('invenietis.app', ['ui.router', 'invenietis.app.common', 'invenietis.app.clients'])
    .config(['$stateProvider', '$urlRouterProvider', '$locationProvider', function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $locationProvider.html5Mode({ enabled: true, requireBase: false });
        $urlRouterProvider.otherwise('/admin');

        $stateProvider.state('app', {
            url: '/admin',
            abstract: true,
            template: '<ui-view/>',
            app: 'invenietis.app'
        });
    }])
    .controller('HomeCtrl', ['$scope', function ($scope) {
        console.log('home');
    }]);