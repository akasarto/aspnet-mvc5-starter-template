(function ($, website) {

	'use strict';

	var defaultValidationService = new ValidationService();

	$(function () {

		defaultValidationService.init();

	});

}).apply(this, [jQuery, window.website]);
