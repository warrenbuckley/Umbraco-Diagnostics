using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.Routing;
using CWS.UmbracoDiagnostics.Web.Models;
using Our.Umbraco.uGoLive.Checks;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.packager;
using umbraco.cms.businesslogic.web;
using Umbraco.Core.Configuration;
using umbraco.interfaces;
using Umbraco.Web.WebApi;
using umbraco.cms.presentation.Trees;
using Package = umbraco.cms.businesslogic.packager.repositories.Package;

namespace CWS.UmbracoDiagnostics.Web.Controllers
{
    public class DiagnosticsAPIController : UmbracoAuthorizedApiController
    {

        public Version GetVersion()
        {
            return UmbracoVersion.Current;
        }

        public string GetVersionAssembly()
        {
            return UmbracoVersion.AssemblyVersion;
        }

        public string GetVersionComment()
        {
            return UmbracoVersion.CurrentComment;
        }

        public ServerInfo GetServerInfo()
        {
            var server              = new ServerInfo();
            server.MachineName      = Environment.MachineName;
            server.ProcessorCount   = Environment.ProcessorCount;
            server.AspNetVersion    = Environment.Version;
            server.IISVersion       = HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"];

            return server;
        }

        public DBInfo GetDBInfo()
        {
            var db          = new DBInfo();
            db.Connection   = DatabaseContext.ConnectionString;
            db.Type         = DatabaseContext.DatabaseProvider.ToString();
            db.Configured   = DatabaseContext.IsDatabaseConfigured;

            return db;
        }

        public IEnumerable<ICheck> GetUGoLiveChecks()
        {
            return CheckFactory.Checks.OrderBy(x => x.Group).ThenBy(x => x.Name);
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


                //MD5
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(item.Location))
                    {
                        assemblyToAdd.ChecksumMD5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                    }
                }

                //SHA1
                using (var sha1 = SHA1.Create()) {
                    using (var stream = File.OpenRead(item.Location)) {
                        assemblyToAdd.ChecksumSHA1 = BitConverter.ToString(sha1.ComputeHash(stream)).Replace("-", "").ToLower();
                    }
                }

                //Add it to the list
                assemblies.Add(assemblyToAdd);

            }

            //Return the list
            return assemblies;

        }

        public IEnumerable<TreeDefinition> GetTrees()
        {
            var allTrees = TreeDefinitionCollection.Instance.OrderBy(x => x.App.alias).ThenBy(x => x.Tree.Alias);
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

                var rules = new List<FolderPermissionItem>();

                //Loop over rules
                //Taken from - http://forums.asp.net/t/1625708.aspx?Folder+rights+on+network
                foreach (FileSystemAccessRule rule in folder.GetAccessControl().GetAccessRules(true, true, typeof(NTAccount)))
                {
                    var permissionItemToAdd     = new FolderPermissionItem();
                    permissionItemToAdd.Type    = rule.FileSystemRights.ToString();
                    permissionItemToAdd.User    = rule.IdentityReference.Value;
                    permissionItemToAdd.Access  = rule.AccessControlType == AccessControlType.Allow ? "grants" : "denies";

                    //Add items to the rule
                    rules.Add(permissionItemToAdd);
                }

                //Set the permissions on object to the rules list
                permissionToAdd.Permissions = rules;

                //Add it to the list
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

        public List<Domain> GetDomains()
        {
            return Domain.GetDomains().ToList();
        }

        public List<User> GetUsers()
        {
            var users = umbraco.BusinessLogic.User.getAll().ToList();

            return users;
        }

        public List<PackageInstance> GetPackages()
        {
            var allPackages = new List<PackageInstance>();

            //Get packages
            var packages = InstalledPackage.GetAllInstalledPackages();

            //loop over them
            foreach (var item in packages)
            {
                allPackages.Add(item.Data);
            }

            //Return the list
            return allPackages;
        }


        public List<string> GetActionHandlers()
        {
            var actionHandlers = new List<string>();

            foreach (Type t in TypesImplementingInterface(typeof(umbraco.BusinessLogic.Actions.IActionHandler)))
            {
                if (!t.Name.Equals("IActionHandler"))
                {
                    var itemToAdd = string.Format("{0} => {1}", t.FullName, new FileInfo(t.Assembly.Location).Name);

                    actionHandlers.Add(itemToAdd);
                }
            }

            //return the list
            return actionHandlers;
        }

        public List<UmbracoEvent> GetEvents()
        {
            //List of items
            var events = new List<UmbracoEvent>();

            //Darren Ferguson's Ace Code...
            foreach (Type t in AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes()
                    .Where(type => type.FullName.ToLower().StartsWith("umbraco"))

                ))
            {
                foreach (EventInfo info in t.GetEvents())
                {
                    var eventToAdd = new UmbracoEvent();


                    Delegate[] handlers = GetEventSubscribers(t, info.Name);
                    if (handlers.Length > 0)
                    {
                        //Add FullName to object
                        eventToAdd.FullName = t.FullName;


                        //Add Name to object
                        eventToAdd.Name = info.Name;

                        var eventItems = new List<UmbracoEventItem>();

                        foreach (Delegate d in handlers)
                        {
                            var eventItemToAdd          = new UmbracoEventItem();
                            eventItemToAdd.Location     = new FileInfo(d.Method.DeclaringType.Assembly.Location).Name;
                            eventItemToAdd.Method       = d.Method.Name;
                            eventItemToAdd.Namespace    = d.Method.DeclaringType.Namespace + "." + d.Method.DeclaringType.Name;

                            eventItems.Add(eventItemToAdd);
                        }

                        //Add the items list to the main object
                        eventToAdd.Items = eventItems;

                        //Add it to the main list
                        events.Add(eventToAdd);
                    }
                }
            }

            //Return the list
            return events;
        }

        //Kudos to Darren Ferguson
        //https://bitbucket.org/darrenjferguson/open-source-umbraco-packages/src/efe07df117578f65dc1e36a64280537616166deb/event-discovery/FM.EventDiscovery/FMEvents.ascx.cs?at=default
        public static IEnumerable<Type> TypesImplementingInterface(Type desiredType)
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => desiredType.IsAssignableFrom(type));
        }

        //Kudos to Darren Ferguson
        //https://bitbucket.org/darrenjferguson/open-source-umbraco-packages/src/efe07df117578f65dc1e36a64280537616166deb/event-discovery/FM.EventDiscovery/FMEvents.ascx.cs?at=default
        protected Delegate[] GetEventSubscribers(Type t, string eventName)
        {
            // Type t = target.GetType();
            List<Delegate> x = new List<Delegate>();

            FieldInfo[] fia = t.GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            foreach (FieldInfo fi in fia)
            {

                // Literal1.Text += fi.Name + " - Name<br/>";
                if (fi.Name.Equals(eventName))
                {
                    // Literal1.Text += fi.Name+ " - Name<br/>";
                    try
                    {
                        
                        object o = fi.GetValue(null);
                        Type oType = o.GetType();
                        // Response.Write(o.GetType().Name + "<br/>");

                        Delegate d = (Delegate)o;
                        foreach (Delegate f in d.GetInvocationList())
                        {
                            x.Add(f);
                        }

                        // x.Add(d);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                // else { Response.Write("no - " + fi.Name + "<br/>"+Environment.NewLine); }
            }


            return x.ToArray();

        }
    }
}
