using App.UI.Mvc5.Models;

namespace App.UI.Mvc5.Areas.Features.Models
{
	public class PromoCodeInfoViewModel : BasePartialViewModel
	{
		public bool IsValid { get; set; }
		public string PromoCode { get; set; }
		public decimal CurrentAmount { get; set; }
		public decimal DiscountValue { get; set; }
		public decimal AmountWithDiscount => CurrentAmount - DiscountValue;
	}
}
