jQuery(document).ready(function () {
    initServiceWindow();
});

function getEnumeration(charterID) {
    $(".item_menu_adminka").removeClass("active");
    $(".item_menu_adminka").find('div.arrow').remove();
    $("#charter_" + charterID).find('li').addClass("active");
    $("#charter_" + charterID).find('li').append("<div class='arrow'></div>");

    $("#areaContent").html('');
    showLoading();

    $("#areaContent").load(
        url + "/Statistics/Enumeration?charterID=" + charterID,
        function success() {
            hideLoading();
        }
    );
}

function getJournal(charterID, numberPage) {
    showLoading();

    var answerIDs = [];
    var chooseAnswers = $(".chooseAnswerID");
    $.each(chooseAnswers, function (ind, item)
    {
        answerIDs.push($(item).val());
    });

    if (answerIDs.length == 0) {
        $(".item_menu_adminka").removeClass("active");
        $(".item_menu_adminka").find('div.arrow').remove();
        $("#journal_" + charterID).find('li').addClass("active");
        $("#journal_" + charterID).find('li').append("<div class='arrow'></div>");
    }

    var fltDate = $("#fltDateVote").val();
    var fltNameOrganization = $("#fltNameOrganization").val();
    var fltNumberVoting = $("#fltVotingID").val();

    $("#areaContent").html('');

    $("#areaContent").load(
        url + "/Statistics/Journal",
        {
            'charterID': charterID,
            'numberPage': numberPage,
            'answers': answerIDs.join('|'),
            'fltDate': fltDate,
            'fltNameOrganization': fltNameOrganization,
            'fltNumberVoting': fltNumberVoting
        },
        function success() {
            hideLoading();
        }
    );
}

function getJournalEmpty(charterID, numberPage) {
    $(".item_menu_adminka").removeClass("active");
    $(".item_menu_adminka").find('div.arrow').remove();
    $("#journal_" + charterID).find('li').addClass("active");
    $("#journal_" + charterID).find('li').append("<div class='arrow'></div>");

    $("#areaContent").html('');

    $("#areaContent").load(
        url + "/Statistics/Journal",
        {
            'charterID': charterID,
            'numberPage': numberPage,
            'answers': '',
            'fltDate': '',
            'fltNameOrganization': '',
            'fltNumberVoting': ''
        },
        function success() {
            hideLoading();
        }
    );
}

function getJournalEvent(charterID, numberPage, event) {
    if (event.keyCode == 13) {
        getJournal(charterID, numberPage);
    }
}

function getJournalMarkCommon(charterID) {
    showLoading();

    $("#areaContent").html('');

    $("#areaContent").load(
        url + "/Statistics/JournalMarkCommon",
        {
            'charterID': charterID
        },
        function success() {
            hideLoading();
        }
    );
}

function getJournalMarkMO(charterID) {
    showLoading();

    $("#areaContent").html('');

    $("#areaContent").load(
        url + "/Statistics/JournalMarkMO",
        {
            'charterID': charterID
        },
        function success() {
            hideLoading();
        }
    );
}

function getJournalMarkSMO(charterID) {
    showLoading();

    $("#areaContent").html('');

    $("#areaContent").load(
        url + "/Statistics/JournalMarkSMO",
        {
            'charterID': charterID
        },
        function success() {
            hideLoading();
        }
    );
}

function ReplaceView() {
    if ($(".intValue").attr("style") == "display: none;") {
        $(".intValue").show();
        $(".txtValue").hide();
    }
    else {
        $(".intValue").hide();
        $(".txtValue").show();
    }
}

function OpenCloseMO(obj) {
    var hideMOAttr = $(obj).parents('.moPart').attr('hide');

    if (hideMOAttr * 1 == 0) {
        var numMOAttr = $(obj).parents('.moPart').attr('num');
        var numSMOAttr = $(obj).parents('.moPart').attr('numSmo');
        if (numSMOAttr != undefined && numSMOAttr != null) {
            $("tr[numMo='" + numMOAttr + "'][numSmo='" + numSMOAttr + "']").hide();
        }
        else {
            $("tr[numMo='" + numMOAttr + "']").hide();
        }
        $(obj).parents('.moPart').attr('hide', 1);
    }
    else {
        var numMOAttr = $(obj).parents('.moPart').attr('num');
        var numSMOAttr = $(obj).parents('.moPart').attr('numSmo');
        if (numSMOAttr != undefined && numSMOAttr != null) {
            $("tr[numMo='" + numMOAttr + "'][numSmo='" + numSMOAttr + "']").show();
        }
        else {
            $("tr[numMo='" + numMOAttr + "']").show();
        }
        $(obj).parents('.moPart').attr('hide', 0);
    }
}

function OpenCloseSMO(obj) {
    var hideAttr = $(obj).parents('.smoPart').attr('hide');

    if (hideAttr * 1 == 0) {
        var numAttr = $(obj).parents('.smoPart').attr('num');
        var vb = $("tr[numSmo='" + numAttr + "']").hide();
        $(obj).parents('.smoPart').attr('hide', 1);
    }
    else {
        var numAttr = $(obj).parents('.smoPart').attr('num');
        var vb = $("tr[numSmo='" + numAttr + "']").show();
        $(obj).parents('.smoPart').attr('hide', 0);

        var mos = $("tr.moPart[numSmo='" + numAttr + "']");

        $.each(mos, function (ind, itemObj) {
            var hideMOAttr = $(itemObj).attr('hide');

            if (hideMOAttr * 1 == 0) {
                var numMOAttr = $(itemObj).attr('num');
                var vbMO = $("tr[numMo='" + numMOAttr + "'][numSmo='" + numAttr + "']").show();
                $(itemObj).parents('.moPart').attr('hide', 1);
            }
            else {
                var numMOAttr = $(itemObj).attr('num');
                var vbMO = $("tr[numMo='" + numMOAttr + "'][numSmo='" + numAttr + "']").hide();
                $(itemObj).parents('.moPart').attr('hide', 0);
            }
        });
    }
}

function openSubMenuWindow(obj) {
    var idElem = "#" + $(obj).attr('id') + "_win";
    var t = $(idElem);

    $(idElem).show();
}

function closeSubMenuWindow(obj) {
    var idElem = "#" + $(obj).attr('id') + "_win";
    var t = $(idElem);

    $(idElem).attr("style", "");
}