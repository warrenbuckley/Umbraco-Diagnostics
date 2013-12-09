angular.module("umbraco").controller("Diagnostics.AssemblyController",
    function ($scope, $http) {
        
        $http.get('/Umbraco/Diagnostics/DiagnosticsApi/GetAllAssemblies').success(function (data) {
            $scope.assemblies = data;
        });

    });