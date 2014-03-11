angular.module("umbraco").controller("Diagnostics.GeneralController",
    function ($scope, $http) {

        $http.get(Umbraco.Sys.ServerVariables.Diagnostics.DiagnosticsBaseUrl + '/GetVersionInfo').success(function (data) {
            $scope.version = data;
        });

        $http.get(Umbraco.Sys.ServerVariables.Diagnostics.DiagnosticsBaseUrl + '/GetServerInfo').success(function (data) {
            $scope.server = data;
        });

        $http.get(Umbraco.Sys.ServerVariables.Diagnostics.DiagnosticsBaseUrl + '/GetDBInfo').success(function (data) {
            $scope.db = data;
        });

    });