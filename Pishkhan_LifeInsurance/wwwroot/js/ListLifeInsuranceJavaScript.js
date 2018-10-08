var main = function () {

    $('#loading').hide();

    //Scroll Method
    checkScrolling();


    //Filter DeopDown
    $('#filterContent').hide();

    $('#showFilter').click(function () {

        $('#filterContent').slideToggle();
    });

    $('#btnCloseFilter').click(function () {

        $('#filterContent').slideToggle();
        clearAllFilterTextBox();
    });
}

$(document).ready(main);


function checkScrolling() {

    $(document).scroll(function () {
        maxScrollVal = $(document).height() - $(window).height();

        if ($(document).scrollTop() >= maxScrollVal - 2) {

            getNextPageInfo();

        }
    });
}

//Get NextPage Info
function getNextPageInfo() {

    var sendInfoToServer = true;

    var totalRow = $('#countTotalRow').val();
    var countRow = $('#countRow').val();



    if (parseInt(countRow) < parseInt(totalRow) && sendInfoToServer == true) {

        sendInfoToServer = false;

        $('#loading').show();
        var Take = parseInt($('#take').val());
        var Skip = parseInt($('#skip').val());

        $('#skip').val(Skip + Take);

        var Row = $('#row').val();

        $.ajax({

            url: '/LifeInsurance/GetNextPageInfo',
            data: { Take: Take, Skip: Skip, Row: Row },
            type: 'POST',
            dataType: 'JSON',
            async: true,
            cache: false,
            success: function (result) {

                countRow = parseInt(countRow) + parseInt(result.countRow);

                $('#countRow').val(countRow);
                $('#row').val(countRow);
                $('#tbl').append(result.html);
                $('#loading').hide();
                sendInfoToServer = true;
            },
            error: function (result) {
            }

        });
    }


}

//Delete Item
function deleteRow(id) {



    var result = confirm('آیا برای پاک کردن مطمئن هستید؟');

    if (result) {

        $.ajax({

            url: '/LifeInsurance/DeleteLifeInsurance',
            data: { id: id },
            type: 'POST',
            dataType: 'JSON',
            async: true,
            cache: false,
            success: function (result) {

                if (result.pm == true) {
                    $('#' + id).hide();
                    location.reload();
                }

                else {
                    alert('خطا در سیستم.')
                }
            },
            error: function (result) {
                alert('خطا در سیستم.')
            }

        });
    }


};



//--------------------------------------------------------------------------------------
//Filter JavaScripts

//Cleare Filter Textbox
function clearAllFilterTextBox() {

    $('#ddlAgent').val('...');
    $('#ddlSupervisior').val('...');
    $('#ddlPishkhan').val('...');
    $('#txtInsuranceNumber').val('');
    $('#txtSerialNumber').val('');
    $('#txtFromDateSoodoor').val('');
    $('#txtToDateSoodoor').val('');
    $('#txtFromDateStart').val('');
    $('#txtToDateStart').val('');
    $('#txtBimegozarName').val('');
    $('#txtBimeShodeName').val('');
    $('#txtBimegozarPhone').val('');
    $('#txtBimeShodePhone').val('');

}

function ddlAgentChange() {

    //$('#ddlAgent').val('...');
    $('#ddlSupervisior').val('...');
    $('#ddlPishkhan').val('...');
    $('#txtInsuranceNumber').val('');
    $('#txtSerialNumber').val('');
    $('#txtFromDateSoodoor').val('');
    $('#txtToDateSoodoor').val('');
    //$('#txtFromDateStart').val('');
    //$('#txtToDateStart').val('');
    $('#txtBimegozarName').val('');
    $('#txtBimeShodeName').val('');
    $('#txtBimegozarPhone').val('');
    $('#txtBimeShodePhone').val('');

}

function ddlSupervisiorChange() {

    $('#ddlAgent').val('...');
    //$('#ddlSupervisior').val('...');
    $('#ddlPishkhan').val('...');
    $('#txtInsuranceNumber').val('');
    $('#txtSerialNumber').val('');
    $('#txtFromDateSoodoor').val('');
    $('#txtToDateSoodoor').val('');
    //$('#txtFromDateStart').val('');
    //$('#txtToDateStart').val('');
    $('#txtBimegozarName').val('');
    $('#txtBimeShodeName').val('');
    $('#txtBimegozarPhone').val('');
    $('#txtBimeShodePhone').val('');
}

