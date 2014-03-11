angular.module("umbraco").controller("Diagnostics.PermissionController",
    function ($scope, $http) {
        
        $http.get(Umbraco.Sys.ServerVariables.Diagnostics.DiagnosticsBaseUrl + '/GetFolderPermissions').success(function (data) {
            $scope.permissions = data;
        });

    });