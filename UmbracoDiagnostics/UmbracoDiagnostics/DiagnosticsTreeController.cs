using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using Umbraco.Core;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace UmbracoDiagnostics
{
    [Tree("developer", "diagnosticsTree", "Diagnostics")]
    [PluginController("Diagnostics")]
    public class DiagnosticsTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            //check if we're rendering the root node's children
            if (id == Constants.System.Root.ToInvariantString())
            {
                //Nodes that we will return
                var nodes   = new TreeNodeCollection();

                //Temp items for testing
                var items = new Dictionary<string, string>();
                items.Add("icon-server", "General");
                items.Add("icon-brick", "Packages");
                items.Add("icon-users", "Users");
                items.Add("icon-globe", "Domains");
                items.Add("icon-binarycode", "Assemblies");
                items.Add("icon-keychain", "Permissions");
                items.Add("icon-brackets", "Events");
                items.Add("icon-directions", "MVC Routes");
                items.Add("icon-tree", "Trees");


                foreach (var item in items)
                {
                    //When clicked - /App_Plugins/Diagnostics/backoffice/diagnosticsTree/edit.html
                    //URL in address bar - /developer/diagnosticsTree/General/someID
                    var route       = string.Format("/developer/diagnosticsTree/view/{0}", item.Value);
                    var nodeToAdd   = CreateTreeNode(item.Value.Replace(" ","-"), null, queryStrings, item.Value, item.Key, false, route);


                    //Add it to the collection
                    nodes.Add(nodeToAdd);
                }

                //Return the nodes
                return nodes;
            }

            //this tree doesn't suport rendering more than 1 level
            throw new NotSupportedException();
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            return null;
        }
    }
}
