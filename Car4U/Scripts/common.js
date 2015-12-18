$(document).ready(function() {
  // Date validator for dd-mm-aaaa format.
  $.validator.addMethod('datePT', function(value, element) {
    return value.match(/^([0-2]?\d|3[0-1])-(0?[0-9]|10|11|12)-\d{4}$/);
  });
  $.validator.addMethod('ccDate', function(value, element) {
    return value.match(/^(0?[0-9]|10|11|12)\/\d{2}$/);
  });
});