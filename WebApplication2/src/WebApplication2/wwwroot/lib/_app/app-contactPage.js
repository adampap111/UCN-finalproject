!function(){"use strict";angular.module("app-contactPage",["ngRoute"]).config(["$routeProvider","$locationProvider",function(o,t){o.when("/",{controller:"contactPageController",controllerAs:"vm",templateUrl:"/views/contactPage.html"}),o.otherwise({redirectTo:"/"})}])}();