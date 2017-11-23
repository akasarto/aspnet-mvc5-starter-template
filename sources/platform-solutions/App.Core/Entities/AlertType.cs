using System.ComponentModel.DataAnnotations;

namespace App.Core.Entities
{
	/// <summary>
	/// Represents the known alert types that the system can handle.
	/// </summary>
	public enum AlertType : byte
	{
		/// <summary>
		/// Error #0
		/// </summary>
		[Display(Name = "AlertTypeError", ResourceType = typeof(SharedResources))]
		Error = 0,

		/// <summary>
		/// Info #1
		/// </summary>
		[Display(Name = "AlertTypeInfo", ResourceType = typeof(SharedResources))]
		Info = 1,

		/// <summary>
		/// Success #2
		/// </summary>
		[Display(Name = "AlertTypeSuccess", ResourceType = typeof(SharedResources))]
		Success = 2,

		/// <summary>
		/// Warning #3
		/// </summary>
		[Display(Name = "AlertTypeWarning", ResourceType = typeof(SharedResources))]
		Warning = 3
	}
}