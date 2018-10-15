/*
Modal Service
-----------------------------------------
- Depends on
	jquery
	magnificPopup

- Usage
	var modalService = new ModalService([options]);
	modalservice.showConfirmationDialog([options]);
*/

var ModalService = function (options) {

	var defaults = {
		modal: true,
		preloader: false,
		itemLabel: ''
	};

	options = options || {};

	var settings = $.extend(true, defaults, options);

	var showDialog = function (options) {
		settings = $.extend(true, settings, options);
		$.magnificPopup.open(settings);
		return $.magnificPopup.instance;
	}

	return {
		showConfirmationDialog: function (options) {
			var deleteDefaults = {
				items: {
					type: 'inline',
					src: '#mainConfirmationDialog'
				},
				callbacks:
				{
					//
					close: function () {
						$('button.confirmation-cancel', $(this.currItem.src)).off('click');
						$('button.confirmation-confirm', $(this.currItem.src)).off('click');
					},

					//
					open: function () {

						var instance = this;
						var $message = $('div.confirmation-content-message', $(this.currItem.src));

						if (settings.itemLabel) {
							$message.html($('div.confirmation-content-template-with-label', $(this.currItem.src)).html());
							$('strong.confirmation-item-label', $message).text(settings.itemLabel);
						} else {
							$message.html($('div.confirmation-content-template', $(this.currItem.src)).text());
						}

						$('button.confirmation-cancel', $(this.currItem.src)).on('click', function (e) {
							e.preventDefault();
							instance.close();
						});

						$('button.confirmation-confirm', $(this.currItem.src)).on('click', function (e) {
							e.preventDefault();
							if (settings.actionConfirmed) {
								settings.actionConfirmed(instance);
							}
						});
					}
				}
			};

			options = options || {};
			options = $.extend(true, deleteDefaults, options);

			return showDialog(options);
		}
	};
};
