//*********************************** VARIABLES ******************************************
var root;
var dataGrid;
var source = "";

var showEditar = false;
var showDelete = false;

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
                    dataField: "coordinacion",
                    caption: "Coordinación",
                    alignment: "center",
                    width: '17%'
                },
                {
                    dataField: "telefono",
                    caption: "Teléfono",
                    alignment: "center",
                    width: '10%'
                },
                {
                    dataField: "email",
                    caption: "Correo electrónico",
                    alignment: "center",
                    width: '10%'
                },
                {
                    dataField: "numeroEncuestas",
                    caption: "Numero de encuestas",
                    alignment: "center",
                    width: '10%'
                },

                {
                    dataField: "Opciones",
                    caption: "Opciones",
                    alignment: "left",
                    allowHeaderFiltering: false,
                    width: '10%',
                    cellTemplate: function (container, options) {

                        var idEnc = options.data.id;
                        var contenido = '<a href="/Encuestador/Details/' + idEnc + '" title="Detalles encuestador ' + idEnc + '" class="btn btn-outline-info btn-xs ml-1" ><i class="fas fa-file-alt"></i></a>'
                        if (showEditar) {
                            contenido += '<a href="/Encuestador/Edit/' + idEnc + '" title="Editar encuestador ' + idEnc + '" class="btn btn-outline-success btn-xs ml-1" ><i class="fas fa-edit"></i></a>'
                        }
                        if (showDelete) {
                            contenido += '<a href="/Encuestador/Delete/' + idEnc + '" title="Borrar encuestador ' + idEnc + '" class="btn btn-outline-danger btn-xs ml-1" ><i class="fas fa-trash"></i></a>'
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
        source = root + "Encuestador/ListAjax/";

        showEditar = $('#ShowEdit').val() == 1;
        showDelete = $('#ShowDelete').val()==1;

        funcLE.instanceDataGrid();
    }
};

//************************************** ON READY **********************************************
$(function () {

    DevExpress.localization.locale("es-US");
    funcLE.init();
});







