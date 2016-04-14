jQuery(document).ready(function () {
    initServiceWindow();
    startQuestionnaire(0);
});

function startQuestionnaire(questionID) {
    var charterID = $("#hdnCharter").val();
    var votingID = $("#hdnVoting").val();

    showLoading();
    $("#winContent").load(
        url + "/QuestionnaireFull/Question?charterID=" + charterID + "&votingID=" + votingID,
        function success() {
            alignmentAnswer();
            hideLoading();
        }
    );
}

function findAnswers(obj) {
    $.ajax({
        url: url + "/Questionnaire/FindDataList?questionID=" + $(obj).attr('questionID') + "&text=" + $(obj).val(),
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

function enclick() {
    $(".save_button").parent().show();
}

function disclick() {
    $(".save_button").parent().hide();
}

function clearRadio(obj) {
    var radios = $(obj).parents(".options_answer").find("input[type='radio']");

    $.each(radios, function (ind, item) {
        $(item).removeAttr('checked');
    });
}

function clearText(obj) {
    var t = $(obj).parents(".options_answer").find(".fullAnswerTextBottom");
    t.find("input[type='text']").val('');
}