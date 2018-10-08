var main = function () {

    $('#loading').hide();


    $('.excelFileName').click(function () {

        var name = this.id;
        var rowid = $(this).attr('rowid');

        var result = confirm('Are you sure to delete ' + name + '?');

        if (result) {

            $('#loading').show();

            $.ajax({

                url: '/Financial/DeleteExcelFile',
                data: { name: name },
                type: 'POST',
                dataType: 'JSON',
                async: true,
                cache: false,
                success: function (result) {

                    if (result.pm == 'ok') {
                        $('#' + rowid).hide();
                        $('#loading').hide();
                    }

                    else {
                        alert('خطا در سیستم.')
                        $('#loading').hide();
                    }
                },
                error: function (result) {
                    alert('خطا در سیستم.')
                    $('#loading').hide();
                }

            });
        }










    });
}

$(document).ready(main);