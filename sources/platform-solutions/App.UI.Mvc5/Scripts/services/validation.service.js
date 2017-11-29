/*
Validation Service
-----------------------------------------
- Depends on
	jquery
	jquery validation
	jquery validation unobstrusive

- Usage
	var validationService = new ValidationService([options]);
	validationService.init([options]);
*/

var ValidationService = function (options) {

	var defaults = {
		formId: '#mainForm',
		//errorClass: 'error',
		//errorElement: 'label',
		//success: function (element) {
		//	var $placement = $(element).closest('.form-group');
		//	if ($placement.get(0)) {
		//		$placement.removeClass('has-error');
		//	}
		//	element.remove();
		//},
		//highlight: function (element) {
		//	var $placement = $(element).closest('.form-group');
		//	if ($placement.get(0)) {
		//		$placement.removeClass('has-success').addClass('has-error');
		//	}
		//	$(document).find('.feedback-validation').addClass('hidden');
		//},
		//unhighlight: function (element) {
		//	$(element).parent().removeClass('has-error').find('label.error').remove();
		//},
		//errorPlacement: function (error, element) {
		//	var placement = element.closest('.input-group');
		//	if (element.is(':checkbox')) {
		//		placement = element.closest('div');
		//	}
		//	if (!placement.get(0)) {
		//		placement = element;
		//	}
		//	if (error.text() !== '') {
		//		placement.after(error);
		//	}
		//},
		//submitHandler: function (form) {
		//	$(document).find('.feedback-validation').addClass('hidden');
		//	form.submit();
		//}
	};

	options = options || {};

	var settings = $.extend(true, defaults, options);

	var getForm = function (formId) {

		var $form = $(formId);

		if (!$form.length) {
			console.log('Attempt to validate invalid form ' + formId);
			return;
		}

		return $form;

	}

	var initValidator = function (options) {

		if (!$.validator) {
			console.log('Error! Validation scripts are missing!');
			return;
		}

		settings = $.extend(true, settings, options);

		var $form = getForm(settings.formId);
		var validator = $.data($form[0], 'validator');

		if (validator) {
			$.extend(true, validator.settings, settings, options);
		}

	};

	return {
		init: function (options) {
			options = options || {};
			return initValidator(options);
		},
		getFormElement: function () {
			return $(settings.formId);
		}
	};

};
