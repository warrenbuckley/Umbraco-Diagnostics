<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dashboard.ascx.cs" Inherits="CWS.UmbracoDiagnostics.Web.App_Plugins.Diagnostics.dashboard" %>
<style>
    /* Used to override Umbraco CSS to force height for iframe */
    .tabpagescrollinglayer .tabpageContent {
        height: 100% !important;
    }
</style>
<iframe src="/App_Plugins/Diagnostics/index.html" name="right" id="diagnosticsiFrame" marginwidth="0" marginheight="0" frameborder="0" scrolling="auto" style="width: 100%; height: 100%;"></iframe>