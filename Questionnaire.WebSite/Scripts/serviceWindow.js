function initServiceWindow() {
    $('#serviceWindow').dialog
    ({
        autoOpen: false,
        width: 260,
        height: 110,
        resizable: false,
        position: ['right', 'bottom']
    });
};

function serviceWindowOpen(message, type) {
    if (type == 0) {
        if ($('#serviceWindow').hasClass('serviceWindowDefault')) {
            $('#serviceWindow').removeClass('serviceWindowDefault');
        }
        $('#serviceWindow').addClass('serviceWindowSuccess');
        $('#serviceWindow').dialog('close');
    }
    else if (type == 1) {
        if ($('#serviceWindow').hasClass('serviceWindowDefault')) {
            $('#serviceWindow').removeClass('serviceWindowDefault');
        }
        $('#serviceWindow').addClass('serviceWindowError');
        $('#serviceWindow').dialog('close');
    }
    else {
        if (!$('#serviceWindow').hasClass('serviceWindowDefault')) {
            $('#serviceWindow').addClass('serviceWindowDefault');
        }
    }
    $('#serviceWindowMessage').empty();
    $('#serviceWindowMessage').append(message);
    $('#serviceWindow').dialog('open');
    
    if (type == 0 || type == 1) {
        $('#serviceWindow').parent().fadeOut(4000, function () {
            $('#serviceWindow').dialog('close');
            if ($('#serviceWindow').hasClass('serviceWindowError')) {
                $('#serviceWindow').removeClass('serviceWindowError');
            }
            if ($('#serviceWindow').hasClass('serviceWindowSuccess')) {
                $('#serviceWindow').removeClass('serviceWindowSuccess');
            }
        });
    }
};