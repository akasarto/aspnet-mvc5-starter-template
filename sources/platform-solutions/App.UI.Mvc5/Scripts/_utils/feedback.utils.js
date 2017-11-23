/*
 * Feedback
 * - Messages parser and presentation manager.
 */

(function ($, website) {

	'use strict';

	var notificationService = new NotificationService();

	// Init
	$(function () {

		$('div.feedback-messages div').each(function () {

			var type = $(this).data("type");
			var content = $(this).data("content");
			var title = $(this).data("title");

			notificationService.show(type, { title: title, text: content });

		});

	});

}).apply(this, [jQuery, window.website]);
