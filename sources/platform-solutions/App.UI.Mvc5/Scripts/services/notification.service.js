/*
Notification Service
-----------------------------------------
- Depends on
	jquery
	pnotify

- Usage
	var notificationService = new NotificationService([options]);
	var ex1 = notificationService.success();
	var ex2 = notificationService.success([options]);
*/

var NotificationService = function (options) {

	var defaults = {
		width: '30%',
		addclass: 'stack-topright',
		stack: {
			"dir1": "down",
			"dir2": "left",
			"firstpos1": 15,
			"firstpos2": 15
		},
		buttons: {
			closer: true,
			sticker: false
		},
		removeOnClick: true
	};

	var settings = $.extend(true, defaults, options);

	var showNotification = function (type, options) {
		settings = $.extend(true, settings, options, {
			type: type
		});
		var instance = new PNotify(settings);
		if (settings.removeOnClick === true) {
			instance.get().click(function () {
				instance.remove();
			});
		}
		return instance;
	};

	return {
		show: function (type, options) {
			return showNotification(type, options);
		},
		showError: function (options) {
			options = options || {};
			options.title = options.title || website.resources.defaultErrorTitle;
			options.text = options.text || website.resources.defaultErrorMessage;
			return showNotification('error', options);
		},
		showInfo: function (options) {
			options = options || {};
			options.title = options.title || website.resources.defaultInfoTitle;
			options.text = options.text || website.resources.defaultInfoMessage;
			return showNotification('info', options);
		},
		showSuccess: function (options) {
			options = options || {};
			options.title = options.title || website.resources.defaultSuccessTitle;
			options.text = options.text || website.resources.defaultSuccessMessage;
			return showNotification('success', options);
		},
		showWarning: function (options) {
			options = options || {};
			options.title = options.title || website.resources.defaultWarningTitle;
			options.text = options.text || website.resources.defaultWarningMessage;
			return showNotification('warning', options);
		}
	};
};
