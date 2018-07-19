using Shared.Extensions;
using Domain.Core;

/// <summary>
/// Overrides the default mvc 'AuthorizeAttribute' class.
/// </summary>
public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
{
	/// <summary>
	/// Use enum flags for roles instead of strings.
	/// </summary>
	public virtual new Role Roles
	{
		get { return base.Roles.ChangeType<Role>(); }
		set { base.Roles = value.ToString(); }
	}
}
