<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BKashAccountAdd.aspx.cs" Inherits="BillingERPSelfCare.Bkash.BKashAccountAdd" EnableViewState="true" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .custom-card {
            width: 70%;
            margin: 0 auto;
            cursor: pointer;
            transition: transform 0.2s;
        }

            .custom-card:hover {
                transform: scale(1.02);
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            }
    </style>
    <script type="text/javascript">
</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row" runat="server" id="DivDeviceEntry">
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                    <div class="card">
                        <div class="card-header" style="font-size: large; font-weight: 400">
                            My BKash Accounts
                        </div>
                        <div class="card-body">
                            <asp:Button ID="Yes" runat="server" Text="Add" UseSubmitBehavior="false" class="btn btn-success" OnClick="btnbKashAdd_OnClick" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" runat="server" id="DivCardStack">
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                    <asp:Repeater ID="rptBkashAccounts" runat="server">
                        <ItemTemplate>
                            <div class="card mb-3 custom-card">
                                <div class="card-body d-flex justify-content-between align-items-center">
                                    <span class="card-number" style="font-size: large; font-weight: 600"><%# Eval("payerAccount") %></span>
                                    <div>
                                        <asp:LinkButton ID="btnPay" runat="server" Text="Pay" CssClass="btn btn-success" CommandArgument='<%# Eval("agreementID") %>' OnClick="btnPay_OnClick" />
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-primary" CommandArgument='<%# Eval("agreementID") %>' OnClick="btnDelete_OnClick" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
