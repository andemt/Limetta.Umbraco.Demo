using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Limetta.Umbraco.Demo.Common.Extensions;
using Limetta.Umbraco.Demo.RenderElements.Controllers;
using Umbraco.Web.Mvc;

namespace Limetta.Umbraco.Demo.RenderElements.Factories
{
	public class ElementsRoutingFactory
	{
		private HtmlHelper _html;
		private IPublishedElement _element;

		public ElementRoutingProperties GetRoutingForElement(HtmlHelper html, IPublishedElement element)
		{
			if (element?.ContentType?.Alias == null)
				return null;

			_html = html;
			_element = element;

			var searchName = element.ContentType.Alias.ToFirstUpper();

			var routing = GetRoutingPropertiesForController($"{searchName}Controller", "Index");
			if(routing == null || routing.Controller.IsInvalid())
				routing = GetRoutingPropertiesForController(typeof(ElementsController).Name, searchName);
			if (routing == null || routing.Controller.IsInvalid())
				routing = GetRoutingPropertiesForPartial(searchName);

			return routing;
		}

		private ElementRoutingProperties GetRoutingPropertiesForPartial(string view)
		{
			if (view.IsInvalid())
				return null;

			var key = $"view-{view}";

			var cache = GetCache(key);
			if (cache != null)
			{
				cache.Model = GetObjectForType(cache.ModelType);
				return cache;
			}

			// Try to find partial view
			var controllerContext = _html.ViewContext.Controller.ControllerContext;
			var result = ViewEngines.Engines.FindView(controllerContext, view, null);

			if (result == null)
			{
				AddCache(key, new ElementRoutingProperties());
				return null;
			}

			// Try to Models builder class
			var asm = Assembly.GetExecutingAssembly();
			var elementType = asm
				.GetTypes()
				.Where(type => typeof(PublishedElementModel).IsAssignableFrom(type))
				.FirstOrDefault(controller => controller.FullName?.Contains(view) ?? false);

			var property = new ElementRoutingProperties
			{
				View = view,
				ModelType = elementType,
				Model = GetObjectForType(elementType),
			};

			AddCache(key, property);

			return property;
		}

		private object GetObjectForType(Type elementType)
		{
			return elementType == null
				? null
				: Activator.CreateInstance(elementType, _element);
		}

		private ElementRoutingProperties GetRoutingPropertiesForController(string controllerName, string methodName)
		{
			if (controllerName.IsInvalid() || methodName.IsInvalid())
				return null;

			var key = $"{controllerName}-{methodName}";

			var cache = GetCache(key);
			if (cache != null)
				return cache;

			// Try to find ElementsController with ProductElement-method
			// or ProductElementController with Index-method
			var asm = Assembly.GetExecutingAssembly();
			var method = asm.GetTypes()
				.Where(type => typeof(SurfaceController).IsAssignableFrom(type)) // elements controllers must be SurfaceController
				.Where(controller => controller.FullName?.Contains(controllerName) ?? false)
				.SelectMany(type => type.GetMethods())
				.Where(m => m.IsPublic
								 && !m.IsDefined(typeof(NonActionAttribute))
								 //&& m.IsDefined(typeof(ChildActionOnlyAttribute))
								 && m.ReturnType == typeof(ActionResult)
								 && m.Name.EqualsIgnoreCase(methodName)
				)
				.ToList()
				.FirstOrDefault();

			if (method?.DeclaringType?.Name == null)
			{
				AddCache(key, new ElementRoutingProperties());
				return null;
			}

			var property = new ElementRoutingProperties
			{
				Controller = method.DeclaringType.Name.Replace("Controller", ""),
				Method = method.Name,
			};

			AddCache(key, property);

			return property;
		}

		private static Dictionary<string, ElementRoutingProperties> _cache;
		private void AddCache(string key, ElementRoutingProperties property)
		{
			if (key.IsInvalid())
				return;

			if (_cache == null)
				_cache = new Dictionary<string, ElementRoutingProperties>();

			if (_cache.ContainsKey(key))
				_cache[key] = property;
			else
				_cache.Add(key, property);
		}

		private ElementRoutingProperties GetCache(string key)
		{
			if (_cache == null || key.IsInvalid())
				return null;

			return _cache.ContainsKey(key)
				? _cache[key]
				: null;
		}
	}

	public class ElementRoutingProperties
	{
		public string Controller { get; set; }
		public string Method { get; set; }
		public string View { get; set; }
		public object Model { get; set; }
		public Type ModelType { get; set; }
	}
}
