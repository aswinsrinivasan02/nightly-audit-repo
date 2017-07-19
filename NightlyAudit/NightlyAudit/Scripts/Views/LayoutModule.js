var app;
(function () {
    app = angular.module("LayoutModule", ['ngRoute', 'ngDialog']);

    app.config(function ($routeProvider) {
        $routeProvider
            .when("/Dashboard", {
                templateUrl: "/Dashboard/Index",
                controller: "LayoutController"
            }).when("/NewAudit", {
                templateUrl: "/Audit/Index",
                controller: "AuditController"
            })
              .when("/ProductSetting", {
                  templateUrl: "/Product/Index",
                  controller: "ProductController"
              })
              .when("/blue", {
                  templateUrl: "blue.htm"
              })
    });

})();

