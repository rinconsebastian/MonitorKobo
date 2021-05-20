//*********************************** VARIABLES ******************************************
var root;
var dataGrid;
var source = "";

var showEditar = false;
var ShowValidar = false;

//*********************************** funcForm ******************************************

var funcForm = {


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
                fileName: "Listado_encuestadores_" + moment().format("DD-MM-YYYY_hh-mm-ss"),
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
            rowAlternationEnabled: false,
            showRowLines: true,
            pager: {
                showPageSizeSelector: true,
                allowedPageSizes: [10, 20, 50, 100, 1000]
            },
            columnChooser: {
                enabled: false
            },
            allowColumnReordering: false,
            allowColumnResizing: true,
            columnAutoWidth: true,
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
                    dataField: "cedula",
                    caption: "Cedula",
                    alignment: "center",
                    width: '9%'
                },
                {
                    dataField: "nombre",
                    caption: "Nombre",
                    alignment: "center",
                    width: '17%'
                },
                {
                    dataField: "municipio",
                    caption: "Municipio",
                    alignment: "center",
                    width: '17%',
                    cellTemplate: function (container, options) {
                        var dep = options.data.departamento;
                        var mun = options.data.municipio;

                        var contenido = mun + ' (' + dep + ')';

                        $("<div class='preventSelection'>")
                            .append(contenido)
                            .appendTo(container);
                    }

                },
                

                {
                    dataField: "Opciones",
                    caption: "Opciones",
                    alignment: "left",
                    allowHeaderFiltering: false,
                    width: '10%',
                    cellTemplate: function (container, options) {

                        var idEnc = options.data.id;
                        var contenido = '<a href="' + root + 'Formalizacion/Details/' + idEnc + '" title="Detalles" class="btn btn-outline-info btn-xs ml-1" ><i class="fas fa-file-alt"></i></a>'
                        if (showEditar) {
                            contenido += '<a href="' + root + 'Formalizacion/Edit/' + idEnc + '" title="Editar " class="btn btn-outline-warning btn-xs ml-1" ><i class="fas fa-edit"></i></a>'
                        }
                        if (ShowValidar) {
                            contenido += '<a href="' + root + 'Formalizacion/Edit/' + idEnc + '" title="Validar" class="btn btn-outline-success btn-xs ml-1" ><i class="far fa-thumbs-up"></i></a>'
                        }

                        $("<div class='preventSelection'>")
                            .append(contenido)
                            .appendTo(container);
                    }

                }
            ],
            summary: {
                totalItems: [{
                    column: "id",
                    summaryType: "count",
                    showInColumn: "cedula",
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

    init: function () {
        // Carga las variables de configuración.
        root = $('#Root').val();
        source = root + "Formalizacion/Ajax/";

        showEditar = $('#ShowEdit').val() == 1;
        ShowValidar = $('#ShowValidate').val() == 1;

        funcForm.instanceDataGrid();
    }
};

//************************************** ON READY **********************************************
$(function () {

    DevExpress.localization.locale("es-US");
    funcForm.init();
});