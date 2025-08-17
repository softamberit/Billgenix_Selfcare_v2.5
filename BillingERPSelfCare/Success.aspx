<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="BillingERPSelfCare.Success" %>
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




    <div class="row-fluid sortable">
                <div class="box span8">
                    <div class="box-header well">
                        <h3><i class="icon-bell"></i> Payment Info</h3>                  
                    </div>
                    <div class="box-content alerts">

                        <div class="alert alert-success"  id="divSuccess" runat="server">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <strong>
                                <asp:Label ID="lblHeading" CssClass="" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                            </strong>
                            <asp:Label ID="lblStatus" CssClass="redLebel" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                        </div>
                        <div class="alert alert-info " id="divWarning" runat="server">
                             <button type="button" class="close" data-dismiss="alert">×</button>
                            <strong>
                                <asp:Label ID="Label1" CssClass="" ClientIDMode="Static" runat="server" Text="Oh snap!"></asp:Label>
                            </strong>
                            <asp:Label ID="lblWarning" CssClass="redLebel" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblNull" CssClass="redLebel" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblTryCatch" CssClass="redLebel" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblurl" CssClass="redLebel" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                        </div>
                    </div>
                    <div style="text-align: right; float: left; width: 100%">
                        <%--<input type="button" class="myButton" value="Pay" onclick="SaveData()" />--%>
                        <asp:Button ID="btnGoBack" CssClass="btn btn-info" runat="server" OnClick="GoBack_Click" Text="Go Back" Visible="false" />
                    </div>
                </div>
                <!--/span-->


            </div>

    
</asp:Content>
