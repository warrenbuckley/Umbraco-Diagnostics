angular.module("umbraco").controller("Diagnostics.DomainController",
    function ($scope, $http) {
        
        $http.get('/Umbraco/Diagnostics/DiagnosticsApi/GetDomains').success(function (data) {
            $scope.domains = data;
        });

    });