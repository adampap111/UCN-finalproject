!function(){"use strict";function e(e,r,o){var t=this;t.mail=[],t.errorMessage="",t.isBusy=!1,t.responseData={};var i="/api/contact/sendMail";t.sentNotifier=!1,t.errorNotifier=!1,t.sendMail=function(){t.isBusy=!0,t.errorMessage="",e.post(i,t.responseData).then(function(e){t.mail.push(e.data),t.sentNotifier=!0,t.errorNotifier=!1,o.scrollTo(0,angular.element(t.sentNotifier).offsetTop)},function(e){t.errorMessage=e,t.sentNotifier=!1,t.errorNotifier=!0,o.scrollTo(0,angular.element(t.errorNotifier).offsetTop)}).finally(function(){t.isBusy=!1})}}e.$inject=["$http","$scope","$window"],angular.module("app-contactPage").controller("contactPageController",e)}();