app.controller('LayoutController', ['$scope', '$sce', '$filter', function ($scope, $sce, $filter) {

    $scope.toggleIn = function () {


        document.getElementById("nav").style.width = "250px";
        document.getElementById("mySidenav").style.opacity = "1"
        document.getElementById("btn_rollover").style.display = "none";
        $('#partialView').css('margin-left', '250px');
    };

    $scope.toggleMenuOut = function () {


        document.getElementById("nav").style.width = "0";
        document.getElementById("mySidenav").style.opacity = "0"
        document.getElementById("btn_rollover").style.display = "block";
        $('#partialView').css('margin-left', '0');

    };


    $('.underline').click(function () {
        $scope.toggleMenuOut();
    });

}]);