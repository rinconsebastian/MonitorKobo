//*********************************** VARIABLES ******************************************
var root;

//*********************************** funcLE ******************************************

var funcPrint = {
    loadPrintFormlz: function () {
        $('body').on('click', '.btn-print-formlz', function (e) {
            var url = $(this).attr("data-link");
            funcPrint.showPrint(url);
        });
    },
    showPrint: function (url) {
        
        myWindow = window.open(url, "_blank", "toolbar=no,titlebar=no,menubar=no,scrollbars=yes,resizable=no,top=0,left=386,width=1000,height=700");
    },
    init: function () {
        // Carga las variables de configuración.
        root = $('#Root').val();
        funcPrint.loadPrintFormlz();
    }
};

//************************************** ON READY **********************************************
$(function () {

    funcPrint.init();
});






