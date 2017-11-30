using App.Core;
using App.UI.Mvc5.Controllers;
using App.UI.Mvc5.Infrastructure;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Management.Controllers
{
	[Authorize(Roles = Role.SuperUser)]
	[TrackMenuItem("management.area")]
	[RouteArea("Management", AreaPrefix = "management")]
	public class __AreaBaseController : __BaseController
	{
	}
}