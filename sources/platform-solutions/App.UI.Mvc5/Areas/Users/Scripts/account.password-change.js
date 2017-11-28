(function ($, website) {

	'use strict';

	var defaultValidationService = new ValidationService();

	// Init
	$(function () {

		defaultValidationService.init();

	});

}).apply(this, [jQuery, window.website]);
