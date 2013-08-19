/// <reference path="https://ajax.googleapis.com/ajax/libs/angularjs/1.0.7/angular.min.js" />
/// 
var umbracoDiagnosticsApp = angular.module('umbracoDiagnosticsApp', []);

umbracoDiagnosticsApp.config(function($routeProvider) {
    $routeProvider
        .when('/',
            {
                controller: 'VersionController',
                templateUrl: 'partials/version.html'
            })
        .when('/packages',
            {
                controller: 'PackagesController',
                templateUrl: 'partials/packages.html'
            })
        .when('/users',
            {
                controller: 'UsersController',
                templateUrl: 'partials/users.html'
            })
        .when('/domains',
            {
                controller: 'DomainsController',
                templateUrl: 'partials/domains.html'
            })
        .when('/assemblies',
            {
                controller: 'AssemblyController',
                templateUrl: 'partials/assemblies.html'
            })
        .when('/permissions',
            {
                controller: 'FolderPermissionsController',
                templateUrl: 'partials/folderpermissions.html'
            })
        .when('/events',
            {
                controller: 'EventsController',
                templateUrl: 'partials/events.html'
            })
        .when('/routes',
            {
                controller: 'MVCRouteController',
                templateUrl: 'partials/MVCRoutes.html'
            })
        .when('/trees',
            {
                controller: 'TreesController',
                templateUrl: 'partials/trees.html'
            })
        .otherwise({ redirectTo: '/' });
});


/*
=====================================
CONTROLLERS
=====================================
*/

//Version Controller
umbracoDiagnosticsApp.controller('VersionController', function ($scope, $http, $rootScope, $location) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetVersion').success(function (data) {
        $scope.version = data;
    });

    $http.get('/Umbraco/Api/DiagnosticsApi/GetVersionAssembly').success(function (data) {
        $scope.assembly = data;
    });
    
    //Pass location url value into an item on our scope object
    $rootScope.locationUrl = $location.$$url;
});

//Packages Controller
umbracoDiagnosticsApp.controller('PackagesController', function ($scope, $http, $rootScope, $location) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetPackages').success(function (data) {
        $scope.packages = data;
    });
    
    //Pass location url value into an item on our scope object
    $rootScope.locationUrl = $location.$$url;
});

//Users Controller
umbracoDiagnosticsApp.controller('UsersController', function ($scope, $http, $rootScope, $location) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetUsers').success(function (data) {
        $scope.users = data;
    });
    
    //Pass location url value into an item on our scope object
    $rootScope.locationUrl = $location.$$url;
});

//Domains Controller
umbracoDiagnosticsApp.controller('DomainsController', function ($scope, $http, $rootScope, $location) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetDomains').success(function (data) {
        $scope.domains = data;
    });
    
    //Pass location url value into an item on our scope object
    $rootScope.locationUrl = $location.$$url;
});

//Domains Controller
umbracoDiagnosticsApp.controller('AssemblyController', function ($scope, $http, $rootScope, $location) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetAllAssemblies').success(function (data) {
        $scope.assemblies = data;
    });
    
    //Pass location url value into an item on our scope object
    $rootScope.locationUrl = $location.$$url;
});

//Permissions Controller
umbracoDiagnosticsApp.controller('FolderPermissionsController', function ($scope, $http, $rootScope, $location) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetFolderPermissions').success(function (data) {
        $scope.permissions = data;
    });
    
    //Pass location url value into an item on our scope object
    $rootScope.locationUrl = $location.$$url;
});

//Events Controller
umbracoDiagnosticsApp.controller('EventsController', function ($scope, $http, $rootScope, $location) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetEvents').success(function (data) {
        $scope.events = data;
    });
    
    //Pass location url value into an item on our scope object
    $rootScope.locationUrl = $location.$$url;
});

//MVC Routes Controller
umbracoDiagnosticsApp.controller('MVCRouteController', function ($scope, $http, $rootScope, $location) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetMvcRoutes').success(function (data) {
        $scope.routes = data;
    });
    
    //Pass location url value into an item on our scope object
    $rootScope.locationUrl = $location.$$url;
});

//Tree Controller
umbracoDiagnosticsApp.controller('TreesController', function ($scope, $http, $rootScope, $location) {
    $http.get('/Umbraco/Api/DiagnosticsApi/GetTrees').success(function (data) {
        $scope.trees = data;
    });
    
    //Pass location url value into an item on our scope object
    $rootScope.locationUrl = $location.$$url;
});
