//*********************************** VARIABLES ******************************************
var root;
var dataGrid;
var source = "";

var showDNI = false;
var showValidation = false;
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
                    dataField: "id",
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
                    dataField: "validation",
                    caption: "Formalización",
                    visible: showValidation,
                    alignment: "center",
                    hidingPriority: 1,
                    width: '100',
                    cellTemplate: function (container, options) {
                        var val = options.data.validation;
                        var contenido = val ? "Si": "No";

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

        source = root + "Kobo/ListadoAjax/?code=" + code;

        funcLE.instanceDataGrid();
    }
};

//************************************** ON READY **********************************************
$(function () {

    DevExpress.localization.locale("es-US");
    funcLE.init();
});







