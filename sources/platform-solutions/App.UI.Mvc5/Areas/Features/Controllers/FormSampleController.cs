using App.UI.Mvc5.Areas.Features.Models;
using App.UI.Mvc5.Infrastructure;
using Omu.ValueInjecter;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Features.Controllers
{
	[RoutePrefix("forms-sample")]
	[TrackMenuItem("features.form-sample")]
	public partial class FormSampleController : __AreaBaseController
	{
		[HttpGet]
		[Route(Name = "Features_FormSample_Index_Post_Get")]
		public ActionResult Index()
		{
			var model = BuildFormsAndValidationViewModel();

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route(Name = "Features_FormSample_Index_Post")]
		public ActionResult Index(FormSampleViewModel model)
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

		private FormSampleViewModel BuildFormsAndValidationViewModel(FormSampleViewModel postedModel = null)
		{
			var model = new FormSampleViewModel();

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
