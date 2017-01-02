(function () {
    "use strict";
    angular.module("app-contactPage")
        .controller("contactPageController", contactPageController);

    function contactPageController($http, $scope, $window) {
        var vm = this;
        vm.mail = [];
        vm.errorMessage = "";
        vm.isBusy = false;
        vm.responseData = {};
        var url = "/api/contact/sendMail";
        vm.sentNotifier = false;
        vm.errorNotifier = false;

        vm.sendMail = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post(url, vm.responseData)
            .then(function (response) {
                //success
                vm.mail.push(response.data);
                vm.sentNotifier = true;
                vm.errorNotifier = false;
                $window.scrollTo(0, angular.element(vm.sentNotifier).offsetTop);
            }, function (error) {
                //failure
                vm.errorMessage =  error;
                vm.sentNotifier = false;
                vm.errorNotifier = true;
                $window.scrollTo(0, angular.element(vm.errorNotifier).offsetTop);
            }).finally(function () {
                vm.isBusy = false;
            });
        };
    }
})();