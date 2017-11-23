using FluentValidation.Resources;

namespace App.UI.Mvc5.Infrastructure
{
	public class ValidationLanguageManager : LanguageManager
	{
		public ValidationLanguageManager()
		{
			// For custom translations
			// AddTranslation("en", "NotNullValidator", "'{PropertyName}' is required.");
			// Or use the .WithMessage(...) when defining the rules.
		}
	}
}