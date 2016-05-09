angular.module('invenietis.app.common')
    .directive('translation', function () {
        var controller = ['$scope', function ($scope) {
            var availableCultures = Invenietis.Cultures.SupportedCultures;

            $scope.getFlag = function (culture) {
                return '/img/flags/' + culture.Id + '-big.png';
            }

            $scope.selector.lang1 = availableCultures[0];
            $scope.selector.lang2 = availableCultures[1];

            $scope.selector.getCulturedItem1 = function () {
                if ($scope.item && $scope.item.Cultures) {
                    return $scope.item.Cultures[$scope.selector.lang1.Id];
                }
            }

            $scope.selector.getCulturedItem2 = function () {
                if ($scope.item && $scope.item.Cultures) {
                    return $scope.item.Cultures[$scope.selector.lang2.Id];
                }
            }

            $scope.selector.availableCulturesLang1 = function () {
                return _.filter(availableCultures, function (c) {
                    return c != $scope.selector.lang2;
                });
            }

            $scope.selector.availableCulturesLang2 = function () {
                return _.filter(availableCultures, function (c) {
                    return c != $scope.selector.lang1;
                });
            }
        }];

        return {
            scope: {
                selector: '=',
                item: '=',
            },
            templateUrl: '/js/app/common/directives/translation/translation.html',
            controller: controller
        };
    });