using FluentValidation.Internal;
using FluentValidation.Validators;
using Shared.Extensions;
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

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			var messageFormatter = new MessageFormatter().AppendPropertyName(metadata.DisplayName);
			var FormattedMessage = messageFormatter.BuildMessage(Options.ErrorMessageSource.GetString(null));

			var rule = new ModelClientValidationRule
			{
				ErrorMessage = FormattedMessage,
				ValidationType = "requiredcheckbox"
			};

			yield return rule;
		}

		protected override bool IsValid(PropertyValidatorContext context)
		{
			var state = context.PropertyValue.ChangeType<bool?>();

			return state.HasValue && state.Value;
		}
	}
}
