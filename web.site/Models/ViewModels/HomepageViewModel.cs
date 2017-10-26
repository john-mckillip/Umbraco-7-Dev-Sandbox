using Renderings;
using Renderings.UmbracoCms;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace web.site.Models.ViewModels
{
    [RenderingDocumentAlias("homepage")]
    public class HomepageViewModel : IUmbracoRendering
    {
        public HomepageViewModel(IPublishedContent content)
        {
            Content = content;  
        }

        ///<summary>
        /// Mapped property for Hero Title
        ///</summary>
        [RenderingPropertyAlias("heroTitle")]
        public string HeroTitle
        {
            get { return Content.GetPropertyValue<string>("heroTitle"); }
        }

        ///<summary>
        /// Mapped property for Hero Subtitle
        ///</summary>
        [RenderingPropertyAlias("heroSubtitle")]
        public string HeroSubtitle
        {
            get { return Content.GetPropertyValue<string>("heroSubtitle"); }
        }

        ///<summary>
        /// Mapped property for Hero Button Link
        ///</summary>
        [RenderingPropertyAlias("heroButtonLink")]
        public RelatedLink HeroLink
        {
            get { return Content.GetPropertyValue<RelatedLinks>("heroButtonLink")?.FirstOrDefault() ?? new RelatedLink() { Caption = "Link not set", Link = "#notset" }; }
        }

        ///<summary>
        /// Mapped property for Hero Subtitle
        ///</summary>
        [RenderingPropertyAlias("heroButtonTitle")]
        public string HeroButtonTitle
        {
            get { return Content.GetPropertyValue<string>("heroButtonTitle") ?? "Learn More" ; }
        }

        #region Interface Implementation

        public UmbracoHelper Umbraco { get; }

        public bool IsFullPage => true;

        public IPublishedContent Content { get; }

        /// <summary>
        /// We don't want to reuse homepage, so return "Empty" which corresponds to /Views/Partials/Empty.cshtml
        /// </summary>
        /// <param name="renderTag"></param>
        /// <returns></returns>
        public string GetPartialView(string renderTag = null)
        {
            return "Empty";
        }

        #endregion
    }
}