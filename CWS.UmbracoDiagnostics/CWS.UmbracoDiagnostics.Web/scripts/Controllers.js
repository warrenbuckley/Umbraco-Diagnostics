function VersionController($scope, $http) {
    
    $http.get('/Umbraco/Api/DiagnosticsApi/GetVersion').success(function (data) {
        $scope.version = data;
    });

    $http.get('/Umbraco/Api/DiagnosticsApi/GetVersionAssembly').success(function (data) {
        $scope.assembly = data;
    });
}


function AssemblyController($scope, $http) {

    $http.get('/Umbraco/Api/DiagnosticsApi/GetAllAssemblies').success(function (data) {
        $scope.assemblies = data;
    });
}

function FolderPermissionsController($scope, $http) {

    $http.get('/Umbraco/Api/DiagnosticsApi/GetFolderPermissions').success(function (data) {
        $scope.permissions = data;
    });
}

function TreeController($scope, $http) {

    $http.get('/Umbraco/Api/DiagnosticsApi/GetTrees').success(function (data) {
        $scope.trees = data;
    });
}

function MVCRouteController($scope, $http) {

    $http.get('/Umbraco/Api/DiagnosticsApi/GetMvcRoutes').success(function (data) {
        $scope.routes = data;
    });
}

function EventsController($scope, $http) {

    $http.get('/Umbraco/Api/DiagnosticsApi/GetEvents').success(function (data) {
        $scope.events = data;
    });
}