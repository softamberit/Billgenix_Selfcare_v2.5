<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PackageUpgradation.aspx.cs" Inherits="BillingERPSelfCare.Sales.PackageUpgradation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function Message(message) {
            $("#msgModal").modal("show");
            $('#msgModalBodyParagraph').html(message);
        }

        function ValidatorUpdateDisplay(val) {
            if (typeof (val.display) == "string") {
                if (val.display == "None") {
                    return;
                }
                if (val.display == "Dynamic") {
                    val.style.display = val.isvalid ? "none" : "inline";
                    return;
                }

            }
            val.style.visibility = val.isvalid ? "hidden" : "visible";
            if (val.isvalid) {
                document.getElementById(val.controltovalidate).style.border = '1px solid #e1e5f1';
            }
            else {
                document.getElementById(val.controltovalidate).style.border = '1px solid red';
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtCustomerID" class="col-sm-4 col-form-label">Customer ID</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCustomerID" class="form-control" runat="server" ReadOnly="true" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-3 col-md-3 col-sm-3">
                                </div>

                                <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtPresentBal" class="col-sm-4 col-form-label">Balance</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPresentBal" class="form-control" ReadOnly="true" runat="server" CssClass="form-control form-control-sm" style="color: red; font-weight: bold; font-size: large"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div ID="Messages" runat="server">
                                <div class="form-row" style="padding-top: 1%">
                                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                        <asp:Label runat="server" ID="lblMessageOne" Style="color: #cf3c3f; font-weight: bold; font-size: small"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-row" style="padding-bottom: 1%">
                                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                        <asp:Label runat="server" ID="lblMessageTwo" Style="color: #cf3c3f; font-weight: bold; font-size: small"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="dpEffectiveDate" class="col-sm-4 col-form-label">Effective Date<span style="color: red;">*</span></label>
                                        <div class="col-sm-8">
                                            <telerik:RadDatePicker ID="dpEffectiveDate" runat="server" AutoPostBack="true" OnSelectedDateChanged="dpEffectiveDate_SelectedDateChanged" Skin="Silk" Culture="en-US" ShowPopupOnFocus="True" Width="100%">
                                                <DateInput runat="server" DateFormat="d/M/yyyy" DisplayDateFormat="d/M/yyyy" AutoCompleteType="Disabled" onkeypress="return false">
                                                </DateInput>
                                            </telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator ID="rfd_dpEffectiveDate" runat="server" ControlToValidate="dpEffectiveDate" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-3 col-md-3 col-sm-3">
                                </div>

                                <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtPresentBal" class="col-sm-4 col-form-label">Notes</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtNotes" class="form-control" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                    <div class="card">
                        <div class="card-header"><span style="font-weight: 600">Package</span></div>
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4">
                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-row" style="padding-top: 10px">
                                                <h5 class="card-title" style="text-decoration: underline;">Existing Package</h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12" style="padding-top: 10px">
                                            <div class="form-group row gutters">
                                                <label for="txtCustomerName" class="col-sm-4 col-form-label">Cust Name</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtCustomerName" class="form-control" ReadOnly="true" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtExistingPackage" class="col-sm-4 col-form-label">Package</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtExistingPackage" class="form-control" ReadOnly="true" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtMRC" class="col-sm-4 col-form-label">MRC</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtMRC" class="form-control" ReadOnly="true" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtDiscount" class="col-sm-4 col-form-label">Discount</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtDiscount" class="form-control" ReadOnly="true" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtNetMRC" class="col-sm-4 col-form-label">NET MRC</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtNetMRC" class="form-control" ReadOnly="true" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2"></div>

                                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6">
                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-row" style="padding-top: 10px">
                                                <h5 class="card-title" style="text-decoration: underline;">New Package</h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtpackage" class="col-sm-4 col-form-label">Balance After Adjusment</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtBalAfterAdj" class="form-control" ReadOnly="true" style="font-weight: bold; font-size: large" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="cmbNewPackage" class="col-sm-4 col-form-label">New Package<span style="color: red;">*</span></label>
                                                <div class="col-sm-6">
                                                    <telerik:RadComboBox ID="cmbNewPackage" DropDownWidth="260" HighlightTemplatedItems="true" class="form-control" runat="server" Skin="Silk" Width="100%"
                                                        EnableLoadOnDemand="true" AutoPostBack="true" OnSelectedIndexChanged="cmbNewPackage_SelectedIndexChanged" OnItemsRequested="cmbNewPackage_ItemsRequested" Filter="Contains">
                                                        <HeaderTemplate>
                                                            <table style="width: 240px">
                                                                <tr>
                                                                    <td style="width: 50px; font-weight: 400;">ID</td>
                                                                    <td style="width: 150px; font-weight: 400;">Package</td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 240px">
                                                                <tr>
                                                                    <td style="width: 50px;"><%# DataBinder.Eval(Container, "Value")%></td>
                                                                    <td style="width: 150px; font-weight: 400;"><%# DataBinder.Eval(Container, "Text")%></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="rfd_cmbNewPackage" runat="server" ControlToValidate="cmbNewPackage"
                                                        ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtNewMRC" class="col-sm-4 col-form-label">MRC</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtNewMRC" class="form-control" runat="server" style="font-weight: bold; font-size: large" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtNewMRC" class="col-sm-4 col-form-label">Cummulative Balance</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtCummulativeBalalnce" class="form-control" runat="server" ReadOnly="true" style="font-weight: bold; font-size: large" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtMinReqBal" class="col-sm-4 col-form-label">Min. Required Amount</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtMinReqBal" class="form-control" ReadOnly="true" style="font-weight: bold; font-size: large; color: #cf3c3f" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row" style="padding-top: 30px">
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                    <p style="color: #cf3c3f; font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif">
                                        <span>
                                            <asp:CheckBox ID="chkDisclaimer" runat="server" Style="height: 55px" />
                                        </span>I am certifying that the data provided in the system by me is accurate to the best of my knowledge and on the basis of my personal investigation and assessment. I am not influenced by anyone. Entry of any false data will make me personally liable.
                                    </p>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12" style="text-align: right">
                                    <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" OnClick="btnSubmit_Click" class="btn btn-primary" ValidationGroup="Save" />
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
                            <div class="modal-header " style="float: right">
                                <p class="modal-title" style="font-weight: 600; padding-left: 320px; color: #b90707">billGENIX FeedBack</p>
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>