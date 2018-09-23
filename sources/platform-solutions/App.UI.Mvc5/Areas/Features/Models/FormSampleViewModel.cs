using App.UI.Mvc5.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Features.Models
{
	public class FormSampleViewModel : BaseViewModel
	{
		[Display(Name = "FirstName", ResourceType = typeof(AreaResources))]
		public string FirstName { get; set; }

		[Display(Name = "LastName", ResourceType = typeof(AreaResources))]
		public string LastName { get; set; }

		[Display(Name = "Username", ResourceType = typeof(AreaResources))]
		public string Username { get; set; }

		[Display(Name = "Email", ResourceType = typeof(AreaResources))]
		public string Email { get; set; }

		[Display(Name = "AddressLine1", ResourceType = typeof(AreaResources))]
		public string AddressLine1 { get; set; }

		[Display(Name = "AddressLine2", ResourceType = typeof(AreaResources))]
		public string AddressLine2 { get; set; }

		[Display(Name = "CountryId", ResourceType = typeof(AreaResources))]
		public int? CountryId { get; set; }

		public SelectList CountrySelectList { get; set; }

		[Display(Name = "StateId", ResourceType = typeof(AreaResources))]
		public int? StateId { get; set; }

		public SelectList StateSelectList { get; set; }

		[Display(Name = "Zip", ResourceType = typeof(AreaResources))]
		public string Zip { get; set; }

		[Display(Name = "DeliverToShipingAddress", ResourceType = typeof(AreaResources))]
		public bool DeliverToShipingAddress { get; set; }

		[Display(Name = "SaveShippingAddress", ResourceType = typeof(AreaResources))]
		public bool SaveShippingAddress { get; set; }

		public SamplePaymentTypesEnum PaymentType { get; set; }

		public List<(SamplePaymentTypesEnum Id, string Description)> PaymentOptions { get; set; }

		[Display(Name = "CardName", ResourceType = typeof(AreaResources))]
		public string CardName { get; set; }

		[Display(Name = "CardNumber", ResourceType = typeof(AreaResources))]
		public string CardNumber { get; set; }

		[Display(Name = "CardExpiration", ResourceType = typeof(AreaResources))]
		public string CardExpiration { get; set; }

		[Display(Name = "CardCVV", ResourceType = typeof(AreaResources))]
		public string CardCVV { get; set; }
	}
}
