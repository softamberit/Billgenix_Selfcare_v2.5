<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassOtp.aspx.cs" Inherits="BillingERPSelfCare.ResetPassOtp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html lang="en">

<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">



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
<script>
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }
</script>
<body class="login-bg">

    <div class="container">
        <div class="login-screen row align-items-center">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                <form runat="server" idd="frmLogin">
                    <asp:ScriptManager ID="scrManager1" runat="server"></asp:ScriptManager>

                    <div class="login-container">
                        <div class="row no-gutters login-box">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 ">

                                    <div class="row">


                                        <div class="">
                                            <a href="#" class="login-logo">
                                                <img src="img/logo.png" alt="Amber IT Ltd." />
                                            </a>

                                            <div class="input-group">

                                                <asp:TextBox ID="txtUserNames" runat="server" CssClass="form-control" AutoCompleteType="Disabled" placeholder="Enter your CID" aria-label="loginid" aria-describedby="loginid" Width="100%" MaxLength="10">
                                                </asp:TextBox>


                                            </div>
                                            <div></div>
                                            <br />


                                            <div class="input-group">

                                                <%-- <telerik:RadTextBox Visible="false" ID="txtPhnMasks" runat="server" CssClass="form-control" ReadOnly="true" AutoCompleteType="Disabled" placeholder="Phone Number" aria-label="Phone Number" aria-describedby="Phone Number" Width="100%">
                                            </telerik:RadTextBox>--%>

                                                <%--<telerik:RadMaskedTextBox ID="txtPhnMaskss" AutoPostBack="true" Visible="false" runat="server"  DisplayFormatPosition="left" 
                                                    DisplayMask="###########" Mask="########" Enabled="False" OnTextChanged="txtPhnMaskss_TextChanged">
                                                </telerik:RadMaskedTextBox>--%>
                                                <div class="input-group" runat="server" id="divPhnMaskss">
                                                    <asp:TextBox ID="txtPhnMaskss" runat="server" CssClass="form-control" Enabled="False" AutoCompleteType="Disabled" Width="100%">
                                                    </asp:TextBox>
                                                </div>
                                                <%--<div class="input-group">
                                                    <telerik:RadMaskedTextBox 
                                                        ID="txtPhnMaskss" 
                                                        AutoPostBack="true" 
                                                        Visible="false" 
                                                        runat="server" 
                                                        Enabled="False" 
                                                        OnTextChanged="txtPhnMaskss_TextChanged">
                                                    </telerik:RadMaskedTextBox>
                                                </div>--%>
                                            </div>
                                            <br>
                                            <div class="input-group">

                                                <asp:TextBox Visible="false" ID="txtConfirmMobiles" runat="server" MaxLength="11" CssClass="form-control" AutoCompleteType="Disabled" placeholder="Phone Number" aria-label="Phone Number" aria-describedby="Phone Number" Width="100%" onkeypress="return isNumberKey(event)">
                                                </asp:TextBox>



                                            </div>

                                            <div class="input-group">

                                                <asp:TextBox Visible="false" ID="txtVerifyCodes" runat="server" CssClass="form-control" AutoCompleteType="Disabled" placeholder="Verification Code" aria-label="Verification Code" aria-describedby="Verification Code" Width="100%" MaxLength="10" onkeypress="return isNumberKey(event)">
                                                </asp:TextBox>


                                            </div>



                                            <div class="input-group">
                                                <asp:Label ID="LoginMsg" runat="server" ForeColor="#da1113"></asp:Label>
                                            </div>


                                        </div>


                                    </div>

                                </div>
                                <div>
                                    <div id="divSMSOption" runat="server">
                                        <div class="input-group">
                                            <asp:RadioButton ID="rdoSMS" GroupName="OTPSMS" runat="server" />Send OTP via SMS
       
                                        </div>
                                        <div class="input-group">

                                            <asp:RadioButton ID="rdoVoice" runat="server" GroupName="OTPSMS" />
                                            Send OTP via Voice Call
                                        </div>
                                     
                                            <asp:Button ID="btnOtp" runat="server" CssClass="btn btn-primary" Text="Submit" ValidationGroup="vg" OnClick="btnSubmitNumber_Click" />
                                        
                                    </div>

                                    <asp:Button Visible="false" ID="btnSubmitCode" runat="server" CssClass="btn btn-primary" Text="Submit OTP" ValidationGroup="vg" OnClick="btnSubmitCode_Click" />
                                    <%-- <asp:Button Visible="false" ID="btnSubmitNumbers" runat="server" CssClass="btn btn-primary" Text="Send OTP(SMS)" OnClick="btnSubmitNumber_Click" />
                                    <asp:Button Visible="false" ID="btnSubmitNumbersCall" runat="server" CssClass="btn btn-primary" Text="Send OTP(Call)" OnClick="btnSubmitNumberCall_Click" />--%>
                                    <asp:Button ID="btnSearchUserNames" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearchUserName_Click" />
                                    <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="Back" OnClick="btnBack_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                </form>
            </div>
        </div>
    </div>



    <footer class="main-footer no-bdr fixed-btm">
        <div class="container">
            Copyright &copy 2024 Amber IT Ltd.
        </div>
    </footer>
</body>
</html>
