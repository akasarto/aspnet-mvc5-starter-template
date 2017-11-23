using FluentValidation;
using SimpleInjector;
using System;

namespace App.UI.Mvc5.Infrastructure
{
	public class SimpleInjectorValidatorFactory : ValidatorFactoryBase
	{
		private Container _container = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public SimpleInjectorValidatorFactory(Container container)
		{
			_container = container;
		}

		public override IValidator CreateInstance(Type validatorType)
		{
			var registration = _container.GetRegistration(validatorType, throwOnFailure: false);

			if (registration == null)
			{
				return null;
			}

			return registration.GetInstance() as IValidator;
		}
	}
}
