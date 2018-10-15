(function ($, website) {

	'use strict';

	//
	var $databus = $.connection.databus;

	$databus.client.dataReceived = function (payload) {

		var existingData = $('#receivedData').text();

		if (existingData) {
			existingData += '\n';
		}

		$('#receivedData').text(existingData + payload);

	};

	$.connection.hub.start().done(function () {
	});

	$(function () {

		//
		$('#realtimeSampleData').on('click', function (e) {

			e.preventDefault();
			e.stopPropagation();

			var url = $(this).attr('href');

			$.post(url, {
				__RequestVerificationToken: $('input[name=__RequestVerificationToken]').val(),
				payload: $('#payload').val()
			});

			$('#payload').val('');

		});

	});

}).apply(this, [jQuery, window.website]);
