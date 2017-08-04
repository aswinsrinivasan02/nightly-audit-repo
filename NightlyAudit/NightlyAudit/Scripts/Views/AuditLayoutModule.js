var app;
(function () {
    app = angular.module("LayoutModule", ['ngRoute', 'ngDialog']);

    app.config(["$routeProvider", "$httpProvider", function ($routeProvider, $httpProvider) {

        // Register interceptors service
        $httpProvider.interceptors.push('interceptors');

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
              .when("/ViewAudit", {
                  templateUrl: "/Audit/ViewAuditJobs",
                  controller: "AuditController"
              }).when("/blue", {
                  templateUrl: "blue.htm"
              })
    }]);

})();

app.factory("interceptors", [function () {

    return {

        // if beforeSend is defined call it
        'request': function (request) {

            if (request.beforeSend)
                request.beforeSend();

            return request;
        },


        // if complete is defined call it
        'response': function (response) {

            if (response.config.complete)
                response.config.complete(response);

            return response;
        }
    };

}]);

app.Ajax = function (_requestType, _url, _data, $http) {

    return $http({
        method: _requestType,
        url: _url,
        data: _data,
        beforeSend: function () {
            app.BlockUI();
        },
        complete: function () {
            app.UnBlockUI();
        }
    });


};

app.BlockUI = function () {

    $.blockUI({
        message: "<h1><img class='loader-please-wait' src='/Content/Images/Loader.gif' /></h1>",
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: 'none',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            color: 'white'
        }
    });
};
app.UnBlockUI = function () {
    $.unblockUI();
};


