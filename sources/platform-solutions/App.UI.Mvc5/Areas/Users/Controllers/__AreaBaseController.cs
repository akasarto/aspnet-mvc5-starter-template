using App.UI.Mvc5.Controllers;
using App.UI.Mvc5.Infrastructure;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Users.Controllers
{
	[Authorize]
	[TrackMenuItem("users.area")]
	[RouteArea("Users", AreaPrefix = "users")]
	public class __AreaBaseController : BaseController
	{
	}
}
