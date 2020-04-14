using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Limetta.Umbraco.Demo.RenderElements.ViewModels;
using Umbraco.Web.Mvc;

namespace Limetta.Umbraco.Demo.RenderElements.Controllers
{
	public class ElementsController : SurfaceController
	{
		public ActionResult ContactElement(IPublishedElement element)
		{
			return PartialView(new ContactElementViewModel(element));
		}
	}
}
