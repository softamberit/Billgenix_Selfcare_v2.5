<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RealtimeTraffic.aspx.cs" Inherits="BillingERPSelfCare.Sales.RealtimeTraffic" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .RadInput_BlackMetroTouch .riTextBox, html body .RadInputMgr_BlackMetroTouch {
            background: #e1e5f1 !important;
            color: black !important;
            border-color: #4e4e4e;
            font-weight: 500 !important;
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
            var cid = $("#<%=hdnCustomerId.ClientID%>")[0].value;
            var baseHubUrl = $("#<%=hdnHubUrl.ClientID%>")[0].value;

            drawGraph(cid);
            debugger;

            const connection = new signalR.HubConnectionBuilder()
                .withUrl(baseHubUrl + "trafficHub?cid=" + cid + "")
                .withAutomaticReconnect()
                .build();
            connection.on("UserConnected", function (data) {

                console.log("Connected: " + data);
                RequestTrafficData(data);
            });
            connection.on("traffics", function (traffic) {
                var data = JSON.parse(traffic);
                console.log(data);
                setData(data);
            });
            connection.on("UpdateActiveUsers", function (message) {

                console.log(message);

            });

            connection.start().catch(function (err) {
                return console.error(err.toString());
            });
        });

        var chart;
        function drawGraph(cid) {
            chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'divCustomerTraffic',
                    type: 'area',
                    marginRight: 10,
                    //events: getData(),
                    zooming: {
                        type: 'x'
                    },
                    panning: true,
                    panKey: 'shift',
                    scrollablePlotArea: {
                        minWidth: 600,
                        scrollPositionX: 1
                    }

                },
                title: {
                    text: 'Realtime Upload/Download Graph'
                },
                subtitle: {
                    text: 'Customer ID: ' + cid,
                    floating: true,
                    align: 'right',
                    verticalAlign: 'bottom',
                    x: -100,
                    y: -110
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
        function getSubtitle() {
            return `<span style='font-size: 60px'>${input.value}</span>`;
        }
        //get data from ajax call

        function RequestTrafficData(data) {
            var lastPos = 0;
            $.ajax({
                type: "POST",
                url: "RealtimeTraffic.aspx/RequestTrafficData",
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: OnSuccess,
                failure: function (response) {
                    console.log(response);
                    /*  alert(response.d);*/
                }
            });
        }
        function OnSuccess(response) {
            console.log(response);
            //setData(response.d);
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnCustomerId" runat="server" Value="" />
            <asp:HiddenField ID="hdnHubUrl" runat="server" Value="" />

            <div class="row gutters">
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="stats-widget">
                                <div class="stats-widget-body">
                                    <div class="row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            

                                            <div>Upload(TX):<span style="color:red;font:bold" id="tx"></span></div>
                                            <div>Download(RX):<span style="color:blue;font:bold" id="rx"></span></div>
                                            
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div id="divCustomerTraffic"></div>
                                        </div>
                                    </div>




                                </div>
                            </div>
                        </div>
                    </div>

                </div>


            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
