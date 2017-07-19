app.controller('ProductController', ['$scope', '$sce', 'ProductService', '$filter', 'ngDialog', function ($scope, $sce, ProductService, $filter, ngDialog) {

    $scope.productsConfigured = [];
    loadAllConfiguredProducts();

    function loadAllConfiguredProducts() {

        var products = ProductService.loadAllConfiguredProducts();
        products.then(function (result) {
            debugger;
            $scope.productsConfigured = result.productList;
        });
    }
    
    $scope.selectedProduct = "--Choose Product--"
    $scope.products = ['CMP', 'Cage'];

    $scope.hideAdd = true;
    $scope.showProductTypes = function (product) {
        if (product != "" && product != "--Choose Product--") {
            $scope.selectedProduct = product;
            $scope.hideAdd = false;
        }
    };


    $scope.showPopUp = function (product) {

        $scope.selectedProductForEdit = {};
        var selectedConfig;
        if (product != null) {
            selectedConfig = $scope.productsConfigured.map(function (img) { return img.ipNumber; }).indexOf(product.ipNumber);
            $scope.selectedProductForEdit = {
                server_Name: product.serverName,
                ip_Number: product.ipNumber
            };
        }

        ngDialog.openConfirm({
            template: 'productConfiguration.html',
            controller: 'ProductController',
            scope: $scope,
            data: $scope.selectedProductForEdit,

        }).then(
             function (value) {

             },
             function (value) {

                 if (product != null && value != "$closeButton") {

                     $scope.productsConfigured[selectedConfig].serverName = value.server_Name
                     $scope.productsConfigured[selectedConfig].ipNumber = value.ip_Number
                 }
                 else if (value != null && value != "$closeButton") {
                     $scope.productsConfigured.push({ id: 1, serverName: value.server_Name, ipNumber: value.ip_Number });
                 }

             }
         );

    };


}]);

app.service("ProductService", function ($http) {

    this.saveProductConfiguration = function () {

        return $http.post("Product/SaveProductConfiguration");
    }

    this.loadAllConfiguredProducts = function () {

        return $http.get("Product/GetAllConfiguredProducts");
    }
});