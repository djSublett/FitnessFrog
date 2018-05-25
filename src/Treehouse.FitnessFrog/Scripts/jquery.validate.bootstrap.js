(function ($) {
    var defaultOptions = {
        validClass: 'has-success',
        errorClass: 'has-error',
        //this highlight function finds the closest css class of form-group and then
        //removes the "validClass" and adds the "errorClass"
        highlight: function (element, errorClass, validClass) {
            $(element).closest('.form-group')
                .removeClass(validClass)
                .addClass(errorClass);
        },

        //this unhighlight function finds the closest css class of form-group and then
        //removes the "errorClass" and adds the "validClass"
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest('.form-group')
                .removeClass(errorClass)
                .addClass(validClass);
        }
    };

    //this jquery validator calls the (setDefaults) function and passes in the defaultOptions object 
    $.validator.setDefaults(defaultOptions);

    //this Microsoft jquery function uses our defaultOptions.errorClass property for an error
    //and the validClass uses the defaultOptions.validClass property for something valid
    $.validator.unobtrusive.options = {
        errorClass: defaultOptions.errorClass,
        validClass: defaultOptions.validClass
    };
})(jQuery);