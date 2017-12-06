(function ($, website) {

	'use strict';

	// Init
	$(function () {

		$('#logsRefresh').on('click', function (e) {
			e.preventDefault();
			var $this = $(this);
			document.location.href = $this.attr('href');
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
