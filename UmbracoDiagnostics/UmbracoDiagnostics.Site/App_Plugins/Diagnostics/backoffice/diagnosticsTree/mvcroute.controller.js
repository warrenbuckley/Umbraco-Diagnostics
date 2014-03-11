angular.module("umbraco").controller("Diagnostics.MVCRouteController",
    function ($scope, $http) {
        
        $http.get(Umbraco.Sys.ServerVariables.Diagnostics.DiagnosticsBaseUrl + '/GetMvcRoutes').success(function (data) {
            $scope.routes = data;
        });

    });