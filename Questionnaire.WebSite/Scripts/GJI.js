jQuery(document).ready(function () {
    initServiceWindow();
    definitionElements();
});

function definitionElements() {
    $("#MunisipalUnions").select2({ dropdownCss: "l660" });
    $("#DateStart").datepicker();
    $("#DateEnd").datepicker();
}

function GetGJIData() {
    $("#winContent").html("<div id='GJITable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");

    var dateStart = $("#DateStart").val();
    var dateEnd = $("#DateEnd").val();
    var munisipalUnions = $("#MunisipalUnions").val();
    
    var munisipalUnionsString = "";

    for (var i = 0; i < munisipalUnions.length; i++) {
        munisipalUnionsString += munisipalUnions[i] + "|";
    }

    $("#hdnDateStart").val(dateStart);
    $("#hdnDateEnd").val(dateEnd);
    $("#hdnMunisipalUnions").val(munisipalUnionsString);

    var widthGrid = $("#GJITable").width() - 12;

    jQuery('#GridTable').jqGrid({
        url: url + '/GJI/LoadData?dateStartText=' + dateStart + '&dateEndText=' + dateEnd + "&munisipalUnions=" + munisipalUnionsString,
        datatype: "json",
        mtype: 'POST',
        caption: "Обращения ГЖИ",
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Название', 'Количество нарушений', 'Кол неустр нарушений', 'Кол устр нарушений', 'Кол нарушений из план-графика'],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'Name', index: 'Contractor.Name', sortable: false, search: true, editable: false, width: 150 },
                    { name: 'CountViolation', index: 'CountViolation', sortable: false, search: false, editable: false, width: 40 },
                    { name: 'CountViolationNoElimination', index: 'CountViolationNoElimination', sortable: false, search: false, editable: false, width: 40 },
                    { name: 'CountViolationElimination', index: 'CountViolationElimination', sortable: false, search: false, editable: false, width: 40 },
                    { name: 'CountViolationSchedule', index: 'CountViolationSchedule', sortable: false, search: false, editable: false, width: 80 }
        ],
        pager: '#GridTableToolbar',
        rowNum: 20,
        height: 470,
        width: widthGrid,
        gridview: true,
        toolbar: [true, "top"],
        viewrecords: true,
        subGrid: true,
        subGridRowExpanded: function (subgrid_id, row_id) {
            var subgrid_table_id, pager_id;
            subgrid_table_id = subgrid_id + "_t";
            pager_id = "p_" + subgrid_table_id;
            $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");
            jQuery("#" + subgrid_table_id).jqGrid({
                url: url + '/GJI/LoadSubData?managmentOrganizationID=' + row_id + '&dateStartText=' + $("#hdnDateStart").val() + '&dateEndText=' + $("#hdnDateEnd").val() + "&munisipalUnions=" + $("#hdnMunisipalUnions").val(),
                datatype: "json",
                mtype: 'POST',
                jsonReader: {
                    repeatitems: false,
                    id: "ID"
                },
                colNames: ['ID', 'Название объекта', '', 'Количество нарушений', '', 'Кол неустр нарушений', '', 'Кол устр нарушений', '', 'Кол нарушений из план-графика'],
                colModel: [
                    { name: 'ID', index: 'ID', width: 15, editable: false, hidden: true, key: true },
                    { name: 'Name', index: 'Name', width: 350, editable: false, search: true, hidden: false },
                    { name: 'actGen', index: 'actGen', width: 15, sortable: false, search: false, width: 50 },
                    { name: 'CountViolation', index: 'CountViolation', editable: false, search: false, width: 240, hidden: false },
                    { name: 'actNoElimination', index: 'actNoElimination', width: 15, sortable: false, search: false, width: 50 },
                    { name: 'CountViolationNoElimination', index: 'CountViolationNoElimination', sortable: false, search: false, editable: false, width: 240 },
                    { name: 'actElimination', index: 'actElimination', width: 15, sortable: false, search: false, width: 50 },
                    { name: 'CountViolationElimination', index: 'CountViolationElimination', sortable: false, search: false, editable: false, width: 240 },
                    { name: 'actSchedule', index: 'actSchedule', width: 15, sortable: false, search: false, width: 50 },
                    { name: 'CountViolationSchedule', index: 'CountViolationSchedule', sortable: false, search: false, editable: false, width: 240 }
                ],
                rowNum: 7,
                height: 150,
                width: widthGrid - 50,
                gridComplete: function () {
                    var ids = jQuery("#" + subgrid_table_id).jqGrid('getDataIDs');
                    for (var i = 0; i < ids.length; i++) {
                        var cl = ids[i];
                        var editGen = "<input type='image' title='Нарушения' src='../../Control/Content/images/Show_icon.png' style='' onclick='ShowViolation(" + row_id + ", " + cl + ", 0)' />";
                        var editNoElimination = "<input type='image' title='Неустраненные нарушения' src='../../Control/Content/images/Show_icon.png' style='' onclick='ShowViolation(" + row_id + ", " + cl + ", 1)' />";
                        var editElimination = "<input type='image' title='Устраненные нарушения' src='../../Control/Content/images/Show_icon.png' style='' onclick='ShowViolation(" + row_id + ", " + cl + ", 2)' />";
                        var editSchedule = "<input type='image' title='Нарушения из план-графика' src='../../Control/Content/images/Show_icon.png' style='' onclick='ShowViolation(" + row_id + ", " + cl + ", 3)' />";

                        jQuery("#" + subgrid_table_id).jqGrid('setRowData', ids[i], { actGen: editGen });
                        jQuery("#" + subgrid_table_id).jqGrid('setRowData', ids[i], { actNoElimination: editNoElimination });
                        jQuery("#" + subgrid_table_id).jqGrid('setRowData', ids[i], { actElimination: editElimination });
                        jQuery("#" + subgrid_table_id).jqGrid('setRowData', ids[i], { actSchedule: editSchedule });
                    }
                },
                onCellSelect: function (rowid, iCol, cellcontent, e) {
                    if (iCol == 3) {
                        ShowViolation(row_id, rowid, 0);
                    }
                    else if (iCol == 5) {
                        ShowViolation(row_id, rowid, 1);
                    }
                    else if (iCol == 7) {
                        ShowViolation(row_id, rowid, 2);
                    }
                    else if (iCol == 9) {
                        ShowViolation(row_id, rowid, 3);
                    }
                }
            });

            jQuery("#" + subgrid_table_id).filterToolbar({ stringResult: true, searchOnEnter: true });
        }
    });

    jQuery("#GridTable").navGrid('#GridTableToolbar', { del: false, add: false, edit: false, search: false, refresh: false });
    jQuery("#GridTable").filterToolbar({ stringResult: true, searchOnEnter: true });

    //var tBar = $("#t_GridTable");
    //tBar.append("<input type='image' title='Обновить' src='../../Content/images/refresh.png' style='' onclick='ReloadGrid()' />");
}

