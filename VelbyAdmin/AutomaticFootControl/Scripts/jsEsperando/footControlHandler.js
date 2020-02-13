$(function () {
    
    $('form').validate({
        submitHandler: function (form) {
            $(form).find('[type="submit"]').attr('disabled', 'disabled').addClass('button-disabled');
            esperando.iniciar();
            form.submit();
        },
        invalidHandler: function (form, validator) {
            $(form).find('[type="submit"]').removeAttr('disabled').removeClass('button-disabled');
        }
    });
});

$(document).ready(function () {
    $(".Esperando").click(function () {
        esperando.iniciar();
    });
});