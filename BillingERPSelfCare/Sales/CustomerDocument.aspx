<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerDocument.aspx.cs" Inherits="BillingERPSelfCare.Sales.CustomerDocument" %>

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


        //(function ($) {
        //    requestStart = function (target, arguments) {
        //        if (arguments.get_eventTarget().indexOf('ButtonSend') > -1) {
        //            arguments.set_enableAjax(false);
        //        }
        //    };
        //})($telerik.$);

        //function validate() {
        //    var maxfilesize = 1024 * 1024,  // 1 Mb
        //        filesize = el.files[0].size,
        //        warningel = document.getElementById('lbError');

        //    if (filesize > maxfilesize) {
        //        warningel.innerHTML = "File too large: " + filesize + ". Maximum size: " + maxfilesize;
        //        return false;
        //    }
        //    else {
        //        warningel.innerHTML = '';
        //        return true;
        //    }
        //}

        //function uploadFile() {
           
        //    var fileInput = document.getElementById('PictureUpload');

        //    var filesize = (fileInput.files[0].size);
        //    if (filesize > 1000000) {
        //        /* $('#fileMsg').html('Invalid File Size');*/
        //        $("#msgModal").modal("show");
        //        $('#msgModalBodyParagraph').html('file');
        //    } else {
               
        //    }

        //}



    </script>


    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
    <contenttemplate>

        <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="requestStart"
                    LoadingPanelID="LoadingPanel1">--%>



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
                                 <label class="col-sm-4 col-form-label">Occupation<span style="color: #cf3c3f">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="cmbOccupation" runat="server"  EnableLoadOnDemand="true" Filter="Contains" 
                                                HighlightTemplatedItems="True" DropDownWidth="250px" Skin="Metro" Width="100%">
                                               <%-- <HeaderTemplate>
                                                    <table style="width: 210px">
                                                        <tr>
                                                            <td style="width: 60px; font-weight: bold">ID</td>
                                                            <td style="width: 150px; font-weight: bold">Name</td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 210px">
                                                        <tr>
                                                            <td style="width: 60px;"><%# DataBinder.Eval(Container, "Value")%></td>
                                                            <td style="width: 150px;"><%# DataBinder.Eval(Container, "Text")%></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="cmbOccupation" runat="server" ID="rfd_cmbOccupation" ValidationGroup="Save" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-row">

                        <div class="col-sm-4">
                            <div class="form-group row gutters">
                                <label for="txtNID" class="col-sm-4 col-form-label">NID No.<span style="color: #cf3c3f">*</span></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtNID" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtNID" runat="server" ID="RequiredFieldValidator1" ValidationGroup="Save" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-row">

                        <div class="col-sm-4">
                            <div class="form-group row gutters">
                                <label for="PictureUpload" class="col-sm-4 col-form-label">Your Photo<span style="color: #cf3c3f">*</span></label>
                                <div class="col-sm-8">
                                      <asp:FileUpload ID="PictureUpload" runat="server" Width="200px"/>

                                   <%-- <telerik:RadUpload ID="PictureUpload" runat="server" MaxFileInputsCount="1" OverwriteExistingFiles="true"
                                        ControlObjectsVisibility="RemoveButtons" Skin="Silk"  OnValidatingFile="PictureUpload_ValidatingFile" >
                                    </telerik:RadUpload>--%>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="col-sm-4">
                            <div class="form-group row gutters">
                                <label for="NIDUpload" class="col-sm-4 col-form-label">Your NID<span style="color: #cf3c3f">*</span></label>
                                <div class="col-sm-8">
                                    <asp:FileUpload ID="NIDUpload" runat="server" Width="200px" />

                                    <%--<telerik:RadUpload ID="NIDUpload" runat="server" MaxFileInputsCount="1" OverwriteExistingFiles="true"
                                      Skin="Silk" ControlObjectsVisibility="RemoveButtons" OnValidatingFile="NIDUpload_ValidatingFile">
                                    </telerik:RadUpload>--%>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-sm-4">
                            <div class="form-group row gutters">
                                <label for="OfficeIDUpload" class="col-sm-4 col-form-label">Your Office ID</label>
                                <div class="col-sm-8">
                                    <asp:FileUpload ID="OfficeIDUpload" runat="server" Width="200px" />
                                    <br />
                                    <span style="font-weight: bold; color: red; font-size: smaller">File size must be less than 1 MB</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="text-align: right">

                        <asp:Button ID="btnSubmitDoc" runat="server" Text="SUBMIT" OnClick="btnSave_Click" class="btn btn-primary" ValidationGroup="Save" />
                    </div>
                </div>
            </div>
        </div>

        <%--<div runat="server" id="CustomerListBody">
                        <div class="row">--%>
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12" id="CustomerListBody" runat="server">
            <div class="card">

                <div class="card-body">

                    <div class="row gutters">
                        <div class="col-sm-12">

                            <telerik:RadGrid ID="grdShowList" runat="server" CellSpacing="0" GridLines="None" Skin="Metro" Width="100%"
                                AutoGenerateColumns="false">


                                <MasterTableView TableLayout="Fixed" CellPadding="0" CellSpacing="0">

                                    <Columns>
                                        <%--<telerik:GridTemplateColumn AllowFiltering="false" ColumnGroupName="Actions" HeaderText="" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <telerik:RadButton ID="btnDelete" OnClick="btnDelete_Click" runat="server" Skin="Silk" Text="D" Width="50px"  Font-Italic="False">
                                                    </telerik:RadButton>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>--%>

                                        <telerik:GridTemplateColumn AllowFiltering="false" ColumnGroupName="Actions" HeaderText="" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <telerik:RadButton ID="btndownload" OnClick="btndownload_Click" runat="server" Skin="Silk" Text="Download" Width="100px" Font-Italic="False">
                                                </telerik:RadButton>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn DataField="DocName" HeaderText="Document" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" UniqueName="DocName">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="DocPath" HeaderText="Doc Path" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" UniqueName="DocPath">
                                        </telerik:GridBoundColumn>

                                    </Columns>
                                </MasterTableView>

                            </telerik:RadGrid>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--</div>
                    </div>--%>


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
        <%--</telerik:RadAjaxPanel>
                <telerik:RadAjaxLoadingPanel ID="LoadingPanel1" runat="server" InitialDelayTime="0" Skin="Default">
                </telerik:RadAjaxLoadingPanel>--%>
    </contenttemplate>
    <%-- </asp:UpdatePanel>--%>
</asp:Content>
