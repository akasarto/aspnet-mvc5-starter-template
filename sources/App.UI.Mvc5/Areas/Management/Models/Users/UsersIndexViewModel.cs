using App.UI.Mvc5.Models;
using System.Collections.Generic;

namespace App.UI.Mvc5.Areas.Management.Models
{
	public class UsersIndexViewModel : BaseViewModel
	{
		public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
	}
}