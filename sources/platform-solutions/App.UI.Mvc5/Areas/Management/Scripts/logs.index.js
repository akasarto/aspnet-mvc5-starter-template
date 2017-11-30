(function ($, website) {

	'use strict';

	// Init
	$(function () {

		$('#logsRefresh').bind('click', function () {
			var $item = $(this);
			document.location.href = $item.data('url');
		});

		$(document).on('click', '.modal-dismiss', function (e) {
			e.preventDefault();
			$.magnificPopup.close();
		});

		$('a.log-details').magnificPopup({
			type: 'inline',
			closeBtnInside: true,
			preloader: false,
			modal: true
		});

	});

}).apply(this, [jQuery, window.website]);
