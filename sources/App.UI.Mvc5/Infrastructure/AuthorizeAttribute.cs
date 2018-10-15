using Domain.Core;
using Shared.Extensions;

public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
{
	public virtual new Role Roles
	{
		get { return base.Roles.ChangeType<Role>(); }
		set { base.Roles = value.ToString(); }
	}
}
