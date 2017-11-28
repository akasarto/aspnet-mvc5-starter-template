using App.Identity;
using App.UI.Mvc5.Areas.Users.Models;
using App.UI.Mvc5.Infrastructure;
using Omu.ValueInjecter;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Users.Controllers
{
	[RoutePrefix("profile")]
	[TrackMenuItem("users.profile")]
	public partial class ProfileController : __AreaBaseController
	{
		private AdminUserManager _userManager = null;
		private AdminSignInManager _signInManager = null;
		private IGlobalizationService _globalizationService = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public ProfileController(AdminUserManager userManager, AdminSignInManager signInManager, IGlobalizationService globalizationService)
		{
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager), nameof(ProfileController));
			_signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager), nameof(ProfileController));
			_globalizationService = globalizationService ?? throw new ArgumentNullException(nameof(globalizationService), nameof(ProfileController));
		}

		[HttpGet]
		[Route("edit", Name = "UsersProfileEditGet")]
		public async Task<ActionResult> Edit()
		{
			var adminUser = await _userManager.FindByIdAsync(User.Id);

			var model = BuildEditProfileViewModel(adminUser);

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("edit", Name = "UsersProfileEditPost")]
		public async Task<ActionResult> Edit(ProfileEditViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Id = User.Id;

				var adminUser = BuildAdminUserEntity(model);

				await _userManager.UpdateProfileAsync(adminUser);

				await _signInManager.RefreshIdentity(User, reloadClaims: true);

				Feedback.AddMessage(FeedbackMessageType.Success, GetLocalizedString("DefaultSuccessMessage"));

				return RedirectToAction(nameof(Edit));
			}

			var entity = await _userManager.FindByIdAsync(User.Id);

			model = BuildEditProfileViewModel(adminUser: entity, postedModel: model);

			return View(model);
		}

		private AdminUserEntity BuildAdminUserEntity(ProfileEditViewModel model)
		{
			var entity = new AdminUserEntity();

			entity.InjectFrom(model);

			return entity;
		}

		private ProfileEditViewModel BuildEditProfileViewModel(AdminUserEntity adminUser = null, ProfileEditViewModel postedModel = null)
		{
			var model = new ProfileEditViewModel();

			if (adminUser != null)
			{
				model.InjectFrom(adminUser);
			}

			if (postedModel != null)
			{
				model.InjectFrom(postedModel);
			}

			var cultures = _globalizationService.GetCultures();
			var uiCultures = _globalizationService.GetUICultures();
			var timeZones = _globalizationService.GetTimeZones();

			model.PictureUploadMaxLengthInBytes = AppSettings.Blobs.FileUploadMaxLengthInBytes;

			model.Cultures = new SelectList(
				cultures.OrderBy(c => c.DisplayName)
				, "Name"
				, "DisplayName"
				, dataGroupField: "Parent.DisplayName"
				, selectedValue: null
			);

			model.UICultures = new SelectList(
				uiCultures.OrderBy(c => c.DisplayName)
				, "Name"
				, "DisplayName"
				, dataGroupField: "Parent.DisplayName"
				, selectedValue: null
			);

			model.TimeZones = new SelectList(
				timeZones.OrderBy(t => t.BaseUtcOffset)
				, "Id"
				, "DisplayName"
				, dataGroupField: "BaseUtcOffset"
				, selectedValue: null
			);

			return model;
		}
	}
}
