using System;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class TrackMenuItemAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public TrackMenuItemAttribute(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new Exception(nameof(TrackMenuItemAttribute), new ArgumentNullException(nameof(key)));
			}

			Key = key;
		}

		public string Key { get; private set; }
	}
}