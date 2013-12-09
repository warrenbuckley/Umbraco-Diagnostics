angular.module("umbraco").controller("Diagnostics.DiagnosticViewController",
    function ($scope, $routeParams) {

        //Currently loading /umbraco/general.html
        //Need it to look at /App_Plugins/

        var viewName    = $routeParams.id;
        viewName        = viewName.replace('%20', '-').replace(' ', '-');

        $scope.templatePartialURL   = '../App_Plugins/Diagnostics/backoffice/diagnosticsTree/partials/' + viewName + '.html';
        $scope.sectionName          = $routeParams.id;

    });