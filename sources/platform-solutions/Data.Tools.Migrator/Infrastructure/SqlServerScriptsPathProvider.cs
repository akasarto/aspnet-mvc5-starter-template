using Data.Core;
using System.IO;
using System.Reflection;

namespace Data.Tools.Migrator.Infrastructure
{
	public class SqlServerScriptsPathProvider : IMigrationScriptPathProvider
	{
		private readonly Assembly _executingAssembly = null;

		public SqlServerScriptsPathProvider()
		{
			_executingAssembly = Assembly.GetExecutingAssembly();
		}

		public string GetPath(string scriptName)
		{
			var baseFolder = Path.GetDirectoryName(_executingAssembly.Location);

			return Path.Combine(baseFolder, "SqlServerScripts", scriptName);
		}
	}
}