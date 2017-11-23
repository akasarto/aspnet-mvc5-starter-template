/*
Model State Service
-----------------------------------------
- Depends on
	mvc json result

- Usage
	var modelStateService = new ModelStateService([options]);
	var errors = modelStateService.getErrors(modelStateDictionaryJson);
	var errorMessages = modelStateService.getErrorMessages(modelStateDictionaryJson);
*/

var ModelStateService = function (options) {

	var defaults = {
	};

	options = options || {};

	var settings = $.extend(true, defaults, options);

	return {
		getErrors: function (state) {
			var errors = [];
			$.each(state, function (fieldsIdx, item) {
				$.each(item.errors, function (errorsIdx, error) {
					errors.push(error);
				});
			});
			return errors;
		},
		getErrorMessages: function (state) {
			var messages = [];
			$.each(state, function (fieldsIdx, item) {
				$.each(item.errors, function (errorsIdx, error) {
					messages.push(error.errorMessage);
				});
			});
			return messages;
		}
	};
};
