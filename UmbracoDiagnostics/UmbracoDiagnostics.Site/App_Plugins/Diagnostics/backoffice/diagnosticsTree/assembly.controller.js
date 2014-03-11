angular.module("umbraco").controller("Diagnostics.AssemblyController",
    function ($scope, $http) {
        
        $http.get(Umbraco.Sys.ServerVariables.Diagnostics.DiagnosticsBaseUrl + '/GetAllAssemblies').success(function (data) {
            $scope.assemblies = data;
        });

    });