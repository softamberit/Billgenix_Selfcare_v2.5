<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="BillingERPSelfCare.ResetPassword" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html lang="en">

<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">


    <%--<link rel="shortcut icon" href="img/favicon.png" />--%>
    <link rel="shortcut icon" href="<%=ResolveUrl("img/favicon.png")%>" />
    <title>Billgenix v2.0</title>

    <!-- Common CSS -->
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="fonts/icomoon/icomoon.css" />

    <!-- Mian and Login css -->
    <link rel="stylesheet" href="css/main.css" />
    <style type="text/css">
        html body .riSingle .riTextBox[type="text"], html body .RadInput_Default .riTextBox {
            height: 34px !important
        }
    </style>

</head>
<body class="login-bg">
    <div class="container">
        <div class="login-screen row align-items-center">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                <form runat="server" idd="frmLogin">
                    <asp:ScriptManager ID="scrManager1" runat="server"></asp:ScriptManager>

                    <div class="login-container">
                        <div class="row no-gutters">
                            <div class="col-xl-4 col-lg-5 col-md-6 col-sm-12">
                                <div class="login-box">
                                    <a href="#" class="login-logo">
                                        <img src="img/logo.png" alt="Amber IT Ltd." />
                                    </a>

                                    <div class="col-md-12">
                                        <h6><font color="red">Please read the following instructions before edit your Password</font></h6>
                                        <ol>
                                            <li>Minimum of <b>eight</b> characters in length</li>
                                            <li>At least one uppercase letter <b>(A-Z)</b> and one lowercase letter (a-z)</li>
                                            <li>At least one number <b>(0-9)</b></li>
                                            <li>At least one special character <b>(e.g., @, #, $, etc.)</b></li>
                                            <li>"New Password" and "Confirm Password" fields must be same.</li>
                                        </ol>


                                        <br />


                                    </div>

                                    <div class="input-group">

                                        <asp:TextBox TextMode="Password" ID="txtNewPassword" runat="server" CssClass="form-control" AutoCompleteType="Disabled" placeholder="New Password" aria-label="newpassword" aria-describedby="newpassword" Width="100%" MaxLength="30">
                                        </asp:TextBox>


                                    </div>
                                    <br>
                                    <div class="input-group">

                                        <asp:TextBox TextMode="Password" ID="txtConfirmPassword" runat="server" CssClass="form-control" AutoCompleteType="Disabled" placeholder="Confirm Password" aria-label="confirmpassword" aria-describedby="confirmpassword" Width="100%" MaxLength="30">
                                        </asp:TextBox>


                                    </div>
                                    <div class="input-group">
                                        <asp:Label ID="LoginMsg" runat="server" ForeColor="#da1113"></asp:Label>
                                    </div>

                                    <div class="actions clearfix">

                                        <asp:Button Visible="true" ID="btnResetPass" runat="server" CssClass="btn btn-primary" Text="Reset Password" ValidationGroup="vg" OnClick="btnResetPass_Click" />
                                        <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="Back" OnClick="btnBack_Click" />
                                    </div>




                                </div>
                            </div>
                            <div class="col-xl-8 col-lg-7 col-md-6 col-sm-12">
                                <div class="login-slider"></div>
                            </div>
                        </div>
                    </div>

                </form>
            </div>
        </div>
    </div>



    <footer class="main-footer no-bdr fixed-btm">
        <div class="container">
            Copyright &copy 2021 Amber IT Ltd.
        </div>
    </footer>
</body>
</html>
