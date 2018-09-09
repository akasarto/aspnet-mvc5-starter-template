using System;
using System.IO;

namespace Shared.Infrastructure
{
	public interface IBlobStorageService
	{
		void Delete(string blobName);

		bool Exists(string blobName);

		Uri GetEndpoint(string blobName);

		Stream ReadStream(string blobName);

		void WriteStream(string blobName, Stream blobStream);
	}
}
