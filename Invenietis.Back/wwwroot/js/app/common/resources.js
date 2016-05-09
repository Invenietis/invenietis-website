angular.module('invenietis.app.common')
    .factory('Clients', ['$resource', function ($resource) {
        return $resource('', {},
             {
                 getAll: { method: 'GET', url: '/Clients/GetAll', isArray: true },
                 edit: { method: 'GET', url: '/Clients/Edit/:id' },
                 save: { method: 'POST', url: '/Clients/Save' },
                 remove: { method: 'DELETE', url: '/Clients/Delete/:id' },
             });
    }])
    .factory('Learnings', ['$resource', function ($resource) {
        return $resource('', {},
             {
                 getAll: { method: 'GET', url: '/Learning/GetAll', isArray: true },
                 edit: { method: 'GET', url: '/Learning/Edit/:id' },
                 save: { method: 'POST', url: '/Learning/Save' },
                 remove: { method: 'DELETE', url: '/Learning/Delete/:id' },
             });
    }])
    .factory('LearningCategories', ['$resource', function ($resource) {
        return $resource('', {},
             {
                 getAll: { method: 'GET', url: '/Learning/GetAllCategories', isArray: true },
                 edit: { method: 'GET', url: '/Learning/EditCategory/:id' },
                 save: { method: 'POST', url: '/Learning/SaveCategory' },
                 remove: { method: 'DELETE', url: '/Learning/DeleteCategory/:id' },
             });
    }])
    .factory('Projects', ['$resource', function ($resource) {
        return $resource('', {},
             {
                 getAll: { method: 'GET', url: '/Projects/GetAll', isArray: true },
                 edit: { method: 'GET', url: '/Projects/Edit/:id' },
                 save: { method: 'POST', url: '/Projects/Save' },
                 remove: { method: 'DELETE', url: '/Projects/Delete/:id' },
             });
    }])
    .factory('ProjectCategories', ['$resource', function ($resource) {
        return $resource('', {},
             {
                 getAll: { method: 'GET', url: '/Projects/GetAllCategories', isArray: true },
                 edit: { method: 'GET', url: '/Projects/EditCategory/:id' },
                 save: { method: 'POST', url: '/Projects/SaveCategory' },
                 remove: { method: 'DELETE', url: '/Projects/DeleteCategory/:id' },
             });
    }]);