using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Domain.Core
{
	public class SharedJsonSettings : JsonSerializerSettings
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public SharedJsonSettings()
		{
			NullValueHandling = NullValueHandling.Ignore;
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			Culture = System.Threading.Thread.CurrentThread.CurrentCulture;
			ContractResolver = new CamelCasePropertyNamesContractResolver();
		}
	}
}
