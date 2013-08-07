using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.cms.presentation;
using umbraco.cms.presentation.Trees;
using Umbraco.Core.Configuration;
using Umbraco.Core.ObjectResolution;
using umbraco.interfaces;
using umbraco.presentation.umbracobase;
using Umbraco.Web;
using Umbraco.Web.BaseRest;

namespace CWS.UmbracoDiagnostics.Web
{
    public partial class diagnostics : System.Web.UI.Page
    {
        public DiagnosticsClient Diagnostics { get; private set; }

        public List<Assembly> AllAssemblies             = new List<Assembly>();
        public List<TreeDefinition> AllTrees            = new List<TreeDefinition>();
        public List<RestExtension> AllRestExtensions    = new List<RestExtension>(); 


        protected void Page_Load(object sender, EventArgs e)
        {

            Diagnostics = new DiagnosticsClient();

            //Get Version
            GetVersion();

            //Get Assemblies
            GetAssemblies();

            //Get Trees
            GetTrees();


        }

        protected void GetVersion()
        {
            //Umbraco Version
            umbAssembly.Text = UmbracoVersion.AssemblyVersion;
            umbVersion.Text = UmbracoVersion.Current.ToString();
        }

        protected void GetAssemblies()
        {
            //Assemblies
            string path = Server.MapPath("~/bin");

            //for each DLL in /bin folder
            foreach (string dll in Directory.GetFiles(path, "*.dll"))
            {
                AllAssemblies.Add(Assembly.LoadFile(dll));
            }

            //Update Assembly counter
            assemblyCount.Text = AllAssemblies.Count.ToString();
        }

        protected void GetTrees()
        {
            //Get all trees
            AllTrees = TreeDefinitionCollection.Instance;

            //Tree Count
            treeCount.Text = AllTrees.Count.ToString();
        }

    }
}