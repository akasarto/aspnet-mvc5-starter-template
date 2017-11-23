
/*
var myService = new MyService();

myService.showAlert(); // => 'Hello World!'
myService.showAlert({ message: 'Hi!' }); // => 'Hi!'
*/

var MyService = function (options) {

	var defaults = {
		message: 'Hello World!'
	};

	options = options || {};

	var settings = $.extend(true, defaults, options);

	return {

		showAlert: function (options) {

			options = options || {};
			settings = $.extend(true, settings, options);

			alert(settings.message);
		}

	};
};
