using System;

namespace App.Identity
{
	public partial class AdminStore
	{
		private IIdentityRepository _identityRepository = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public AdminStore(IIdentityRepository identityRepository)
		{
			_identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository), nameof(AdminStore));
		}

		protected virtual void Dispose(bool disposing) { }

		public void Dispose() => Dispose(true);
	}
}
