jQuery(document).ready(function () {
    initServiceWindow();
});

var minusWidthGrid = 12;

function getQuestion(charterID, nameCharter) {
    
    $(".item_menu_adminka").removeClass("active");
    $(".item_menu_adminka").find('div.arrow').remove();
    $("#questions").find('li').addClass("active");
    $("#questions").find('li').append("<div class='arrow'></div>");
    
    var titleGrid = "Вопросы анкетирования: " + nameCharter;
    
    $("#areaContent").html("<div id='questionTable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");

    var widthGrid = $("#questionTable").width() - minusWidthGrid;

    jQuery('#GridTable').jqGrid({
        url: url + '/Admin/LoadDataQuestion?charterID=' + charterID,
        datatype: "json",
        mtype: 'POST',
        caption: titleGrid,
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Вопрос', 'Раздел', ''],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'TextQuestion', index: 'Value', sortable: false, search: false, editable: false, width: 100 },
                    { name: 'TypeQuestionName', index: 'TypeQuestionName', sortable: false, search: false, editable: false, width: 60 },
                    { name: 'act', index: 'act', width: 15, sortable: false, search: false, width: 30 }
        ],
        pager: '#GridTableToolbar',
        rowNum: 15,
        height: 470,
        width: widthGrid,
        gridview: true,
        toolbar: [true, "top"],
        viewrecords: true,
        gridComplete: function () {
            var ids = jQuery("#GridTable").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var edit = "<input type='image' title='Редактировать' src='../../.." + url + "/Content/images/Edit_icon.png' style='' onclick='EditQuestion(" + cl + ", " + charterID + ")' />";
                var del = "<input type='image' title='Удалить' src='../../.." + url + "/Content/images/Delete_icon.png' style='' onclick='DeleteQuestion(" + cl + ", " + charterID + ")' />";
                jQuery("#GridTable").jqGrid('setRowData', ids[i], { act: edit + del });
            }
        }
    });

    jQuery("#GridTable").navGrid('#GridTableToolbar', { del: false, add: false, edit: false, search: false, refresh: false });
    jQuery("#GridTable").filterToolbar({ stringResult: true, searchOnEnter: true });

    var tBar = $("#t_GridTable");
    tBar.append("<input type='image' title='Новый' src='../../.." + url + "/Content/images/Create_tb.png' style='' onclick='EditQuestion(0, " + charterID + ")' />");
    tBar.append("<input type='image' title='Обновить' src='../../.." + url + "/Content/images/refresh.png' style='' onclick='ReloadGrid()' />");
}

function EditQuestion(id, charterID) {
    $("#popup").show();

    $("#popupContent").html('');
    showLoading();

    $("#popupContent").load(
        url + "/Admin/EditQuestion?questionID=" + id + "&charterID=" + charterID,
        function success() {
            hideLoading();
        }
    );
}

function DeleteQuestion(id, charterID) {
    $.ajax({
        url: url + "/Admin/DeleteQuestion?questionID=" + id + "&charterID=" + charterID,
        success: function (data) {
            ReloadGrid();
        },
        error: function () {
            alert("error");
        },
    });
}

function AddAnswer() {
    var newAnswer = "";
    var itemAnswer = $("#listAnswers").find('div.answer:first')[0];

    $("#listAnswers").append($("<div>").append($(itemAnswer).clone()).html());

    itemAnswer = $("#listAnswers").find('div.answer:last')[0];

    $(itemAnswer).find('.idAnswer:first').val('0');
    $(itemAnswer).find('.textAnswer:first').val('');
    $(itemAnswer).find('.textAnswerAdditional1:first').val('');
    $(itemAnswer).find('.textAnswerAdditional2:first').val('');
    $(itemAnswer).find('.score:first').val('');
    $(itemAnswer).find('.numberSequence:first').val('');
    $(itemAnswer).find('.nextQuestion:first option:selected').removeAttr("selected");

    RecalculationAnswers();
}

function RemoveAnswer(obj) {
    $(obj).parents('.answer').remove();

    RecalculationAnswers();
}

