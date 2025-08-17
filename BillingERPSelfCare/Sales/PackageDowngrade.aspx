<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PackageDowngrade.aspx.cs" Inherits="BillingERPSelfCare.Sales.PackageDowngrade" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
    </script>

    <style type="text/css">

        .Space label {
            margin-left: 10px;
            color: red;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                    <div class="card">

                        <div class="card-body">

                            <div class="form-row">
                                <div class="col-sm-5">
                                    <div class="form-group row gutters">
                                        <label for="txtCustomerID" class="col-sm-4 col-form-label">Customer ID</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCustomerID" class="form-control" runat="server" ReadOnly="true" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" Width="70%"></asp:TextBox>

                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCustomerID"
                                            ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-6">
                                    <div class="form-group row gutters">
                                        <label for="txtCustomerBalance" class="col-sm-4 col-form-label">Customer Balance</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCustomerBalance" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm" Style="color: red; font-weight: 800" Width="60%"></asp:TextBox>
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
                                                <label for="txtpackage" class="col-sm-4 col-form-label">Package</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtpackage" class="form-control" ReadOnly="true" runat="server" CssClass="form-control form-control-sm" Width="100%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtmrc" class="col-sm-4 col-form-label">MRC</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtmrc" class="form-control" ReadOnly="true" runat="server" CssClass="form-control form-control-sm" Width="100%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtDiscount" class="col-sm-4 col-form-label">Discount</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtDiscount" class="form-control" ReadOnly="true" runat="server" CssClass="form-control form-control-sm" Width="100%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtnetmrc" class="col-sm-4 col-form-label">NET MRC</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtnetmrc" class="form-control" ReadOnly="true" runat="server" CssClass="form-control form-control-sm" Width="100%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2"></div>

                                <div class="col-xl-5 col-lg-5 col-md-5 col-sm-5">
                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-row" style="padding-top: 10px">
                                                <h5 class="card-title" style="text-decoration: underline;">New Package</h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12" style="padding-top: 10px">
                                            <div class="form-group row gutters">
                                                <label for="cmbPackage" class="col-sm-4 col-form-label">Package<span style="color: red;">*</span></label>
                                                <div class="col-sm-8">
                                                    <telerik:RadComboBox ID="cmbPackage" DropDownWidth="260" HighlightTemplatedItems="true" Height="150px" class="form-control" runat="server" Skin="Silk" Width="80%"
                                                        EnableLoadOnDemand="true" AutoPostBack="true" OnSelectedIndexChanged="cmbPackage_SelectedIndexChanged" OnItemsRequested="cmbPackage_ItemsRequested" Filter="Contains">
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmbPackage"
                                                        ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtNewMRC" class="col-sm-4 col-form-label">MRC</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtNewMRC" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <asp:Label ID="Label1" runat="server" Style="color: #cf3c3f; font-weight: bold;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="dpEffectiveDate" class="col-sm-4 col-form-label">Effective Date<span style="color: red;">*</span></label>
                                                <div class="col-sm-8">
                                                    <telerik:RadDatePicker ID="dpEffectiveDate" runat="server" DateInput-ReadOnly="true" DatePopupButton-Enabled="false" Skin="Silk" Culture="en-US" MaxDate="3000-01-01" MinDate="1000-01-01" ShowPopupOnFocus="True" Width="80%">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-BackColor="#ee0000">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput AutoPostBack="false" DateFormat="d/M/yyyy" DisplayDateFormat="d/M/yyyy" LabelWidth="40%" ForeColor="#cc0000" runat="server" AutoCompleteType="Disabled" onkeypress="return false">
                                                        </DateInput>
                                                        <DatePopupButton />
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dpEffectiveDate" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-sm-12">
                                            <asp:Label ID="lblEffectiveDate" runat="server" style="color: #cf3c3f; font-weight: bold;"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group row gutters">
                                                <label for="txtnotes" class="col-sm-4 col-form-label">Notes</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtnotes" class="form-control" TextMode="MultiLine" AutoCompleteType="Disabled" runat="server" CssClass="form-control form-control-sm" Width="80%"></asp:TextBox>
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
                                        </span>I am certifying that the data (Package,MRC,Effective Date) provided in the system by me is accurate to the best of my knowledge and on the basis of my personal investigation and assessment. I am not influenced by anyone. Entry of any false data will make me personally liable.
                                    </p>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12" style="text-align: right">
                                    <asp:Button ID="btnSave" runat="server" Text="SUBMIT" OnClick="btnSave_Click" class="btn btn-primary" ValidationGroup="Save" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" id="MasterInfo">

                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                    <div class="card">
                        <div class="card-header">
                            <div>
                                <div style="text-align: left">
                                    <span style="font-weight: 600">Customer Info </span>
                                </div>

                            </div>
                        </div>
                        <div class="card-body">


                            <div class="form-row">
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtCustomerType" class="col-sm-4 col-form-label">Type</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtCustomerType" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtCustomerName" class="col-sm-4 col-form-label">Name</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtCustomerName" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtPop" class="col-sm-4 col-form-label">Pop</label>
                                        <div class="col-sm-7">

                                            <asp:TextBox ID="txtPop" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="form-row">
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtAddress" class="col-sm-4 col-form-label">Address</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtAddress" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtMobile" class="col-sm-4 col-form-label">Mobile</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtMobile" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtEmail" class="col-sm-4 col-form-label">Email</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtEmail" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtNationality" class="col-sm-4 col-form-label">Nationality</label>
                                        <div class="col-sm-7">

                                            <asp:TextBox ID="txtNationality" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtNIDPassport" class="col-sm-4 col-form-label">NID/Passport</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtNIDPassport" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtOccupation" class="col-sm-4 col-form-label">Occupation</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtOccupation" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
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
                    <div class="modal-header " style="float:right">
                       <%-- <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                        <p class="modal-title" style="font-weight: 600; padding-left:320px; color:#b90707 ">billGENIX FeedBack</p>
                    </div>
                    <div class="modal-body">
                        <p id="msgModalBodyParagraph" style="text-align: center; color:#e50d0d"></p>
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
