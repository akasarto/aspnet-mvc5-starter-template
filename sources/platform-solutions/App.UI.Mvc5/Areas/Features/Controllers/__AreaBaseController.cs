using App.UI.Mvc5.Controllers;
using App.UI.Mvc5.Infrastructure;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Features.Controllers
{
	[Authorize]
	[TrackMenuItem("features.area")]
	[RouteArea("Features", AreaPrefix = "features")]
	public class __AreaBaseController : __BaseController
	{
	}
}
