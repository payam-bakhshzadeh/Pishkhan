var main = function () {

    $('#firstHeader').click(function () {
       // $('#firstBody').slideToggle();

        if ($('#firstBody').is(':hidden')) {

            $('#firstBody').show();
            $('#firstPanel').removeClass('hidden-print');
        }

        else {
            $('#firstBody').hide();
            $('#firstPanel').addClass('hidden-print');
        }
    });

    $('#doubleHeader').click(function () {
        if ($('#doubleBody').is(':hidden')) {

            $('#doubleBody').show();
            $('#doublePanel').removeClass('hidden-print');
        }

        else {
            $('#doubleBody').hide();
            $('#doublePanel').addClass('hidden-print');
        }
    });

    $('#noPriceHeader').click(function () {

        if ($('#noPriceBody').is(':hidden')) {

            $('#noPriceBody').show();
            $('#noPricePanel').removeClass('hidden-print');
        }

        else {
            $('#noPriceBody').hide();
            $('#noPricePanel').addClass('hidden-print');
        }
    });
}

$(document).ready(main);