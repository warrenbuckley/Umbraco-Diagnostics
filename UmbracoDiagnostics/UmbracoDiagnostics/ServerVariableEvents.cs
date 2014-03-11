using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Web.UI.JavaScript;
using System.Web;
using System.Web.Routing;
using Umbraco.Web;

namespace UmbracoDiagnostics
{
    public class ServerVariableEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ServerVariablesParser.Parsing += Parsing;
            base.ApplicationStarted(umbracoApplication, applicationContext);
        }

        private void Parsing(object sender, Dictionary<string, object> e)
        {
            if (HttpContext.Current == null) throw new InvalidOperationException("HttpContext is null");
            var urlHelper = new UrlHelper(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()));

            var mainDictionary = new Dictionary<string, object>
            {
                {
                    "DiagnosticsBaseUrl",
                    urlHelper.GetUmbracoApiServiceBaseUrl<DiagnosticsApiController>(controller => controller.GetMvcRoutes())
                }
            };
            e.Add("Diagnostics", mainDictionary);
        }
    }
}