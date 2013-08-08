using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace CWS.UmbracoDiagnostics.Web.Models
{

    public class AssemblyItem
    {
        public string AssemblyName { get; set; }

        public Version AssemblyVersion { get; set; }
    }

    public class FolderPermission
    {
        public string FolderName { get; set; }

        public List<string> Permissions { get; set; }
    }

    public class UmbracoEvent
    {
        public string FullName { get; set; }

        public string Name { get; set; }

        public List<UmbracoEventItem> Items { get; set; } 
    }

    public class UmbracoEventItem
    {
        public string Location { get; set; }

        public string Method { get; set; }

        public string Namespace { get; set; }
    }
}