function ddlPishkhanChange() {

    $('#ddlAgent').val('...');
    $('#ddlSupervisior').val('...');
    //$('#ddlPishkhan').val('...');
    $('#txtInsuranceNumber').val('');
    $('#txtSerialNumber').val('');
    $('#txtFromDateSoodoor').val('');
    $('#txtToDateSoodoor').val('');
    //$('#txtFromDateStart').val('');
    //$('#txtToDateStart').val('');
    $('#txtBimegozarName').val('');
    $('#txtBimeShodeName').val('');
    $('#txtBimegozarPhone').val('');
    $('#txtBimeShodePhone').val('');
}

function txtInsuranceNumberChange() {
    $('#ddlAgent').val('...');
    $('#ddlSupervisior').val('...');
    $('#ddlPishkhan').val('...');
    //$('#txtInsuranceNumber').val('');
    $('#txtSerialNumber').val('');
    $('#txtFromDateSoodoor').val('');
    $('#txtToDateSoodoor').val('');
    $('#txtFromDateStart').val('');
    $('#txtToDateStart').val('');
    $('#txtBimegozarName').val('');
    $('#txtBimeShodeName').val('');
    $('#txtBimegozarPhone').val('');
    $('#txtBimeShodePhone').val('');
}

function txtSerialNumberChange() {
    $('#ddlAgent').val('...');
    $('#ddlSupervisior').val('...');
    $('#ddlPishkhan').val('...');
    $('#txtInsuranceNumber').val('');
    //$('#txtSerialNumber').val('');
    $('#txtFromDateSoodoor').val('');
    $('#txtToDateSoodoor').val('');
    $('#txtFromDateStart').val('');
    $('#txtToDateStart').val('');
    $('#txtBimegozarName').val('');
    $('#txtBimeShodeName').val('');
    $('#txtBimegozarPhone').val('');
    $('#txtBimeShodePhone').val('');
}

function txtDateSoodoorChange() {
    $('#ddlAgent').val('...');
    $('#ddlSupervisior').val('...');
    $('#ddlPishkhan').val('...');
    $('#txtInsuranceNumber').val('');
    $('#txtSerialNumber').val('');
    //$('#txtFromDateSoodoor').val('');
    //$('#txtToDateSoodoor').val('');
    $('#txtFromDateStart').val('');
    $('#txtToDateStart').val('');
    $('#txtBimegozarName').val('');
    $('#txtBimeShodeName').val('');
    $('#txtBimegozarPhone').val('');
    $('#txtBimeShodePhone').val('');
}

function txtDateStartChange() {
    //$('#ddlAgent').val('...');
    //$('#ddlSupervisior').val('...');
    //$('#ddlPishkhan').val('...');
    $('#txtInsuranceNumber').val('');
    $('#txtSerialNumber').val('');
    $('#txtFromDateSoodoor').val('');
    $('#txtToDateSoodoor').val('');
    //$('#txtFromDateStart').val('');
    //$('#txtToDateStart').val('');
    $('#txtBimegozarName').val('');
    $('#txtBimeShodeName').val('');
    $('#txtBimegozarPhone').val('');
    $('#txtBimeShodePhone').val('');
}

function txtNameChange() {
    $('#ddlAgent').val('...');
    $('#ddlSupervisior').val('...');
    $('#ddlPishkhan').val('...');
    $('#txtInsuranceNumber').val('');
    $('#txtSerialNumber').val('');
    $('#txtFromDateSoodoor').val('');
    $('#txtToDateSoodoor').val('');
    $('#txtFromDateStart').val('');
    $('#txtToDateStart').val('');
    //$('#txtBimegozarName').val('');
    //$('#txtBimeShodeName').val('');
    $('#txtBimegozarPhone').val('');
    $('#txtBimeShodePhone').val('');
}

function txtPhoneChange() {
    $('#ddlAgent').val('...');
    $('#ddlSupervisior').val('...');
    $('#ddlPishkhan').val('...');
    $('#txtInsuranceNumber').val('');
    $('#txtSerialNumber').val('');
    $('#txtFromDateSoodoor').val('');
    $('#txtToDateSoodoor').val('');
    $('#txtFromDateStart').val('');
    $('#txtToDateStart').val('');
    $('#txtBimegozarName').val('');
    $('#txtBimeShodeName').val('');
    //$('#txtBimegozarPhone').val('');
    //$('#txtBimeShodePhone').val('');
}