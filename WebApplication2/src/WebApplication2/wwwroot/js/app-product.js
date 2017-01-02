(function () {
    "use strict";
    angular.module("app-product", ["ngRoute"])
    .config(function ($routeProvider) {
        $routeProvider.when('/', {
            controller: "productController",
            controllerAs: "vm",
            templateUrl: "/views/mainProductInfo.html"
        });

        $routeProvider.otherwise({ redirectTo: "/" });

    });

})();