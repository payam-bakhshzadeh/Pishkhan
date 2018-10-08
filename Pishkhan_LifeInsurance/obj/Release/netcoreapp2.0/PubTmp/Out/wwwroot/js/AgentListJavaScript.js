var main = function () {

    //Get the user info
    $('.edit').click(function () {
        $('#content').hide();
        $('#loading').show();
        var id = this.id;

        $.ajax({

            url: '/Users/GetUserByIdAsync', 
            data: { Id: id }, 
            type: 'POST', 
            dataType: 'JSON',
            async: true, 
            cache : false , 
            success: function (result) {
                $('#content').show();
                $('#loading').hide();

                $('#agentID').val(result.agentID);
                $('#agentName').val(result.name);
                $('#agentEmail').val(result.email);
                $('#userId').val(result.id);
                var a = $('#userId').val()

            },
            error: function (result) {
                alert('امکان واکشی اطلاعات از سرور وجود ندارد');
            }

        });
    });


    //update the user info
    $('#btnUpdatePressed').click(function () {

        $('#loading').show();

        var ID = $('#userId').val();
        var AgentID = $('#agentID').val();
        var Name = $('#agentName').val();
        var Email = $('#agentEmail').val();

        $.ajax({

            url: '/Users/UpdateUserInfoById',
            data: { ID: ID, AgentID: AgentID, Name: Name, Email: Email },
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

