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
        <li ng-class="{active: locationUrl == '/'}">
            <a href="#/">Version</a>
        </li>
        <li ng-class="{active: locationUrl == '/packages'}">
            <a href="#/packages">Packages</a>
        </li>
        <li ng-class="{active: locationUrl == '/users'}">
            <a href="#/users">Users</a>
        </li>
        <li ng-class="{active: locationUrl == '/domains'}">
            <a href="#/domains">Domains</a>
        </li>
        <li ng-class="{active: locationUrl == '/assemblies'}">
            <a href="#/assemblies">Assemblies</a>
        </li>
        <li ng-class="{active: locationUrl == '/permissions'}">
            <a href="#/permissions">Permissions</a>
        </li>
        <li ng-class="{active: locationUrl == '/events'}">
            <a href="#/events">Events</a>
        </li>
        <li ng-class="{active: locationUrl == '/routes'}">
            <a href="#/routes">MVC Routes</a>
        </li>
        <li ng-class="{active: locationUrl == '/trees'}">
            <a href="#/trees">Trees</a>
        </li>
    </ul>

    <!-- Placeholder for views -->
    <div ng-view=""></div>
    
    <!-- JS -->
    <script type="text/javascript" src=h"https://ajax.googleapis.comttps://ajax.googleapis.com/ajax/libs/angularjs/1.0.7/angular.min.js"></script>
    <script type="text/javascript" src="/scripts/app.js"></script>

    <!-- Latest compiled and minified JavaScript -->
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0-rc1/js/bootstrap.min.js"></script>
</body>
</html>
