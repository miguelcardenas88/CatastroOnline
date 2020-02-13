

function ejecutePost(url, datos) {
    debugger;
    esperando.iniciar();
    const datosLlamada = datos;
    if (typeof (datosLlamada) === 'object') {
        datosLlamada.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
    }
    $.post(url, datosLlamada, function (response) {
        $("#panelPrincipal").html(response);
    }).done(function () {
        esperando.terminar();
    }).always(function () {
        esperando.terminar();
    });
}

$(document).ready(function () {
    $("input[type='text'].foramto-fecha").datetimepicker({
        language: 'es',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        format: 'dd/mm/yyyy'
    });

    $("input[type='text'].numeros-monto").attr({
        type: 'number',
    });

    $("input[type='text'].numeros").attr({
        type: 'number',
    });
});


$(document).ready(function () {
    $("input[type='text'].foramto-hora").datetimepicker({
        format: 'LT'
    });


});

var ejecutar = {
    accionControladorLista: function (url) {
        //esperando.iniciar();

        //var form = $('#__AjaxAntiForgeryForm');
        var token = $('input[name="__RequestVerificationToken"]').val();
        debugger;
        $.ajax({
            url: url,
            type: 'POST',
            data: {
                __RequestVerificationToken: token
            },
            success: function (result) {
                $('#panelPrincipal').empty();
                $('#panelPrincipal').append(result);
            }
        });
        return false;

        //$.post(url, function (data) {
        //    if (data) {
        //        $('#panelPrincipal').empty();
        //        $('#panelPrincipal').append(data);
        //    }
        //});
        //esperando.terminar();
    }
};