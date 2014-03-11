angular.module("umbraco").controller("Diagnostics.UserController",
    function ($scope, $http) {
        
        $http.get(Umbraco.Sys.ServerVariables.Diagnostics.DiagnosticsBaseUrl + '/GetUsers').success(function (data) {
            $scope.users        = data;
            $scope.userCount    = data.length;
        });

    });