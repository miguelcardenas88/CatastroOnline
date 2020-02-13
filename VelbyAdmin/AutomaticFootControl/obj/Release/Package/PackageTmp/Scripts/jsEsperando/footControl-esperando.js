
var esperando = {
    iniciar: function () {
        $(window).keydown(function (event) {
            if (event.keyCode === 13) {
                event.preventDefault();
                return false;
            }
        });

        window.loading_screen = window.pleaseWait({
            //logo: '../../AdminBellezApp/Content/assets/img/SmartCode.png',
            logo: '../.././Content/assets/img/SmartCode.png',
            backgroundColor: '#ffffff',
            loadingHtml: "<div><p class='loading-message'></p><h4>Cargando por favor espere..</h4><p></p><div class='sk-three-bounce'> <div class='sk-child sk-bounce1'></div> <div class='sk-child sk-bounce2'></div><div class='sk-child sk-bounce3'></div> </div></div>"
        });
    },
    terminar: function () {
        if (window.loading_screen !== undefined) {
            window.loading_screen.finish();
        }
    }
};

