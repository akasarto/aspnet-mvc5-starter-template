namespace App.UI.Mvc5.Infrastructure
{
	public enum Area : int
	{
		Root,
		Features,
		Management,
		Users,
		Docs
	}

	public class AppAreas
	{
		public static string GetAreaName(Area area)
		{
			if (area == Area.Root)
			{
				return string.Empty;
			}

			return area.ToString();
		}
	}
}
