(function ($, website) {

	'use strict';

	var validationService = new ValidationService();

	// Init
	$(function () {

		//
		validationService.init();

		//
		$('#userRealms').multiSelect({
			dblClick: true,
			cssClass: 'multiselect-full-width'
		});

		//
		$('#userRoles').multiSelect({
			dblClick: true,
			cssClass: 'multiselect-full-width'
		});
	});

}).apply(this, [jQuery, window.website]);
