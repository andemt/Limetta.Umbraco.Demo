using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Composing;
using Umbraco.Web.Runtime;

namespace Limetta.Umbraco.Demo.RenderElements.Composers
{
	[ComposeAfter(typeof(WebInitialComposer))]
	public class ViewEngineComposer : ComponentComposer<ViewEngineComponent>
	{ }

	public class ViewEngineComponent : IComponent
	{
		public void Initialize()
		{
			ViewEngines.Engines.Add(new ElementViewEngine());
		}

		public void Terminate()
		{ }
	}

	public class ElementViewEngine : RazorViewEngine
	{
		private static readonly string[] ExtendedPartialViewsLocations = {
			"/Elements/{0}.cshtml",
			"/Elements/{1}.cshtml",
		};

		public ElementViewEngine()
		{
			PartialViewLocationFormats = PartialViewLocationFormats.Union(
				ExtendedPartialViewsLocations
					.Select(x => "~/Views" + x)
				)
				.ToArray();
		}
	}
}
