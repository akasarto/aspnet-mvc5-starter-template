(function ($, website) {

	'use strict';

	var uploadService = new UploadService();
	var validationService = new ValidationService();

	var checkExistingFiles = function () {

		var $galleryWrapper = $('div.event-blob-wrapper');
		var $emptyElement = $('.event-blobs-gallery-empty');

		if ($galleryWrapper.length) {
			$emptyElement.addClass('hidden');
		} else {
			$emptyElement.removeClass('hidden');
		}

	};

	var setGalleryEvents = function () {

		$('.delete-blob-event').each(function () {

			var $item = $(this);

			// Delete confirmation
			if (!$item.hasClass('gallery-delete-ready')) {
				$item.confirmation({
					title: website.resources.defaultWarningTitle,
					btnOkLabel: website.resources.confirm,
					btnCancelLabel: website.resources.cancel,
					onConfirm: function (event, element) {
						event.preventDefault();
						var wrapper = $(element).closest('div.event-blob-wrapper');
						if (wrapper.length > 0) {
							wrapper.remove();
							checkExistingFiles();
						}
					}
				});
				$item.addClass('gallery-delete-ready');
			}

		});

	};

	// Init
	$(function () {

		//
		validationService.init();

		//
		uploadService.init({
			uploadParams: {
				previewThumbWidth: 160,
				previewThumbHeight: 182
			},
			uploadSuccess: function (data) {
				$('.event-blobs-gallery').append(data.blobHtml);
				setGalleryEvents();
			},
			uploadComplete: function () {
				$('.uploading-status-event').addClass("hidden");
				checkExistingFiles();
			}
		});

		//
		$('.date-picker').each(function (item) {
			var $input = $(this);
			var pickerOptions = {
				format: $input.data("format")
			};
			$input.datepicker(pickerOptions);
		});

		//
		$('.time-picker').each(function (item) {
			var $input = $(this);
			var pickerOptions = {
				template: false,
				showInputs: false,
				minuteStep: 1,
				defaultTime: false,
				showMeridian: $input.data("meridiem")
			};
			$input.timepicker(pickerOptions);
		});

		//
		$('.event-blobs-gallery').magnificPopup({
			delegate: 'a.event-gallery-item',
			type: 'image',
			tLoading: $('.event-blobs-gallery').data('loading-msg-tpl'),
			mainClass: 'mfp-img-mobile',
			gallery: {
				enabled: true,
				navigateByImgClick: true,
				preload: [0, 1]
			},
			image: {
				tError: $('.event-blobs-gallery').data('loading-error-msg-tpl'),
			}
		});

		setGalleryEvents();

	});

}).apply(this, [jQuery, window.website]);
