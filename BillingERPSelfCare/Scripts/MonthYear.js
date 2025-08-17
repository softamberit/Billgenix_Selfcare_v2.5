function LoadMonth(ddl) {
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: '--Select Month--',
            allowClear: true

        }
        );

    var data = [
        { id: 1, tag: 'January' }, { id: 2, tag: 'February' }, { id: 3, tag: 'March' }, { id: 4, tag: 'April' },
        { id: 5, tag: 'May' }, { id: 6, tag: 'June' }, { id: 7, tag: 'July' }, { id: 8, tag: 'August' },
        { id: 9, tag: 'September' }, { id: 10, tag: 'October' }, { id: 11, tag: 'November' }, { id: 12, tag: 'December' }
    ];

    $.each(data, function (key, value) {
        $("#" + ddl).append($("<option></option>").val(value.id).html(value.tag));
    });


   
}


function LoadYear(ddl) {
    $("#" + ddl).append($("<option></option>").val('').html(''));
    $("#" + ddl).select2(
        {
            placeholder: '--Select Year--',
            allowClear: true

        }
        );



    var data = [];
    var year = new Date().getFullYear();
    
    for (var i = 0; i < 10; i++)
    {

       // data.push([year])
       
        $("#" + ddl).append($("<option></option>").val(year).html(year));
        year = year + 1;
    }   

   



}