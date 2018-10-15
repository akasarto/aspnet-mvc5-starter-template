/*
 * JQuery validate configs
 * - Customize global options here.
 */

// Custom validation rules

$.validator.addMethod("requiredcheckbox", function (value, element, param) {
	return element.checked;
});

// Dates, numbers and currency will alway be validated by the server.

$.validator.methods.date = function (value, element) {
	return true;
};

$.validator.methods.number = function (value, element) {
	return true;
};

$.validator.methods.min = function (value, element, param) {
	return true;
};

$.validator.methods.max = function (value, element, param) {
	return true;
};

$.validator.methods.range = function (value, element, param) {
	return true;
};
