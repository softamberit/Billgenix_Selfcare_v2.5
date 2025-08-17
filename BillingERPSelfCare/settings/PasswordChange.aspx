<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PasswordChange.aspx.cs" Inherits="BillingERPSelfCare.Settings.PasswordChange" %>

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


    <script type="text/javascript">


</script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                    <div class="card">

                        <div class="card-body">

                            <div class="form-row">

                                <div class="col-sm-12">
                                    <div class="form-group row gutters">

                                        <div class="col-sm-12">

                                            <h4><font color="red">Please read the following instructions before change your Password</font></h4>
                                            <ol>
                                                <li>Minimum of <b>eight</b> characters in length</li>
                                                <li>At least one uppercase letter <b>(A-Z)</b> and one lowercase letter (a-z)</li>
                                                <li>At least one number <b>(0-9)</b></li>
                                                <li>At least one special character <b>(e.g., @, #, $, etc.)</b></li>
                                                <li>"New Password" and "Confirm Password" fields must be same.</li>
                                            </ol>
                                            <br />


                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-6">
                                    <div class="form-group row gutters">
                                        <label for="txtLoginId" class="col-sm-4 col-form-label">Login Id :</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtLoginId" class="form-control" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" Enabled="false" MaxLength="10"></asp:TextBox>

                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-6">
                                    <div class="form-group row gutters">
                                        <label for="txtCurrentPassword" class="col-sm-4 col-form-label">Current Password : <span style="color: red">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCurrentPassword" class="form-control" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" MaxLength="30"></asp:TextBox>

                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-6">
                                    <div class="form-group row gutters">
                                        <label for="txtNewPassword" class="col-sm-4 col-form-label">Type New Password : <span style="color: red">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtNewPassword" class="form-control" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" MaxLength="30"></asp:TextBox>
                                        </div>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" ErrorMessage="Password must be 8 characters or longer</br> with at least one uppercase character and one numeric character and one special Character" ForeColor="Red" ValidationExpression="^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$" ControlToValidate="txtNewPassword" runat="server" ValidationGroup="valSaveDB">
                                        </asp:RegularExpressionValidator>
                                    </div>

                                </div>
                            </div>


                            <div class="form-row">
                                <div class="col-sm-6">
                                    <div class="form-group row gutters">
                                        <label for="txtReTypeNewPassword" class="col-sm-4 col-form-label">Re-Type New Password : <span style="color: red">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtReTypeNewPassword" class="form-control" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" MaxLength="30"></asp:TextBox>

                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-6">

                                    <asp:Label ID="Label1" runat="server" Width="250px"></asp:Label>
                                    <asp:Label ID="Label2" runat="server" Width="250px"></asp:Label>
                                    <asp:Label ID="Label3" runat="server" Width="250px"></asp:Label>


                                </div>
                            </div>

                            <div style="text-align: right">

                                <asp:Button ID="Button1" runat="server" Text="SAVE" class="btn btn-primary" ValidationGroup="valSaveDB"
                                    OnClick="btnSave_Click" />

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
                                <p class="modal-title" style="font-weight: 600; padding-left: 320px; color: #ff0000">billGENIX FeedBack</p>
                            </div>
                            <div class="modal-body">
                                <p id="msgModalBodyParagraph" style="text-align: center; color: #ff0000"></p>
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
