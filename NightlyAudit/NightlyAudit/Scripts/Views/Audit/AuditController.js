app.controller('AuditController', ['$scope', '$sce', 'AuditService', '$filter', function ($scope, $sce, AuditService, $filter) {

    $scope.auditDTOList = [];
    $scope.auditParameterList = [];
    $scope.selectedAuditJob = "Select Audit Job";
    $scope.selectedAuditType;
    $scope.hideDynamicControl = true;
    getAuditTypes(0);
    $scope.tpl = {};
    $scope.tpl.contentUrl = "";
    $scope.showDaily = false;
    $scope.showWeekly = false;
    $scope.showMonthly = false;
    $scope.isApplyClassOn = true;
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

    $scope.showScheduler = function (showTypeCurrent) {

        if (showTypeCurrent != null) {

            if (showTypeCurrent == 'showOneTime') {
                $scope.showDaily = false;
                $scope.showWeekly = false;
                $scope.showMonthly = false;
            }
            else if (showTypeCurrent == 'showDaily') {
                $scope.showDaily = true;
                $scope.showWeekly = false;
                $scope.showMonthly = false;
                $scope.recurrenceText = "days"
            }
            else if (showTypeCurrent == 'showWeekly') {

                $scope.showWeekly = true;
                $scope.showDaily = true;
                $scope.showMonthly = false;
                $scope.recurrenceText = "weeks on:"
            }
            else if (showTypeCurrent == 'showMonthly') {

                $scope.showWeekly = false;
                $scope.showDaily = false;
                $scope.showMonthly = true;
            }
            else if (showTypeCurrent == 'showMonthly') {

                $scope.showWeekly = false;
                $scope.showDaily = false;
                $scope.showMonthly = true;
            }
            else if (showTypeCurrent=='Days') {
                $scope.isApplyClassDays = false;
                $scope.isApplyClassOn = true;
            }
            else if (showTypeCurrent=='On') {
                $scope.isApplyClassOn = false;
                $scope.isApplyClassDays = true;
            }
        }

    };



}]);


app.service("AuditService", function ($http) {

    this.getAuditTypes = function (auditId) {

        return app.Ajax('GET', 'Audit/GetAuditTypes?auditId=' + auditId, '', $http);
    }
});