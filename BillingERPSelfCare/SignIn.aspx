<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="BillingERPSelfCare.SignIn" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">


    <link rel="shortcut icon" href="img/favicon.ico" />
    <title>My Swift | Amber IT</title>

    <!-- Common CSS -->
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="fonts/icomoon/icomoon.css" />

        <script src="<%=ResolveUrl("js/jquery.js")%>"></script>


    <!-- Mian and Login css -->
    <link rel="stylesheet" href="css/main.css" />
    <style type="text/css">
        html body .riSingle .riTextBox[type="text"], html body .RadInput_Default .riTextBox {
            height: 34px !important;
        }

        .toggle-password {
            float: right;
            position: relative;
            z-index: 2;
        }

        .input-group-addon {
            color: #2b2e32;
        }
    </style>

    <script>    
        function FunctionShowOtp() {
            debugger;
            var x = document.getElementById("txtOtp");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }
        function FunctionShowPassword() {

            var x = document.getElementById("txtPassword");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }

        $(".toggle-password").click(function () {

            $(this).toggleClass("icon-eye icon-eye-blocked");

        });

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function CheckCheckboxes() {

            if (document.getElementid('CheckBox1').checked == true) {
                document.getElementid('CheckBox2').checked = false;
            }
            else if (document.getElementid('CheckBox2').checked == true) {
                document.getElementid('CheckBox1').checked = false;
            }

        }
    </script>
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
                                    <a href="https://www.amberit.com.bd/" class="login-logo">
                                        <img src="img/SelfCare_logo.png" alt="Amber IT Limited" />
                                    </a>
                                    <div runat="server" id="divlogin">
                                        <div class="input-group">
                                            <span class="input-group-addon" id="username"><i class="icon-account_circle"></i></span>

                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" AutoCompleteType="Disabled" placeholder="CID" aria-label="username" aria-describedby="username" Width="100%" MaxLength="10">
                                            </asp:TextBox>
                                        </div>
                                        <div class="input-group">
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="vg" runat="server" ControlToValidate="txtUserName" ErrorMessage="CID is Required" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <br>
                                        <%--<div class="input-group">
                                            <span class="input-group-addon" id="prefix88" style="background-color: #F5F6FA; padding: 6px 6px; color: #333;">+88
                                        </span>
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" AutoCompleteType="Disabled" placeholder="MobileNo"
                                                aria-label="MobileNo" aria-describedby="MobileNo" Width="100%" MaxLength="11" />
                                        </div>
                                        <div class="input-group">
                                            <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="vg" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Mobile No Required" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="actions clearfix">
                                            <asp:Button ID="btnOtp" runat="server" CssClass="btn btn-primary" Text="Send OTP for Login" ValidationGroup="vg" OnClick="btnOtp_Click" />
                                        </div>
                                        <div class="input-group">
                                            <asp:Label ID="LoginMsg" runat="server" ForeColor="Red"></asp:Label>
                                        </div>--%>
                                        <div class="input-group">
                                            <span class="input-group-addon" id="password"><i class="icon-verified_user"></i></span>

                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" AutoCompleteType="Disabled" placeholder="Password" aria-label="Password" aria-describedby="password" TextMode="Password" Width="100%" MaxLength="100">
                                            </asp:TextBox>
                                            <span toggle="#txtPassword" class="input-group-addon password-toggle-icon " onclick="FunctionShowPassword()"><i class="toggle-password icon-eye"></i></span>
                                        </div>
                                        <div class="input-group">
                                            <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="vg" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password Required" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="input-group">

                                            <asp:RadioButton ID="rdoVoice" runat="server" GroupName="OTPSMS" Checked="true" />
                                            Send OTP via Voice Call
                                        </div>
                                        <div class="input-group">
                                            <asp:RadioButton ID="rdoSMS" GroupName="OTPSMS" runat="server" />Send OTP via SMS
                                            
                                        </div>

                                        <br />
                                        <div class="">
                                            <asp:Button ID="btnOtp" runat="server" CssClass="btn btn-primary" Text="Send" ValidationGroup="vg" OnClick="btnOtp_Click" />
                                        </div>
                                        <%-- <div class="actions clearfix">
                                            <asp:Button ID="btnOtpByVoice" runat="server" CssClass="btn btn-primary" Text="Send OTP via Voice Call" ValidationGroup="vg" OnClick="btnOtpByVoice_Click" />
                                        </div>--%>
                                        <div class="input-group">
                                            <asp:Label ID="LoginMsg" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="actions clearfix">
                                            <asp:LinkButton ID="myBtn" runat="server" Text="Forget Password" OnClick="btnForgetPass_Click" />
                                        </div>
                                    </div>
                                    <div runat="server" id="divotp">
                                        <label>Check your registered mobile number</label>
                                        <div class="input-group">
                                            <span class="input-group-addon" id="otp"><i class="icon-account_circle"></i></span>

                                            <asp:TextBox ID="txtOtp" runat="server" CssClass="form-control" AutoCompleteType="Disabled" placeholder="OTP" aria-label="otp" aria-describedby="otp" Width="100%" MaxLength="10" onkeypress="return isNumberKey(event)">
                                            </asp:TextBox>
                                        </div>
                                        <div class="input-group">
                                            <asp:RequiredFieldValidator ID="rfv3" ValidationGroup="vg" runat="server" ControlToValidate="txtOtp" ErrorMessage="OTP Required" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <br />

                                        <div class="input-group">
                                            <asp:Label ID="OtpMsg" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                        <br>
                                        <div>
                                            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="Verify" ValidationGroup="vg" OnClick="btnLogin_Click" />
                                            <asp:Button ID="Button2" runat="server" CssClass="btn" Text="Back" OnClick="btnBack_Click" />
                                        </div>

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
            Copyright &copy 2024 <a href="https://www.amberit.com.bd/" target="_blank">Amber IT Limited</a>
        </div>
    </footer>
</body>
</html>