function RecalculationAnswers() {
    var itemsAnswers = $('div.answer');

    itemsAnswers.each(function (key, elem) {
        $(elem).attr("data-index", key);

        $(elem).find('.idAnswer:first').attr('name', 'Answers[' + key + '].ID');
        $(elem).find('.idAnswer:first').attr('id', 'Answers_' + key + '__ID');

        $(elem).find('.textAnswer:first').attr('name', 'Answers[' + key + '].TextAnswer');
        $(elem).find('.textAnswer:first').attr('id', 'Answers_' + key + '__TextAnswer');

        $(elem).find('.textAnswerAdditional1:first').attr('name', 'Answers[' + key + '].TextAnswerAdditional1');
        $(elem).find('.textAnswerAdditional1:first').attr('id', 'Answers_' + key + '__TextAnswerAdditional1');

        $(elem).find('.textAnswerAdditional2:first').attr('name', 'Answers[' + key + '].TextAnswerAdditional2');
        $(elem).find('.textAnswerAdditional2:first').attr('id', 'Answers_' + key + '__TextAnswerAdditional2');

        $(elem).find('.score:first').attr('name', 'Answers[' + key + '].Score');
        $(elem).find('.score:first').attr('id', 'Answers_' + key + '__Score');

        $(elem).find('.numberSequence:first').attr('name', 'Answers[' + key + '].NumberSequence');
        $(elem).find('.numberSequence:first').attr('id', 'Answers_' + key + '__NumberSequence');

        $(elem).find('.nextQuestion:first').attr('name', 'Answers[' + key + '].NextQuestionID');
        $(elem).find('.nextQuestion:first').attr('id', 'Answers_' + key + '__NextQuestionID');

        if (itemsAnswers.length == 1) {
            $(elem).find("div.delete_answer:first").hide();
        }
        else {
            $(elem).find("div.delete_answer:first").show();
        }
    });
}

function SetTypeQuestion(type) {
    if (type == 1 ||
        type == 4) {
        $("#listAnswers").hide();
        $(".add_button").hide();
    }
    else {
        $("#listAnswers").show();
        $(".add_button").show();
    }
}

function getUser() {

    $(".item_menu_adminka").removeClass("active");
    $(".item_menu_adminka").find('div.arrow').remove();
    $("#users").find('li').addClass("active");
    $("#users").find('li').append("<div class='arrow'></div>");

    var titleGrid = "Пользователи";

    $("#areaContent").html("<div id='userTable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");

    var widthGrid = $("#userTable").width() - minusWidthGrid;

    jQuery('#GridTable').jqGrid({
        url: url + '/Admin/LoadDataUser',
        datatype: "json",
        mtype: 'POST',
        caption: titleGrid,
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Имя', 'Отчество', 'Фамилия', 'E-mail', 'Роль', ''],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'FirstName', index: 'FirstName', sortable: false, search: false, editable: false, width: 60 },
                    { name: 'SecondName', index: 'SecondName', sortable: false, search: false, editable: false, width: 60 },
                    { name: 'LastName', index: 'LastName', sortable: false, search: false, editable: false, width: 60 },
                    { name: 'Email', index: 'Email', sortable: false, search: false, editable: false, width: 60 },
                    { name: 'NameRole', index: 'NameRole', sortable: false, search: false, editable: false, width: 60 },
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
                var edit = "<input type='image' title='Редактировать' src='../../.." + url + "/Content/images/Edit_icon.png' style='' onclick='EditUser(" + cl + ")' />";
                jQuery("#GridTable").jqGrid('setRowData', ids[i], { act: edit });
            }
        }
    });

    jQuery("#GridTable").navGrid('#GridTableToolbar', { del: false, add: false, edit: false, search: false, refresh: false });
    jQuery("#GridTable").filterToolbar({ stringResult: true, searchOnEnter: true });

    var tBar = $("#t_GridTable");
    tBar.append("<input type='image' title='Новый' src='../../.." + url + "/Content/images/Create_tb.png' style='' onclick='EditUser(0)' />");
    tBar.append("<input type='image' title='Обновить' src='../../.." + url + "/Content/images/refresh.png' style='' onclick='ReloadGrid()' />");
}

function EditUser(id) {
    $("#popup").show();

    $("#popupContent").html('');
    showLoading();

    $("#popupContent").load(
        url + "/Admin/EditUser?userID=" + id,
        function success() {
            hideLoading();
        }
    );
}

