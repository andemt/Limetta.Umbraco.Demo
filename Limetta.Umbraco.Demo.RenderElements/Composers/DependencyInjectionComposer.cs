using Umbraco.Core;
using Umbraco.Core.Composing;
using Limetta.Umbraco.Demo.RenderElements.Factories;

namespace Limetta.Umbraco.Demo.RenderElements.Composers
{
	public class DependencyInjectionComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			// our umbraco helpers
			// factories
			composition.Register<ElementsRoutingFactory>();
		}
	}
}
