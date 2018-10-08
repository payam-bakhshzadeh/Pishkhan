var main = function () {

    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $('#loading').hide();
    $('#btnModal').hide();

    $('.btnDeleteWage').click(function () {

        var id = this.id;
        var rowId = $(this).attr('parentId')

        var result = confirm('آیا برای پاک کردن مطمئن هستید؟');

        if (result) {

            $('#loading').show();

            $.ajax({

                url: '/Financial/DeleteWage',
                data: { id: id },
                type: 'POST',
                dataType: 'JSON',
                async: true,
                cache: false,
                success: function (result) {

                    if (result.pm == false) {

                        $('#divPm').text('خطا در حذف اطلاعات. لطفا مجددا تلاش نمایید');
                        $('#loading').hide();
                    }

                    else {
                        $('#divPm').text(result.pm);
                        $('#' + rowId).hide();
                        $('#btnModal').click();
                        $('#loading').hide();
                    }
                },
                error: function (result) {

                    $('#divPm').text('خطا در حذف اطلاعات. لطفا مجددا تلاش نمایید');
                    $('#loading').hide();

                }

            });

        }
    });


}

$(document).ready(main);