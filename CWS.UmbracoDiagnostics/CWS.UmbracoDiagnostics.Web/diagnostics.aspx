<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="diagnostics.aspx.cs" Inherits="CWS.UmbracoDiagnostics.Web.diagnostics" %>
<%@ Import Namespace="System.Web.Routing" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Umbraco Diagnostics</title>
    <style>
        ul li {
            margin: 0 0 10px 0;
        }
    </style>
</head>
<body ng-app="umbracoDiagnosticsApp">
    
    <h1>Diagnostics</h1>
    
    <!-- Nav -->
    <ul>
        <li>
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
</body>
</html>
