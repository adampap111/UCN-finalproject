(function () {
    "use strict";
    angular.module("app-profilePage", ["ngRoute"])
    .config(function ($routeProvider, $locationProvider) {

        $routeProvider.when("/", {
            controller: "profilePageController",
            controllerAs: "vm",
            templateUrl: "/views/profilePage.html"
        });
        
        $routeProvider.otherwise({ redirectTo: "/" });
      
    });
   

})();