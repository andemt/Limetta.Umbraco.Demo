using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Umbraco.Core.Models.PublishedContent;
using Limetta.Umbraco.Demo.Common.Extensions;
using Limetta.Umbraco.Demo.RenderElements.Factories;

namespace Limetta.Umbraco.Demo.RenderElements.Extensions
{
	public static class ElementExtensions
	{
		public static void RenderElements(this HtmlHelper helper, IEnumerable<IPublishedElement> elements)
		{
			TryRender(helper, elements, (element, controller, method) =>
			{
				helper.RenderAction(method, controller, new { element });
			}, (element, view, model) =>
			{
				helper.RenderPartial(view, model ?? element);
			});
		}

		public static MvcHtmlString Elements(this HtmlHelper helper, IEnumerable<IPublishedElement> elements)
		{
			var html = new MvcHtmlString("");
			TryRender(helper, elements, (element, controller, method) =>
			{
				var elementHtml = helper.Action(method, controller, new { element });
				html = html.Concat(elementHtml);
			}, (element, view, model) =>
			{
				var elementHtml = helper.Partial(view, model ?? element);
				html = html.Concat(elementHtml);
			});
			return html;
		}

		private static void TryRender(
			HtmlHelper helper,
			IEnumerable<IPublishedElement> elements,
			Action<IPublishedElement, string, string> actionAction,
			Action<IPublishedElement, string, object> actionPartial)
		{
			if (elements.IsEmpty())
				return;

			foreach (var element in elements)
			{
				if (element?.ContentType == null)
					continue;

				var routing = GetFactory().GetRoutingForElement(helper, element);
				if(routing == null)
					continue;

				if (routing.View.IsValid())
					actionPartial(element, routing.View, routing.Model);

				if (routing.Controller.IsValid() && routing.Method.IsValid())
					actionAction(element, routing.Controller, routing.Method);
			}
		}

		private static MvcHtmlString Concat(this MvcHtmlString item, params MvcHtmlString[] items)
		{
			if (items.IsEmpty())
				return item;

			var sb = new StringBuilder();
			foreach (var moreItem in items.Where(i => i != null))
				sb.Append(moreItem.ToHtmlString());

			if (sb.Length < 1)
				return item;

			return MvcHtmlString.Create(item.ToHtmlString() + sb);
		}

		private static ElementsRoutingFactory _factory;
		private static ElementsRoutingFactory GetFactory()
		{
			return _factory ?? (_factory = DependencyResolver.Current.GetService<ElementsRoutingFactory>());
		}
	}
}
