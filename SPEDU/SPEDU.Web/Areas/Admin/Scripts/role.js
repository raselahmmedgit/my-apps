//-----------------------------------------------------
//start Refresh Kendo Grid Funtion For Index Page
function KendoGridReloader(gridId) {
    //Get Grid
    var kdGrid = $(gridId).data('kendoGrid');
    if (kdGrid != null) {
        kdGrid.dataSource.read();
        kdGrid.refresh();
    }
}
//-----------------------------------------------------
//end Refresh Kendo Grid Funtion For Index Page

//-------------------------------------------------------
//start of kendo grid loader

function KendoGridLoader(gridId, readUrl, pageSize) {

    $(gridId).kendoGrid({
        dataSource: {
            transport: {
                read: readUrl
            },
            schema: {
                data: function (data) {
                    return data.Data;
                },
                model: {
                    fields: {
                        RoleId: { type: "number" },
                        RoleName: { type: "string" },

                        ActionLink: { type: "string" }
                    }
                }, //end model
                total: function (data) {
                    return data.Total;
                }
            },
            pageSize: pageSize,
            serverPaging: true,
            serverFiltering: false,
            serverSorting: true
        },
        height: 400,
        filterable: true,
        groupable: true,
        sortable: true,
        pageable: {
            input: true,
            numeric: true,
            info: true,
            previousNext: true,
            refresh: true,
            pageSizes: true
        },
        columns: [{ field: "RoleId", title: "RoleId", hidden: true, filterable: false, sortable: false },
              { field: "RoleName", title: "Role Name", filterable: filterOption },

              { field: "ActionLink", title: "Actions", width: "12%", filterable: false, sortable: false, template: "#= ActionLink #" }
        ]
    });
}

//end of kendo grid loader
//-------------------------------------------------------

$(document).ready(function () {

});