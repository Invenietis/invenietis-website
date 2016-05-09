angular.module('invenietis.app.clients', ['ui.router', 'invenietis.app.common']).config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $stateProvider.state('app.clients', {
        url: '/clients',
        abstract: true,
        template: '<ui-view/>',
        app: 'invenietis.app.clients'
    });

    $stateProvider
        .state('app.clients.list', {
            url: '',
            templateUrl: '/js/app/clients/list/list.html',
            controller: 'ListCtrl as ctrl',
        });

    $stateProvider
        .state('app.clients.edit', {
            url: '/{id:int}',
            templateUrl: '/js/app/clients/edit/edit.html',
            controller: 'EditCtrl as ctrl',
            params: { id: 0 }
        });
}]);