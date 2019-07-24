using App.UI.Mvc5.Areas.Features.Models;
using App.UI.Mvc5.Infrastructure;
using FluentValidation.Mvc;
using Omu.ValueInjecter;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

//
// Attention!
// This is just a sample to demonstrate validation and forms
//

namespace App.UI.Mvc5.Areas.Features.Controllers
{
	[RoutePrefix("forms-and-validation")]
	[TrackMenuItem("features.forms-and-validation")]
	public partial class FormsAndValidationController : __AreaBaseController
	{
		[HttpGet]
		[Route(Name = "Features_FormsAndValidation_Index_Post_Get")]
		public ActionResult Index()
		{
			var model = BuildFormsAndValidationViewModel();

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route(Name = "Features_FormsAndValidation_Index_Post")]
		public ActionResult Index(FormsAndValidationViewModel model)
		{
			if (ModelState.IsValid)
			{
				Feedback.AddMessage(FeedbackMessageType.Success, "All fields validated correctly.", "Contratulations!");
			}
			else
			{
				Feedback.AddMessage(FeedbackMessageType.Error, "Some fields are not valid.", "Oops!");
			}

			model = BuildFormsAndValidationViewModel(model);

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("redeem-code", Name = "Features_FormsAndValidation_RedeemCode_Post")]
		public ActionResult RedeemCode([CustomizeValidator(Properties = "PromoCode")] FormsAndValidationViewModel model)
		{
			// This shows how to test specific fields in isolation.

			if (ModelState.IsValid)
			{
				var code = model.PromoCode ?? string.Empty;
				var isValidPromoCode = "test".Like(code);

				var result = new PromoCodeInfoViewModel()
				{
					IsValid = isValidPromoCode,
					CurrentAmount = 25,
					DiscountValue = isValidPromoCode ? 5 : decimal.Zero,
					PromoCode = code.ToUpper()
				};

				return Json(result);
			}

			return JsonError(ModelState, HttpStatusCode.BadRequest);
		}

		private FormsAndValidationViewModel BuildFormsAndValidationViewModel(FormsAndValidationViewModel postedModel = null)
		{
			var model = new FormsAndValidationViewModel();

			if (postedModel != null)
			{
				// Use a mapper to associate the previously posted information to the new model.
				model.InjectFrom(postedModel);
			}

			var countries = new List<object>()
			{
				new { Id = 1, Name = "United States" }
			};

			model.CountrySelectList = new SelectList(
				countries,
				dataValueField: "Id",
				dataTextField: "Name",
				selectedValue: model.CountryId
			);

			var states = new List<object>()
			{
				new { Id = 1, Name = "Michigan" },
				new { Id = 2, Name = "New York" },
				new { Id = 3, Name = "Washington" }
			};

			model.StateSelectList = new SelectList(
				states,
				dataValueField: "Id",
				dataTextField: "Name",
				selectedValue: model.StateId
			);

			// Build a named tuple list from payment enum
			model.PaymentOptions = Enum.GetValues(typeof(SamplePaymentTypesEnum)).Cast<SamplePaymentTypesEnum>().Select(r =>
				(
					Id: r,
					Description: r.GetDisplayName()
				)
			).ToList();

			return model;
		}
	}
}
