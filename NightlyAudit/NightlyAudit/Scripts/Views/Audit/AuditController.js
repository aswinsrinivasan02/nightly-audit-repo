app.controller('AuditController', ['$scope', '$sce', 'AuditService', '$filter', function ($scope, $sce, AuditService, $filter) {

    //getAuditTypes();


    function getAuditTypes() {
        var auditTypes = AuditService.getAuditTypes();
        auditTypes.then(function (d) {

            $scope.auditTypes = d.data.AuditTypes;
        });
    }

    $scope.loadDynamicControls = function () {

        alert($scope.selectAuditType);

    };

}]);


app.service("AuditService", function ($http) {

    this.getAuditTypes = function () {

        return $http.get("Audit/GetAuditTypes");
    }
});