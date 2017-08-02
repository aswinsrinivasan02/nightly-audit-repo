app.controller('AuditController', ['$scope', '$sce', 'AuditService', '$filter', function ($scope, $sce, AuditService, $filter) {

    $scope.auditDTOList = [];
    $scope.auditParameterList = [];
    $scope.selectedAuditJob = "Select Audit Job";
    $scope.selectedAuditType;
    $scope.hideDynamicControl = true;
    getAuditTypes(0);
    $scope.tpl = {};
    $scope.tpl.contentUrl = "";

    loadFileForNgInclude = function () {

        $scope.tpl.contentUrl = '/Audit/LoadPartialView?controlType=' + $scope.parameterType
    };

    function getAuditTypes(auditId) {

        var auditTypes = AuditService.getAuditTypes(auditId);
        auditTypes.then(function (result) {

            $scope.auditDTOList = result.data.auditDetailList;


            if ($scope.auditDTOList != null && $scope.selectedAuditType != null) {

                var selectedAuditIndex;
                selectedAuditIndex = $scope.auditDTOList.map(function (img) { return img.AuditId; }).indexOf($scope.selectedAuditType.AuditId);
                $scope.auditParameterList = $scope.auditDTOList[selectedAuditIndex];

                //to check if there can be multiple paramters
                $scope.paramaterLabel = $scope.auditParameterList.AuditParameters[0].ParameterName + " " + ":";

                $scope.parameterType = $scope.auditParameterList.AuditParameters[0].ParameterType;

                $scope.auditParameterValuesList = $scope.auditParameterList.AuditParameters[0].ParameterValues;
                                
                $scope.hideDynamicControl = false;

                loadFileForNgInclude();

            }


        });
    }

    $scope.loadDynamicControls = function (auditType) {

        if (auditType != null) {

            $scope.selectedAuditType = auditType;
            $scope.selectedAuditJob = auditType.AuditName;
            getAuditTypes(auditType.AuditId);
        }
    };



}]);


app.service("AuditService", function ($http) {

    this.getAuditTypes = function (auditId) {

        return app.Ajax('GET', 'Audit/GetAuditTypes?auditId=' + auditId, '', $http);
    }
});