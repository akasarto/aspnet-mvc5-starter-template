using App.Core;
using App.Identity;
using App.UI.Mvc5.Areas.Users.Models;
using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Sarto.Extensions;
using Sarto.Infrastructure;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Users.Controllers
{
	[RoutePrefix("account")]
	public partial class AccountController : __AreaBaseController
	{
		private AdminUserManager _adminUserManager = null;
		private AdminSignInManager _adminSignInManager = null;
		private IAuthenticationManager _authenticationManager = null;
		private IEmailDispatcherService _emailDispatcherService = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public AccountController(AdminUserManager adminUserManager, AdminSignInManager adminSignInManager, IAuthenticationManager authenticationManager, IEmailDispatcherService emailDispatcherService)
		{
			_adminUserManager = adminUserManager ?? throw new ArgumentNullException(nameof(adminUserManager), nameof(AccountController));
			_adminSignInManager = adminSignInManager ?? throw new ArgumentNullException(nameof(adminSignInManager), nameof(AccountController));
			_authenticationManager = authenticationManager ?? throw new ArgumentNullException(nameof(authenticationManager), nameof(AccountController));
			_emailDispatcherService = emailDispatcherService ?? throw new ArgumentNullException(nameof(emailDispatcherService), nameof(AccountController));
		}

		[AllowAnonymous]
		[Route("box", Name = "AccountBoxGet")]
		public ActionResult Box()
		{
			var model = new EmptyPartialViewModel();

			return PartialView(model);
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("locked", Name = "AccountLockedGet")]
		public ActionResult Locked()
		{
			return View(new EmptyViewModel());
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("login", Name = "AccountLogInGet")]
		public ActionResult LogIn(string returnUrl)
		{
			if (Request.IsAuthenticated)
			{
				return RedirectToLocal();
			}

			ViewBag.ReturnUrl = returnUrl;

			return View(new AccountLogInViewModel());
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		[Route("login", Name = "AccountLogInPost")]
		public async Task<ActionResult> LogIn(AccountLogInViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				AdminUserEntity user = null;

				//
				if (_RegularExpressions.SimpleEmail.IsMatch(model.EmailOrUsername))
				{
					user = await _adminUserManager.FindByEmailAsync(model.EmailOrUsername);
				}
				else
				{
					user = await _adminUserManager.FindByNameAsync(model.EmailOrUsername);
				}

				//
				if (user != null && user.Realms.Contains(Realm.AdminWebsite))
				{
					_adminSignInManager.InitialPersistenceState = model.RememberMe;

					var result = await _adminSignInManager.PasswordSignInAsync(
						user.UserName,
						model.Password,
						model.RememberMe,
						shouldLockout: true
					);

					switch (result)
					{
						case SignInStatus.LockedOut:
							{
								return RedirectToAction("Locked", "Account");
							}

						case SignInStatus.Success:
							{
								return RedirectToLocal(returnUrl);
							}
					}
				}

				ModelState.AddModelError("credentials", GetLocalizedString("Auth_InvalidCredentialsMessage"));
			}

			ViewBag.ReturnUrl = returnUrl;

			return View(model);
		}

		[Route("logoff", Name = "AccountLogOff")]
		public ActionResult LogOff()
		{
			_authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

			return RedirectToLocal();
		}

		[HttpGet]
		[Route("password/change", Name = "UserPasswordChangeGet")]
		public ActionResult PasswordChange()
		{
			var model = new AccountPasswordChangeViewModel();

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("password/change", Name = "UserPasswordChangePost")]
		public async Task<ActionResult> PasswordChange(AccountPasswordChangeViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _adminUserManager.ChangePasswordAsync(
					User.Id,
					model.Password,
					model.NewPassword
				);

				if (result.Succeeded)
				{
					Feedback.AddMessage(FeedbackMessageType.Success, GetLocalizedString("DefaultSuccessMessage"));

					return RedirectToAction("Index", "Home", new { area = AppAreas.GetAreaName(Area.Root) });
				}

				ModelState.AddModelError("Password", GetLocalizedString("Users_InvalidPasswordMessage"));
			}

			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("password/recover", Name = "AccountPasswordRecoverGet")]
		public ActionResult PasswordRecover(AccountActionStatus? status)
		{
			if (Request.IsAuthenticated)
			{
				return RedirectToLocal();
			}

			switch (status)
			{
				case AccountActionStatus.PasswordRecoverMessageSent:
					return View("PasswordRecoverSuccess", new EmptyViewModel());
			}

			return View(new AccountPasswordRecoverViewModel());
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		[Route("password/recover", Name = "AccountPasswordRecoverPost")]
		public async Task<ActionResult> PasswordRecover(AccountPasswordRecoverViewModel model)
		{
			if (ModelState.IsValid)
			{
				//
				var adminUser = await _adminUserManager.FindByEmailAsync(model.Email);

				//
				if (adminUser != null && adminUser.Realms.Contains(Realm.AdminWebsite))
				{
					//
					var mailModel = new AccountPasswordRecoverMessageViewModel()
					{
						Name = adminUser.FullName ?? adminUser.UserName,
						PageTitle = GetLocalizedString("Auth_RecoverPasswordTitle"),
						ResetLink = Url.Action(nameof(PasswordSetNew), "Account", new
						{
							resetToken = await _adminUserManager.GeneratePasswordResetTokenAsync(adminUser.Id),
							area = AppAreas.GetAreaName(Area.Users)
						}, protocol: Request.Url.Scheme)
					};

					//
					var subject = $"[{GetLocalizedString("_App_Name")}] {mailModel.PageTitle}";
					var message = RenderViewToString("PasswordRecoverMessage", model: mailModel);

					//
					_emailDispatcherService.Dispatch(message, subject, new MailAddress(adminUser.Email, adminUser.FullName));
				}

				return RedirectToAction(nameof(PasswordRecover), new { status = AccountActionStatus.PasswordRecoverMessageSent.ToLowerCaseString() });
			}

			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("password/setnew", Name = "AccountPasswordSetNewGet")]
		public ActionResult PasswordSetNew(string resetToken)
		{
			if (Request.IsAuthenticated)
			{
				return RedirectToLocal();
			}

			if (resetToken == null)
			{
				return ErrorResult(HttpStatusCode.BadRequest);
			}

			var model = new AccountPasswordRecoverResponseViewModel()
			{
				ResetToken = resetToken
			};

			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		[Route("password/setnew", Name = "AccountPasswordSetNewPost")]
		public async Task<ActionResult> PasswordSetNew(AccountPasswordRecoverResponseViewModel model)
		{
			if (ModelState.IsValid)
			{
				var adminUser = await _adminUserManager.FindByEmailAsync(model.Email);

				if (adminUser != null && adminUser.Realms.Contains(Realm.AdminWebsite))
				{
					var result = await _adminUserManager.ResetPasswordAsync(
						adminUser.Id,
						model.ResetToken,
						model.NewPassword
					);

					if (result.Succeeded)
					{
						await _adminSignInManager.PasswordSignInAsync(
							adminUser.UserName,
							model.NewPassword,
							isPersistent: false,
							shouldLockout: false
						);

						return RedirectToLocal();
					}
				}

				ModelState.AddModelError(string.Empty, GetLocalizedString("Auth_SetNewPasswordGeneralError"));
			}

			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("signup", Name = "AccountSignUpGet")]
		public ActionResult SignUp(string returnUrl)
		{
			if (Request.IsAuthenticated)
			{
				return RedirectToLocal();
			}

			ViewBag.ReturnUrl = returnUrl;

			return View(new AccountSignUpViewModel());
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		[Route("signup", Name = "AccountSignUpPost")]
		public async Task<ActionResult> SignUp(AccountSignUpViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var adminUser = new AdminUserEntity
				{
					FullName = model.Name,
					Email = model.Email,
					CultureId = User.Culture.Name,
					UICultureId = User.UICulture.Name,
					TimeZoneId = User.TimeZone.Id
				};

				//
				adminUser.SetDefaultUserName();

				//
				adminUser.Realms.Add(Realm.AdminWebsite);

				//
				adminUser.Roles.Add(Role.Basic);

				//
				var result = await _adminUserManager.CreateAsync(adminUser, model.Password);

				if (result.Succeeded)
				{
					var isPersistent = false;

					_adminSignInManager.InitialPersistenceState = isPersistent;

					await _adminSignInManager.SignInAsync(
						adminUser,
						isPersistent,
						rememberBrowser: false
					);

					return RedirectToLocal(returnUrl);
				}

				ModelState.AddModelError(string.Empty, GetLocalizedString("DefaultErrorMessage"));
			}

			ViewBag.ReturnUrl = returnUrl;

			return View(model);
		}

		[HttpGet]
		[Route("verify/email", Name = "UsersVerifyEmailGet")]
		public ActionResult VerifyEmail(AccountActionStatus? status)
		{
			var model = new EmptyViewModel();

			switch (status)
			{
				case AccountActionStatus.VerifyEmailRequestSent:
					return View("VerifyEmailMessageSent", model);

				case AccountActionStatus.VerifyEmailResponseSuccess:
					return View("VerifyEmailSuccess", model);
			}

			return View(model);
		}

		[HttpGet]
		[Route("verify/email/request", Name = "UsersVerifyEmailRequestGet")]
		public async Task<ActionResult> VerifyEmailRequest()
		{
			var userId = User.Id;

			//
			if (await _adminUserManager.IsEmailConfirmedAsync(userId))
			{
				return RedirectToAction(nameof(VerifyEmail), new { status = AccountActionStatus.VerifyEmailResponseSuccess.ToLowerCaseString() });
			}

			//
			var adminUser = await _adminUserManager.FindByIdAsync(userId);

			var mailModel = new AccountVerifyEmailMessageViewModel()
			{
				Name = adminUser.FullName ?? adminUser.UserName,
				PageTitle = GetLocalizedString("Users_VerifyEmailTitle"),
				ConfirmationLink = Url.Action("VerifyEmailResponse", "Account", new
				{
					verifyToken = await _adminUserManager.GenerateEmailConfirmationTokenAsync(adminUser.Id)
				}, protocol: Request.Url.Scheme)
			};

			var subject = $"[{GetLocalizedString("_App_Name")}] {mailModel.PageTitle}";
			var message = RenderViewToString("VerifyEmailMessage", model: mailModel);

			_emailDispatcherService.Dispatch(message, subject, new MailAddress(adminUser.Email, adminUser.FullName));

			return RedirectToAction(nameof(VerifyEmail), new { status = AccountActionStatus.VerifyEmailRequestSent.ToLowerCaseString() });
		}

		[HttpGet]
		[Route("verify/email/response", Name = "UsersVerifyEmailResponseGet")]
		public async Task<ActionResult> VerifyEmailResponse(string verifyToken)
		{
			var userId = User.Id;

			var result = await _adminUserManager.ConfirmEmailAsync(userId, verifyToken);

			if (!result.Succeeded)
			{
				return ErrorResult(HttpStatusCode.BadRequest, GetLocalizedString("Users_VerifyEmailResponseError"));
			}

			User.EmailConfirmed = true;

			await _adminSignInManager.RefreshIdentity(User);

			return RedirectToAction(nameof(VerifyEmail), new { status = AccountActionStatus.VerifyEmailResponseSuccess.ToLowerCaseString() });
		}

		[NonAction]
		private ActionResult RedirectToLocal(string returnUrl = null)
		{
			if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}

			return RedirectToAction("Index", "Home", new { area = AppAreas.GetAreaName(Area.Root) });
		}
	}
}
