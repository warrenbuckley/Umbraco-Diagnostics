angular.module("umbraco").controller("Diagnostics.MVCRouteController",
    function ($scope, $http) {
        
        $http.get('/Umbraco/Diagnostics/DiagnosticsApi/GetMvcRoutes').success(function (data) {
            $scope.routes = data;
        });

    });