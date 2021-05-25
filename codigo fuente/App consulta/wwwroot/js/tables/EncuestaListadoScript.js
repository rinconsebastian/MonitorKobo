//*********************************** VARIABLES ******************************************
var root;
var dataGrid;
var source = "";

var showDNI = false;
var showValidation = false;
var loadValidation = false;
var code = "";
//*********************************** funcLE ******************************************

var funcLE = {


    instanceDataGrid: function () {
        dataGrid = $("#gridContainer").dxDataGrid({
            dataSource: source,
            selection: {
                mode: "none",
                showCheckBoxesMode: "always",
                selectAllMode: "allPages"
            },
            noDataText: "No hay datos disponibles.",
            export: {
                enabled: false,
                fileName: "Listado_encuestas_" + moment().format("DD-MM-YYYY_hh-mm-ss"),
                allowExportSelectedData: true
            },

            stateStoring: {
                enabled: false,
                type: "localStorage",
                storageKey: "storage"
            },
            loadPanel: {
                enabled: true
            },

            paging: {
                pageSize: 50
            },

            scrolling: {
                mode: "virtual"
            },
            height: '72vh',
            width: '100%',
            columnFixing: {
                enabled: true
            },
            wordWrapEnabled: false,
            rowAlternationEnabled: true,
            columnHidingEnabled: true,
            showRowLines: true,
            grouping: {
                contextMenuEnabled: true,
                expandMode: "rowClick"
            },
            groupPanel: {
                emptyPanelText: "haga click derecho en una columna para agruparla",
                visible: true
            },
            pager: {
                showPageSizeSelector: true,
                allowedPageSizes: [10, 20, 50, 100, 1000]
            },
            columnChooser: {
                enabled: false
            },
            allowColumnReordering: false,
            allowColumnResizing: true,
            columnAutoWidth: false,
            showBorders: true,
            filterRow: {
                visible: true,
                applyFilter: "auto"
            },
            searchPanel: {
                visible: true,
                width: 160,
                placeholder: "Buscar..."
            },
            headerFilter: {
                visible: true
            },
            columns: [

                {
                    dataField: "user",
                    caption: "Encuestador",
                    alignment: "center",
                    visible: showDNI,
                    width: '100',
                    hidingPriority: 5
                },
                {
                    dataField: "datetime",
                    caption: "Fecha",
                    alignment: "center",
                    width: '150',
                    hidingPriority: 3
                },
                {
                    dataField: "dep",
                    caption: "Depto.",
                    alignment: "center",
                    width: '120',
                    hidingPriority: 2
                    

                },
                {
                    dataField: "mun",
                    caption: "Municipio",
                    alignment: "center",
                    width: '120',
                    hidingPriority: 4
                   

                },
                {
                    
                    caption: "Formalización",
                    visible: showValidation,
                    alignment: "center",
                    hidingPriority: 1,
                    width: '100',
                    cellTemplate: function (container, options) {

                        var idKobo = options.data.idKobo;
                        var val = options.data.validation;
                        var formId = options.data.formalizacionId;

                        var contenido = "No";
                        if (formId != 0) {
                            contenido = '<a href="' + root + 'Formalizacion/Details/' + formId + '" title="Detalles" class="btn btn-outline-info btn-xs ml-1" ><i class="fas fa-file-alt"></i></a>'
                            if (loadValidation) {
                                contenido += '<a href="' + root + 'Formalizacion/Edit/' + formId + '" title="Editar " class="btn btn-outline-warning btn-xs ml-1" ><i class="fas fa-edit"></i></a>'
                            }
                        } else if (val && loadValidation) {
                            contenido = '<button class="btn btn-outline-success btn-xs ml-1 load-formlz" data-id="' + idKobo + '" title="Cargar datos"><i class="fas fa-download"></i></button>';
                        }

                        $("<div class='preventSelection'>")
                            .append(contenido)
                            .appendTo(container);
                    }

                }
            ],
            summary: {
                totalItems: [{
                    column: "user",
                    summaryType: "count",
                    showInColumn: "mun",
                    displayFormat: "Total: {0}",
                }],

            },

            onToolbarPreparing: function (e) {
                var dataGrid = e.component;
                e.toolbarOptions.items.unshift(
                    {
                        location: "after",
                        widget: "dxButton",
                        options: {
                            icon: "refresh",
                            onClick: function () {
                                dataGrid.refresh();
                            }
                        }
                    });
            }
        }).dxDataGrid('instance');
    },

    loadFormalizacion: function () {
        $('body').on('click', '.load-formlz', function (e) {
            var idKobo = $(this).data('id');

            $(this).find('i').removeClass('fa-download');
            $(this).find('i').addClass('fa-cog fa-spin');
            $(this).attr('disabled', 'disabled');

            var fullurl = root + "Formalizacion/Cargar/";
            
            $.post(fullurl, { idKobo: idKobo }).
                done(function (data) {
                    $(this).find('i').addClass('fa-download');
                    $(this).find('i').removeClass('fa-cog fa-spin');
                    $(this).removeAttr('disabled');
                    if (data != null) {
                        if (data.success) {
                            if (data.url != null) {
                                window.location.href = root + data.url;
                            } else {
                                funcGenerico.mostrarMensaje(data.message, "success");
                            }
                        } else {
                            funcGenerico.mostrarMensaje(data.message, "error");
                        }
                    } else {
                        funcGenerico.mostrarMensaje("Error en la respuesta del servidor.", "error");
                    }
                }).fail(function (data) {
                    $(this).find('i').addClass('fa-download');
                    $(this).find('i').removeClass('fa-cog fa-spin');
                    $(this).removeAttr('disabled');
                    funcGenerico.mostrarMensaje("Error al solicitar la operación.", "error");
                });
        });
    },

    init: function () {
        // Carga las variables de configuración.
        root = $('#Root').val();
        code = $('#code').val();

        if (typeof myShowDni !== "undefined") {
            showDNI = myShowDni;
        }
        if (typeof myShowValidation !== "undefined") {
            showValidation = myShowValidation;
        }
        if (typeof myLoadValidation !== "undefined") {
            loadValidation = myLoadValidation;
        }
       
        if (code != "") {
            source = root + "Kobo/ListadoEncuestasUsuario/?code=" + code;
        } else {
            source = root + "Kobo/ListadoEncuestas";
        }
        

        funcLE.instanceDataGrid();
        funcLE.loadFormalizacion();
    }
};

//************************************** ON READY **********************************************
$(function () {

    DevExpress.localization.locale("es-US");
    funcLE.init();
});







