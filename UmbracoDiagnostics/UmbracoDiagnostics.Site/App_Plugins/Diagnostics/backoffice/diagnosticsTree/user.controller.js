angular.module("umbraco").controller("Diagnostics.UserController",
    function ($scope, $http) {
        
        $http.get('/Umbraco/Diagnostics/DiagnosticsApi/GetUsers').success(function (data) {
            $scope.users        = data;
            $scope.userCount    = data.length;
        });

    });