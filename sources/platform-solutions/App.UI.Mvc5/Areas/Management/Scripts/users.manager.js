(function ($, website) {

	'use strict';

	var validationService = new ValidationService();

	// Init
	$(function () {

		//
		validationService.init();

		//
		$('#userRealms').multiSelect({
			cssClass: 'multiselect-full-width'
		});

		//
		$('#userRoles').multiSelect({
			cssClass: 'multiselect-full-width'
		});
	});

}).apply(this, [jQuery, window.website]);
