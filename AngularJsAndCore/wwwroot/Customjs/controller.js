/// <reference path="../lib/angular.js/angular.js" />
app.controller('myController', function ($scope, myService) {
    
    $scope.newProduct = {};
    $scope.clickedProduct = {};
    $scope.message = "";
    ProductList();
    function ProductList() {      
        myService.productList().then(function (result) {
            $scope.products = result.data;
        });
    };
    $scope.AddProduct = function () {       
        myService.addProduct($scope.newProduct).then(function (result) {
            //alert(result);
            //$scope.newProduct = {};
            $scope.message = "Product saved successfully";
            ProductList();
        });
    };

    $scope.SelectedProduct = function (product) {
        $scope.clickedProduct = product;
    };

    $scope.UpdateProduct = function () {
        myService.updateProduct($scope.clickedProduct).then(function (result) {
            $scope.message = "Data updated successfully";
            $scope.products = result.data;
            ProductList();
        }, function () {
            alert(result);
        });
    };

    $scope.DeleteProduct = function () {        
        myService.deleteProduct($scope.clickedProduct.ProductId).then(function () {
            $scope.message = "Product deleted successfully"
            ProductList();           
        }, function (result) {
            alert(result);
        })
    };

    $scope.ClearMessage = function () {
        $scope.message = "";
    }
})