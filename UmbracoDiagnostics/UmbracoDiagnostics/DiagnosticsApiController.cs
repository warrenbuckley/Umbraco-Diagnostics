using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web.Routing;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.packager;
using umbraco.cms.businesslogic.web;
using umbraco.cms.presentation.Trees;
using Umbraco.Core.Configuration;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Strings;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;
using System.Web;
using Umbraco.Web.Routing;

namespace UmbracoDiagnostics
{
    [PluginController("Diagnostics")]
    public class DiagnosticsApiController : UmbracoAuthorizedJsonController
    {
        //GENERAL

        public DiagnosticApiModels.VersionInfo GetVersionInfo()
        {
            var version             = new DiagnosticApiModels.VersionInfo();
            version.CurrentVersion  = UmbracoVersion.Current;
            version.AssemblyVersion = UmbracoVersion.AssemblyVersion;
            version.VersionComment  = UmbracoVersion.CurrentComment;

            return version;
        }

        public DiagnosticApiModels.ServerInfo GetServerInfo()
        {
            var server              = new DiagnosticApiModels.ServerInfo();
            server.MachineName      = Environment.MachineName;
            server.ProcessorCount   = Environment.ProcessorCount;
            server.AspNetVersion    = Environment.Version;
            server.IISVersion       = HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"];

            return server;
        }

        public DiagnosticApiModels.DBInfo GetDBInfo()
        {
            var db          = new DiagnosticApiModels.DBInfo();
            db.Connection   = DatabaseContext.ConnectionString;
            db.Type         = DatabaseContext.DatabaseProvider.ToString();
            db.Configured   = DatabaseContext.IsDatabaseConfigured;

            return db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets a list of all Users in the installation
        /// </summary>
        /// <returns>The list of users</returns>
        public List<User> GetUsers()
        {
            var users = umbraco.BusinessLogic.User.getAll().ToList();
            return users;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Domain> GetDomains()
        {
            return Domain.GetDomains().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DiagnosticApiModels.AssemblyItem> GetAllAssemblies()
        {
            var assemblies = new List<DiagnosticApiModels.AssemblyItem>();

            //Assemblies
            string path = HttpContext.Current.Server.MapPath("~/bin");

            //for each DLL in /bin folder add it to the list
            foreach (string dll in Directory.GetFiles(path, "*.dll"))
            {
                //Load DLL
                var item = Assembly.LoadFile(dll);

                //Get the values from the DLL
                var assemblyToAdd               = new DiagnosticApiModels.AssemblyItem();
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
                using (var sha1 = SHA1.Create())
                {
                    using (var stream = File.OpenRead(item.Location))
                    {
                        assemblyToAdd.ChecksumSHA1 = BitConverter.ToString(sha1.ComputeHash(stream)).Replace("-", "").ToLower();
                    }
                }

                //Add it to the list
                assemblies.Add(assemblyToAdd);
            }

            //Return the list
            return assemblies;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DiagnosticApiModels.FolderPermission> GetFolderPermissions()
        {
            //Create a list of folder permissions
            var permissions = new List<DiagnosticApiModels.FolderPermission>();

            //Root folder path
            string rootFolderPath = HttpContext.Current.Server.MapPath("~/");

            //Get the root folder
            var rootFolder = new DirectoryInfo(rootFolderPath);

            //Loop over folders
            foreach (var folder in rootFolder.GetDirectories())
            {
                var permissionToAdd         = new DiagnosticApiModels.FolderPermission();
                permissionToAdd.FolderName  = folder.Name;

                var rules = new List<DiagnosticApiModels.FolderPermissionItem>();

                //Loop over rules
                //Taken from - http://forums.asp.net/t/1625708.aspx?Folder+rights+on+network
                foreach (FileSystemAccessRule rule in folder.GetAccessControl().GetAccessRules(true, true, typeof(NTAccount)))
                {
                    var permissionItemToAdd     = new DiagnosticApiModels.FolderPermissionItem();
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DiagnosticApiModels.UmbracoEvent> GetEvents()
        {
            //List of items
            var events = new List<DiagnosticApiModels.UmbracoEvent>();

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
                    var eventToAdd = new DiagnosticApiModels.UmbracoEvent();


                    Delegate[] handlers = CodeApiHelper.GetEventSubscribers(t, info.Name);
                    if (handlers.Length > 0)
                    {
                        //Add FullName to object
                        eventToAdd.FullName = t.FullName;


                        //Add Name to object
                        eventToAdd.Name = info.Name;

                        var eventItems = new List<DiagnosticApiModels.UmbracoEventItem>();

                        foreach (Delegate d in handlers)
                        {
                            var eventItemToAdd          = new DiagnosticApiModels.UmbracoEventItem();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<String> GetMvcRoutes()
        {
            var allRoutes = new List<String>();

            //Get the routes from route table
            var routes = RouteTable.Routes;

            //loop over them
            foreach (var route in routes)
            {
                //Cast it
                var item = (Route)route;

                //Add it to the list
                allRoutes.Add(item.Url);
            }

            //Return the list
            return allRoutes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TreeDefinition> GetTrees()
        {
            var allTrees = TreeDefinitionCollection.Instance.OrderBy(x => x.App.alias).ThenBy(x => x.Tree.Alias);
            return allTrees;
        }



        //RESOLVERS
        //TODO
        /*
            TreeControllers
            RestExtensions
            
         * 
         * === DONE ===
         * ContentFinderResolver
         * UrlProviderResolver
         * PropertyValueConverters
         * UrlSegmentProviderResolver
         * 
         * 
         * 
         * === TODO ===
         * SurfaceControllerResolver
         * UmbracoApiControllerResolver
         * XsltExtensionsResolver
         * PublishedContentModelFactoryResolver 
         * ImageUrlProviderResolver 
         * SiteDomainHelperResolver 
         * DatabaseTypeResolver 
         * ThumbnailProvidersResolver 
         * RepositoryResolver
         * ProfilerResolver
         * DataTypesResolver 
         * PreValueDisplayResolver 
         * MacroFieldEditorsResolver 
         * CacheRefreshersResolver
         * ServerMessengerResolver
         * PropertyEditorResolver
         * ParameterEditorResolver
         * ActionsResolver
         * ShortStringHelperResolver
         * PackageActionsResolver
         * DependencyResolver
         * ValidatorsResolver
         * TabsAndPropertiesResolver 
         * ServerRegistrarResolver
         * MacroPropertyTypeResolver
         * AvailablePropertyEditorsResolver
         * DatabaseTypeResolver
         * ContentLastChanceFinderResolver
        */

        // RESOLVERS

        public IEnumerable<IContentFinder> GetContentFinders()
        {
            return ContentFinderResolver.Current.Finders;
        }

        public IEnumerable<IUrlProvider> GetUrlProviders()
        {
            return UrlProviderResolver.Current.Providers;
        }

        public IEnumerable<IPropertyValueConverter> GetPropertyValueConverters()
        {
            return PropertyValueConvertersResolver.Current.Converters;
        }

        public IEnumerable<IUrlSegmentProvider> GetUrlSegmentProvider()
        {
            return UrlSegmentProviderResolver.Current.Providers;
        }

        public IContentFinder GetContentLastChanceFinder()
        {
            return ContentLastChanceFinderResolver.Current.Finder;
        }
    }
}
