(function ($, website) {

	'use strict';

	var validationService = new ValidationService();

	// Init
	$(function () {

		//
		validationService.init();

		//
		$('.date-picker').each(function (item) {
			var $input = $(this);
			var pickerOptions = {
				format: $input.data("format")
			};
			$input.datepicker(pickerOptions);
		});

		//
		$('.time-picker').each(function (item) {
			var $input = $(this);
			var pickerOptions = {
				template: false,
				showInputs: false,
				minuteStep: 1,
				defaultTime: false,
				showMeridian: $input.data("meridiem")
			};
			$input.timepicker(pickerOptions);
		});

		//
		$('#sampleMulti').multiSelect({
			dblClick: true,
			cssClass: 'multiselect-full-width'
		});

	});

}).apply(this, [jQuery, window.website]);
