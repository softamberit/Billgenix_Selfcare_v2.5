<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerLedger.aspx.cs" Inherits="BillingERPSelfCare.ApplicationReports.CustomerLedger" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=7.2.13.1016, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
        <div class="card">
            <div class="card-header">
                <div class="row">

                    <div class="col-md-6" style="text-align: left">
                        <asp:Button ID="btn" CssClass="btn btn-primary" Style="line-height: .85" runat="server" OnClick="btn_Click" Text="VIEW YOUR LEDGER" />
                    </div>
                </div>
            </div>

            <div class="card-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                        <telerik:ReportViewer ID="ReportViewer1" runat="server" Height="480px" Width="100%" ViewMode="PrintPreview">
                        </telerik:ReportViewer>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
