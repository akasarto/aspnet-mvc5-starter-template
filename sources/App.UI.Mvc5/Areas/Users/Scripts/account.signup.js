(function ($, website) {

	'use strict';

	var defaultValidationService = new ValidationService();

	$(function () {

		defaultValidationService.init();

		$('.terms-link').on('click', function () {

			$.magnificPopup.open({
				type: 'inline',
				fixedContentPos: false,
				fixedBgPos: true,
				overflowY: 'auto',
				closeBtnInside: true,
				preloader: false,
				midClick: true,
				removalDelay: 300,
				mainClass: 'my-mfp-zoom-in',
				items: {
					type: 'inline',
					src: '#termsModal'
				}
			});

		});

	});

}).apply(this, [jQuery, window.website]);
