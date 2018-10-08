var main = function () {

    $('#txtNameSupervisior').focus(function () {

        $('#pm').hide();
        $('#txtNameSupervisior').focus();
    });
}

$(document).ready(main);