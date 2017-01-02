(function () {
    "use strict";
    angular.module("app-contactPage", ["ngRoute"])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when("/", {
            controller: "contactPageController",
            controllerAs: "vm",
            templateUrl: "/views/contactPage.html"
        });
        $routeProvider.otherwise({ redirectTo: "/" });
    });
})();