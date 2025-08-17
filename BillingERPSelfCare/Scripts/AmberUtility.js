



function CheckDuplicateProduct(myTable)
{
    var f = 0;
    var pcode1 = "";
    var pcode2 = "";
    for (var r = 0, n = myTable.rows.length; r < n; r++) {
        for (var i = 0, m = myTable.rows.length; i < m; i++) {
            //alert(myTable.rows.length);
            if (r != i) {

                try {
                    pcode1 = myTable.rows[r].cells[0].childNodes[2].id;

                }
                catch (e) {
                    pcode1 = myTable.rows[r].cells[0].childNodes[1].id;
                }


                try {                   
                    pcode2 = myTable.rows[i].cells[0].childNodes[2].id;
                }
                catch (e) {                   
                    pcode2 = myTable.rows[i].cells[0].childNodes[1].id;
                }



                var ProductId1 = $('#' + pcode1).val();
                var ProductId2 = $('#' + pcode2).val();
                if (ProductId1 == ProductId2) {
                    f = 1;
                    var dupRowNumber1 = r + 1;
                    var dupRowNumber2 = i + 1;
                    break;
                }
            }

        }

    }

    return f;

}

function CheckDuplicateProductHdnField(myTable) {
    var f = 0;
    var pcode1 = "";
    var pcode2 = "";
    for (var r = 0, n = myTable.rows.length; r < n; r++) {
        for (var i = 0, m = myTable.rows.length; i < m; i++) {
            //alert(myTable.rows.length);
            

            if (r != i) {

                try {
                    pcode1 = myTable.rows[r].cells[1].childNodes[1].id;

                }
                catch (e) {
                    pcode1 = myTable.rows[r].cells[1].childNodes[0].id;
                }


                try {
                    pcode2 = myTable.rows[i].cells[1].childNodes[1].id;
                }
                catch (e) {
                    pcode2 = myTable.rows[i].cells[1].childNodes[0].id;
                }



                var ProductId1 = $('#' + pcode1).val();
                var ProductId2 = $('#' + pcode2).val();
                if (ProductId1 == ProductId2) {
                    f = 1;
                    dupRowNumber1 = r + 1;
                    dupRowNumber2 = i + 1;
                    break;
                }
            }

        }

    }

    return f;

}

function CheckDuplicateFirstCellTextbox(myTable) {
    var f = 0;
    var pcode1 = "";
    var pcode2 = "";
    for (var r = 0, n = myTable.rows.length; r < n; r++) {
        for (var i = 0, m = myTable.rows.length; i < m; i++) {
            //alert(myTable.rows.length);


            if (r != i) {

                try {
                    pcode1 = myTable.rows[r].cells[0].childNodes[1].id;

                }
                catch (e) {
                    pcode1 = myTable.rows[r].cells[0].childNodes[0].id;
                }


                try {
                    pcode2 = myTable.rows[i].cells[0].childNodes[1].id;
                }
                catch (e) {
                    pcode2 = myTable.rows[i].cells[0].childNodes[0].id;
                }



                var ProductId1 = $('#' + pcode1).val();
                var ProductId2 = $('#' + pcode2).val();
                if (ProductId1 == ProductId2) {
                    f = 1;
                    dupRowNumber1 = r + 1;
                    dupRowNumber2 = i + 1;
                    break;
                }
            }

        }

    }

    return f;

}



function CheckAll() {

    var a;
    if ($("#chkAll")[0].checked) {
        a = "true";
    } else {
        a = "false";
    }

    var myTable = document.getElementById('myDataTable').tBodies[0];
    //var tabRowCount = myTableee.rows.length;


    if (a == "true") {
        for (var r = 0, n = myTable.rows.length; r < n; r++) {

            var chk = 'chkAll' + r;
            $("#" + chk).prop("checked", true);
        }
    }

    if (a == "false") {
        for (var r = 0, n = myTable.rows.length; r < n; r++) {

            var chk = 'chkAll' + r;
            $("#" + chk).prop("checked", false);

        }
    }


    //  alert(tabRowCount);

}


function isNumeric(keyCode) {
    return ((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || keyCode == 190 || (keyCode >= 96 && keyCode <= 105) || keyCode == 110);
}

function checkValidInputNumber(txt) {
    $("#" + txt + "").on("keypress", function (event) {

        if (event.which != 8 && isNaN(String.fromCharCode(event.which)) && event.which!=110) {
            event.preventDefault(); //stop character from entering input

        }

    });
    $("#" + txt + "").on("cut copy paste", function (event) {
        event.preventDefault();
    });
}

function checkDecimal(_control) {

    

    var str = _control.value;
    var regexp = /^[+]?([0-9]+(?:[\.][0-9]*)?|\.[0-9]+)$/;
    var result = regexp.test(str);

    if (result == false) {
        _control.value = "";

        MessageBox("Please provide a valid Decimal value", "Warning");
    }


}



function GetAmount(txtQuantity, txtRate, txtAmount) {


    var Quantity = $('#' + txtQuantity.id + '').val();
    var Price = $('#' + txtRate.id + '').val();
    var am = Quantity * Price;
    var Amount = am.toFixed(2);


    $('#' + txtAmount.id + '').val(Amount);
}


function GetTotalBag(FromBag, ToBag, Bag) {


   
    var fromBag = $('#' + FromBag.id + '').val();
    var toBag = $('#' + ToBag.id + '').val();

    
        var total = toBag - fromBag;
       // var TotalBag = total.toFixed(2);
    total = total + 1;


    $('#' + Bag.id + '').val(total);
}

function CheckBag(FromBag, ToBag, Bag)
{

    var fromBag = $('#' + FromBag.id + '').val();
    var toBag = $('#' + ToBag.id + '').val();




        
        if (parseInt(fromBag) > parseInt(toBag)) {
            MessageBox("To Bag must be greater than From Bag", "Error");
            $('#' + ToBag.id + '').val("0");

            var total = parseInt(toBag) - parseInt(fromBag);
            $('#' + Bag.id + '').val(total);
        }
  




    
   
    
}




function IsDuplicate(methodUrl,txtBox,tableName,fieldName,compareText) {

    var fieldValue = compareText;
    if (fieldValue != '') {

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: methodUrl,
            data: "{ 'FieldName': '" + fieldName + "','TableNmae': '" + tableName + "','FieldValue': '" + fieldValue + "'}",
            dataType: "json",
            success: function (data) {

                if (data.d == 'true') {
                    $('#' + txtBox).val('');
                    MessageBox("Customer Id is already exist. Please enter a diiferent ID.");
                }

            },
            error: function (result) {
                alert("Error");
            }
        });
    }

}


function SessionCheck(Method_url, logout_Url) {


    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: Method_url,
        data: "{}",
        dataType: "json",
        success: function (data) {


            if (data.d == "") {
                window.location.href = logout_Url;
            }


        },
        error: function (result) {
            alert("Error");
        }
    });


}


function validatePhone(control) {
    
    var a = control.value;

    //var filter = /^[0-9-+]+$/;
    //var filter = /^((?!(0))[0-9]{10})$/;
    var filter = /^([0-9]{11})$/;
    var test = filter.test(a);
    if (test == false) {
        control.value = "";

        MessageBox("Please provide a valid Phone number", "Warning");
    }

}

//Code Ends


function validateEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var result= regex.test(email.value);
    if (result == false) {
        email.value = "";

        MessageBox("Please provide a valid Email!", "Warning");
    }
}

