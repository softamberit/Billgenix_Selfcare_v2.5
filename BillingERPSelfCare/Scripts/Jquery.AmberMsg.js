


function MessageBox(_Content, _Title) {

    var _div = "<div>\
<p>" + _Content + "<span class='ui-icon ui-icon-alert' style='float:left; margin:0 7px 20px 0;'></span><div/></p>\
        </div>";

    $(function () {
        $(_div).dialog(
                    {
                        height: 150,
                        width: 350,
                        modal: true,
                        title: _Title,
                        buttons: {
                            Ok: function () {
                                $(this).dialog("close");
                            }
                        }
                    }
                    );
    });


}



function MessageBoxValidation(_Content) {

    _Content = "You did not enter " + _Content + ", Please enter " + _Content +" to continue";
   var _Title = "Error";


    var _div = "<div>\
<p>" + _Content + "<span class='ui-icon ui-icon-alert' style='float:left; margin:0 7px 20px 0;'></span><div/></p>\
        </div>";

    $(function () {
        $(_div).dialog(
                    {
                        height: 200,
                        width: 350,
                        modal: true,
                        title: _Title,
                        buttons: {
                            Ok: function () {
                                $(this).dialog("close");
                            }
                        }
                    }
                    );
    });

    preventDefault();
}






function MessageBoxValidationIsNullOrEmpty(_Content, _ControlValue) {


    if (_ControlValue == ""|| _ControlValue == null|| _ControlValue==0) {


        _Content = "You did not enter " + _Content + ", Please enter " + _Content + " to continue";
        var _Title = "Error";


        var _div = "<div>\
<p>" + _Content + "<span class='ui-icon ui-icon-alert' style='float:left; margin:0 7px 20px 0;'></span><div/></p>\
        </div>";

        $(function () {
            $(_div).dialog(
                        {
                            height: 200,
                            width: 350,
                            modal: true,
                            title: _Title,
                            buttons: {
                                Ok: function () {
                                    $(this).dialog("close");
                                }
                            }
                        }
                        );
        });

        preventDefault();
    }
}













function ValidateDate(dtp)
{

    var str = dtp.value;
    if (str !== "") {

        var regexp = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;
        var result = regexp.test(str);

        if (result == false) {

            dtp.value = "";
            MessageBox("Please provide a valid Date", "Warning");

        }
    }



}



