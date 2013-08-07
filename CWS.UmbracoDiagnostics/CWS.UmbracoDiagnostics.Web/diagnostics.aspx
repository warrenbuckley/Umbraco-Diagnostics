<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="diagnostics.aspx.cs" Inherits="CWS.UmbracoDiagnostics.Web.diagnostics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Diagnostics</h1>

        <h2>Version</h2>
        <p>
            <strong>Umbraco Version:</strong> <asp:Label runat="server" ID="umbVersion"/><br/>
            <strong>Umbraco Assembly:</strong> <asp:Label runat="server" ID="umbAssembly"/>
        </p>
        
        <hr/>
        
        <h2>Assemblies <asp:Label runat="server" ID="assemblyCount"/></h2>
        <ul>
            <%
                foreach (var dll in AllAssemblies)
                {
            %>
                    <li>
                        <%= dll.GetName().Name %> - <%=dll.GetName().Version %>
                    </li>
            <%
                }
            %>
        </ul>

        <hr/>
        
        <h2>Assemblies (Alternative) <%=Diagnostics.Assemblies.Length %></h2>
        <table border="1">
            <tr>
                <th>Name</th>
                <th>Location</th>
                <th>Version</th>
                <th>File Version</th>
                <th>Description</th>
                <th>Author</th>
                <th>MD5 Checksum</th>
                <th>SHA1 Checksum</th>
            </tr>
            <%
                foreach (var assembly in Diagnostics.Assemblies) {

            %>
                    <tr>
                        <td style="vertical-align: top; white-space: nowrap;"><%=assembly.Assembly.GetName().Name %></td>
                        <td style="vertical-align: top; white-space: nowrap;" title="<%=assembly.Location %>"><%=assembly.Location.Substring(0, 15) %>...<%=assembly.Location.Substring(assembly.Location.Length - 20) %></td>
                        <td style="vertical-align: top;"><%=assembly.Version %></td>
                        <td style="vertical-align: top;"><%=assembly.FileVersion.FileVersion %></td>
                        <td style="vertical-align: top;"><%=String.IsNullOrEmpty(assembly.FileVersion.Comments) ? "<em>N/A</em>" : assembly.FileVersion.Comments %></td>
                        <td style="vertical-align: top; white-space: nowrap;"><%=String.IsNullOrEmpty(assembly.FileVersion.CompanyName) ? "<em>N/A</em>" : assembly.FileVersion.CompanyName %></td>
                        <td style="vertical-align: top;"><%=assembly.ChecksumMD5 %></td>
                        <td style="vertical-align: top;"><%=assembly.ChecksumSHA1 %></td>
                    </tr>
            <%
                }
            %>
        </table>

        <hr/>
        
        <h2>uGoLive Checks <%=Diagnostics.uGoLiveChecks.Length %></h2>
        <table border="1">
            <tr>
                <th>ID</th>
                <th>Name</th>
                <th>Description</th>
                <th>Group</th>
                <th colspan="2">Status</th>
                <th>Can rectify?</th>
            </tr>
            <%
                foreach (var check in Diagnostics.uGoLiveChecks) {

                    var status = check.Check();
            %>
                    <tr>
                        <td style="vertical-align: top; white-space: nowrap;"><%=check.Id %></td>
                        <td style="vertical-align: top; white-space: nowrap;"><%=check.Name %></td>
                        <td style="vertical-align: top;"><%=check.Description %></td>
                        <td style="vertical-align: top;"><%=check.Group %></td>
                        <td style="vertical-align: top;"><%=status.Status %></td>
                        <td style="vertical-align: top;"><%=status.Message %></td>
                        <td style="vertical-align: top;"><%=check.CanRectify ? "Yes" : "No" %></td>
                    </tr>
            <%
                }
            %>
        </table>

        <hr/>
        
        <h2>Trees <asp:Label runat="server" ID="treeCount"/></h2>
        <ul>
            <%
                foreach (var tree in AllTrees.OrderBy(x => x.App.name))
                {
            %>
                    <li>
                        <strong>Tree Alias:</strong> <%=tree.Tree.Alias %><br/>
                        <strong>App Alias:</strong> <%=tree.App.alias %>
                    </li>
            <%      
                }
            %>
        </ul>
        
        <hr/>
        
        <h2>Rest Extensions</h2>
        
    </form>
</body>
</html>
