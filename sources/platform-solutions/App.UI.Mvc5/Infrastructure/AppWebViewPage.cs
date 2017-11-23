using App.Identity;
using System.Threading;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public abstract class AppWebViewPage<TModel> : WebViewPage<TModel>
	{
		public MenuHelperViewPages Menu { get; private set; }

		public new AdminPrincipal User => Thread.CurrentPrincipal as AdminPrincipal;

		public override void InitHelpers()
		{
			base.InitHelpers();

			Menu = new MenuHelperViewPages(Html);
		}

		public string GetLocalizedString(string resourceKey, params object[] formatParams)
		{
			return GlobalizationManager.GetLocalizedString(resourceKey, formatParams);
		}
	}
}
