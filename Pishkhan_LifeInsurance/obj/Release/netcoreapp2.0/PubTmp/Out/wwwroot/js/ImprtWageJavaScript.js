var main = function () {

    $('#btnModal').hide();

    //btn submit click
    $('#btnSubmit').click(function (e) {

        e.preventDefault();
        $('#loading').addClass('progress-bar-striped active');
        $('#pm').text('');

        //find dropdown list value
        var ddlAgent = $('#ddlAgent').find(':selected').val();
        var ddlYear = $('#ddlYear').find(':selected').val();
        var ddlMonth = $('#ddlMonth').find(':selected').val();

        //find radiobutton selected item value
        var ExcelFiles = $('input[name=ExcelFiles]:checked').val(); 


        //send info to server
        $.ajax({

            url: '/Financial/ImportWage',
            data: { ddlAgent: ddlAgent, ddlYear: ddlYear, ddlMonth: ddlMonth, ExcelFiles: ExcelFiles },
            type: 'POST',
            dataType: 'JSON',
            async: true,
            cache: false,
            success: function (result) {

                //خطا در هنگا انجام عملیات
                if (result.pm != null) {

                    $('#pm').text(result.pm)
                    $('#loading').removeClass('progress-bar-striped active');
                }

                //عملیات به درستی انجام شذ
                else {
                    $('#modalTotalSave').text(result.successCountSaveDatabase);
                    $('#modalTotalExcel').text(result.totalExcelRow);
                    $('#modalExcelError').text(result.errorCountExcel);
                    $('#modalCompareError').text(result.errorCountCompare);
                    $('#txtExcelError').text(result.errorExcelText);
                    $('#txtCompareError').text(result.errorCompareText);

                    $('#btnModal').click();
                    $('#loading').removeClass('progress-bar-striped active');
                }



            },
            error: function (result) {
                alert('خطا در سیستم.')
                $('#loading').removeClass('progress-bar-striped active');
            }

        });


    });
}

$(document).ready(main);