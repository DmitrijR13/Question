var url = "";

function getDeal() {
    var titleGrid = "Характер обращений";

    $("#areaContent").html("<div id='dealTable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");
    jQuery('#GridTable').jqGrid({
        url: url + '/Complaint/LoadDataDeal',
        datatype: "json",
        mtype: 'POST',
        caption: titleGrid,
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Название', 'Удален?', ''],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'Name', index: 'Name', sortable: false, search: false, editable: false, width: 100 },
                    { name: 'IsDelete', index: 'IsDelete', sortable: false, search: false, editable: false, width: 100, formatter: 'checkbox' },
                    { name: 'act', index: 'act', width: 15, sortable: false, search: false, width: 30 }
        ],
        pager: '#GridTableToolbar',
        rowNum: 15,
        height: 470,
        width: 920,
        gridview: true,
        toolbar: [true, "top"],
        viewrecords: true,
        gridComplete: function () {
            var ids = jQuery("#GridTable").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var edit = "<input type='image' title='Редактировать' src='../../.." + url + "/Content/images/Edit_icon.png' style='' onclick='EditDeal(" + cl + ")' />";
                var del = "<input type='image' title='Удалить' src='../../.." + url + "/Content/images/Delete_icon.png' style='' onclick='DeleteDeal(" + cl + ")' />";
                jQuery("#GridTable").jqGrid('setRowData', ids[i], { act: edit + del });
            }
        }
    });

    jQuery("#GridTable").navGrid('#GridTableToolbar', { del: false, add: false, edit: false, search: false, refresh: false });
    jQuery("#GridTable").filterToolbar({ stringResult: true, searchOnEnter: true });

    var tBar = $("#t_GridTable");
    tBar.append("<input type='image' title='Новый' src='../../.." + url + "/Content/images/Create_tb.png' style='' onclick='EditDeal(0)' />");
    tBar.append("<input type='image' title='Обновить' src='../../.." + url + "/Content/images/refresh.png' style='' onclick='ReloadGrid()' />");
}

function EditDeal(id) {
    $("#popup").show();

    $("#popupContent").html('');
    //showLoading();

    $("#popupContent").load(
        url + "/Complaint/EditDeal?dealID=" + id,
        function success() {
            //hideLoading();
        }
    );
}

function DeleteDeal(id) {
    $.ajax({
        url: url + "/Complaint/DeleteDeal?dealID=" + id,
        success: function (data) {
            ReloadGrid();
        },
        error: function () {
            alert("error");
        },
    });
}

//-------------------------------------

function getState() {
    var titleGrid = "Состояние обращений";

    $("#areaContent").html("<div id='stateTable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");

    jQuery('#GridTable').jqGrid({
        url: url + '/Complaint/LoadDataState',
        datatype: "json",
        mtype: 'POST',
        caption: titleGrid,
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Название', 'Удален?', ''],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'Name', index: 'Name', sortable: false, search: false, editable: false, width: 100 },
                    { name: 'IsDelete', index: 'IsDelete', sortable: false, search: false, editable: false, width: 100, formatter: 'checkbox' },
                    { name: 'act', index: 'act', width: 15, sortable: false, search: false, width: 30 }
        ],
        pager: '#GridTableToolbar',
        rowNum: 15,
        height: 470,
        width: 920,
        gridview: true,
        toolbar: [true, "top"],
        viewrecords: true,
        gridComplete: function () {
            var ids = jQuery("#GridTable").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var edit = "<input type='image' title='Редактировать' src='../../.." + url + "/Content/images/Edit_icon.png' style='' onclick='EditState(" + cl + ")' />";
                var del = "<input type='image' title='Удалить' src='../../.." + url + "/Content/images/Delete_icon.png' style='' onclick='DeleteState(" + cl + ")' />";
                jQuery("#GridTable").jqGrid('setRowData', ids[i], { act: edit + del });
            }
        }
    });

    jQuery("#GridTable").navGrid('#GridTableToolbar', { del: false, add: false, edit: false, search: false, refresh: false });
    jQuery("#GridTable").filterToolbar({ stringResult: true, searchOnEnter: true });

    var tBar = $("#t_GridTable");
    tBar.append("<input type='image' title='Новый' src='../../.." + url + "/Content/images/Create_tb.png' style='' onclick='EditState(0)' />");
    tBar.append("<input type='image' title='Обновить' src='../../.." + url + "/Content/images/refresh.png' style='' onclick='ReloadGrid()' />");
}

function EditState(id) {
    $("#popup").show();

    $("#popupContent").html('');
    //showLoading();

    $("#popupContent").load(
        url + "/Complaint/EditState?stateID=" + id,
        function success() {
            //hideLoading();
        }
    );
}

function DeleteState(id) {
    $.ajax({
        url: url + "/Complaint/DeleteState?stateID=" + id,
        success: function (data) {
            ReloadGrid();
        },
        error: function () {
            alert("error");
        },
    });
}

//----------------------------------------------------------

