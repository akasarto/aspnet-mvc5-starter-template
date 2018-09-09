using System;

namespace Shared.Infrastructure
{
	public interface IBlobThumbService
	{
		Uri GetThumbEndpoint(string blobName, string label, int? width = null, int? height = null);
	}
}
