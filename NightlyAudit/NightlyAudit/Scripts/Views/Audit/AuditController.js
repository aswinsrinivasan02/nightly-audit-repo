app.controller('AuditController', ['$scope', '$sce', 'AuditService', '$filter', function ($scope, $sce, AuditService, $filter) {

    $scope.auditDTOList = [];
    $scope.auditParameterList = [];
    $scope.selectedAuditJob = "Select Audit Job";
    $scope.selectedAuditType;
    $scope.hideDynamicControl = true;
    getAuditTypes(0);
    $scope.tpl = {};
    $scope.tpl.contentUrl = "";
    $scope.showHourlyDaily = true;
    $scope.recurrenceText = "days"
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

    $scope.schedulerForm = {

        'ReoccurEveryX': null,
        'scheduleStartsAt': null,
        'ReoccurEveryXMonthsOn': null
    };

    function clearControls() {
        $scope.schedulerForm = {};
        $scope.daysOfWeek = {};
        angular.element("#datetimepicker >input")[0].value = null;
        $('#weekDays').multiselect('deselectAll', false);
        $('#weekDays').multiselect('updateButtonText');

        $('#selectedWeek').multiselect('deselectAll', false);
        $('#selectedWeek').multiselect('updateButtonText')
    }

    $scope.loadDynamicControls = function (auditType) {

        if (auditType != null) {

            $scope.selectedAuditType = auditType;
            $scope.selectedAuditJob = auditType.AuditName;
            getAuditTypes(auditType.AuditId);
        }
    };

    $scope.showScheduler = function (showTypeCurrent) {

        clearControls();

        if (showTypeCurrent != null) {

            if (showTypeCurrent == 'showHourly') {
                $scope.showHourlyDaily = true;
                $scope.showMonthly = false;
                $scope.showWeekly = false;
                $scope.recurrenceText = "hour(s)"
                $scope.showDaily = false;
                $scope.showHourly = true;
            }
            else if (showTypeCurrent == 'showHourlyDaily') {

                $scope.showWeekly = false;
                $scope.showMonthly = false;
                $scope.showHourlyDaily = true;
                $scope.recurrenceText = "days"
                $scope.showHourly = false;
            }
            else if (showTypeCurrent == 'showWeekly') {


                $scope.showMonthly = false;
                $scope.showWeekly = true;
                $scope.showHourlyDaily = false;
                $scope.showDaily = false;
                $scope.showHourly = false;

            }
            else if (showTypeCurrent == 'showMonthly') {

                $scope.showWeekly = false;
                $scope.showHourlyDaily = false;
                $scope.showMonthly = true;
                $scope.showDaily = false;
                $scope.showHourly = false;
            }
            else if (showTypeCurrent == 'showDaily') {
                $scope.showHourlyDaily = true;
                $scope.showDaily = true;
                $scope.recurrenceText = "days"
                $scope.showMonthly = false;
                $scope.showWeekly = false;
                $scope.showHourly = false;
            }
        }

    };

    $scope.scheduleType = {

        type: 'Hourly'
    };

    $scope.saveTask = function () {
        
        var selectedDays = [];
        var control = $scope.daysOfWeek;
        if ($scope.scheduleType.type == 'Monthly') {
            control = $('#weekDays option:selected');
        }

        angular.forEach(control, function (dayOfWeek) {
            if ($scope.scheduleType.type == 'Monthly') {

                selectedDays.push(dayOfWeek.value);
            }
            else {
                selectedDays.push(dayOfWeek);
            }

        });


        var scheduleObjectDTO = {

            ScheduleType: $scope.scheduleType.type,
            ReoccurEveryX: $scope.ReoccurEveryX,
            SelectedDates: selectedDays,
            ReoccurEveryXMonths: $scope.ReoccurEveryXMonths == null ? $scope.ReoccurEveryXMonthsOn : $scope.ReoccurEveryXMonths,
            StartsAt: $('#datetimepicker').data('date'),
            SelectedWeek: $('#selectedWeek option:selected').val()

        };

    };

}]);


app.service("AuditService", function ($http) {

    this.getAuditTypes = function (auditId) {

        return app.Ajax('GET', 'Audit/GetAuditTypes?auditId=' + auditId, '', $http);
    }
});

