/// <reference path="lib/angular/angular.js" />
(function () {
    "use strict";
    angular.module("app-product")
    .controller("productController", productController);

    function productController($http, $scope) {
        var vm = this;
        $scope.products = [];
        $scope.cartItems = [];
      
        vm.errorMessage = "";
        vm.isBusy = true;
        var url = "/api/";
        var urlCartItems = "/cart/";

        $http.get(url)
        .then(function (response) {
            //success
            angular.copy(response.data, $scope.products);

        }, function (error) {
            //failure
            vm.errorMessage = "Failed to load data" + error;

        })
        .finally(function () {
            vm.isBusy = false;
        });

        //get all cart items
        $http.get(urlCartItems)
        .then(function (response) {
            //success
            angular.copy(response.data, $scope.cartItems);
        }, function (error) {
            //failure
            vm.errorMessage = "Failed to load data" + error;
        })
        .finally(function () {
            vm.isBusy = false;
        });

        vm.addToCart = function (id) {
            vm.isBusy = true;
            vm.errorMessage = "";
            vm.quantity(id);
            $scope.cartItems.push(id);
            $('.shop-badge .badge').text($scope.cartItems.length);

            $http.post("/cart/AddToCart", $scope.products[id])
            .then(function (response) {
                //success

            }, function (error) {
                //failure
                vm.errorMessage = error;
            }).finally(function () {
                vm.isBusy = false;
            });
        };
        vm.quantity = function (i) {
            $scope.products[i].quantity = 1;
        };

    }

})();