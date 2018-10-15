(function ($, website) {

	'use strict';

	var notificationService = new NotificationService();
	var sampleFormValidation = new ValidationService({
		formId: '#sampleForm'
		// By convention, if no 'formId' is specified,
		// the '#mainForm' value is used.
	});

	// Init
	$(function () {

		//
		sampleFormValidation.init();

		//
		$('.redeem-code').on('click', function (e) {

			e.preventDefault();
			e.stopPropagation();

			var url = $(this).attr('href');
			var $promocode = $('#PromoCode');

			$.post(url, {
				__RequestVerificationToken: $('input[name=__RequestVerificationToken]').val(),
				promoCode: $promocode.val()
			}).done(function (response) {
				if (!response.isValid) {
					setPromoCodeError(response.amountWithDiscount);
				} else {
					setPromoCodeSuccess(response.promoCode, response.amountWithDiscount);
				}
			}).fail(function (response) {
				showPromoCodeError('Error. Please try again.');
			});

			$promocode.val('');

		});

		function showPromoCodeError(errorMessage) {
			$('#promoCodeItem').removeClass('d-flex');
			$('#promoCodeItem').addClass('d-none');

			notificationService.showError({
				title: 'Promo Sample',
				text: 'Invalid promo code!'
			});
		}

		function setPromoCodeError(amountWithDiscount) {
			$('#promoCodeItem').removeClass('d-flex');
			$('#promoCodeItem').addClass('d-none');
			$('#promoCodeValue').text('$' + amountWithDiscount);
			$('#promoCodeKey').text('');
			showPromoCodeError('Invalid promo code');
		}

		function setPromoCodeSuccess(promoCode, amountWithDiscount) {
			$('#promoCodeErrorMessageContainer').addClass('d-none');
			$('#promoCodeValue').text('$' + amountWithDiscount);
			$('#promoCodeKey').text(promoCode);
			$('#promoCodeItem').addClass('d-flex');
			$('#promoCodeItem').removeClass('d-none');

			notificationService.showSuccess({
				title: 'Promo Sample',
				text: 'Promo code successfully applied!'
			});
		}

	});

}).apply(this, [jQuery, window.website]);
