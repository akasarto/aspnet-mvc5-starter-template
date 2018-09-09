using App.UI.Mvc5.Areas.Management.Models;
using App.UI.Mvc5.Infrastructure;
using Domain.Core;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using Microsoft.AspNet.Identity;
using Omu.ValueInjecter;
using Shared.Extensions;
using Shared.Infrastructure;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Management.Controllers
{
	[RoutePrefix("users")]
	[TrackMenuItem("management.users")]
	public partial class UsersController : __AreaBaseController
	{
		private IEmailDispatcherService _emailDispatcherService = null;
		private IUsersRepository _managementUsersRepository = null;
		private IPasswordHasher _passwordHasher = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public UsersController(IUsersRepository managementUsersRepository, IEmailDispatcherService emailDispatcherService, IPasswordHasher passwordHasher)
		{
			_managementUsersRepository = managementUsersRepository ?? throw new ArgumentNullException(nameof(managementUsersRepository), nameof(UsersController));
			_emailDispatcherService = emailDispatcherService ?? throw new ArgumentNullException(nameof(emailDispatcherService), nameof(UsersController));
			_passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher), nameof(UsersController));
		}

		[HttpGet]
		[Route("add", Name = "ManagementUsersAddGet")]
		public ActionResult Add()
		{
			var model = BuildUserViewModel();

			return View("Manager", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("add", Name = "ManagementUsersAddPost")]
		public ActionResult Add(UserViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new UserEntity
				{
					FullName = model.FullName,
					Email = model.Email,
					Realms = model.Realms.Select(realm => realm.ChangeType<Realm>()).ToList(),
					Roles = model.Roles.Select(role => role.ChangeType<Role>()).ToList()
				};

				//
				user.SetDefaultUserName();
				user.LockoutEnabled = _Constants.UsersLockoutEnabledByDefault;
				user.EmailConfirmed = true;

				//
				var initialPassword = user.GetInitialPassword();
				user.PasswordHash = _passwordHasher.HashPassword(initialPassword);

				//
				user = _managementUsersRepository.Create(user);

				if (user.Id > 0)
				{
					//
					var mail = new UserCreatedMessageViewModel()
					{
						Email = user.Email,
						FullName = user.FullName,
						InitialPassword = initialPassword,
						PageTitle = GetLocalizedString("Management_Users_UserCreatedMessageSubject"),
					};

					// Provide the user information on how to access allowed realms.
					foreach (var realm in user.Realms)
					{
						switch (realm)
						{
							case Realm.AdminWebsite:
								{
									var link = Url.Action(
										"Index",
										"Home",
										new { area = AppAreas.GetAreaName(Area.Root) },
										protocol: Request.Url.Scheme);

									mail.AllowedRealms.Add(new UserCreatedMessageViewModel.AllowedRealmInfo()
									{
										Realm = realm,
										RealmUri = new Uri(link, UriKind.Absolute)
									});
								}
								break;
						}
					}

					//
					var subject = $"[{GetLocalizedString("_App_Name")}] {mail.PageTitle}";
					var message = RenderViewToString("UserCreatedMessage", model: mail);

					//
					_emailDispatcherService.Dispatch(message, subject, new MailAddress(user.Email, user.FullName));

					//
					Feedback.AddMessage(FeedbackMessageType.Success, GetLocalizedString("DefaultSuccessMessage"));

					//
					return RedirectToAction(nameof(Index));
				}

				ModelState.AddModelError(string.Empty, GetLocalizedString("DefaultErrorMessage"));
			}

			model = BuildUserViewModel(postedModel: model);

			return View("Manager", model);
		}

		[HttpGet]
		[Route("{id:int}/edit", Name = "ManagementUsersEditGet")]
		public ActionResult Edit(int id)
		{
			var user = _managementUsersRepository.GetById(id);

			if (user.Id <= 0)
			{
				return ErrorResult(HttpStatusCode.NotFound);
			}

			var model = BuildUserViewModel(userEntity: user);

			return View("Manager", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("{id:int}/edit", Name = "ManagementUsersEditPost")]
		public ActionResult Edit(int id, UserViewModel model)
		{
			var entity = _managementUsersRepository.GetById(id);

			if (ModelState.IsValid)
			{
				entity.Realms = model.Realms.Select(r => r.ChangeType<Realm>()).ToList();
				entity.Roles = model.Roles.Select(r => r.ChangeType<Role>()).ToList();

				_managementUsersRepository.Update(entity);

				Feedback.AddMessage(FeedbackMessageType.Success, GetLocalizedString("DefaultSuccessMessage"));

				return RedirectToAction(nameof(Index));
			}

			model = BuildUserViewModel(userEntity: entity, postedModel: model);

			return View("Manager", model);
		}

		[HttpGet]
		[Route(Name = "ManagementUsersIndexGet")]
		public ActionResult Index()
		{
			var model = new UsersIndexViewModel();

			var users = _managementUsersRepository.GetAll();

			model.Users = users.Select(user => BuildUserViewModel(user)).ToList();

			return View(model);
		}

		/// <summary>
		/// Buids an <see cref="UserViewModel"/> instance to be passed to the views.
		/// </summary>
		/// <param name="userEntity">The <see cref="Domain.Core.UserEntity"/> instance to map initial data from.</param>
		/// <param name="postedModel">If in edit action, this is the instance that was posted.</param>
		/// <returns>An instance of <see cref="UserViewModel"/> with relevant data loaded.</returns>
		private UserViewModel BuildUserViewModel(UserEntity userEntity = null, UserViewModel postedModel = null)
		{
			var model = new UserViewModel();

			if (userEntity != null)
			{
				//
				model.InjectFrom(userEntity);

				//
				model.Realms = userEntity.Realms.Select(r => r.ToString()).ToList();
				model.Roles = userEntity.Roles.Select(r => r.ToString()).ToList();
			}

			if (postedModel != null)
			{
				//
				model.InjectFrom(postedModel);
			}

			var realms = Enum.GetValues(typeof(Realm)).Cast<Realm>().Where(r => r != Realm.None).Select(r => new
			{
				id = r.ToString(),
				value = r.GetDisplayName()
			}).OrderBy(e => e.id).ToList();

			var roles = Enum.GetValues(typeof(Role)).Cast<Role>().Where(r => r != Role.None).Select(r => new
			{
				id = r.ToString(),
				value = r.GetDisplayName()
			}).OrderBy(e => e.id).ToList();

			model.RealmOptions = new MultiSelectList(
				realms,
				"id",
				"value",
				model.Realms
			);

			model.RoleOptions = new MultiSelectList(
				roles,
				"id",
				"value",
				model.Roles
			);

			return model;
		}
	}
}
