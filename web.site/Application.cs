using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace web.site
{
    /// <summary>
    /// Custom global application class responsible for executing DotNetStarter
    /// </summary>
    public class Application : Umbraco.Web.UmbracoApplication
    {
        public Application()
        {
            IList<Assembly> discoverableAssemblies = DotNetStarter.ApplicationContext.GetScannableAssemblies();
            IEnumerable<Assembly> preFilteredAssemblies = new Assembly[]
            {
                    // needed for Umbraco UI with custom dependency resolver
                    typeof(Umbraco.Web.UmbracoApplication).Assembly,
                    typeof(Umbraco.Forms.Web.Controllers.UmbracoFormsController).Assembly,

                    // umbraco plugin, needing a nuget install...
                    typeof(Diplo.TraceLogViewer.Controllers.TraceLogTreeController).Assembly,

                    // types in this project
                    typeof(Application).Assembly
            };

            preFilteredAssemblies = preFilteredAssemblies.Union(discoverableAssemblies);

            DotNetStarter.ApplicationContext.Startup(assemblies: preFilteredAssemblies);
        }
    }
}