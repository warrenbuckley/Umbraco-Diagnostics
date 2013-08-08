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
<body ng-app="">
        <h1>Diagnostics</h1>
        
        <div ng-controller="VersionController">
            <h2>Version</h2>
            <strong>Version:</strong> {{ version._Major + '.' + version._Minor + '.' + version._Build }} <br/>
            <strong>Assembly:</strong> {{ assembly }}
        </div>

        <hr/>
        
        <div ng-controller="AssemblyController">
            <h2>Assemblies</h2>
            <ul>
                <li ng-repeat="dll in assemblies">
                    <strong>Assembly:</strong> {{ dll.AssemblyName }}<br/>
                    <strong>Version:</strong> {{ dll.AssemblyVersion._Major + '.' + dll.AssemblyVersion._Minor + '.' + dll.AssemblyVersion._Build }}
                </li>
            </ul>
        </div>

        <hr/>
        
        <div ng-controller="FolderPermissionsController">
            <h2>Folder Permissions</h2>
            <ul>
                <li ng-repeat="folder in permissions">
                    <strong>Folder:</strong> {{ folder.FolderName }}<br/>
                    <strong>Permissions:</strong>
                    <ul>
                        <li ng-repeat="permission in folder.Permissions">
                            {{ permission }}
                        </li>
                    </ul>
                </li>
            </ul>    
        </div>
        
        <hr/>
        
        <div ng-controller="TreeController">
            <h2>Trees</h2>
            <ul>
                <li ng-repeat="item in trees">
                    <strong>Alias:</strong> {{ item.Tree.Alias }}<br/>
                    <strong>App:</strong> {{ item.App.alias }}
                </li>
            </ul>
        </div>

        <hr/>
        
        <h2>Rest Extensions</h2>
       

        <hr/>
        
        <h2>XSLT Extensions</h2>
        
        <hr/>
        
        <h2>Surface Controllers</h2>
        
        <hr/>
        
        <h2>Web API Controllers</h2>
        
        <hr/>
        
        <h2>Package Actions</h2>
        
        <hr/>
        
        <h2>Macro Parameters</h2>
        
        <hr/>
        
        <div ng-controller="MVCRouteController">
            <h2>MVC Routes</h2>
            <ul>
                <li ng-repeat="route in routes">
                    <strong>Route URL:</strong> {{ route }}
                </li>
            </ul>
        </div>
        
        
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/angularjs/1.0.7/angular.min.js"></script>
    <script type="text/javascript" src="/scripts/Controllers.js"></script>
</body>
</html>