function getCategory() {

    $(".item_menu_adminka").removeClass("active");
    $(".item_menu_adminka").find('div.arrow').remove();
    $("#categories").find('li').addClass("active");
    $("#categories").find('li').append("<div class='arrow'></div>");

    var titleGrid = "Категории";

    $("#areaContent").html("<div id='categoryTable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");

    var widthGrid = $("#categoryTable").width() - 12;

    jQuery('#GridTable').jqGrid({
        url: url + '/Admin/LoadDataCategory',
        datatype: "json",
        mtype: 'POST',
        caption: titleGrid,
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Название', ''],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'Name', index: 'Name', sortable: false, search: false, editable: false, width: 60 },
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

//функция изменения каталогов
function EditCategory(id) {
    $("#popup").show();

    $("#popupContent").html('');
    showLoading();

    $("#popupContent").load(
        url + "/Admin/EditCategory?categoryID=" + id,
        function success() {
            hideLoading();
        }
    );
}

//функция удаления каталогов
function DeleteCategory(id) {
    $.ajax({
        url: url + "/Admin/DeleteCategory?categoryID=" + id,
        success: function (data)
        {
            ReloadGrid();
        },
        error: function () {
            alert("error");
        },
    });
}

function getIPAddress() {

    $(".item_menu_adminka").removeClass("active");
    $(".item_menu_adminka").find('div.arrow').remove();
    $("#ipAddress").find('li').addClass("active");
    $("#ipAddress").find('li').append("<div class='arrow'></div>");

    var titleGrid = "IP организаций";

    $("#areaContent").html("<div id='ipAddressTable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");

    var widthGrid = $("#ipAddressTable").width() - 12;

    jQuery('#GridTable').jqGrid({
        url: url + '/Admin/LoadDataIPAddress',
        datatype: "json",
        mtype: 'POST',
        caption: titleGrid,
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Название организации', 'IP адрес', ''],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'NameOrganization', index: 'NameOrganization', sortable: false, search: false, editable: false, width: 60 },
                    { name: 'IPAddress', index: 'IPAddress', sortable: false, search: false, editable: false, width: 60 },
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
                var edit = "<input type='image' title='Редактировать' src='../../.." + url + "/Content/images/Edit_icon.png' style='' onclick='EditIPAddress(" + cl + ")' />";
                jQuery("#GridTable").jqGrid('setRowData', ids[i], { act: edit });
            }
        }
    });

    jQuery("#GridTable").navGrid('#GridTableToolbar', { del: false, add: false, edit: false, search: false, refresh: false });
    jQuery("#GridTable").filterToolbar({ stringResult: true, searchOnEnter: true });

    var tBar = $("#t_GridTable");
    tBar.append("<input type='image' title='Новый' src='../../.." + url + "/Content/images/Create_tb.png' style='' onclick='EditIPAddress(0)' />");
    tBar.append("<input type='image' title='Обновить' src='../../.." + url + "/Content/images/refresh.png' style='' onclick='ReloadGrid()' />");
}

//функция изменения ip адресов
function EditIPAddress(id) {
    $("#popup").show();

    $("#popupContent").html('');
    showLoading();

    $("#popupContent").load(
        url + "/Admin/EditIPAddress?ipAddressID=" + id,
        function success() {
            hideLoading();
        }
    );
}

function getRole() {

    $(".item_menu_adminka").removeClass("active");
    $(".item_menu_adminka").find('div.arrow').remove();
    $("#role").find('li').addClass("active");
    $("#role").find('li').append("<div class='arrow'></div>");

    var titleGrid = "Роли и разрешения";

    $("#areaContent").html("<div id='roleTable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");

    var widthGrid = $("#roleTable").width() - 12;

    jQuery('#GridTable').jqGrid({
        url: url + '/Admin/LoadDataRole',
        datatype: "json",
        mtype: 'POST',
        caption: titleGrid,
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Название роли', ''],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'Name', index: 'Name', sortable: false, search: false, editable: false, width: 60 },
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
                var edit = "<input type='image' title='Редактировать' src='../../.." + url + "/Content/images/Edit_icon.png' style='' onclick='EditRole(" + cl + ")' />";
                jQuery("#GridTable").jqGrid('setRowData', ids[i], { act: edit });
            }
        }
    });

    jQuery("#GridTable").navGrid('#GridTableToolbar', { del: false, add: false, edit: false, search: false, refresh: false });
    jQuery("#GridTable").filterToolbar({ stringResult: true, searchOnEnter: true });

    var tBar = $("#t_GridTable");
    tBar.append("<input type='image' title='Новый' src='../../.." + url + "/Content/images/Create_tb.png' style='' onclick='EditRole(0)' />");
    tBar.append("<input type='image' title='Обновить' src='../../.." + url + "/Content/images/refresh.png' style='' onclick='ReloadGrid()' />");
}

//функция изменения ролей
function EditRole(id) {
    $("#popup").show();

    $("#popupContent").html('');
    showLoading();

    $("#popupContent").load(
        url + "/Admin/EditRole?roleID=" + id,
        function success() {
            hideLoading();
        }
    );
}

function ReloadGrid() {
    jQuery('#GridTable').trigger('reloadGrid');

    activeItemID = $("li.active").parent().attr("id");

    $(".menu_adminka").load(
        url + "/Admin/MenuItems",
        function success() {
            $("#" + activeItemID).children('li').addClass('active');
        }
    );

    $("header").load(
        url + "/Home/CreateMenuItems",
        function success() {
        }
    );
}

