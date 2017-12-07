(function ($, website) {

	'use strict';

	var sampleForm1Validation = new ValidationService({ formId: '#sampleForm1Validation' });
	//var sampleForm2Validation = new ValidationService({ formId: '#sampleForm2Validation' });

	// Init
	$(function () {

		//
		sampleForm1Validation.init();
		//sampleForm2Validation.init(); => Not initialize the service to disable client validation

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
