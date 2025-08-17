<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs"  Inherits="BillingERPSelfCare.Dashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <style type="text/css">
        
        .container{
    margin-top: 8%;
    margin-left: 8%;
}

.card:hover{
    -webkit-box-shadow: -1px 9px 40px -12px rgba(0,0,0,0.75);
    -moz-box-shadow: -1px 9px 40px -12px rgba(0,0,0,0.75);
    box-shadow: -1px 9px 40px -12px rgba(0, 0, 0, 0.75);
}

    </style>
 
<script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/9.0.6/signalr.min.js" integrity="sha512-kkMt8UThSmWcdXLYFaGZ/U6vyWSNLZMUWQ5SMeF80pGqrEkH5ei9D/3MbVQpB8p7D5C3A4vlX7BpsWTT2BfB6A==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
<!--highchart-->

<script src="https://code.highcharts.com/highcharts.js"></script>
<script src="https://code.highcharts.com/modules/data.js"></script>
<script src="https://code.highcharts.com/modules/series-label.js"></script>
<script src="https://code.highcharts.com/modules/exporting.js"></script>
<script src="https://code.highcharts.com/modules/export-data.js"></script>
<script src="https://code.highcharts.com/modules/accessibility.js"></script>
    <script>

        $(document).ready(function () {
            drawGraph();
            $('.col-4-lg').hover(
                // trigger when mouse hover
                function () {
                    $(this).animate({
                        marginTop: "-=1%",
                    }, 200);
                },

                // trigger when mouse out
                function () {
                    $(this).animate({
                        marginTop: "0%"
                    }, 200);
                }
            );
        });

        const connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7015/trafficHub?username=74039")
            .withAutomaticReconnect()
            .build();
        connection.on("UserConnected", function (message) {

            console.log("Connected: " + message);
        });
        connection.on("traffics", function (traffic) {
            var data = JSON.parse(traffic);

            setData(data);
        });
        connection.on("UpdateActiveUsers", function (message) {

            console.log(message);

        });

        connection.start().catch(function (err) {
            return console.error(err.toString());
        });


        var chart;
        function drawGraph() {
            chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'divCustomerTraffic',
                    type: 'column',
                    marginRight: 10,
                    events: getData()
                },
                title: {
                    text: 'Real Time Download-Upload Graph'
                },
                subtitle: {
                    text: 'Username: 74039 <br> Router name: Agargoan'
                },

                xAxis: {
                    type: 'datetime',
                    labels: {
                        format: '{value:%H:%M:%S}'
                    },
                    tickPixelInterval: 100
                },

                yAxis: {
                    title: {
                        text: 'Speed'
                    }
                },
                plotOptions: {
                    series: {
                        pointWidth: 2
                    }
                },

                tooltip: {
                    //the pop up one
                    formatter: function () {
                        var unit = "";
                        var value = this.y;
                        //console.log(value);
                        if (value > 1000) { value = value / 1000; unit = 'kbps'; }
                        if (value > 1000) { value = value / 1000; unit = 'mbps'; }
                        return '<b>' + this.series.name + '</b><br/>' +
                            Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br/>' +
                            Highcharts.numberFormat(value, 2) + unit;
                    }
                },

                legend: {
                    enabled: false
                },

                series: [{
                    name: 'Tx',
                    color: 'red',
                    data: initializeGraph(),
                    maxPointWidth: 2
                },
                {
                    name: 'Rx',
                    color: 'blue',
                    data: initializeGraph(),
                    maxPointWidth: 2
                }],
                responsive: {
                    rules: [{
                        condition: {
                            maxWidth: screen.width,
                            maxHeight: screen.height
                        }
                    }]
                }
            });
        }

        //get data from ajax call

        function getData() {
            var lastPos = 0;	
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetTrafficData",
                data: '{cid: "' + $("#<%=hdnCustomerId.ClientID%>")[0].value + '" }',
                 contentType: "application/json; charset=utf-8",
                dataType: "json",
                xhrFields: {
                    onprogress: function (e) {
                      
                        // Partial response while readyState == 3
                        var responseText =e.currentTarget.responseText;

                        var response = responseText.substring(lastPos);
                        lastPos = responseText.length;

                        var data = JSON.parse(responseText);
                      
                        console.log("Partial Data:", responseText);
                        setData(data);
                    }
                },
                success: OnSuccess,
                failure: function (response) {
                    console.log(response);
                   /*  alert(response.d);*/
                 }
             });
        }
        function OnSuccess(response) {
            console.log(response);
            setData(response.d);
        }

        function setData(traffic) {



            var currentDate = new Date();
            var rx = parseFloat(traffic.Rx);
            var tx = parseFloat(traffic.Tx);

            var unit_rx = "bps", unit_tx = "bps";
            var value_rx = rx, value_tx = tx;
            if (value_rx > 1000) { value_rx = value_rx / 1000; unit_rx = 'kbps'; }
            if (value_rx > 1000) { value_rx = value_rx / 1000; unit_rx = 'mbps'; }

            if (value_tx > 1000) { value_tx = value_tx / 1000; unit_tx = 'kbps'; }
            if (value_tx > 1000) { value_tx = value_tx / 1000; unit_tx = 'mbps'; }

            document.getElementById("rx").innerHTML = value_rx.toFixed(2) + " " + unit_rx;
            document.getElementById("tx").innerHTML = value_tx.toFixed(2) + " " + unit_tx;
            chart.series[0].addPoint([currentDate.getTime(), rx], true, true);
            chart.series[1].addPoint([currentDate.getTime() + 200, tx], true, true);



        }

        function initializeGraph() {
            // generate graph for an array of random data
            var data = [],
                time = (new Date()).getTime(),
                i;
            for (i = -99; i <= 0; i++) {
                data.push({
                    x: time + i * 1000,
                    y: 0
                });
            }
            return data;
        }

        Highcharts.setOptions({
            global: {
                useUTC: false
            }
        });


    </script>
    <asp:HiddenField ID="hdnCustomerId" runat="server" Value="" />
  <!-- BEGIN .main-heading -->
                <header class="main-heading">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6">
                                <div class="page-icon">
                                    <i class="icon-laptop_windows"></i>
                                </div>
                                <div class="page-title">
                                    <h5>Dashboard</h5>
                                    <h6 class="sub-heading">Welcome to Amber IT SelfCare</h6>
                                </div>
                            </div>
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6" id="DivHyperLink" runat="server">
                               <%-- <div>
                                    <a href="/Sales/CustomerDocument.aspx" target="_self" style="color:blue;font-weight:bold;text-decoration:underline;float:right">Upload your NID and Photo</a>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </header>
                <!-- END: .main-heading -->


    <div class="row gutters">

        <div class="col-xl-3 col-lg-3 col-md-3 col-sm-6">
            <div class="card">
                <div class="card-body">
                    <div class="stats-widget">
                        <div class="stats-widget-header">
                            <i class="icon-coin-dollar"></i>
                        </div>
                        <div class="stats-widget-body">
                            <!-- Row start -->
                            <ul class="row no-gutters">
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <%--<h5 class="title" style="color:rgb(0, 0, 0)">Balance</h5>--%>
                                    <label style="color:rgb(0, 0, 0); font-size:medium">Balance</label>
                                </li>
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <h4 class="total" runat="server" id="txtBalance"></h4>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-xl-3 col-lg-3 col-md-3 col-sm-6">
            <div class="card">
                <div class="card-body">
                    <div class="stats-widget">
                        <div class="stats-widget-header">
                            <i class="icon-file-text2"></i>
                        </div>
                        <div class="stats-widget-body">
                            <!-- Row start -->
                            <ul class="row no-gutters">
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <%--<h5 class="title" style="color:rgb(0, 0, 0)">No of Invoice</h5>--%>
                                    <label style="color:rgb(0, 0, 0); font-size:medium">No of Invoice</label>
                                </li>
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <h4 class="total" runat="server" id="txtInvoice"></h4>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-xl-3 col-lg-3 col-md-3 col-sm-6">
            <div class="card">
                <div class="card-body">
                    <div class="stats-widget">

                        <div class="stats-widget-header">
                            <i class="icon-bargraph"></i>
                        </div>
                        <div class="stats-widget-body">
                            <!-- Row start -->
                            <ul class="row no-gutters">
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <%--<h5 class="title" style="color:rgb(0, 0, 0)">No of Payment</h5>--%>
                                    <label style="color:rgb(0, 0, 0); font-size:medium">No of Payment</label>
                                </li>
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <h4 class="total" runat="server" id="txtPayment"></h4>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-xl-3 col-lg-3 col-md-3 col-sm-6">
            <div class="card">
                <div class="card-body">
                    <div class="stats-widget">

                        <div class="stats-widget-header">
                            <i class="icon-credit-card"></i>
                        </div>
                        <div class="stats-widget-body">
                            <!-- Row start -->
                            <ul class="row no-gutters">
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <%--<h5 class="title" style="color:rgb(0, 0, 0)">Invoice</h5>--%>
                                    <label style="color:rgb(0, 0, 0); font-size:medium">Invoice</label>
                                </li>
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <h4 class="total" runat="server" id="txtTotalInvoice"></h4>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row gutters">
        <div class="col-xl-3 col-lg-3 col-md-3 col-sm-6">
            <div class="card">
                <div class="card-body">
                    <div class="stats-widget">
                        <div class="stats-widget-header">
                            <i class="icon-chart-line-outline"></i>
                        </div>
                        <div class="stats-widget-body">
                            <!-- Row start -->
                            <ul class="row no-gutters">     
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <%--<h5 class="title" style="color:rgb(0, 0, 0)">Paid Amount</h5>--%>
                                    <label style="color:rgb(0, 0, 0); font-size:medium">Paid Amount</label>
                                </li>
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <h4 class="total" runat="server" id="txtTotalAmount"></h4>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
                <div class="col-xl-3 col-lg-3 col-md-3 col-sm-6">
            <div class="card">
                <div class="card-body">
                    <div class="stats-widget">
                        <div class="stats-widget-header">
                            <i class="icon-connection"></i>
                        </div>
                        <div class="stats-widget-body">
                            <!-- Row start -->
                            <ul class="row no-gutters">
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <%--<h8 class="total" runat="server" id="txtPackage" style="/*font-size:medium; color: crimson*/">MRC</h8>--%>
                                <label runat="server" id="txtPackage" style="font-size:medium; color: crimson">MRC</label>
                                </li>
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <h4 class="total" runat="server" id="txtMRC" style="color: crimson"></h4>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-xl-3 col-lg-3 col-md-3 col-sm-6">
            <div class="card">
                <div class="card-body">
                    <div class="stats-widget">

                        <div class="stats-widget-header">
                            <i class="icon-calendar"></i>
                        </div>
                        <div class="stats-widget-body">
                            <!-- Row start -->
                            <ul class="row no-gutters">
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <%--<h5 class="title" style="color:rgb(0, 0, 0)"></h5>--%>
                                     <label style="color:rgb(0, 0, 0); font-size:medium">Next Cycle Date</label>
                                </li>
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <h5 class="total" runat="server" id="txtCycleDate" style="color: crimson"></h5>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-3 col-lg-3 col-md-3 col-sm-6">
            <div class="card">
                <div class="card-body">
                    <div class="stats-widget">

                        <div class="stats-widget-header">
                            <i class="icon-accessibility"></i>
                        </div>
                        <div class="stats-widget-body">
                            <!-- Row start -->
                            <ul class="row no-gutters">
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <%--<h6 class="title" style="color:rgb(0, 0, 0)">Pending Request</h6>--%>
                                    <label style="color:rgb(0, 0, 0); font-size:medium">Pending Request</label>
                                </li>
                                <li class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col">
                                    <h4 class="total" runat="server" id="txtPendingRequest"></h4>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

         <div class="row gutters">
          <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
               <div class="card">
    <div class="card-body">
        <div class="stats-widget">          
            <div class="stats-widget-body">
   <h3 style="color:rgb(0, 0, 0); font-size:medium">Usage Graph</h3>
                      
                       
                         
    RX <div id="tx"></div>
    TX  <div id="rx"></div>
 
                    <div id="divCustomerTraffic"></div>
 
 
            </div>
        </div>
    </div>
</div>

</div>


     </div>
    

   <%-- <div class="row gutters">
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">

            <div class="btn-group ">
                  <asp:Button ID="Button1" Text="BILLS PAY" runat="server"  class="btn btn-primary"  
                      style="position: fixed; bottom: 7%; right: 2%" OnClick="btnPayment_Click" />
                </div>                  
            </div>
          
        </div>--%>

</asp:Content>
