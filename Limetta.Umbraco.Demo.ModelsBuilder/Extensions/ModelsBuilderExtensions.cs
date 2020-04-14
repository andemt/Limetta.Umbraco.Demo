using System;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Limetta.Umbraco.Demo.Common.Extensions;
using Umbraco.Web;

namespace Limetta.Umbraco.Demo.ModelsBuilder.Extensions
{
	public static class ModelsBuilderExtensions
	{
		public static T ToPage<T>(this IPublishedContent content)
			where T : PublishedContentModel
		{
			var alias = GetContentTypeAlias<T>();
			return CreatePage<T>(content, alias);
		}

		public static T ToElement<T>(this IPublishedElement content)
			where T : PublishedElementModel
		{
			var alias = GetContentTypeAlias<T>();
			return CreateElement<T>(content, alias);
		}

		private static string GetContentTypeAlias<T>()
			where T : class
		{
			var attribute = typeof(T)
				.GetCustomAttributes(typeof(PublishedModelAttribute), true)
				.FirstOrDefault() as PublishedModelAttribute;

			return attribute?.ContentTypeAlias ?? string.Empty;
		}

		private static T CreatePage<T>(IPublishedContent content, string documentTypeAlias)
			where T : PublishedContentModel
		{
			var page = CreateContent<T>(content);

			if (page == null)
				return null;
			if (documentTypeAlias.IsInvalid())
				return page;
			if (page.ContentType.Alias.EqualsIgnoreCase(documentTypeAlias))
				return page;
			if (page.IsComposedOf(documentTypeAlias))
				return page;

			return null;
		}

		private static T CreateElement<T>(IPublishedElement content, string documentTypeAlias)
			where T : PublishedElementModel
		{
			var element = CreateContent<T>(content);

			if (element == null)
				return null;
			if (documentTypeAlias.IsInvalid())
				return element;
			if (element.ContentType.Alias.EqualsIgnoreCase(documentTypeAlias))
				return element;
			if (element.IsComposedOf(documentTypeAlias))
				return element;

			return null;
		}

		private static T CreateContent<T>(IPublishedElement content)
			where T : class
		{
			if (content == null)
				return null;

			return (T)Activator.CreateInstance(typeof(T), content);
		}
	}
}
