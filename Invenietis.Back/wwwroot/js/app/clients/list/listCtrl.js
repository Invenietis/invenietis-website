angular.module('invenietis.app.clients').controller('ListCtrl', ['$scope', 'Clients', function ($scope, Clients) {
    var vm = this;

    this.clients = [];

    function init() {
        Clients.getAll(function (result) {
            vm.clients = result;
        });
    }

    this.remove = function (client) {
        var confirm = window.confirm('Confirmer la suppression ?');
        if (confirm) {
            Clients.remove({ id: client.ClientId }, function (result) {
                vm.clients.splice(vm.clients.indexOf(client), 1);
            }, function (error) {
                alert('Une erreur est survenue');
            });
        }
    }

    init();
}]);