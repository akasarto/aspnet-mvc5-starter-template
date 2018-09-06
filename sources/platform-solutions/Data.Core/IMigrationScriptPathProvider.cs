namespace Data.Core
{
	public interface IMigrationScriptPathProvider
	{
		string GetPath(string scriptName);
	}
}
