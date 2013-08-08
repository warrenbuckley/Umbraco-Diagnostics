/// <reference path="https://ajax.googleapis.com/ajax/libs/angularjs/1.0.7/angular.min.js" />
/// 
var umbracoDiagnosticsApp = angular.module('umbracoDiagnosticsApp', []);

umbracoDiagnosticsApp.config(function($routeProvider) {
    $routeProvider
        .when('/',
            {
                controller: 'VersionController',
                templateUrl: '/partials/version.html'
            })
        .when('/packages',
            {
                controller: 'PackagesController',
                templateUrl: '/partials/packages.html'
            })
        .when('/users',
            {
                controller: 'UsersController',
                templateUrl: '/partials/users.html'
            })
        .when('/domains',
            {
                controller: 'DomainsController',
                templateUrl: '/partials/domains.html'
            })
        .when('/assemblies',
            {
                controller: 'AssemblyController',
                templateUrl: '/partials/assemblies.html'
            })
        .when('/permissions',
            {
                controller: 'FolderPermissionsController',
                templateUrl: '/partials/folderpermissions.html'
            })
        .when('/events',
            {
                controller: 'EventsController',
                templateUrl: '/partials/events.html'
            })
        .when('/routes',
            {
                controller: 'MVCRouteController',
                templateUrl: '/partials/MVCRoutes.html'
            })
        .when('/trees',
            {
                controller: 'TreesController',
                templateUrl: '/partials/trees.html'
            })
        .otherwise({ redirectTo: '/' });
});


/*
=====================================
CONTROLLERS
=====================================
*/

//Version Controller
umbracoDiagnosticsApp.controller('VersionController', function ($scope, $http) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetVersion').success(function (data) {
        $scope.version = data;
    });

    $http.get('/Umbraco/Api/DiagnosticsApi/GetVersionAssembly').success(function (data) {
        $scope.assembly = data;
    });
});

//Packages Controller
umbracoDiagnosticsApp.controller('PackagesController', function ($scope, $http) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetPackages').success(function (data) {
        $scope.packages = data;
    });
});

//Users Controller
umbracoDiagnosticsApp.controller('UsersController', function ($scope, $http) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetUsers').success(function (data) {
        $scope.users = data;
    });
});

//Domains Controller
umbracoDiagnosticsApp.controller('DomainsController', function ($scope, $http) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetDomains').success(function (data) {
        $scope.domains = data;
    });
});

//Domains Controller
umbracoDiagnosticsApp.controller('AssemblyController', function ($scope, $http) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetAllAssemblies').success(function (data) {
        $scope.assemblies = data;
    });
});

//Permissions Controller
umbracoDiagnosticsApp.controller('FolderPermissionsController', function ($scope, $http) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetFolderPermissions').success(function (data) {
        $scope.permissions = data;
    });
});

//Events Controller
umbracoDiagnosticsApp.controller('EventsController', function ($scope, $http) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetEvents').success(function (data) {
        $scope.events = data;
    });
});

//MVC Routes Controller
umbracoDiagnosticsApp.controller('MVCRouteController', function ($scope, $http) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetMvcRoutes').success(function (data) {
        $scope.routes = data;
    });
});

//Tree Controller
umbracoDiagnosticsApp.controller('TreesController', function ($scope, $http) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetTrees').success(function (data) {
        $scope.trees = data;
    });
});

