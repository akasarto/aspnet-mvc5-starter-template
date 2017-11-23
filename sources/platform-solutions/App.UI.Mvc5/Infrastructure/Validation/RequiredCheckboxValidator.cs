using FluentValidation.Internal;
using FluentValidation.Validators;
using Sarto.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public class RequiredCheckboxValidator : PropertyValidator, IClientValidatable
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public RequiredCheckboxValidator() : base("'{PropertyName}' is required!")
		{
		}

		protected override bool IsValid(PropertyValidatorContext context)
		{
			var state = context.PropertyValue.ChangeType<bool?>();

			return state.HasValue && state.Value;
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			var messageFormatter = new MessageFormatter().AppendPropertyName(metadata.DisplayName);
			var FormatterdMessage = messageFormatter.BuildMessage(ErrorMessageSource.GetString(null));

			var rule = new ModelClientValidationRule
			{
				ErrorMessage = FormatterdMessage,
				ValidationType = "requiredcheckbox"
			};

			yield return rule;
		}
	}
}
