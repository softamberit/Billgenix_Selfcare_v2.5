<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangeRequest.aspx.cs" Inherits="BillingERPSelfCare.Sales.ChangeRequest" %>

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
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
        <div class="card">
            <div class="card-body">
                <div class="form-row">

                    <div class="col-sm-6">
                        <div class="form-group row gutters">
                            <label for="txtCustomerId" class="col-sm-2 col-form-label" style="color: #000000; font-weight: 600">Customer ID</label>
                            <div class="col-sm-8" style="padding-top: 7px">
                                <asp:TextBox ID="txtCustomerId" runat="server" ReadOnly="true" CssClass="form-control form-control-sm" Style="color: red; font-weight: 800"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group row gutters">
                            <label for="txtBalance" class="col-sm-4 col-form-label">Balance</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtBalance" runat="server" ReadOnly="true" CssClass="form-control form-control-sm" Style="color: red; font-weight: 800"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-row">

                    <div class="col-sm-6">
                        <div class="form-group row gutters">
                            <label for="dpEffectiveDate" class="col-sm-2 col-form-label" style="color: #000000; font-weight: 600">Effective Date</label>
                            <div class="col-sm-8" style="padding-top: 7px">
                                <telerik:RadDatePicker ID="dpEffectiveDate" runat="server" AutoPostBack="true" OnSelectedDateChanged="dpEffectiveDate_SelectedDateChanged" Skin="Silk" Culture="en-US" ShowPopupOnFocus="True" Width="100%">
                                    <DateInput runat="server" DateFormat="d/M/yyyy" DisplayDateFormat="d/M/yyyy" AutoCompleteType="Disabled" onkeypress="return false">
                                    </DateInput>
                                </telerik:RadDatePicker>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group row gutters">
                            <label for="txtTotalPayable" class="col-sm-4 col-form-label">Total Payable</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtTotalPayable" runat="server" ReadOnly="true" CssClass="form-control form-control-sm" Style="color: red; font-weight: 800"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="Messages" runat="server">
                    <div class="form-row" style="padding-bottom: 1%">
                        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                            <asp:Label ID="lblEffectiveDate" runat="server" Style="color: #cf3c3f; font-weight: bold;"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="form-row">

                    <div class="col-sm-6"></div>
                    <div class="col-sm-6">
                        <div class="form-group row gutters">
                            <label for="txtNote" class="col-sm-4 col-form-label">Note</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
        <div class="card">
            <%--<div class="card-header" style="font-weight: 600">Approval Info</div>--%>
            <div class="card-body">
                <%-- <h5 class="card-title" style="text-decoration: underline;">Customer Info</h5>--%>
                <div class="form-row">
                    <div class="col-sm-4">
                        <div class="form-group row gutters">
                            <label class="col-sm-4 col-form-label">Package Name</label>
                            <div class="col-sm-8">
                                <asp:Label runat="server" ID="lblPackageName" Font-Bold="true" Font-Size="12" Style="text-align: left; vertical-align: central"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group row gutters">
                            <label class="col-sm-4 col-form-label">Acquired Services</label>
                            <div class="col-sm-8">
                                <asp:Label runat="server" ID="lblAcquiredService" Font-Bold="true" Font-Size="12" Style="text-align: left; vertical-align: central"></asp:Label>
                            </div>
                        </div>

                    </div>
                    <div class="col-sm-2">
                        <div class="form-group row gutters">
                            <label class="col-sm-4 col-form-label">Add/Remove Services</label>
                            <div class="col-sm-8">
                                <asp:CheckBox ID="chkAddRemoveServices" runat="server" AutoPostBack="True" OnCheckedChanged="chkAddRemoveServices_OnCheckedChanged"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


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
                        <div class="form-row">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                <div class="form-group row gutters">
                                    <label for="txtServiceCharge" class="col-sm-4 col-form-label">Service Charge</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtServiceCharge" class="form-control" ReadOnly="true" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                <div class="form-group row gutters">
                                    <telerik:RadGrid ID="grdServiceList" runat="server" Skin="Metro" CellSpacing="0" GridLines="None">
                                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="SERVICEID" HeaderStyle-ForeColor="White">

                                            <Columns>

                                                <telerik:GridBoundColumn DataField="ServiceID" HeaderText="Service ID" UniqueName="ServiceID" Visible="false">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="ServiceName" HeaderText="Service Name" UniqueName="ServiceName">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ServicePrice" HeaderText="Service Price" UniqueName="ServicePrice">
                                                </telerik:GridBoundColumn>

                                            </Columns>

                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2"></div>

                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6">

                        <div class="form-row">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                <div class="form-row" style="padding-top: 10px">
                                    <h5 class="card-title" style="text-decoration: underline;">New Custom Service</h5>
                                </div>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                <div class="form-group row gutters">
                                    <label for="txtServiceChargeNew" class="col-sm-4 col-form-label">Service Charge</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtServiceChargeNew" class="form-control" runat="server" ReadOnly="true" Style="font-weight: bold; font-size: large" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                <div class="form-group row gutters">
                                    <telerik:RadGrid ID="grdServiceListNew" runat="server" Skin="Metro" CellSpacing="0" GridLines="None">
                                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="SERVICEID" HeaderStyle-ForeColor="White">

                                            <Columns>

                                                <telerik:GridBoundColumn DataField="SERVICEID" HeaderText="SERVICE ID" UniqueName="SERVICEID">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="SERVICENAME" HeaderText="SERVICE NAME" UniqueName="SERVICENAME">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ServicePrice" HeaderText="Price" UniqueName="ServicePrice">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridTemplateColumn UniqueName="CheckBox">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chbIsActive" runat="server" OnCheckedChanged="chbSelectAll_CheckedChanged" AutoPostBack="True" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="chbIsActiveAll" runat="server" Text="IS ACTIVE" />
                                                    </HeaderTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridBoundColumn DataField="IsActive" HeaderText="Status"
                                                    Visible="true" UniqueName="IsActive">
                                                </telerik:GridBoundColumn>

                                            </Columns>

                                        </MasterTableView>
                                    </telerik:RadGrid>
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

    <div class="container" runat="server">
        <div id="msgModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <p class="modal-title" style="font-weight: 600">System FeedBack</p>
                    </div>
                    <div class="modal-body">
                        <p id="msgModalBodyParagraph" style="text-align: center"></p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger" data-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
