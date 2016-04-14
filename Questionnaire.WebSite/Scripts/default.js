var url = "";

function showLoading() {
    serviceWindowOpen("Идет загрузка...", 3);
}

function hideLoading() {
    redirectLogin();
    $('#serviceWindow').dialog('close');
}

function focusStyleInput() {
    $("input[type='text']").bind('focusin', function () {
        $(this).attr("style", "background-color: #FFFF99; border-color: #993300");
    });

    $("input[type='text']").bind('focusout', function () {
        $("input[type='text']").attr("style", "");
    });

    $("select").bind('focusin', function () {
        $(this).attr("style", "background-color: #FFFF99; border-color: #993300");
    });

    $("select").bind('focusout', function () {
        $(this).attr("style", "");
    });
}

function redirectLogin() {
    var val = $("#hidLoginPage").val();
    if (val == "login") {
        debugger;
        location.href = url + "/Account/LogOn";
        debugger;
    }
}

function setAjaxForm(objContent) {
    $("#ajaxForm").ajaxForm({
        //replaceTarget: false,
        //target: '#winContent',
        success: function (data) {
            hideLoading();

            $("#" + objContent).html(data);

            var it = $("#IsNew").val();
            if (it == "True") {
                serviceWindowOpen("Успешное сохранение", 0);
                ClosePopup();
                ReloadGrid();
            }
            else {
            }
        },
        error: function () {
            hideLoading();
            serviceWindowOpen("Сохранение не удалось. Попробуйте еще раз.", 1);
            enclick();
        }
    });
}

function setAjaxSubmit(submitUrl, objContent) {
    $("#ajaxForm").ajaxSubmit({
        //replaceTarget: false,
        //target: '#winContent',
        url: submitUrl,
        success: function (data) {
            hideLoading();

            $("#" + objContent).html(data);

            var it = $("#IsNew").val();
            if (it == "True") {
                serviceWindowOpen("Успешное сохранение", 0);
                ClosePopup();
            }
            else {
            }
        },
        error: function () {
            hideLoading();
            serviceWindowOpen("Сохранение не удалось", 1);
            enclick();
        }
    });
}

function ClosePopup() {
    $("#popup").hide();
    $("#titlePopup").text('');
}

function ChangeIsBlind() {
    $.ajax({
        url: url + "/Account/ChangeIsBlind",
        success: function (data) {
            location.reload();
        },
        error: function () {
            alert("error");
        },
    });
}

function hideDaysFromCalendar() {
    var thisCalendar = $(this);
    $('.ui-datepicker-calendar').hide();

    var t = $('button[data-handler="hide"]')[0];
    $(t).html('Выбрать');
}

function findMOOrganization() {
    $.ajax({
        url: url + "/Home/FindMOOrganization?text=" + $("#tbFindMOOrganization").val(),
        success: function (data) {
            var strHtml = "";
            $.each(data, function (index, item) {
                strHtml += "<option value='" + item.ID + "'>" + item.Name + "</option>";
            });

            $("#moOrganizations").html(strHtml);
        },
        error: function () {
            alert("error");
        },
    });
}

function findSMOOrganization() {
    $.ajax({
        url: url + "/Home/FindSMOOrganization?text=" + $("#tbFindSMOOrganization").val(),
        success: function (data) {
            var strHtml = "";
            $.each(data, function (index, item) {
                strHtml += "<option value='" + item.ID + "'>" + item.Name + "</option>";
            });

            $("#smoOrganizations").html(strHtml);
        },
        error: function () {
            alert("error");
        },
    });
}

function beginQuestionnaire(type) {
    $("#hdnTypeTransition").val(type);
    $("#mainForm").submit();
}

function alignmentAnswer() {
    var elems = $(".options_answer");

    $.each(elems, function (ind, item) {
        var daugElems = $(item).children();

        var maxWidth = 0;

        $.each(daugElems, function (indDaug, itemDaug) {
            var widthElem = $(itemDaug).width();
            if (widthElem > maxWidth) {
                if (widthElem <= 590) {
                    maxWidth = widthElem;
                }
                else {
                    maxWidth = 590;
                }
            }
        });

        $(item).attr("style", "padding-left: 0px; margin: 0 auto; width: " + (maxWidth * 1 + 10) + "px;");
    });
}