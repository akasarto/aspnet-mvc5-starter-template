using App.UI.Mvc5.Infrastructure;
using System.Net;

namespace App.UI.Mvc5.Models
{
	public class ErrorViewModel : BaseViewModel
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public ErrorViewModel(HttpStatusCode statusCode, string errorMessage = null)
		{
			Code = (int)statusCode;

			Message = errorMessage;

			if (!string.IsNullOrWhiteSpace(Message))
			{
				return;
			}

			switch (statusCode)
			{
				case HttpStatusCode.BadRequest:
					Message = GlobalizationManager.GetLocalizedString("Errors_BadRequestMessage");
					break;

				case HttpStatusCode.Forbidden:
					Message = GlobalizationManager.GetLocalizedString("Errors_ForbiddenMessage");
					break;

				case HttpStatusCode.NotFound:
					Message = GlobalizationManager.GetLocalizedString("Errors_NotFoundMessage");
					break;

				case HttpStatusCode.Unauthorized:
					Message = GlobalizationManager.GetLocalizedString("Errors_UnauthorizedMessage");
					break;

				default:
					Message = GlobalizationManager.GetLocalizedString("Errors_GeneralErrorMessage");
					break;
			}
		}

		public int Code { get; private set; }

		public string Message { get; private set; }
	}
}
