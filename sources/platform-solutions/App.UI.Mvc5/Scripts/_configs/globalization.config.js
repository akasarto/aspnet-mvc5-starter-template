/*
 * Globalization configs
 * - Customize global options here.
 */

var lang = $('html').attr('lang');
var region = $('html').data('region');

moment.locale(region);
