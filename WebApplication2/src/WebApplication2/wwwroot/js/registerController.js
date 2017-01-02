(function () {
    "use strict";
    angular.module("app-register")
    .controller("registerController", registerController);

    function registerController($http,$window) {
        var vm = this;

        vm.newUser = {};

        vm.errorMessage = "";
        vm.isBusy = true;


        vm.addUser = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.post("/api/register", vm.newUser)
            .then(function (response) {
                //success               
                vm.newUser = {};
                $window.location.href = "/";
            }, function (error) {
                //failure
                vm.errorMessage = "Failed to save the data" + error;
            }).finally(function () {
                vm.isBusy = false;
            });
        };

        //var login = function () {
        //    $http.post("/Auth/Login", vm.newUser)
        //        .then(function (response) {
        //            //success
        //            $window.location.href = "/";
        //    }, function (error) {
        //        //failure
        //        vm.errorMessage = "Failed to login" + error;
        //    }).finally(function () {
        //        vm.isBusy = false;
        //    });
        //};
    }

})(window, window.angular);