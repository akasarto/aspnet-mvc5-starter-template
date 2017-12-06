(function ($, website) {

	'use strict';

	// Init
	$(function () {

		//
		$('#realtimeSampleAlert').on('click', function (e) {

			e.preventDefault();
			e.stopPropagation();

			var url = $(this).attr('href');

			$.post(url, {
				__RequestVerificationToken: $('input[name=__RequestVerificationToken]').val()
			});

		});

	});

}).apply(this, [jQuery, window.website]);
