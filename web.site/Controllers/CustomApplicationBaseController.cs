using Renderings;
using Renderings.UmbracoCms;
using System.Globalization;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace web.site.Controllers
{
    public class CustomApplicationBaseController : RenderMvcController
    {
        private readonly IRenderingCreatorScoped _ViewModelCreator;

        public CustomApplicationBaseController(IRenderingCreatorScoped viewModelCreator)
        {
            _ViewModelCreator = viewModelCreator;
        }

        public override ActionResult Index(RenderModel model)
        {
            var viewModel = BuildViewModel(model.Content, model.CurrentCulture);

            if (viewModel?.IsFullPage == false)
            {
                return new HttpNotFoundResult();
            }

            return CurrentTemplate(viewModel);
        }

        private IUmbracoRendering BuildViewModel(IPublishedContent content, CultureInfo cultureInfo)
        {
            var viewCreator = _ViewModelCreator.GetCreator<IPublishedContent>(content.DocumentTypeAlias);
            var returnModel = viewCreator.Invoke(content) as IUmbracoRendering;

            if (returnModel is IUmbracoRenderingWithCulture cultureModel)
            {
                cultureModel.CurrentCulture = cultureInfo;
            }

            return returnModel;
        }
    }
}