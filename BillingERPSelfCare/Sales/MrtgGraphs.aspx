<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MrtgGraphs.aspx.cs" Inherits="BillingERPSelfCare.Sales.MrtgGraphs" %>

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
                                    </div>
                                    <div class="row">
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6">
                                            <h5>"Daily" Graph (5 Minute Average)</h5>
                                            <img id="imgDailyGraph" runat="server" alt="Daily Graph" />
                                        </div>
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6">
                                            <h5>"Weekly" Graph (30 Minute Average)</h5>
                                            <img id="imgWeekly" runat="server" alt="Weekly Graph" />
                                        </div>
                                       
                                    </div>
                                    <div class="row">
                                        
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6">
                                            <h5>"Monthly" Graph (2 Hour Average)</h5>
                                            <img id="imgMonthly" runat="server" alt="Monthly Graph" />
                                        </div>
                                        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6">
                                            <h5>"Yearly" Graph (1 Day Average)</h5>
                                            <img id="imgYearly" runat="server" alt="Yearly Graph" />
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
