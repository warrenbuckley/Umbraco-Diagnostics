using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace CWS.UmbracoDiagnostics.Web {

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