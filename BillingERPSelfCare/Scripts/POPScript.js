function LoadPOPDropDown(methodUrl, ddl) {


    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select POP--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.Id).html(value.PopName));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });



}


function LoadRouterDropDown(methodUrl, ddl, popId) {


    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select Router--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{'popId':" + popId + "}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.RouterId).html(value.RouterName));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });



}



function LoadSplitterDropDown(methodUrl, ddl) {


    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select Splitter--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.Splitter).html(value.Splitter));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });



}

function LoadSplitterL1DropDownByOlt(methodUrl, ddl, OltId) {


    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select Splitter--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{'oltId':" + OltId + "}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.SplitterId).html(value.Splitter));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });



}

function LoadSplitterL2DropDown(methodUrl, ddl, splitterL1Id) {


    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select Splitter--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{'SplitterL1Id':" + splitterL1Id + "}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.SplitterL2Id).html(value.Splitter));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });



}


function LoadOltDropDown(methodUrl, ddl, routerId) {


    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select OLT--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{'routerId':" + routerId + "}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.OltId).html(value.OltName));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });



}



function LoadONTDropDown(methodUrl, ddl) {


    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select ONT--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.OntId).html(value.ONTName));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });



}



function LoadTechnicalUser(methodUrl, ddl) {


    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select Install By --",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.Id).html(value.Name));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });









}











function LoadServiceDropDown(methodUrl, ddl,option) {


    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select Services--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{'option':'" + option + "'}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.ServiceID).html(value.ServiceName));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });



}

function LoadBWInfoDropDown(methodUrl, ddl) {


    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select Bandwidth--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.BID).html(value.Bandwidth));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });



}


function LoadChangeTypeDropDown(methodUrl, ddl) {


    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select Change Type--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.ChangeTypeId).html(value.ChangeTypeName));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });



}







function LoadCustomerDropDown(methodUrl, ddl) {

    
    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.CustomerID1).html(value.CustomerName1));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });



}







function LoadService_AllSelectedByServiceID(Method_url, ddl, ServiceID,option) {




    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select PI--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: Method_url,
        data: "{'option':'" + option + "'}",
        dataType: "json",
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.ServiceID).html(value.ServiceName));
            });

            $("#" + ddl).val(ServiceID);
            $("#" + ddl).select2(
       {
           placeholder: "--Select PI--",
           allowClear: true
       }
       );
        },
        error: function (result) {
            alert("Error");
        }
    });

}

function LoadBW_AllSelectedByBID(Method_url, ddl, BID) {




    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--Select PI--",
            allowClear: true
        }
        );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: Method_url,
        data: "{}",
        dataType: "json",
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.BID).html(value.Bandwidth));
            });

            $("#" + ddl).val(BID);
            $("#" + ddl).select2(
       {
           placeholder: "--Select PI--",
           allowClear: true
       }
       );
        },
        error: function (result) {
            alert("Error");
        }
    });

}


function LoadAllDropdownCommon(methodUrl, ddl, placeHolder) {


    $("#" + ddl).empty();
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: "--"+placeHolder+"--",
            allowClear: true
        }
    );



    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: methodUrl,
        data: "{}",
        dataType: "json",
        async: false,
        success: function (data) {

            $.each(data.d, function (key, value) {
                $("#" + ddl).append($("<option></option>").val(value.ValId).html(value.ValName));
            });

        },
        error: function (result) {
            alert("Error");
        }
    });



}
