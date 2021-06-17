//*********************************** VARIABLES ******************************************
var root;
var dataGrid;
var source = "";

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
                    width: '100',
                    hidingPriority: 2,
                },
                {
                    dataField: "nombre",
                    caption: "Nombre",
                    alignment: "center",
                    width: '200',
                    hidingPriority: 3,
                },
                {
                    dataField: "municipio",
                    caption: "Municipio",
                    alignment: "center",
                    width: '200',
                    hidingPriority: 4,
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
                    dataField: "coordinacion",
                    caption: "Coordinación",
                    alignment: "center",
                    width: '200',
                    hidingPriority: 5
                },
                {
                    dataField: "fecha",
                    caption: "Fecha",
                    alignment: "center",
                    width: '100',
                    hidingPriority: 7
                },
                {
                    dataField: "nombreEstado",
                    caption: "Estado",
                    alignment: "center",
                    width: '100',
                    hidingPriority: 6,
                    cellTemplate: function (container, options) {
                        var nombre = options.data.nombreEstado;
                        var color = "";
                        switch (options.data.estado) {
                            case 1:
                                color = "bg-warning text-dark";
                                break;
                            case 2:
                                color = "bg-success text-white";
                                break;
                            case 3:
                                color = "bg-danger text-white";
                                break;
                            case 4:
                                color = "bg-info text-white";
                                break;
                        }
                        contenido = '<h6 class="mb-0"><span class="badge ' + color + '">' + nombre + '</span></h6>';
                        $("<div class='preventSelection'>")
                            .append(contenido)
                            .appendTo(container);
                    }
                },

                {
                    dataField: "Opciones",
                    caption: "Opciones",
                    alignment: "center",
                    allowHeaderFiltering: false,
                    width: '100',
                    hidingPriority: 1,
                    cellTemplate: function (container, options) {

                        var formId = options.data.id;
                        var status = options.data.estado;

                        var contenido = "";
                        if (formId != 0) {
                            contenido = '<a href="' + root + 'Formalizacion/Details/' + formId + '" title="Detalles" class="btn btn-outline-info btn-xs ml-1" ><i class="fas fa-file-alt"></i></a>'
                            if (ShowValidar && status == 1) {
                                contenido += '<a href="' + root + 'Formalizacion/Edit/' + formId + '" title="Editar " class="btn btn-outline-warning btn-xs ml-1" ><i class="fas fa-edit"></i></a>'
                            }
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


        ShowValidar = $('#ShowValidate').val() == 1;

        funcForm.instanceDataGrid();
    }
};

//************************************** ON READY **********************************************
$(function () {

    DevExpress.localization.locale("es-US");
    funcForm.init();
});