angular.module('invenietis.app.common')
    .factory('Clients', ['$resource', function ($resource) {
        return $resource('', {},
             {
                 getAll: { method: 'GET', url: '/Clients/GetAll', isArray: true },
                 get: { method: 'GET', url: '/Clients/Edit/:id' },
                 save: { method: 'POST', url: '/Clients/Save' },
                 remove: { method: 'DELETE', url: '/Clients/Delete/:id' },
             });
    }]);