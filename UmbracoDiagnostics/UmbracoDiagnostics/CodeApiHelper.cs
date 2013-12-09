using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoDiagnostics
{
    public static class CodeApiHelper
    {
        //Kudos to Darren Ferguson
        //https://bitbucket.org/darrenjferguson/open-source-umbraco-packages/src/efe07df117578f65dc1e36a64280537616166deb/event-discovery/FM.EventDiscovery/FMEvents.ascx.cs?at=default

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public static Delegate[] GetEventSubscribers(Type t, string eventName)
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

        //Kudos to Darren Ferguson
        //https://bitbucket.org/darrenjferguson/open-source-umbraco-packages/src/efe07df117578f65dc1e36a64280537616166deb/event-discovery/FM.EventDiscovery/FMEvents.ascx.cs?at=default

        /// <summary>
        /// 
        /// </summary>
        /// <param name="desiredType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> TypesImplementingInterface(Type desiredType)
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => desiredType.IsAssignableFrom(type));
        }

    }
}
