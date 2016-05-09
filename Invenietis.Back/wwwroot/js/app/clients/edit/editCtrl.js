angular.module('invenietis.app.clients').controller('EditCtrl', ['$scope', '$stateParams', 'Clients', function ($scope, $stateParams, Clients) {
    var vm = this;

    console.log('clients.edit', $stateParams);

    this.client = null;
    this.translation = {};

    function init() {
        Clients.get({ id: $stateParams.id }, function (r) {
            vm.client = r;
        });
    }

    this.save = function () {
        Clients.save(vm.client, function (r) {
            console.log(r);
        });
    }

    init();
}]);