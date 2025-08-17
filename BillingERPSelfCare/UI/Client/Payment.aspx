<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="BillingERPSelfCare.UI.Client.Payment" EnableViewState="false" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">


        function Message(message) {

            $("#msgModal").modal("show");
            $('#msgModalBodyParagraph').html(message);
        }

        function confirm1(message1) {
            $('#myModal').modal('show');
            $('#confirmMessage').html(message1);
        }


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }


    </script>


    <div class="row" runat="server" id="DivDeviceEntry">
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
            <div class="card">
                <div class="card-header" style="font-size: large; font-weight: 600">
                    Payment Through Online
                </div>
                <div class="card-body">

                    <div class="form-row">
                        <div class="col-sm-6">
                            <div class="form-group row gutters">
                                <label class="col-sm-4 col-md-4 col-form-label" style="font-weight: 600; color: black">Amount (BDT)<span style="color: red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtInvoiceAmount" CssClass="form-control" AutoCompleteType="Disabled" runat="server" onkeypress="return isNumberKey(event)" Width="80%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvInvoiceMaount" runat="server" ControlToValidate="txtInvoiceAmount"
                                        ValidationGroup="save" SetFocusOnError="true" ForeColor="red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="col-sm-6">
                            <div class="form-group row gutters">
                                <label class="col-sm-4 col-md-4 col-form-label" style="font-weight: 600; color: black">Reference No</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtReference" CssClass="form-control" AutoCompleteType="Disabled" runat="server" Width="80%"></asp:TextBox>
                                    <label style="color: red">Reference no. field is optional; use it if needed.</label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="col-sm-6">
                            <div class="form-group row gutters">
                                <label class="col-sm-4 col-md-4 col-form-label pt-3" style="font-weight: 600; color: black">
                                    Select Payment
                                </label>
                                <div class="col-sm-8">
                                    <div class="input-group">
                                        <asp:RadioButton ID="bkashRadio" runat="server" GroupName="PaymentGroup" Checked="true" />
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/img/bkash.svg" CssClass="img-class" Style="width: 100px" />
                                    </div>
                                    <div class="input-group">
                                        <asp:RadioButton ID="sslRadio" runat="server" GroupName="PaymentGroup"  />
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/img/ssl.png" CssClass="img-class" Style="width: 150px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="form-row" style="padding-top: 40px">
                        <div class="col-sm-12" style="text-align: right">
                            <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:Button ID="btn" CssClass="btn btn-primary" AutoPostBack="true" ValidationGroup="save" runat="server" OnClientClick="confirm1('After your confirmation this page will be redirected to online payment gateway. During payment process, please do not close the browser until you get the payment success message.')" Text="PAY" Style="line-height: .90" />
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <div class="container" runat="server">
        <div id="msgModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header " style="text-align: right;">
                        <p class="modal-title" style="font-weight: 600; color: #b90707">billGENIX FeedBack</p>
                    </div>
                    <div class="modal-body">
                        <p id="msgModalBodyParagraph" style="text-align: center; color: #e50d0d"></p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnClose" runat="server" Text="CLOSE" class="btn btn-danger" data-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">

                    <p class="modal-title" style="font-weight: 600; color: #b90707">Confirmation</p>
                </div>
                <div class="modal-body">
                    <p id="confirmMessage" style="text-align: center; color: #e50d0d"></p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Yes" runat="server" Text="Yes" UseSubmitBehavior="false" class="btn btn-primary" data-dismiss="modal" OnClick="btnYes_OnClick" />
                    <asp:Button ID="No" runat="server" Text="No" class="btn btn-danger" data-dismiss="modal" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
