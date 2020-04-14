using System;
using Umbraco.Core.Models.PublishedContent;
using Limetta.Umbraco.Demo.ModelsBuilder.Extensions;

namespace Limetta.Umbraco.Demo.RenderElements.ViewModels
{
	public abstract class BaseElementViewModel<TElement>
		where TElement : PublishedElementModel
	{
		protected BaseElementViewModel(TElement element)
		{
			Element = element;
		}

		protected BaseElementViewModel(IPublishedElement content)
		{
			Element = content.ToElement<TElement>();
		}

		public TElement Element { get; set; }

		public Guid ElementId => Element?.Key ?? Guid.Empty;
	}
}
