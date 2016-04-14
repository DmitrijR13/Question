jQuery(document).ready(function () {
    initServiceWindow();
    startQuestionnaire(0);
});

function startQuestionnaire(questionID) {
    var charterID = $("#hdnCharter").val();
    var votingID = $("#hdnVoting").val();

    showLoading();
    $("#winContent").load(
        url + "/Questionnaire/NextQuestion?questionID=" + questionID + "&charterID=" + charterID + "&votingID=" + votingID + "&isFirstQuery=1",
        function success() {
            hideLoading();
        }
    );
}

function getPreviewQuestion() {
    disclick();

    var questionID = $("#hdnQuestion").val();
    var charterID = $("#hdnCharter").val();
    var votingID = $("#hdnVoting").val();

    showLoading();
    $("#winContent").load(
        url + "/Questionnaire/PrevQuestion?questionID=" + questionID + "&charterID=" + charterID + "&votingID=" + votingID,
        function success() {
            enclick();
            alignmentAnswer();
            hideLoading();
        }
    );
}

function enclick() {
    $("#back_quation").parent().show();
    $("#next_quation").parent().show();
}

function disclick() {
    $("#back_quation").parent().hide();
    $("#next_quation").parent().hide();
    $("#none_next_quation").parent().hide();
}

function findAnswers() {
    $.ajax({
        url: url + "/Questionnaire/FindDataList?questionID=" + $("#hdnQuestion").val() + "&text=" + $("#tbFind").val(),
        success: function (data) {
            var strHtml = "";
            $.each(data, function (index, item) {
                strHtml += "<option value='" + item.ID + "'>" + item.TextAnswer + "</option>";
            });

            $("#answers").html(strHtml);
        },
        error: function () {
            alert("error");
        },
    });
}

function clearRadio(obj) {
    var radios = $(obj).parents(".options_answer").find("input[type='radio']");

    $.each(radios, function (ind, item) {
        $(item).removeAttr('checked');
    });
}

function clearText(obj) {
    var t = $(obj).parents(".options_answer").find(".answerMoreWidthBottom");
    t.find("input[type='text']").val('');
}