function ShowViolation(managmentOrganizationID, realtyObjectID, type) {
    var _url = "";

    if (type == 0) { _url = "/GJI/ShowViolation?managmentOrganizationID=" + managmentOrganizationID + "&realtyObjectID=" + realtyObjectID + "&dateStartText=" + $("#hdnDateStart").val() + "&dateEndText=" + $("#hdnDateEnd").val() + "&munisipalUnions=" + $("#hdnMunisipalUnions").val() + "&type=0"; }
    else if (type == 1) { _url = "/GJI/ShowViolation?managmentOrganizationID=" + managmentOrganizationID + "&realtyObjectID=" + realtyObjectID + "&dateStartText=" + $("#hdnDateStart").val() + "&dateEndText=" + $("#hdnDateEnd").val() + "&munisipalUnions=" + $("#hdnMunisipalUnions").val() + "&type=1"; }
    else if (type == 2) { _url = "/GJI/ShowViolation?managmentOrganizationID=" + managmentOrganizationID + "&realtyObjectID=" + realtyObjectID + "&dateStartText=" + $("#hdnDateStart").val() + "&dateEndText=" + $("#hdnDateEnd").val() + "&munisipalUnions=" + $("#hdnMunisipalUnions").val() + "&type=2"; }
    else if (type == 3) { _url = "/GJI/ShowViolation?managmentOrganizationID=" + managmentOrganizationID + "&realtyObjectID=" + realtyObjectID + "&dateStartText=" + $("#hdnDateStart").val() + "&dateEndText=" + $("#hdnDateEnd").val() + "&munisipalUnions=" + $("#hdnMunisipalUnions").val() + "&type=3"; }

    $("#popup").show();

    $("#popupContent").html('');
    showLoading();

    $("#popupContent").load(
        url + _url,
        function success() {
            hideLoading();
        }
    );
}

function ReloadGrid() {
    jQuery('#GridTable').trigger('reloadGrid');
}

function ClosePopup() {
    $("#popup").hide();
}