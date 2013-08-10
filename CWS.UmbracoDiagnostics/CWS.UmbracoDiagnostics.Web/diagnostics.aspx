<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="diagnostics.aspx.cs" Inherits="CWS.UmbracoDiagnostics.Web.diagnostics" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Umbraco Diagnostics</title>

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.0.0-rc1/css/bootstrap.min.css">
</head>
<body ng-app="umbracoDiagnosticsApp">
    
    <h1>Diagnostics</h1>
    
    <!-- Nav -->
    <ul class="nav nav-tabs">
        <li class="active">
            <a href="#/">Version</a>
        </li>
        <li>
            <a href="#/packages">Packages</a>
        </li>
        <li>
            <a href="#/users">Users</a>
        </li>
        <li>
            <a href="#/domains">Domains</a>
        </li>
        <li>
            <a href="#/assemblies">Assemblies</a>
        </li>
        <li>
            <a href="#/permissions">Permissions</a>
        </li>
        <li>
            <a href="#/events">Events</a>
        </li>
        <li>
            <a href="#/routes">MVC Routes</a>
        </li>
        <li>
            <a href="#/trees">Trees</a>
        </li>
    </ul>

    <!-- Placeholder for views -->
    <div ng-view=""></div>
    
    <!-- JS -->
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/angularjs/1.0.7/angular.min.js"></script>
    <script type="text/javascript" src="/scripts/app.js"></script>

    <!-- Latest compiled and minified JavaScript -->
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0-rc1/js/bootstrap.min.js"></script>
</body>
</html>
