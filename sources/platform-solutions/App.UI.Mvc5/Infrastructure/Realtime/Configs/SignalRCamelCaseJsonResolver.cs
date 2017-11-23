using Microsoft.AspNet.SignalR.Infrastructure;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace App.UI.Mvc5.Infrastructure
{
	public class SignalRCamelCaseJsonResolver : IContractResolver
	{
		private readonly Assembly _assembly;
		private readonly IContractResolver _camelCaseContractResolver;
		private readonly IContractResolver _defaultContractSerializer;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public SignalRCamelCaseJsonResolver()
		{
			_assembly = typeof(Connection).Assembly;
			_defaultContractSerializer = new DefaultContractResolver();
			_camelCaseContractResolver = new CamelCasePropertyNamesContractResolver();
		}

		public JsonContract ResolveContract(Type type)
		{
			if (type.Assembly.Equals(_assembly))
			{
				return _defaultContractSerializer.ResolveContract(type);
			}

			return _camelCaseContractResolver.ResolveContract(type);
		}
	}
}