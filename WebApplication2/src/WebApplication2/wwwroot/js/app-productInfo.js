/// <reference path="lib/angular/angular.js" />
(function () {
    "use strict";
    angular.module("app-productInfo", ["ngRoute"])
    .config(function ($routeProvider, $locationProvider) {

        $routeProvider.when("/", {
            controller: "productInfoController",
            controllerAs: "vm",
            templateUrl: "/views/productInfo.html"
        });
       
        
        $routeProvider.otherwise({ redirectTo: "/" });
      
    });
   

})();