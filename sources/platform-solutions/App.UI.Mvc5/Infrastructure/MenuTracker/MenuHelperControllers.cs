using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public class MenuHelperControllers
	{
		private ControllerContext _controllerContext = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public MenuHelperControllers(ControllerContext controllerContext)
		{
			_controllerContext = controllerContext;
		}

		public void TrackItem(string key)
		{
			var itemsKey = nameof(MenuData);

			var menuData = _controllerContext.HttpContext.Items[itemsKey] as MenuData;

			if (menuData == null)
			{
				menuData = new MenuData();
			}

			menuData.Items.Add(key);

			_controllerContext.HttpContext.Items[itemsKey] = menuData;
		}
	}
}
