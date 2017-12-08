(function ($, website) {

	'use strict';

	var sampleFormValidation = new ValidationService({ formId: '#sampleForm' }); // By convention, if no 'formId' is specified, the '#mainForm' value is used.

	// Init
	$(function () {

		//
		sampleFormValidation.init();

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
			cssClass: 'multiselect-full-width'
		});

	});

}).apply(this, [jQuery, window.website]);
