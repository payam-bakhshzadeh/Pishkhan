var main = function () {

    $('#ddlAgent').focus();


    //حذف پیام اطلاعات ثبت گردید وقتی که کاربر در حال وارد کردن اطلاعات جدید می باشد
    $('#txtSerial').blur(function () {
        $('#pm').hide();
    });

    //تنظیمات نام و نام خانوادگی بیمه گذار و بیمه شده
    $('#txtBimegozarname').blur(function () {

        var a = $('#txtBimegozarname').val();
        $('#txtBimeshodename').val(a);
    });

    //تنظیمات تلفن
    $('#txtBimegozarphone').blur(function () {

        var a = $('#txtBimegozarname').val();
        var b = $('#txtBimeshodename').val();

        if (a == b) {
            var tell = $('#txtBimegozarphone').val();
            $('#txtBimeshodephone').val(tell);
        }
    })

    //تنظیمات تاریخ
    $('#txtDatesoodoor').blur(function () {

        var a = $('#txtDatesoodoor').val();
        $('#txtDatestart').val(a);
    });


    //تنظیمات دگمه submit--------------------------------------------------------------------------
    $('#btnAdd').click(function (e) {

        e.preventDefault();

        var a = $('#LifeInsuranceVM_Payment_Price').val();

        a = a.replace(",", "");
        a = a.replace(",", "");
        a = a.replace(",", "");
        a = a.replace(",", "");
        a = a.replace(",", "");

        $('#LifeInsuranceVM_Payment_Price').val(a);

        $('#myForm').submit();
    });


    //تنظیمات تاریخ های میلادی-------------------------------------------------------------------------
    $('#txtDatesoodoor').blur(function () {

        var date = $('#txtDatesoodoor').val();

        $.ajax({

            url: '/LifeInsurance/ConvertDateToMiladi',
            data: { date: date },
            type: 'POST',
            dataType: 'JSON',
            async: true,
            cache: false,
            success: function (result) {
                $('#txtDatesoodoorMiladi').val(result.miladiDate);
            },
            error: function (result) {
            }

        });
    });


    $('#txtDatestart').blur(function () {

        var date = $('#txtDatestart').val();

        $.ajax({

            url: '/LifeInsurance/ConvertDateToMiladi',
            data: { date: date },
            type: 'POST',
            dataType: 'JSON',
            async: true,
            cache: false,
            success: function (result) {
                $('#txtDatestartMiladi').val(result.miladiDate);
            },
            error: function (result) {
            }

        });
    });

    //جدا کردن سه رقم اعشار--------------------------------------------------------------------------
    $('#LifeInsuranceVM_Payment_Price').blur(function () {

        
        var price = $('#LifeInsuranceVM_Payment_Price').val();
        if (price != 0) {

             ConvetrToPrice(price);

        }
    });


    //textbox = 1 => txtprice send info

    function ConvetrToPrice(price , textbox) {

        $.ajax({

            url: '/LifeInsurance/ConvertToMony',
            data: { price: price },
            type: 'POST',
            dataType: 'JSON',
            async: true,
            cache: false,
            success: function (result) {
                $('#LifeInsuranceVM_Payment_Price').val(result.mony);
            },
            error: function (result) {
            }

        });
    }



    //تنظیمات کلید اینتر------------------------------------------------------------------------------
    $('#ddlAgent').keydown(function (event) {

        if (event.which == 13) {

            $('#ddlPishkhan').focus();
        }
    });

    $('#ddlPishkhan').keydown(function (event) {

        if (event.which == 13) {

            $('#ddlSupervisior').focus();
        }
    });



    $('.form-control').keyup(function (e) {

        if (e.which == 13) {


            var tabindex = parseInt(document.activeElement.getAttribute('tabindex'));
            tabindex = tabindex + 1;
            $('input[tabindex=' + tabindex + ']').focus();
        }
    });

    $('#sepordeAvaliye').keydown(function (event) {

        if (event.which == 13) {

            $('#ddlPaymentType').focus();
        }
    });

    $('#ddlPaymentType').keydown(function (event) {

        if (event.which == 13) {

            $('#ddlInsuranceStatus').focus();
        }
    });

    $('#ddlInsuranceStatus').keydown(function (event) {

        if (event.which == 13) {

            $('#btnAdd').focus();
        }
    });





    
}

$(document).ready(main);




