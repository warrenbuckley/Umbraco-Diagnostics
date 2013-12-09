angular.module("umbraco").controller("Diagnostics.GeneralController",
    function ($scope, $http) {

        $http.get('/Umbraco/Diagnostics/DiagnosticsApi/GetVersionInfo').success(function (data) {
            $scope.version = data;
        });

        $http.get('/Umbraco/Diagnostics/DiagnosticsApi/GetServerInfo').success(function (data) {
            $scope.server = data;
        });

        $http.get('/Umbraco/Diagnostics/DiagnosticsApi/GetDBInfo').success(function (data) {
            $scope.db = data;
        });

    });