/*
Upload Service
-----------------------------------------
- Depends on
	jquery

- Usage
	var uploadService = new UploadService([options]);
	uploadService.init([options]);
*/

var UploadService = function (options) {

	var defaults = {
		formId: '#mainForm',
		triggerId: '#mainUploadTrigger',
		fileInputId: '#mainUploadFileInput',
		previewContainerId: '#mainPreviewContainer'
	};

	options = options || {};

	var settings = $.extend(true, defaults, options);
	var uploadProgressService = new ProgressService();
	var uploadModelStateService = new ModelStateService();
	var uploadNotificationService = new NotificationService();

	var initialize = function (options) {

		settings = $.extend(true, {
			processData: false,
			contentType: false,
			url: settings.url,
			xhr: function () {
				var xhrRequest = $.ajaxSettings.xhr();
				if (xhrRequest.upload) {
					xhrRequest.upload.addEventListener('progress', function (progress) {
						if (!progress.lengthComputable)
							return;
						var perc = progress.loaded / progress.total;
						uploadProgressService.set(perc);
						if (settings.uploadProgress) {
							settings.uploadProgress(perc);
						}
					}, false);
				}
				return xhrRequest;
			},
			beforeSend: function (params) {
				uploadProgressService.start();
				$(settings.previewContainerId).trigger('loading-overlay:show');
				if (settings.uploadStarting) {
					settings.uploadStarting(params);
				}
			},
			error: function (request) {
				var messages = uploadModelStateService.getErrorMessages(request.responseJSON);
				$.each(messages, function (idx, message) {
					uploadNotificationService.showError({ text: message });
				});
				if (settings.uploadError) {
					settings.uploadError(request);
				}
			},
			success: function (data) {
				if (settings.uploadSuccess) {
					settings.uploadSuccess(data);
				}
			},
			complete: function () {
				uploadProgressService.complete();
				$(settings.previewContainerId).trigger('loading-overlay:hide');
				if (settings.uploadComplete) {
					settings.uploadComplete();
				}
			}
		}, settings, options);

		var $trigger = $(settings.triggerId);

		settings.url = settings.url || $trigger.data('url') || $trigger.attr('href');

		$(settings.triggerId).on('click', function (event) {
			event.preventDefault();
			$(settings.fileInputId).click();
		});

		$(settings.fileInputId).on('change', function () {

			if (this.files.length <= 0)
				return;

			for (var idx = 0; idx < this.files.length; idx++) {

				var file = this.files[idx];
				var formData = new FormData();
				var $fileInput = $(settings.fileInputId);

				formData.append($fileInput.attr('name'), file);

				if (settings.uploadParams) {
					for (var key in settings.uploadParams) {
						formData.append(key, settings.uploadParams[key]);
					}
				}

				settings = $.extend(true, settings, {
					data: formData
				});

				$.post(settings);
			}
		});
	};

	return {
		init: function (options) {
			options = options || {};
			return initialize(options);
		},
		getFormElement: function () {
			return $(settings.formId);
		}
	};
};
