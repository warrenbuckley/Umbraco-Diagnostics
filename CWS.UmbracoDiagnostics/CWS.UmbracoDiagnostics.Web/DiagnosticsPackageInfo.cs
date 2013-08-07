using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.cms.businesslogic.packager;

namespace CWS.UmbracoDiagnostics.Web {

    public class DiagnosticsPackageInfo {

        public InstalledPackage Package { get; private set; }

        public int Id {
            get { return Package.Data.Id; }
        }

        public string Name {
            get { return Package.Data.Name; }
        }

        public string Version {
            get { return Package.Data.Version; }
        }

        public bool HasUpdate {
            get { return Package.Data.HasUpdate; }
        }

        public string Author {
            get { return Package.Data.Author; }
        }

        public string AuthorUrl {
            get { return Package.Data.AuthorUrl; }
        }

        public string License {
            get { return Package.Data.License; }
        }

        public string LicenseUrl {
            get { return Package.Data.LicenseUrl; }
        }
        
        public DiagnosticsPackageInfo(InstalledPackage package) {
            Package = package;
        }
    
    }

}