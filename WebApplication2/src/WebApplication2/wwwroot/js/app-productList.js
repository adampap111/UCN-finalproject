(function () {
    "use strict";
    angular.module("app-productList", ["ngRoute"])
    .config(function ($routeProvider, $locationProvider) {

        $routeProvider.when("/", {
            controller: "productListController",
            controllerAs: "vm",
            templateUrl: "/views/productList.html"
        });
        
        $routeProvider.otherwise({ redirectTo: "/" });
      
    });
   

})();