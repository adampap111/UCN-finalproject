(function () {
    "use strict";
    angular.module("app-shoppingCart", ["ngRoute", 'ui.bootstrap'])
    .config(function ($routeProvider) {
        $routeProvider.when('/', {
            controller: "shoppingCartController",
            controllerAs: "vm",
            templateUrl: "/views/shoppingCart.html"
        });

        $routeProvider
              .when("/submit/", {
                  controller: "shoppingCartController",
                  controllerAs: "vm",
                  templateUrl: "/views/shoppingCartSubmit.html"
              });

        $routeProvider.otherwise({ redirectTo: "/" });

    });

})();