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
		errorClass: 'is-invalid',
		//highlight: function (element, errorClass, validClass) {
		//	$(element).addClass(errorClass).removeClass(validClass);
		//	$(element.form).find("label[for=" + element.id + "]").addClass(errorClass);
		//},
		//unhighlight: function (element, errorClass, validClass) {
		//	$(element).removeClass(errorClass).addClass(validClass);
		//	$(element.form).find("label[for=" + element.id + "]").removeClass(errorClass);
		//},
		errorPlacement: function (error, element) {
			var placement = element.closest('.input-group');
			if (element.is(':checkbox')) {
				placement = element.closest('div');
			}
			if (!placement.get(0)) {
				placement = element;
			}
			if (error.text() !== '') {
				placement.after(error);
			}
		},
		success: function (element) {
			var $placement = $(element).closest('.form-group');
			if ($placement.get(0)) {
				$placement.removeClass('is-invalid');
			}
			element.remove();
		},
		submitHandler: function (form) {
			$(':submit', form).attr('disabled', 'disabled');
			form.submit();
		}
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
