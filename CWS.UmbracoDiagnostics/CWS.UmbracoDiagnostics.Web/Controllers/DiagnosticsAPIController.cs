using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web;
using System.Web.Routing;
using CWS.UmbracoDiagnostics.Web.Models;
using Umbraco.Core.Configuration;
using umbraco.interfaces;
using Umbraco.Web.WebApi;
using umbraco.cms.presentation.Trees;

namespace CWS.UmbracoDiagnostics.Web.Controllers
{
    public class DiagnosticsAPIController : UmbracoApiController
    {

        public Version GetVersion()
        {
            return UmbracoVersion.Current;
        }

        public string GetVersionAssembly()
        {
            return UmbracoVersion.AssemblyVersion;
        }

        public List<AssemblyItem> GetAllAssemblies()
        {
            var assemblies = new List<AssemblyItem>();

            //Assemblies
            string path = HttpContext.Current.Server.MapPath("~/bin");

            //for each DLL in /bin folder add it to the list
            foreach (string dll in Directory.GetFiles(path, "*.dll"))
            {
                //Load DLL
                var item = Assembly.LoadFile(dll);

                //Get the values from the DLL
                var assemblyToAdd               = new AssemblyItem();
                assemblyToAdd.AssemblyName      = item.GetName().Name;
                assemblyToAdd.AssemblyVersion   = item.GetName().Version;

                //Add it to the list
                assemblies.Add(assemblyToAdd);

            }

            //Return the list
            return assemblies;

        }

        public TreeDefinitionCollection GetTrees()
        {
            var allTrees = TreeDefinitionCollection.Instance;
            return allTrees;
        }


        public List<FolderPermission> GetFolderPermissions()
        {
            //Create a list of folder permissions
            var permissions = new List<FolderPermission>();

            //Root folder path
            string rootFolderPath = HttpContext.Current.Server.MapPath("~/");

            //Get the root folder
            var rootFolder = new DirectoryInfo(rootFolderPath);

            //Loop over folders
            foreach (var folder in rootFolder.GetDirectories())
            {
                var permissionToAdd         = new FolderPermission();
                permissionToAdd.FolderName  = folder.Name;

                var rules = new List<string>();

                //Loop over rules
                //Taken from - http://forums.asp.net/t/1625708.aspx?Folder+rights+on+network
                foreach (FileSystemAccessRule rule in folder.GetAccessControl().GetAccessRules(true, true, typeof(NTAccount)))
                {
                    var item = string.Format("Rule {0} {1} access to {2}",
                        rule.AccessControlType == AccessControlType.Allow ? "grants" : "denies",
                        rule.FileSystemRights.ToString(),
                        rule.IdentityReference.Value);

                    //Add items to the rule
                    rules.Add(item);
                }

                //Set the permissions on object to the rules list
                permissionToAdd.Permissions = rules;

                //Add it to the lsit
                permissions.Add(permissionToAdd);
            }

            //Return the list
            return permissions;
        }

        public List<String> GetMvcRoutes()
        {
            var allRoutes = new List<String>();

            //Get the routes from route table
            var routes = RouteTable.Routes;

            //loop over them
            foreach (var route in routes)
            {
                //Cast it
                var item = (Route) route;

                //Add it to the list
                allRoutes.Add(item.Url.ToString());
            }

            //Return the list
            return allRoutes;
        }
    }
}