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
 

    <script>

        $(document).ready(function () {
            


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

       
    

   <%-- <div class="row gutters">
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">

            <div class="btn-group ">
                  <asp:Button ID="Button1" Text="BILLS PAY" runat="server"  class="btn btn-primary"  
                      style="position: fixed; bottom: 7%; right: 2%" OnClick="btnPayment_Click" />
                </div>                  
            </div>
          
        </div>--%>

</asp:Content>
