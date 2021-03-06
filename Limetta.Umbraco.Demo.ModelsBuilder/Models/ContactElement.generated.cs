//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v8.6.0
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder.Embedded;

namespace Limetta.Umbraco.Demo.ModelsBuilder.Models
{
	/// <summary>Contact Element</summary>
	[PublishedModel("contactElement")]
	public partial class ContactElement : PublishedElementModel
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.6.0")]
		public new const string ModelTypeAlias = "contactElement";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.6.0")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.6.0")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.6.0")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<ContactElement, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public ContactElement(IPublishedElement content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Email
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.6.0")]
		[ImplementPropertyType("contactEmail")]
		public string ContactEmail => this.Value<string>("contactEmail");

		///<summary>
		/// First name
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.6.0")]
		[ImplementPropertyType("contactFirstName")]
		public string ContactFirstName => this.Value<string>("contactFirstName");

		///<summary>
		/// Image
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.6.0")]
		[ImplementPropertyType("contactImage")]
		public global::Umbraco.Core.Models.PublishedContent.IPublishedContent ContactImage => this.Value<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>("contactImage");

		///<summary>
		/// Last name
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.6.0")]
		[ImplementPropertyType("contactLastName")]
		public string ContactLastName => this.Value<string>("contactLastName");
	}
}
