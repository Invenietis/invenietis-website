angular.module('invenietis.app.clients').controller('EditCtrl', ['$scope', '$stateParams', '$state', 'Clients', function ($scope, $stateParams, $state, Clients) {
    var vm = this;

    this.client = null;
    this.translation = {};

    function init() {
        Clients.edit({ id: $stateParams.id }, function (r) {
            vm.client = r;
        });
    }

    this.save = function () {
        Clients.save(vm.client, function (r) {
            alert('Sauvegarde effectuée.');
            $state.go('app.clients.list');
        });
    }

    init();
}]);