<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="BillingERPSelfCare.Error" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">


    <link rel="shortcut icon" href="img/favicon.ico" />
    <title>Amber IT SelfCare</title>

    <!-- Common CSS -->
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="fonts/icomoon/icomoon.css" />

    <!-- Mian and Login css -->
    <link rel="stylesheet" href="css/main.css" />
    <style type="text/css">
        html body .riSingle .riTextBox[type="text"], html body .RadInput_Default .riTextBox {
            height: 34px !important;
        }
    </style>
</head>
<body class="login-bg">
    <div class="login-screen row align-items-center">
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
            <form id="form1" runat="server">
                <div class="container">
                    <b>Some Error Has Been Occured.</b><br /><hr />
                    <div>
                        <asp:Button ID="btnRedirect"  CssClass="btn btn-primary" runat="server" Text="Redirect to Dashboard" OnClick="btnRedirect_Click" />
                    </div>
                    
                </div>
            </form>
        </div>
    </div>
    <footer class="main-footer no-bdr fixed-btm">
        <div class="container">
            Copyright &copy 2019 Amber Software Solutions Ltd.
        </div>
    </footer>
</body>
</html>
