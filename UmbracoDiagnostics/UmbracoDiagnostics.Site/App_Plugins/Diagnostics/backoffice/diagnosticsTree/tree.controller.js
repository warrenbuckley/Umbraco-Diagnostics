angular.module("umbraco").controller("Diagnostics.TreeController",
    function ($scope, $http) {
        
        $http.get('/Umbraco/Diagnostics/DiagnosticsApi/GetTrees').success(function (data) {
            $scope.trees = data;
        });

    });