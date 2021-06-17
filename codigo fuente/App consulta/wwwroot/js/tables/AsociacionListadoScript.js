//*********************************** VARIABLES ******************************************
var root_A;
var dataGrid_A;
var source_A = "";

var showDNI_A = false;
var code_A = "";
//*********************************** funcAsoc ******************************************

var funcAsoc = {
    instanceDataGrid: function () {
        dataGrid_A = $("#gridContainerAsoc").dxDataGrid({
            dataSource: source_A,
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
                    visible: showDNI_A,
                    width: '150',
                    hidingPriority: 1
                },
                {
                    dataField: "datetime",
                    caption: "Fecha",
                    alignment: "center",
                    width: '200',
                    hidingPriority: 5
                },
                {
                    dataField: "name",
                    caption: "Nombre",
                    alignment: "center",
                    width: '250',
                    hidingPriority: 3
                },
                {
                    dataField: "dep",
                    caption: "Depto.",
                    alignment: "center",
                    width: '200',
                    hidingPriority: 2
                },
                {
                    dataField: "mun",
                    caption: "Municipio",
                    alignment: "center",
                    width: '200',
                    hidingPriority: 4
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

    
    init: function () {
        // Carga las variables de configuración.
        root_A = $('#Root').val();
        code_A = $('#code').val();

        if (typeof myShowDni !== "undefined") {
            showDNI_A = myShowDni;
        }

         if (code_A != "") {
             source_A = root_A + "Kobo/ListadoAsociacionesUsuario/?code=" + code_A;
        } else {
             source_A = root_A + "Kobo/ListadoAsociacionesUsuario";
        }


        funcAsoc.instanceDataGrid();
    }
};

//************************************** ON READY **********************************************
    $(function() {

        DevExpress.localization.locale("es-US");
        funcAsoc.init();
    });







