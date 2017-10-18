using DotNetStarter.Abstractions;
using Renderings;
using Renderings.UmbracoCms;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace web.site
{
    [StartupModule(typeof(DotNetStarter.RegisterConfiguration))]
    public class CustomConfigure : ILocatorConfigure
    {
        public void Configure(ILocatorRegistry registry, IStartupEngine engine)
        {
            registry.Add<IRelatedLinksToRenderingConverterScoped, CustomRelatedLinksToRenderingConverter>(lifetime: LifeTime.Scoped);
        }
    }

    public class CustomRelatedLinksToRenderingConverter : IRelatedLinksToRenderingConverterScoped
    {
        private readonly IRenderingAliasResolver _DescriptorResolver;
        private readonly IRenderingCreatorScoped _ViewModelCreatorScoped;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="publishedContentAliasResolver"></param>
        /// <param name="viewModelCreatorScoped"></param>
        public CustomRelatedLinksToRenderingConverter(IRenderingAliasResolver publishedContentAliasResolver, IRenderingCreatorScoped viewModelCreatorScoped)
        {
            _DescriptorResolver = publishedContentAliasResolver;
            _ViewModelCreatorScoped = viewModelCreatorScoped;
        }

        /// <summary>
        /// Converts related links to IRendering instances
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relatedLinks"></param>
        /// <param name="allowedTypes"></param>
        /// <returns></returns>
        public virtual IList<T> ConvertLinks<T>(IEnumerable<RelatedLink> relatedLinks, ICollection<Type> allowedTypes)
        {
            relatedLinks = relatedLinks ?? Enumerable.Empty<RelatedLink>();
            var list = new List<T>();
            var aliases = _DescriptorResolver.ResolveTypes(allowedTypes);

            foreach (var item in relatedLinks)
            {
                if (aliases.Any(a => a == item.Content?.DocumentTypeAlias))
                {
                    T content = (T)_ViewModelCreatorScoped.GetCreator<IPublishedContent>(item.Content.DocumentTypeAlias).Invoke(item.Content);
                    var relatedContentLink = content as ISetRelatedLink;
                    relatedContentLink?.SetLink(item); // give the link data to the model

                    list.Add(content);
                }
            }

            return list;
        }
    }
}