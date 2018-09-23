using System.ComponentModel.DataAnnotations;

namespace App.UI.Mvc5.Areas.Features.Models
{
	public enum SamplePaymentTypesEnum : int
	{
		[Display(Name = "DebitCard", ResourceType = typeof(AreaResources))]
		DebitCard,

		[Display(Name = "CreditCard", ResourceType = typeof(AreaResources))]
		CreditCard,

		[Display(Name = "PayPal", ResourceType = typeof(AreaResources))]
		Paypal
	}
}
