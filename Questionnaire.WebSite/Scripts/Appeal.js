jQuery(document).ready(function () {
    initServiceWindow();
    getAppeal(0);
});

// 0 - все
// 1 - необработанные
// 2 - обработанные
// 3 - просроченные
function getAppeal(type) {
    if (type != -1) {
        $("#win_show .active").removeClass("active");
    }

    var titleGrid = "";

    if (type == 0) { $("#all").addClass("active"); $("#hdnType").val(0); titleGrid = "Все обращения"; }
    else if (type == 1) { $("#untreated").addClass("active"); $("#hdnType").val(1); titleGrid = "Необработанные"; }
    else if (type == 2) { $("#treated").addClass("active"); $("#hdnType").val(2); titleGrid = "Обработанные"; }
    else if (type == 3) { $("#overdue").addClass("active"); $("#hdnType").val(3); titleGrid = "Просроченные"; }

    if ($("#hdnType").val() == undefined ||
        $("#hdnType").val() == null ||
        $("#hdnType").val() == -1) {
        return;
    }

    $("#winContent").html("<div id='appealTable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");

    var widthGrid = $("#appealTable").width() - 12;

    jQuery('#GridTable').jqGrid({
        url: url + '/Appeal/LoadData?type=' + $("#hdnType").val()/* + '&docDateText=' + $("#DocDate").val()*/,
        datatype: "json",
        mtype: 'POST',
        caption: "Все обращения",
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Номер', 'Дата обращения', 'Название УК', 'Заявитель', 'Текст проблемы', 'Статус', ''],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'Numberdoc', index: 'Numberdoc', sortable: false, search: false, editable: false, width: 25 },
                    { name: 'DocDate', index: 'DocDate', align: 'center', sortable: false, search: true, searchoptions: { dataInit: datePick }, editable: false, width: 60 },
                    { name: 'NameUK', index: 'NameUK', sortable: false, search: true, editable: false, width: 150 },
                    { name: 'ApplicantName', index: 'Applicant.Name', sortable: false, search: false, editable: false, width: 110 },
                    { name: 'Description', index: 'Description', sortable: false, search: false, editable: false, width: 240 },
                    { name: 'AnswerStatusText', index: 'AnswerStatusText', sortable: false, search: false, editable: false, width: 40 },
                    { name: 'act', index: 'act', width: 15, sortable: false, search: false, width: 30 }
        ],
        pager: '#GridTableToolbar',
        rowNum: 10,
        height: 470,
        width: widthGrid,
        gridview: true,
        toolbar: [true, "top"],
        viewrecords: true,
        gridComplete: function () {
            var ids = jQuery("#GridTable").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var edit = "<input type='image' title='Редактировать' src='../../Control/Content/images/Show_icon.png' style='' onclick='ShowAppeal(" + cl + ")' />";
                jQuery("#GridTable").jqGrid('setRowData', ids[i], { act: edit });
            }

            var listTD = $("#GridTable").find("td[aria-describedby='GridTable_NameUK'], td[aria-describedby='GridTable_ApplicantName']");

            $.each(listTD, function (key, value) {
                $(listTD[key]).attr('style', 'white-space:normal;');
            });
        },
        onSelectRow: function (id) {
            ShowAppeal(id);
        }
    });

    jQuery("#GridTable").navGrid('#GridTableToolbar', { del: false, add: false, edit: false, search: false, refresh: false });
    jQuery("#GridTable").filterToolbar({ stringResult: true, searchOnEnter: true });

    //var tBar = $("#t_GridTable");
    //tBar.append("<input type='image' title='Обновить' src='../../Content/images/refresh.png' style='' onclick='ReloadGrid()' />");
}

datePick = function (elem) {
    jQuery(elem).datepicker({
        onSelect: function () {
            this.focus();
        }
    });
}

function ReloadGrid() {
    jQuery('#GridTable').trigger('reloadGrid');
}

function ShowAppeal(id) {
    $("#popup").show();

    $("#popupContent").html('');
    showLoading();
    
    $("#popupContent").load(
        url + "/Appeal/ShowInfoAppeal?appealID=" + id,
        function success() {
            hideLoading();
        }
    );
}

function ShowAppealInfo() {
    $("#answer").hide();
    $("#info").show();
}

function ShowAppealAnswer() {
    $("#info").hide();
    $("#answer").show();
}

function ClosePopup() {
    $("#popup").hide();
}