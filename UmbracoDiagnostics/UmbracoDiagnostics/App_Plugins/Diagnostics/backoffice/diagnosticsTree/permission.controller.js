angular.module("umbraco").controller("Diagnostics.PermissionController",
    function ($scope, $http) {
        
        $http.get('/Umbraco/Diagnostics/DiagnosticsApi/GetFolderPermissions').success(function (data) {
            $scope.permissions = data;
        });

    });