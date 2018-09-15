using App.Identity;
using System.Threading;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public abstract class AppWebViewPage<TModel> : WebViewPage<TModel>
	{
		public MenuHelperViewPages Menu { get; private set; }

		public new AppPrincipal User => Thread.CurrentPrincipal as AppPrincipal;

		public string GetLocalizedString(string resourceKey, params object[] formatParams)
		{
			return GlobalizationManager.GetLocalizedString(resourceKey, formatParams);
		}

		public string GetLocalizedString<TResource>(string resourceKey, params object[] formatParams)
		{
			return GlobalizationManager.GetLocalizedString<TResource>(resourceKey, formatParams);
		}

		public override void InitHelpers()
		{
			base.InitHelpers();

			Menu = new MenuHelperViewPages(Html);
		}
	}
}
