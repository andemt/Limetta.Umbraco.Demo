## Rendering Nested Content/Elements "automatically"
In Umbraco there is no built-in Nested Elements renderer. Here is one way to automatically setup a renderer. It supports strongly type models from ModelsBuilder and IPublishedElement. It also supports a custom controller, so you can use ViewModel, and it supports if you only want a razor partial file.

It's magic! :)

    @inherits UmbracoViewPage<HomePage>
    <! -- Render elements: -->
    @Html.Elements(Model.Elements)
    <!-- Or... -->
    @{ Html.RenderElements(Model.Elements); }

### Example
You have two IPublishedElements (Nested Content): [HeaderAndTextElement](Limetta.Umbraco.Demo.ModelsBuilder/Models/HeaderAndTextElement.generated.cs) and [ContactElement](Limetta.Umbraco.Demo.ModelsBuilder/Models/ContactElement.generated.cs)

#### Views
[Views/Elements/HeaderAndTextElement.cshtml](Limetta.Umbraco.Demo.Web/Views/Elements/HeaderAndTextElement.cshtml) ([ModelsBuilder](https://our.umbraco.com/documentation/reference/templating/modelsbuilder/) model. No controller needed)

    @model HeaderAndTextElement
    <div>
        <h2>@Model.Header</h2>
        @Model.Text
    </div>
    
[Views/Elements/ContactElement.cshtml](Limetta.Umbraco.Demo.Web/Views/Elements/ContactElement.cshtml) (View model from controller)

    @model ContactElementViewModel
    <div>
        <h2>@Model.Name</h2>
	    @if (Model.ImageUrl.IsValid())
	    {
		    <img src="@Model.ImageUrl" alt="Contact image"/>
	    }
    </div>
    
 #### Controller for [ContactElement](Limetta.Umbraco.Demo.RenderElements/Controllers/ContactElementController.cs)
 
    public class ContactElementController : SurfaceController
    {
        public ActionResult Index(IPublishedElement element)
        {
            return PartialView(new ContactElementViewModel(element));
        }
    }
    
..or you can have a [default controller](Limetta.Umbraco.Demo.RenderElements/Controllers/ElementsController.cs) with many elements:

    public class ElementsController : SurfaceController
    {
        public ActionResult ContactElement(IPublishedElement element)
        {
            return PartialView(new ContactElementViewModel(element));
        }
    }
