using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Umbraco.Core.Configuration;

namespace CWS.UmbracoDiagnostics.Web
{
    public partial class diagnostics : System.Web.UI.Page
    {
        public List<Assembly> AllAssemblies = new List<Assembly>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Umbraco Version
            umbAssembly.Text    = UmbracoVersion.AssemblyVersion;
            umbVersion.Text     = UmbracoVersion.Current.ToString();

            //Assemblies
            string path         = Server.MapPath("~/bin");

            //for each DLL in /bin folder
            foreach (string dll in Directory.GetFiles(path, "*.dll"))
            {
                AllAssemblies.Add(Assembly.LoadFile(dll));
            }

            //Update Assembly counter
            assemblyCount.Text = AllAssemblies.Count.ToString();

        }
    }
}