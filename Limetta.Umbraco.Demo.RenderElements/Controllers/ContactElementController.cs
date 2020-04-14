using System.Web.Mvc;
using Limetta.Umbraco.Demo.RenderElements.ViewModels;
using Umbraco.Core.Models.PublishedContent;
using Limetta.Umbraco.Demo.RenderElements.ViewModels;
using Umbraco.Web.Mvc;

namespace Limetta.Umbraco.Demo.RenderElements.Controllers
{
	public class ContactElementController : SurfaceController
	{
		public ActionResult Index(IPublishedElement element)
		{
			return PartialView(new ContactElementViewModel(element));
		}
	}
}
