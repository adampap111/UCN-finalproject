/// <reference path="lib/angular/angular.js" />
(function () {
    "use strict";

    angular.module("app-productList")
        .controller("productListController", productListController);

    angular.module("app-productList")
        .filter('filterMultiple', ['$filter', function ($filter) {
            return function (items, keyObj) {
                var filterObj = {
                    data: items,
                    filteredData: [],
                    applyFilter: function (obj, key) {
                        var fData = [];
                        if (this.filteredData.length == 0)
                            this.filteredData = this.data;
                        if (obj) {
                            var fObj = {};
                            if (!angular.isArray(obj)) {
                                fObj[key] = obj;
                                fData = fData.concat($filter('filter')(this.filteredData, fObj));
                            } else if (angular.isArray(obj)) {
                                if (obj.length > 0) {
                                    for (var i = 0; i < obj.length; i++) {
                                        if (angular.isDefined(obj[i])) {
                                            fObj[key] = obj[i];
                                            fData = fData.concat($filter('filter')(this.filteredData, fObj));
                                        }
                                    }
                                }
                            }
                            // show everything if criteria does not match anything
                            if (fData.length > 0) {
                                this.filteredData = fData;
                            }
                            // show nothing if criteria does not match anything
                            //this.filteredData = fData;
                        }
                    }
                };
                if (keyObj) {
                    angular.forEach(keyObj, function (obj, key) {
                        filterObj.applyFilter(obj, key);
                    });
                }
                return filterObj.filteredData;
            }
        }]);

    angular.module("app-productList")
    .filter('unique', function () {
        return function (items, filterOn) {
            if (filterOn === false) {
                return items;
            }
            if ((filterOn || angular.isUndefined(filterOn)) && angular.isArray(items)) {
                var hashCheck = {}, newItems = [];
                var extractValueToCompare = function (item) {
                    if (angular.isObject(item) && angular.isString(filterOn)) {
                        return item[filterOn];
                    } else {
                        return item;
                    }
                };
                angular.forEach(items, function (item) {
                    var valueToCheck, isDuplicate = false;
                    for (var i = 0; i < newItems.length; i++) {
                        if (angular.equals(extractValueToCompare(newItems[i]), extractValueToCompare(item))) {
                            isDuplicate = true;
                            break;
                        }
                    }
                    if (!isDuplicate) {
                        newItems.push(item);
                    }
                });
                items = newItems;
            }
            return items;
        };
    });


    function productListController($http, $location, $scope, $parse, $filter) {
        var vm = this;
        vm.errorMessage = "";
        vm.isBusy = true;
        $scope.responseData = [];
        $scope.cartItems = [];


        vm.categoryFilter = [];
        vm.categoryFilter.isChecked = [];
        vm.categoryFilter.categoryName = [];

        vm.brandFilter = [];
        vm.brandFilter.isChecked = [];
        vm.brandFilter.brandName = [];

        vm.sizeFilter = [];
        vm.sizeFilter.isChecked = [];
        vm.sizeFilter.size = [];

        $scope.pageNumber = 1;
        vm.minProductNr = 0;
        vm.maxProductNr = 9;
        vm.limit = 9;
        vm.order = "brand";
        vm.ordering = "Márka";
        vm.isActivePage = 1;
        vm.filteredProducts = [];

        var splitPath1 = $location.absUrl().split("App/Product/")[0];
        var splitPath = $location.absUrl().split("App/Product/")[1];
        vm.category = splitPath.split("#/")[0];
        vm.productInfoUrl = splitPath1 + "App/ProductInfo/";
        var url = "/api/product/" + vm.category;
        var urlCartItems = "/cart/";

        $http.get(url)
          .then(function (response) {
              angular.copy(response.data, $scope.responseData);
              $scope.pageNumber = Math.ceil($scope.responseData.product.length / 9) - 1;
          }, function (error) {
              vm.errorMessage = "Failed to load data";
          })
          .finally(function () {
              vm.isBusy = false;
          });

        //get all cart items
        $http.get(urlCartItems)
        .then(function (response) {
            //success
            angular.copy(response.data, $scope.cartItems);
        }, function (error) {
            //failure
            vm.errorMessage = "Failed to load data" + error;
        })
        .finally(function () {
            vm.isBusy = false;
        });

        vm.addToCart = function (id) {
            vm.isBusy = true;
            vm.errorMessage = "";
            vm.quantity(id);
            $scope.cartItems.push(id);
            $('.shop-badge .badge').text($scope.cartItems.length);
           
            $http.post("/cart/AddToCart", $scope.responseData.product[id])
            .then(function (response) {
                //success

            }, function (error) {
                //failure
                vm.errorMessage = error;
            }).finally(function () {
                vm.isBusy = false;
            });
        };

        vm.quantity = function (i) {
            $scope.responseData.product[i].quantity = 1;
        };

        $scope.getNumber = function (num) {
            return new Array(num);
        };

        //page steps
        vm.nextPage = function () {
            if ($scope.pageNumber >= 1 && $scope.responseData.product.length > vm.maxProductNr) {
                vm.minProductNr = vm.minProductNr + 9;
                vm.maxProductNr = vm.maxProductNr + 9;
            }
        };

        vm.previousPage = function () {
            if ($scope.pageNumber >= 1 && vm.minProductNr > 8) {
                vm.minProductNr = vm.minProductNr - 9;
                vm.maxProductNr = vm.maxProductNr - 9;
            }
        };

        vm.jumpToPage = function (pageNr) {
            vm.minProductNr = (pageNr * vm.limit) - vm.limit;
            vm.maxProductNr = pageNr * vm.limit;
            vm.isActivePage = pageNr;
        }

        //ordering products
        vm.orderByBrand = function () {
            vm.order = 'brand';
            vm.ordering = "Márka";
        }

        vm.orderByCategory = function () {
            vm.order = 'category';
            vm.ordering = "Kategória";
        }

        vm.orderBySize = function () {
            vm.order = 'size';
            vm.ordering = "Méret";
        }

        //product limit on page
        vm.setNine = function () {
            vm.maxProductNr = vm.minProductNr + 9;
            vm.limit = 9;
        };

        vm.setEightteen = function () {
            vm.maxProductNr = vm.minProductNr + 18;
            vm.limit = 18;
        };

        vm.setTwentyseven = function () {
            vm.maxProductNr = vm.minProductNr + 27;
            vm.limit = 27;
        };

        // filters
        vm.stateChanged = function (qId) {
            vm.minProductNr = 0;
            vm.maxProductNr = 9;
            if (vm.brandFilter.isChecked[qId]) {
                vm.brandFilter.brandName.push($scope.responseData.brand[qId]);
            }
            else {
                var elementToRemove = $scope.responseData.brand[qId];
                var index = vm.brandFilter.brandName.indexOf(elementToRemove);
                if (index > -1) {
                    vm.brandFilter.brandName.splice(index, 1);
                }
            }
            $scope.pageNumber = Math.ceil(vm.filteredProducts.length / 9);
        };

        vm.categoryStateChanged = function (qId) {
            vm.minProductNr = 0;
            vm.maxProductNr = 9;
            if (vm.categoryFilter.isChecked[qId]) {
                vm.categoryFilter.categoryName.push($scope.responseData.category[qId]);
            }
            else {
                var elementToRemove = $scope.responseData.category[qId];
                var index = vm.categoryFilter.categoryName.indexOf(elementToRemove);
                if (index > -1) {
                    vm.categoryFilter.categoryName.splice(index, 1);
                }
            }
            $scope.pageNumber = Math.ceil(vm.filteredProducts.length / 9);
        };

        vm.sizeStateChanged = function (qId) {
            vm.minProductNr = 0;
            vm.maxProductNr = 9;
            if (vm.sizeFilter.isChecked[qId]) {
                vm.sizeFilter.size.push($scope.responseData.size[qId]);
            }
            else {
                var elementToRemove = $scope.responseData.size[qId];
                var index = vm.sizeFilter.size.indexOf(elementToRemove);
                if (index > -1) {
                    vm.sizeFilter.size.splice(index, 1);
                }
            }
            $scope.pageNumber = Math.ceil(vm.filteredProducts.length / 9);
        };

        //get number of products
        $scope.getBrandCount = function (strCat) {
            return $filter('filter')($scope.responseData.product, { brand: strCat }).length;
        }

        $scope.getCatCount = function (strCat) {
            return $filter('filter')($scope.responseData.product, { subCategory: strCat }).length;
        }

        $scope.getSizeCount = function (strCat) {
            return $filter('filter')($scope.responseData.product, { size: strCat }).length;
        }



        //vm.filterProducts = function (isCheckedArray, attrArray) {           
        //vm.filteredProducts = [];
        //vm.filteredProducts = vm.filterProductsGeneric($scope.responseData.product, isCheckedArray, attrArray);
        //if (vm.filteredProducts.length == 0) {
        //    vm.filteredProducts = $scope.responseData.product;
        //} 
        //};

        //vm.filterProductsByBrand = function (array) {
        //    var tempArray = [];
        //    if (array instanceof Array && array.length > 0) {
        //        for (var i = 0; i < array.length; i++) {
        //            var ind = 0;
        //            var found = false;
        //            while (!found && ind < vm.brandFilter.brandName.length) {
        //                if (vm.brandFilter.isChecked[ind]) {
        //                    if (array[i].brand === vm.brandFilter.brandName[ind]) {
        //                        tempArray.push(array[i]);
        //                        found = true;
        //                    }
        //                    else {
        //                        ind++;
        //                    }
        //                }
        //                else {
        //                    ind++;
        //                }
        //            }
        //        }
        //    }
        //    return tempArray;
        //};

        //vm.filterProductsByCategory = function (array) {
        //    var tempArray = [];
        //    if (array instanceof Array && array.length > 0) {
        //        for (var i = 0; i < array.length; i++) {
        //            var ind = 0;
        //            var found = false;
        //            while (!found && ind < vm.categoryFilter.categoryName.length) {
        //                if (vm.categoryFilter.isChecked[ind]) {
        //                    if (array[i].subCategory === vm.categoryFilter.categoryName[ind]) {
        //                        tempArray.push(array[i]);
        //                        found = true;
        //                    }
        //                    else {
        //                        ind++;

        //                    }
        //                }
        //                else {
        //                    ind++;
        //                }
        //            }
        //        }
        //    }
        //    return tempArray;
        //};

        //vm.filterProductsGeneric = function (array, isCheckedArray, attrArray) {
        //    var tempArray = [];
        //    if (array instanceof Array && array.length > 0) {
        //        for (var i = 0; i < array.length; i++) {
        //            var ind = 0;
        //            var found = false;
        //            while (!found && ind < attrArray.length) {
        //                if (isCheckedArray[ind]) {
        //                    if (array[i].brand === attrArray[ind] || array[i].subCategory === attrArray[ind] || array[i].size === attrArray[ind]) {
        //                        tempArray.push(array[i]);
        //                        found = true;
        //                    }
        //                    else {
        //                        ind++;
        //                    }
        //                }
        //                else {
        //                    ind++;
        //                    vm.errorMessage = vm.errorMessage + " " + tempArray.length;
        //                }
        //            }
        //        }
        //    }
        //    return tempArray;
        //};
    }
})();