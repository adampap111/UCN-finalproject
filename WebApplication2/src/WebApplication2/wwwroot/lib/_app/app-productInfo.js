!function(){"use strict";angular.module("app-productInfo",["ngRoute"]).config(["$routeProvider","$locationProvider",function(o,r){o.when("/",{controller:"productInfoController",controllerAs:"vm",templateUrl:"/views/productInfo.html"}),o.otherwise({redirectTo:"/"})}])}();