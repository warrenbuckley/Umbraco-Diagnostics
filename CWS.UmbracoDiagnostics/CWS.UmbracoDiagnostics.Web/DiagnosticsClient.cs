using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using Our.Umbraco.uGoLive.Checks;

namespace CWS.UmbracoDiagnostics.Web {

    public class DiagnosticsClient {

        private ICheck[] _uGoLiveChecks;
        private DiagnosticsAssemblyInfo[] _assemblies;

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

    }

    public class DiagnosticsAssemblyInfo {

        public Assembly Assembly { get; private set; }

        public string Location {
            get { return Assembly.Location; }
        }
        
        public Version Version {
            get { return Assembly.GetName().Version; }
        }
        
        public FileVersionInfo FileVersion {
            get { return FileVersionInfo.GetVersionInfo(Assembly.Location); }
        }

        public string ChecksumMD5 { get; private set; }
        public string ChecksumSHA1 { get; private set; }

        public DiagnosticsAssemblyInfo(Assembly assembly) {
        
            Assembly = assembly;

            using (var md5 = MD5.Create()) {
                using (var stream = File.OpenRead(assembly.Location)) {
                    ChecksumMD5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
            
            using (var sha1 = SHA1.Create()) {
                using (var stream = File.OpenRead(assembly.Location)) {
                    ChecksumSHA1 = BitConverter.ToString(sha1.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        
        }
        
    }

}