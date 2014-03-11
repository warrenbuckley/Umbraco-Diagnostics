angular.module("umbraco").controller("Diagnostics.PackageController",
    function ($scope, $http) {
        
        $http.get(Umbraco.Sys.ServerVariables.Diagnostics.DiagnosticsBaseUrl + '/GetPackages').success(function (data) {
            $scope.packages = data;
        });

    });