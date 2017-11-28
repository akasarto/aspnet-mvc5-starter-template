(function ($, website) {

	'use strict';

	//
	var calendarOptions = {
		clearBtn: true,
		todayBtn: 'linked',
		toggleActive: true
	};

	//
	var $calendar = null;

	//
	var notificationService = new NotificationService();

	//
	var userSidebarScheduleWidgetViewModel = new Vue({
		el: '#userSidebarScheduleWidgetView',
		data: {
			events: []
		}
	});

	//
	var loadEvents = function (date) {

		var parameter = null;

		if (date) {
			parameter = {
				day: date.getDate(),
				month: date.getMonth() + 1,
				year: date.getFullYear()
			};
		}

		$.post({
			data: parameter,
			url: $calendar.data('load-events-url'),
			success: function (data) {
				var items = [];
				if (data.length > 0) {
					for (var idx = 0; idx < data.length; idx++) {
						items.push(data[idx]);
					}
				}
				userSidebarScheduleWidgetViewModel.events = items;
			},
			error: function (request) {
				notificationService.showError();
			}
		});
	}

	// Init
	$(function () {

		$calendar = $('#userScheduleSidebarCalendar');

		$calendar.datepicker(calendarOptions).on('changeDate', function (e) {
			loadEvents(e.date);
		});

		$calendar.data('datepicker').picker.addClass('datepicker-dark');

		loadEvents();

	});

}).apply(this, [jQuery, window.website]);
