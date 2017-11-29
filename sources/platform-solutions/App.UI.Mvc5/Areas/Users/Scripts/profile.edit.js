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

	});

}).apply(this, [jQuery, window.website]);
