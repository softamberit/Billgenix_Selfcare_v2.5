<% @Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Success1.aspx.cs" Inherits="BillingERPSelfCare.Success1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 
            <table>


                <tr>
                    <td></td>
                    <td></td>
                    <td>&nbsp;</td>
                    <td></td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td></td>
                    <td>&nbsp;</td>
                    <td></td>
                    <td></td>
                </tr>


            </table>

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
