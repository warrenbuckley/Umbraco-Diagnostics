angular.module("umbraco").controller("Diagnostics.EventController",
    function ($scope, $http) {

        $scope.isLoading = true;

        $http.get(Umbraco.Sys.ServerVariables.Diagnostics.DiagnosticsBaseUrl + '/GetEvents').success(function (data) {
            $scope.events = data;
            
            //Now data is loaded - set to false
            $scope.isLoading = false;
        });
        
    });