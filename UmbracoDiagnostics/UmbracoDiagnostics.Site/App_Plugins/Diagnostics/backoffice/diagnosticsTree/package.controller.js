angular.module("umbraco").controller("Diagnostics.PackageController",
    function ($scope, $http) {
        
        $http.get('/Umbraco/Diagnostics/DiagnosticsApi/GetPackages').success(function (data) {
            $scope.packages = data;
        });

    });