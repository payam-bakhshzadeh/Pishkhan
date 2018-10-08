var main = function () {

    //Get the user info
    $('.edit').click(function () {
        $('#content').hide();
        $('#loading').show();
        var id = this.id;

        $.ajax({

            url: '/Users/GetPishkhanByIdAsync',
            data: { Id: id },
            type: 'POST',
            dataType: 'JSON',
            async: true,
            cache: false,
            success: function (result) {
                $('#content').show();
                $('#loading').hide();

                $('#pishkhanId').val(result.id);
                $('#pishkhanName').val(result.name);
                $('#pishkhanCode').val(result.pishkhan_Code);
            },
            error: function (result) {
                alert('امکان واکشی اطلاعات از سرور وجود ندارد');
            }

        });
    });



    //update the user info
    $('#btnUpdatePressed').click(function () {

        $('#loading').show();

        var ID = $('#pishkhanId').val();
        var Name = $('#pishkhanName').val();
        var Pishkhan_Code = $('#pishkhanCode').val();

        $.ajax({

            url: '/Users/UpdatePishkhanInfoById',
            data: { ID: ID, Name: Name, Pishkhan_Code: Pishkhan_Code },
            type: 'POST',
            dataType: 'JSON',
            async: true,
            cache: false,
            success: function (result) {
                $('#loading').hide();

                if (result.pm == 'ok') {

                    location.reload();
                }
                else {
                    alert(result.pm);
                }


            },
            error: function (result) {
                alert(result.pm);
            }

        });
    });   



};

$(document).ready(main);