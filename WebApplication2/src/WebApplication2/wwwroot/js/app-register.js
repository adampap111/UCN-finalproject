(function () {
    "use strict";
    angular.module("app-register", ["ngRoute"])
    .config(function ($routeProvider) {
        $routeProvider.when('/', {
            controller: "registerController",
            controllerAs: "vm",
            templateUrl: "/views/register.html"
        });
           
        $routeProvider.otherwise({ redirectTo: "/" });
     
    });

})();