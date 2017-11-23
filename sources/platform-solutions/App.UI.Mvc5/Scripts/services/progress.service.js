/*
Progress Service
-----------------------------------------
- Depends on
	jquery
	NProgress

- Usage
	var progressService = new ProgressService([options]);
	progressService.start();
	progressService.set(0.5); // 50%
	progressService.step(); // Small auto increment
	progressService.reset();
	progressService.complete();
*/

var ProgressService = function (options) {

	var defaults = {
	};

	options = options || {};

	var settings = $.extend(true, defaults, options);

	return {
		start: function () {
			NProgress.start();
		},
		set: function (percentage) {
			NProgress.set(percentage);
		},
		step: function () {
			NProgress.inc();
		},
		reset: function () {
			NProgress.set(0);
		},
		complete: function () {
			NProgress.done();
		}
	};
};