function getCategory() {
    var titleGrid = "Категории обращений";

    $("#areaContent").html("<div id='categoryTable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");

    jQuery('#GridTable').jqGrid({
        url: url + '/Complaint/LoadDataCategory',
        datatype: "json",
        mtype: 'POST',
        caption: titleGrid,
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Название', 'Удален?', ''],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'Name', index: 'Name', sortable: false, search: false, editable: false, width: 100 },
                    { name: 'IsDelete', index: 'IsDelete', sortable: false, search: false, editable: false, width: 100, formatter: 'checkbox' },
                    { name: 'act', index: 'act', width: 15, sortable: false, search: false, width: 30 }
        ],
        pager: '#GridTableToolbar',
        rowNum: 15,
        height: 470,
        width: 920,
        gridview: true,
        toolbar: [true, "top"],
        viewrecords: true,
        gridComplete: function () {
            var ids = jQuery("#GridTable").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var edit = "<input type='image' title='Редактировать' src='../../.." + url + "/Content/images/Edit_icon.png' style='' onclick='EditCategory(" + cl + ")' />";
                var del = "<input type='image' title='Удалить' src='../../.." + url + "/Content/images/Delete_icon.png' style='' onclick='DeleteCategory(" + cl + ")' />";
                jQuery("#GridTable").jqGrid('setRowData', ids[i], { act: edit + del });
            }
        }
    });

    jQuery("#GridTable").navGrid('#GridTableToolbar', { del: false, add: false, edit: false, search: false, refresh: false });
    jQuery("#GridTable").filterToolbar({ stringResult: true, searchOnEnter: true });

    var tBar = $("#t_GridTable");
    tBar.append("<input type='image' title='Новый' src='../../.." + url + "/Content/images/Create_tb.png' style='' onclick='EditCategory(0)' />");
    tBar.append("<input type='image' title='Обновить' src='../../.." + url + "/Content/images/refresh.png' style='' onclick='ReloadGrid()' />");
}

function EditCategory(id) {
    $("#popup").show();

    $("#popupContent").html('');
    //showLoading();

    $("#popupContent").load(
        url + "/Complaint/EditCategory?categoryID=" + id,
        function success() {
            //hideLoading();
        }
    );
}

function DeleteCategory(id) {
    $.ajax({
        url: url + "/Complaint/DeleteCategory?categoryID=" + id,
        success: function (data) {
            ReloadGrid();
        },
        error: function () {
            alert("error");
        },
    });
}

// -----------------------------------------------------------------

function getComplaint() {
    var titleGrid = "Жалобы";

    $("#areaContent").html("<div id='complaintTable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");
    jQuery('#GridTable').jqGrid({
        url: url + '/Complaint/LoadDataComplaint',
        datatype: "json",
        mtype: 'POST',
        caption: titleGrid,
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Имя', 'Отчество', 'Фамилия', 'Федеральный код МО', 'Телефон', 'Характер обращения', 'Состояние', ''],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'FirstName', index: 'FirstName', sortable: false, search: false, editable: false, width: 100 },
                    { name: 'SecondName', index: 'SecondName', sortable: false, search: false, editable: false, width: 100 },
                    { name: 'LastName', index: 'LastName', sortable: false, search: false, editable: false, width: 100 },
                    { name: 'FederalCodeMO', index: 'FederalCodeMO', sortable: false, search: false, editable: false, width: 100 },
                    { name: 'Phone', index: 'Phone', sortable: false, search: false, editable: false, width: 100 },
                    { name: 'TypeDealValue', index: 'TypeDeal.Name', sortable: false, search: false, editable: false, width: 100 },
                    { name: 'TypeStateValue', index: 'TypeState.Name', sortable: false, search: false, editable: false, width: 100 },
                    { name: 'act', index: 'act', width: 15, sortable: false, search: false, width: 30 }
        ],
        pager: '#GridTableToolbar',
        rowNum: 15,
        height: 470,
        width: 920,
        gridview: true,
        toolbar: [true, "top"],
        viewrecords: true,
        gridComplete: function () {
            var ids = jQuery("#GridTable").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var edit = "<input type='image' title='Редактировать' src='../../.." + url + "/Content/images/Edit_icon.png' style='' onclick='EditComplaint(" + cl + ")' />";
                jQuery("#GridTable").jqGrid('setRowData', ids[i], { act: edit });
            }
        }
    });

    jQuery("#GridTable").navGrid('#GridTableToolbar', { del: false, add: false, edit: false, search: false, refresh: false });
    jQuery("#GridTable").filterToolbar({ stringResult: true, searchOnEnter: true });

    var tBar = $("#t_GridTable");
    tBar.append("<input type='image' title='Обновить' src='../../.." + url + "/Content/images/refresh.png' style='' onclick='ReloadGrid()' />");
}

function EditComplaint(id) {
    $("#popup").show();

    $("#popupContent").html('');

    $("#popupContent").load(
        url + "/Complaint/EditComplaint?complaintID=" + id,
        function success() {
        }
    );
}

function ReloadGrid() {
    jQuery('#GridTable').trigger('reloadGrid');

    ClosePopup();
}