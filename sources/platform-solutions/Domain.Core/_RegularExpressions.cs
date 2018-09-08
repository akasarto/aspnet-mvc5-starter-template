using System.Text.RegularExpressions;

public static class _RegularExpressions
{
	public const string MultiWhiteSpacesPattern = @"\s+";
	public const string SimpleEmailPattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
	public const string SpecialCharsPattern = @"[^a-zA-Z0-9]";
	public const string UserNamePattern = @"^[a-zA-Z][a-zA-Z0-9]+$";
	public static readonly Regex MultiWhiteSpaces = new Regex(MultiWhiteSpacesPattern, RegexOptions.Compiled);
	public static readonly Regex SimpleEmail = new Regex(SimpleEmailPattern, RegexOptions.Compiled);
	public static readonly Regex SpecialChars = new Regex(SpecialCharsPattern, RegexOptions.Compiled);
	public static readonly Regex UserName = new Regex(UserNamePattern, RegexOptions.Compiled);
}
