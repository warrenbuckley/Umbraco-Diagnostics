angular.module("umbraco").controller("Diagnostics.DomainController",
    function ($scope, $http) {
        
        $http.get(Umbraco.Sys.ServerVariables.Diagnostics.DiagnosticsBaseUrl +'/GetDomains').success(function (data) {
            $scope.domains = data;
        });

    });