function getMOOrganization() {

    $(".item_menu_adminka").removeClass("active");
    $(".item_menu_adminka").find('div.arrow').remove();
    $("#moOrganizations").find('li').addClass("active");
    $("#moOrganizations").find('li').append("<div class='arrow'></div>");

    var titleGrid = "Медицинские организации";

    $("#areaContent").html("<div id='moOrganizationTable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");

    var widthGrid = $("#moOrganizationTable").width() - 12;

    jQuery('#GridTable').jqGrid({
        url: url + '/Admin/LoadDataMOOrganization',
        datatype: "json",
        mtype: 'POST',
        caption: titleGrid,
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Код', 'Название', ''],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'Code', index: 'Code', sortable: false, search: false, editable: false, width: 60 },
                    { name: 'Name', index: 'Name', sortable: false, search: false, editable: false, width: 60 },
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
                var edit = "<input type='image' title='Редактировать' src='../../.." + url + "/Content/images/Edit_icon.png' style='' onclick='EditMOOrganization(" + cl + ")' />";
                jQuery("#GridTable").jqGrid('setRowData', ids[i], { act: edit });
            }
        }
    });

    jQuery("#GridTable").navGrid('#GridTableToolbar', { del: false, add: false, edit: false, search: false, refresh: false });
    jQuery("#GridTable").filterToolbar({ stringResult: true, searchOnEnter: true });

    var tBar = $("#t_GridTable");
    tBar.append("<input type='image' title='Новый' src='../../.." + url + "/Content/images/Create_tb.png' style='' onclick='EditMOOrganization(0)' />");
    tBar.append("<input type='image' title='Обновить' src='../../.." + url + "/Content/images/refresh.png' style='' onclick='ReloadGrid()' />");
}

//функция изменения мо организации
function EditMOOrganization(id) {
    $("#popup").show();

    $("#popupContent").html('');
    showLoading();

    $("#popupContent").load(
        url + "/Admin/EditMOOrganization?moOrganizationID=" + id,
        function success() {
            hideLoading();
        }
    );
}

function getSMOOrganization() {

    $(".item_menu_adminka").removeClass("active");
    $(".item_menu_adminka").find('div.arrow').remove();
    $("#smoOrganizations").find('li').addClass("active");
    $("#smoOrganizations").find('li').append("<div class='arrow'></div>");

    var titleGrid = "Страховые медицинские организации";

    $("#areaContent").html("<div id='smoOrganizationTable'><table id='GridTable'></table><div id='GridTableToolbar'></div></div>");

    var widthGrid = $("#smoOrganizationTable").width() - 12;

    jQuery('#GridTable').jqGrid({
        url: url + '/Admin/LoadDataSMOOrganization',
        datatype: "json",
        mtype: 'POST',
        caption: titleGrid,
        multiselect: false,
        jsonReader: {
            repeatitems: false,
            id: "ID"
        },
        colNames: ['ID', 'Код', 'Название', ''],
        colModel: [
                    { name: 'ID', index: 'ID', editable: false, hidden: true },
                    { name: 'Code', index: 'Code', sortable: false, search: false, editable: false, width: 60 },
                    { name: 'Name', index: 'Name', sortable: false, search: false, editable: false, width: 60 },
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
                var edit = "<input type='image' title='Редактировать' src='../../.." + url + "/Content/images/Edit_icon.png' style='' onclick='EditSMOOrganization(" + cl + ")' />";
                jQuery("#GridTable").jqGrid('setRowData', ids[i], { act: edit });
            }
        }
    });

    jQuery("#GridTable").navGrid('#GridTableToolbar', { del: false, add: false, edit: false, search: false, refresh: false });
    jQuery("#GridTable").filterToolbar({ stringResult: true, searchOnEnter: true });

    var tBar = $("#t_GridTable");
    tBar.append("<input type='image' title='Новый' src='../../.." + url + "/Content/images/Create_tb.png' style='' onclick='EditSMOOrganization(0)' />");
    tBar.append("<input type='image' title='Обновить' src='../../.." + url + "/Content/images/refresh.png' style='' onclick='ReloadGrid()' />");
}

//функция изменения смо организации
function EditSMOOrganization(id) {
    $("#popup").show();

    $("#popupContent").html('');
    showLoading();

    $("#popupContent").load(
        url + "/Admin/EditSMOOrganization?smoOrganizationID=" + id,
        function success() {
            hideLoading();
        }
    );
}

// Получение всех МО для ответов
function GetAllMO(charterID) {
    showLoading();
    $("#listAnswers").load(
        url + "/Admin/GetAnswersFromLkp?type=0&charterID=" + charterID,
        function success() {
            hideLoading();
        }
    );
}

// Получение всех СМО для ответов
function GetAllSMO(charterID) {
    showLoading();
    $("#listAnswers").load(
        url + "/Admin/GetAnswersFromLkp?type=1&charterID=" + charterID,
        function success() {
            hideLoading();
        }
    );
}

var activeItemID = "";