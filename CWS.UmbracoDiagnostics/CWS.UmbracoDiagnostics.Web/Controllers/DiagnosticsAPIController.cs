using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        
    }
}