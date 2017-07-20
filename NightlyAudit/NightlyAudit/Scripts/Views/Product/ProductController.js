app.controller('ProductController', ['$scope', '$sce', 'ProductService', '$filter', 'ngDialog', function ($scope, $sce, ProductService, $filter, ngDialog) {

    $scope.productsConfigured = [];
    $scope.productsConfiguredFiltered = [];
    loadAllConfiguredProducts();

    function loadAllConfiguredProducts() {

        var products = ProductService.loadAllConfiguredProducts();
        products.then(function (result) {

            $scope.productsConfigured = result.data.productList;

            if ($scope.selectedProduct != null) {
                $scope.productsConfiguredFiltered = $.grep($scope.productsConfigured, function (e) { return e.ProductType == $scope.selectedProduct.ProductId });
            }
            else {

                $scope.productsConfiguredFiltered = $scope.productsConfigured;
            }

        });
    }

    $scope.selectedProductName = "All";
    $scope.products = [{ ProductId: 1, Name: "CMP" },
                         { ProductId: 2, Name: "CAGE" }];

    $scope.hideAdd = true;
    $scope.showProductTypes = function (product) {

        if (product != null) {

            if (product != "All") {
                $scope.productsConfiguredFiltered = $.grep($scope.productsConfigured, function (e) { return e.ProductType == product.ProductId });
                $scope.hideAdd = false;
                $scope.selectedProductName = product.Name;

            }
            else {
                $scope.selectedProductName = "All"
                $scope.productsConfiguredFiltered = $scope.productsConfigured;
                $scope.hideAdd = true;
            }

            $scope.selectedProduct = product;

        }

    };


    $scope.showPopUp = function (product) {

        $scope.selectedProductForEdit = {};
        var selectedConfig;
        if (product != null) {
            selectedConfig = $scope.productsConfigured.map(function (img) { return img.ProductId; }).indexOf(product.ProductId);
            $scope.selectedProductForEdit = {
                ProductCode: product.ProductCode,
                IPInfo: product.IPInfo
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

                 if (value != "$closeButton") {

                     var productDTO = {

                         ProductId: product != null ? product.ProductId : 0,
                         IPInfo: value.IPInfo,
                         ProductCode: value.ProductCode,
                         ProductType: $scope.selectedProduct.ProductId,
                         IsDelete: product != null ? product.IsDelete : false
                     };
                     var isProductsaved = ProductService.saveProductConfiguration(productDTO);
                     isProductsaved.then(function (result) {

                         if (result.data.isProductSaved) {
                             loadAllConfiguredProducts();
                         }
                     });
                 }

             }
         );

    };


}]);

app.service("ProductService", function ($http) {

    this.loadAllConfiguredProducts = function () {

        return app.Ajax('GET', 'Product/GetAllConfiguredProducts', '', $http);
        //return $http.get("Product/GetAllConfiguredProducts");
    }

    this.saveProductConfiguration = function (productDTO) {

        return app.Ajax('POST', "Product/SaveProductConfiguration", productDTO, $http);
        //return $http.post("Product/SaveProductConfiguration", productDTO);
    }
});