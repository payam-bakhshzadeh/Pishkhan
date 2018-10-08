var main = function () {

    //Get the user info
    $('.edit').click(function () {
        $('#content').hide();
        $('#loading').show();
        var id = this.id;

        $.ajax({

            url: '/Users/GetSupervisiorByIdAsync',
            data: { Id: id },
            type: 'POST',
            dataType: 'JSON',
            async: true,
            cache: false,
            success: function (result) {
                $('#content').show();
                $('#loading').hide();

                $('#supervisiorId').val(result.id);
                $('#supervisiorName').val(result.name);
            },
            error: function (result) {
                alert('امکان واکشی اطلاعات از سرور وجود ندارد');
            }

        });
    });

    //update the user info
    $('#btnUpdatePressed').click(function () {

        $('#loading').show();

        var ID = $('#supervisiorId').val();
        var Name = $('#supervisiorName').val();

        $.ajax({

            url: '/Users/UpdateSupervisiorInfoById',
            data: { ID: ID, Name: Name},
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
}

$(document).ready(main);