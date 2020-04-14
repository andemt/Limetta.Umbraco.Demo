using Umbraco.Core.Models.PublishedContent;
using Limetta.Umbraco.Demo.Common.Extensions;
using Limetta.Umbraco.Demo.ModelsBuilder.Models;
using Umbraco.Web;

namespace Limetta.Umbraco.Demo.RenderElements.ViewModels
{
	public class ContactElementViewModel : BaseElementViewModel<ContactElement>
	{
		public ContactElementViewModel(IPublishedElement element)
			: base(element)
		{
			ImageUrl = Element?.ContactImage?.GetCropUrl(200);
			Name = Element?.ContactFirstName.MergeWithSpace(Element.ContactLastName);
		}

		public string Name { get; set; }
		public string ImageUrl { get; set; }
	}
}
