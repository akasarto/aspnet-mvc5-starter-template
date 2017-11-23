/*
Table Service
-----------------------------------------
- Depends on
	jquery
	dataTables

- Usage
	var tableService = new TableService([options]);
	var instance = tableService.create([options]);
*/

var TableService = function (options) {

	var defaults = {
		"order": [],
		"columnDefs": [{
			"targets": 'nosort',
			"orderable": false
		}],
		tableSelector: '#mainTable'
	};

	options = options || {};

	var settings = $.extend(true, defaults, options);

	var createInstance = function (options) {
		settings = $.extend(true, settings, options);
		return $(settings.tableSelector).DataTable(settings);
	};

	return {
		create: function (options) {
			options = options || {};
			return createInstance(options);
		},
		getTableElement: function () {
			return $(settings.tableSelector);
		}
	};
};
