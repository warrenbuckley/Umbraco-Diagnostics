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

        public string ChecksumMD5 { get; set; }

        public string ChecksumSHA1 { get; set; }
    }

    public class FolderPermission
    {
        public string FolderName { get; set; }

        public List<FolderPermissionItem> Permissions { get; set; }
    }

    public class FolderPermissionItem
    {
        public string User { get; set; }

        public string Type { get; set; }

        public string Access { get; set; }
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

    public class ServerInfo
    {
        public string MachineName { get; set; }

        public int ProcessorCount { get; set; }

        public Version AspNetVersion { get; set; }

        public string IISVersion { get; set; }

    }

    public class DBInfo
    {
        public string Connection { get; set; }

        public string Type { get; set; }

        public bool Configured { get; set; }
        
    }

}