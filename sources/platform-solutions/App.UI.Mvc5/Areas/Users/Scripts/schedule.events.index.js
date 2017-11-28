(function ($, website) {

	'use strict';

	var modalService = new ModalService();
	var tableService = new TableService();
	var notificationService = new NotificationService();

	// Init
	$(function () {

		var tableInstance = tableService.create();

		var $table = tableService.getTableElement();

		// Delete
		$('a.delete', $table).on('click', function (e) {

			e.preventDefault();
			e.stopPropagation();

			var $a = $(this);
			var $row = $(this).closest('tr');

			var dialog = modalService.showConfirmationDialog(
				{
					items: {
						src: '#mainDeleteDialog'
					},
					itemLabel: $a.data('item-label'),
					actionConfirmed: function (instance) {
						$.post($a.attr('href'))
							.done(function () {
								// Avoid recalculating sort and pagination
								tableInstance.row($row.get(0)).remove().draw('page');
								notificationService.showSuccess();
							})
							.fail(function () {
								notificationService.showError();
							})
							.always(function () {
								instance.close();
							});
					}

				}
			);

		});

	});

}).apply(this, [jQuery, window.website]);
