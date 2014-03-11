angular.module("umbraco").controller("Diagnostics.TreeController",
    function ($scope, $http) {
        
        $http.get(Umbraco.Sys.ServerVariables.Diagnostics.DiagnosticsBaseUrl + '/GetTrees').success(function (data) {
            $scope.trees = data;
        });

    });