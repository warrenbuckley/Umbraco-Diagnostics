using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using Our.Umbraco.uGoLive.Checks;
using umbraco.BusinessLogic;
using umbraco.DataLayer;
using umbraco.cms.businesslogic.packager;
using umbraco.cms.businesslogic.web;

namespace CWS.UmbracoDiagnostics.Web {

    public class DiagnosticsClient {

        private ICheck[] _uGoLiveChecks;
        private DiagnosticsAssemblyInfo[] _assemblies;
        private DiagnosticsPackageInfo[] _packages;
        private Domain[] _domains;

        /// <summary>
        /// Gets the machine name of the server.
        /// </summary>
        public string MachineName {
            get { return Environment.MachineName; }
        }

        /// <summary>
        /// The number of processors on the server.
        /// </summary>
        public int ProcessorCount {
            get { return Environment.ProcessorCount; }
        }

        /// <summary>
        /// Gets the version of the current ASP.NET framework.
        /// </summary>
        public Version AspNetVersion {
            get { return Environment.Version; }
        }

        /// <summary>
        /// Gets the version of the IIS server.
        /// </summary>
        public string IISVersion {
            get { return HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"]; }
        }

        /// <summary>
        /// Gets the current Umbraco version.
        /// </summary>
        public string UmbracoVersion {
            get { return Umbraco.Core.Configuration.UmbracoVersion.Current.ToString(); }
        }

        /// <summary>
        /// Gets the version of the Umbraco assembly.
        /// </summary>
        public string UmbracoAssemblyVersion {
            get { return Umbraco.Core.Configuration.UmbracoVersion.AssemblyVersion; }
        }

        /// <summary>
        /// Gets the database type based on the connection string in web.config.
        /// </summary>
        public string DatabaseType { get; private set; }

        /// <summary>
        /// Gets the database host if available.
        /// </summary>
        public string DatabaseHost { get; private set; }

        /// <summary>
        /// Gets an array of all default and custom uGoLive checks.
        /// </summary>
        public ICheck[] uGoLiveChecks {
            get { return _uGoLiveChecks ?? (_uGoLiveChecks = CheckFactory.Checks.ToArray()); }
        }

        /// <summary>
        /// Gets an array loaded assemblies for the current domain.
        /// </summary>
        public DiagnosticsAssemblyInfo[] Assemblies {
            get {
                return _assemblies ?? (_assemblies = (
                    from path in Directory.GetFiles(HttpContext.Current.Server.MapPath("~/bin"), "*.dll")
                    let assembly = Assembly.LoadFile(path)
                    where !assembly.IsDynamic
                    select new DiagnosticsAssemblyInfo(assembly)
                ).ToArray());
            }
        }

        /// <summary>
        /// Gets an array of all installed packages.
        /// </summary>
        public DiagnosticsPackageInfo[] InstalledPackages {
            get {
                return _packages ?? (_packages = (
                    from package in InstalledPackage.GetAllInstalledPackages()
                    select new DiagnosticsPackageInfo(package)
                ).ToArray());
            }
        }

        /// <summary>
        /// Gets an array of all domains (host names) added to Umbraco.
        /// </summary>
        public Domain[] Domains {
            get {
                if (_domains == null) {
                    Dictionary<int, Domain> temp = new Dictionary<int, Domain>();
                    using (IRecordsReader reader = Application.SqlHelper.ExecuteReader("select id, domainName from umbracoDomains")) {
                        while (reader.Read()) {
                            int id = reader.GetInt("id");
                            if (!temp.ContainsKey(id)) temp.Add(id, new Domain(id));
                        }
                    }
                    _domains = temp.Values.ToArray();
                }
                return _domains;
            }
        }

        public DiagnosticsClient() {
            ParseDatabaseType();
        }

        public void ParseDatabaseType() {

            var dsn = ConfigurationManager.ConnectionStrings["umbracoDbDSN"];
            if (dsn == null) return;

            // TODO: Handle more scenarios

            if (dsn.ProviderName.Contains("SqlServerCe")) {
                DatabaseType = "SqlServer Compact Edition";
            } else if (dsn.ProviderName.Equals("System.Data.SqlClient")) {
                DatabaseType = "SqlServer";
                Match m = Regex.Match(dsn.ConnectionString, "server=([a-zA-Z0-9\\.]+);");
                if (m.Success) {
                    DatabaseHost = m.Groups[1].Value;
                }
            }
        
        }

    }

}