//*********************************** VARIABLES ******************************************
var root_A;
var dataGrid_A;
var source_A = "";

var showExport = false;
var showDNI_A = false;

var showFull = false;
var tablename = "";

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
                enabled: showExport,
                fileName: "Listado_asociaciones_" + moment().format("DD-MM-YYYY_hh-mm-ss"),
                allowExportSelectedData: false
            },
            onExporting: function (e) {
                e.component.beginUpdate();
                if (!showDNI_A) {
                    e.component.columnOption("user", "visible", true);
                    e.component.columnOption("userName", "visible", true);
                }

            },
            onExported: function (e) {
                if (!showDNI_A) {
                    e.component.columnOption("user", "visible", false);
                    e.component.columnOption("userName", "visible", false);
                }
                e.component.endUpdate();
            },
            stateStoring: {
                enabled: true,
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
                emptyPanelText: "Haga clic derecho en una columna para agruparla",
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
                    name: tablename + "name",
                    dataField: "name",
                    caption: "Nombre\r\nAsociación",
                    headerCellTemplate: function (header, info) {
                        $("<div>").html(info.column.caption.replace(/\r\n/g, "<br/>")).appendTo(header);
                    },
                    alignment: "center",
                    width: showFull ? '350': '300',
                    hidingPriority: 6
                },
                {
                    name: tablename + "mun",
                    dataField: "mun",
                    caption: "Muninicipio",
                    alignment: "center",
                    width: showFull ?  '170' : '140',
                    hidingPriority: 4
                },
                {
                    name: tablename + "dep",
                    dataField: "dep",
                    caption: "Departamento",
                    alignment: "center",
                    width: showFull ? '170' : '140',
                    hidingPriority: 2
                },
                {
                    name: tablename + "datetime",
                    dataField: "datetime",
                    caption: "Fecha",
                    alignment: "center",
                    width: showFull ? '120' : '100',
                    hidingPriority: 5,
                    dataType: "date",
                    calculateFilterExpression: function (value, selectedFilterOperations, target) {
                        if (target === "headerFilter" && value === "weekends") {
                            return [[getOrderDay, "=", 0], "or", [getOrderDay, "=", 6]];
                        }
                        return this.defaultCalculateFilterExpression.apply(this, arguments);
                    }
                },
                {
                    name: tablename + "user",
                    dataField: "user",
                    caption: "Cedula\r\nEncuestador",
                    headerCellTemplate: function (header, info) {
                        $("<div>").html(info.column.caption.replace(/\r\n/g, "<br/>")).appendTo(header);
                    },
                    alignment: "center",
                    visible: showDNI_A,
                    width: showFull ? '150': '150',
                    hidingPriority: 3
                },
                {
                    name: tablename + "username",
                    dataField: "userName",
                    caption: "Nombre\r\nEncuestador",
                    headerCellTemplate: function (header, info) {
                        $("<div>").html(info.column.caption.replace(/\r\n/g, "<br/>")).appendTo(header);
                    },
                    alignment: "center",
                    visible: showDNI_A,
                    width: showFull ? '280' : '200',
                    hidingPriority: 1
                },
                
            ],
            summary: {
                totalItems: [{
                    column: "user",
                    summaryType: "count",
                    showInColumn: "name",
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
                            hint: "Actualizar",
                            onClick: function () {
                                dataGrid.refresh();
                            }
                        }
                    },
                    {
                        location: "after",
                        widget: "dxButton",
                        options: {
                            icon: "clearformat",
                            hint: "Borrar filtros",
                            onClick: function () {
                                dataGrid.state(null);
                            }
                        }
                    }
                );
            }
        }).dxDataGrid('instance');
    },

    
    init: function () {
        // Carga las variables de configuración.
        root_A = $('#Root').val();
        code_A = $('#code').val();
        showExport = $('#showExport').val() == 1;

        if (typeof myShowFull !== "undefined") {
            showFull = myShowFull;
        }
        tablename = showFull ? "asoc_" : "asoc_min_";

        if (typeof myShowDni !== "undefined") {
            showDNI_A = myShowDni;
        }

         if (code_A != "") {
             source_A = root_A + "Kobo/ListadoAsociacionesUsuario/?code=" + code_A;
        } else {
             source_A = root_A + "Kobo/ListadoAsociaciones";
        }


        funcAsoc.instanceDataGrid();
    }
};

//************************************** ON READY **********************************************
    $(function() {

        DevExpress.localization.locale("es-US");
        funcAsoc.init();
    });



function getOrderDay(rowData) {
    return (new Date(rowData.OrderDate)).getDay();
}



