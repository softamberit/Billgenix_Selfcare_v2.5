<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerDetail.aspx.cs" Inherits="BillingERPSelfCare.Sales.CustomerDetail" %>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                <div class="card">
                    <div class="card-body">
                        <div class="form-row">
                          <%--  <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtCustomerID" class="col-sm-4 col-form-label">Customer ID</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtCustomerID" runat="server" ReadOnly="true" CssClass="form-control form-control-sm" ValidationGroup="Search" onkeypress="return isNumberKey(event)" Style="color: red" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtCustomerID" runat="server" ID="rfd_txtCustomerID" ValidationGroup="Search" />
                                    </div>
                                </div>
                            </div>--%>
                            <div class="col-sm-8" style="border:groove; color:#ff0000">
                                            <div class="form-group row gutters">
                                                <label for="txtCustomerMKStatus" class="col-sm-2 col-form-label" style="color:#000000; font-weight:600">Status</label>
                                                <div class="col-sm-10" style="padding-top:7px">
                                                   <asp:Label runat="server" ID="lbDiscontinueSource" Font-Bold="true" Font-Size="12" style="text-align:left; vertical-align:central"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtBalance" class="col-sm-4 col-form-label">Balance</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtBalance" runat="server" ReadOnly="true" CssClass="form-control form-control-sm" Style=" color: red; font-weight:800"></asp:TextBox>
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
                        <h5 class="card-title" style="text-decoration: underline;">Customer Info</h5>
                        <div class="form-row">
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtCustomerType" class="col-sm-4 col-form-label">Type</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtCustomerType" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtCustomerName" class="col-sm-4 col-form-label">Name</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtContactName" class="col-sm-4 col-form-label">Cont. Name</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtContactName" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtAddress" class="col-sm-4 col-form-label">Address</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtAddress" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtMobile" class="col-sm-4 col-form-label">Mobile</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtMobile" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtEmail" class="col-sm-4 col-form-label">Email</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtEmail" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtNationality" class="col-sm-4 col-form-label">Nationality</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtNationality" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtNIDPassport" class="col-sm-4 col-form-label">NID/Passport</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtNIDPassport" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtOccupation" class="col-sm-4 col-form-label">Occupation</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtOccupation" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtPop" class="col-sm-4 col-form-label">Pop</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtPop" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtSalesDate" class="col-sm-4 col-form-label">Sales Date</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtSalesDate" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtNotes" class="col-sm-4 col-form-label">Notes</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-row" style="padding-top: 10px">
                            <h5 class="card-title" style="text-decoration: underline;">Package Details</h5>
                        </div>

                        <div class="form-row">
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtPackage" class="col-sm-4 col-form-label">Package</label>
                                        <div class="col-sm-8">

                                            <asp:TextBox ID="txtPackage" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label class="col-sm-4 col-form-label">Package MRC</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPackageMRC" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label class="col-sm-4 col-form-label">Package OTC</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPackageOTC" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtCollection" class="col-sm-4 col-form-label">Coll. Mode</label>
                                        <div class="col-sm-8">

                                            <asp:TextBox ID="txtCollection" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4" id="discount" runat="server">
                                    <div class="form-group row gutters">
                                        <label for="txtDiscount" class="col-sm-4 col-form-label">Disc. Amount</label>
                                        <div class="col-sm-8">
                                            <div>
                                                <asp:TextBox ID="txtDiscount" class="form-control" AutoCompleteType="Disabled" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                               <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtOTCVat" class="col-sm-4 col-form-label">OTC Vat</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtOTCVat" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                               
                            </div>

                            <div class="form-row">

                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtRefNo" class="col-sm-4 col-form-label">Ref No.</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtRefNo" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtMRC" class="col-sm-4 col-form-label">Total MRC</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtMRC" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtOTC" class="col-sm-4 col-form-label">OTC</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtOTC" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                               
                                
                            </div>

                            <div class="form-row">
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtAmnt" class="col-sm-4 col-form-label">Amount</label>
                                        <div class="col-sm-8">

                                            <asp:TextBox ID="txtAmnt" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm" Style="color: red; font-weight: 800"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label class="col-sm-4 col-form-label">MRC VAT</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtMRCVat" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4"></div>
                                
                            </div>


                            <div class="form-row" runat="server">

                                <div class="col-sm-4"></div>
                                <div class="col-sm-4">
                                    <div class="form-group row gutters">
                                        <label for="txtNetMRC" class="col-sm-4 col-form-label">NET MRC</label>
                                        <div class="col-sm-8">

                                            <asp:TextBox ID="txtNetMRC" class="form-control" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4"></div>
                            </div>
                    </div>
                </div>
            </div>

     
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                <div class="card">
                    <div class="card-header" style="font-weight: 600">Handover Info</div>
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-sm-4">
                                <div class="form-group row gutters">
                                    <label for="txtPhysicalDate" class="col-sm-4 col-form-label">Date</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtHandoverDate" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
