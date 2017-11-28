(function ($, website) {

	'use strict';

	var uploadService = new UploadService();
	var validationService = new ValidationService();

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
				$('input[name=PictureBlobId]').val(data.blob.id);
				$('input[name=PictureBlobName]').val(data.blob.name);
				$('#userPicturePreview').attr('src', data.thumbnail);
			}
		});

		//
		$('#deleteUserPicture').on('click', function () {
			$('input[name=PictureBlobId]').val(null);
			$('input[name=PictureBlobName]').val(null);
			$('#userPicturePreview').addClass('marked-for-deletion');
		});

		//
		var screenAutoLockMinutes = '#ScreenAutoLockMinutes';

		var updateScreenLockInfo = function (value) {
			var $autoLockInfo = $('#autoLockOutput');
			if (value > 0) {
				$autoLockInfo.html($autoLockInfo.data('value-template').replace(/{interval}/g, value));
			} else {
				$autoLockInfo.html($autoLockInfo.data('value-zero'));
			}
		}

		$('#autoLockSlider').slider({
			range: "min",
			min: 0,
			max: 60,
			step: 5,
			slide: function (event, ui) {
				var value = ui.value;
				$(screenAutoLockMinutes).val(value);
				updateScreenLockInfo(value);
			},
			change: function (event, ui) {
				var value = ui.value;
				updateScreenLockInfo(value);
			},
			create: function (event, ui) {
				$(this).slider('value', $(screenAutoLockMinutes).val());
			}
		});

	});

}).apply(this, [jQuery, window.website]);
