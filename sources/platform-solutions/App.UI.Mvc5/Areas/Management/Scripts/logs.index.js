(function ($, website) {

	'use strict';

	// Init
	$(function () {

		$('#logsRefresh').bind('click', function () {
			var $item = $(this);
			document.location.href = $item.data('url');
		});

		$('a.log-details').magnificPopup({
			type: 'inline',
			closeOnBgClick: true,
			enableEscapeKey: true,
			closeBtnInside: true,
			showCloseBtn: true,
			preloader: false
		});

	});

}).apply(this, [jQuery, window.website]);
