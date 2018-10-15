using App.Identity.Repositories;
using System;

namespace App.Identity.UserStore
{
	public partial class AppUserStore
	{
		private IIdentityRepository _identityRepository = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public AppUserStore(IIdentityRepository identityRepository)
		{
			_identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository), nameof(AppUserStore));
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		public void Dispose() => Dispose(true);
	}
}
