using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.BusinessLogic;
using Umbraco.Web.UI.Controls;

namespace CWS.UmbracoDiagnostics.Web.App_Plugins.Diagnostics
{
    public partial class dashboard : UmbracoUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get current umbraco backoffice user
            var currentUser = User.GetCurrent();

            //If not logged in
            if (currentUser == null)
            {
                return;
            }

            //If not admin
            if (!currentUser.IsAdmin())
            {
                return;
            }
        }
    }
}