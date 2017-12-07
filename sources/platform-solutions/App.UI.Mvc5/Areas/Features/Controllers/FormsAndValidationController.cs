using App.UI.Mvc5.Areas.Features.Models;
using App.UI.Mvc5.Infrastructure;
using Omu.ValueInjecter;
using System;
using System.Linq;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Features.Controllers
{
	[RoutePrefix("forms-and-validation")]
	[TrackMenuItem("features.formsandvalidation")]
	public partial class FormsAndValidationController : __AreaBaseController
	{
		[Route(Name = "FormsAndValidationIndexGet")]
		public ActionResult Index()
		{
			var model = BuildFormsAndValidationViewModel();

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route(Name = "FormsAndValidationIndexPost")]
		public ActionResult Index(FormsAndValidationViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Here you would persist information and redirect to a success page, etc...
				Feedback.AddMessage(FeedbackMessageType.Success, "All fields validated correctly.", "Contratulations!");
			}
			else
			{
				Feedback.AddMessage(FeedbackMessageType.Success, "Some fields are not valid.", "Oops!");
			}

			model = BuildFormsAndValidationViewModel(model);

			return View(model);
		}

		private FormsAndValidationViewModel BuildFormsAndValidationViewModel(FormsAndValidationViewModel postedModel = null)
		{
			var model = new FormsAndValidationViewModel();

			if (postedModel != null)
			{
				// Use a mapper to associate the previously posted information to the new model.
				model.InjectFrom(postedModel);
			}

			// Combo boxes expect a collection of objects to build the options list.
			// This could be loaded from a database or any other source. For demonstration purposes we will use the sample enum.
			var sampleValues = Enum.GetValues(typeof(SamplesEnum)).Cast<SamplesEnum>().Select(r => new
			{
				id = r.ToString(),
				value = r.ToString()
			}).OrderBy(e => e.id).ToList();

			// This is a list that should be always available when presenting the single select options.
			model.SampleOptionsSingle = new SelectList(
				sampleValues,
				dataValueField: "id", // Specify the fields to be used. Here we will use the ones we created above from the enum.
				dataTextField: "value",
				selectedValue: model.SelectedSingle
			);

			// This is a list that should be always available when presenting the multi select options.

			model.SampleOptionsMulti = new MultiSelectList(
				sampleValues,
				dataValueField: "id", // Specify the fields to be used. Here we will use the ones we created above from the enum.
				dataTextField: "value",
				selectedValues: model.SelectedMulti
			);

			return model;
		}
	}
}