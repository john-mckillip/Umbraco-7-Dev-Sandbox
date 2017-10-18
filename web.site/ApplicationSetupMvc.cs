using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;
using web.site.Controllers;

namespace web.site
{
    /// <summary>
    /// This class registers the base application controller and setups up error page routing
    /// </summary>
    public class ApplicationSetupMvc : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarting(umbracoApplication, applicationContext);

            RouteTable.Routes.MapRoute
            (
                "ErrorPages",
                "error/{action}",
                new { controller = "ErrorPages", action = "Index" }
            );

            // note: this will set all routes to be hijacked by this base controller
            Umbraco.Web.Mvc.DefaultRenderMvcControllerResolver.Current.SetDefaultControllerType(typeof(CustomApplicationBaseController));
        }
    }
}