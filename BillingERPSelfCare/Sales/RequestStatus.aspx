<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequestStatus.aspx.cs" Inherits="BillingERPSelfCare.Sales.RequestStatus" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <style>
        .form-group {
            margin-bottom: .2rem !important;
        }

        .pad {
            padding-top: 10px;
        }

        .labelFont {
            font-weight: bold;
        }

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
    </script>

      <asp:UpdatePanel runat="server">

        <ContentTemplate>

             <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12" runat="server" style="overflow: scroll" id="DivGrid">
                <div class="card" >
                        <div class="card-body" style="width:100%">              
                    <telerik:RadGrid ID="grdRequestList" runat="server" CellSpacing="0" GridLines="None" Height="100%" Skin="Metro" Width="100%"
                        AllowPaging="true"
                        PageSize="10">
                        <ClientSettings>
                        </ClientSettings>
                        <MasterTableView DataKeyNames="CustomerID" HeaderStyle-Font-Bold="false" AutoGenerateColumns="true" HeaderStyle-Font-Size="Medium">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderStyle-Width="20px">
                                    <ItemTemplate>
                                        
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>

                    </telerik:RadGrid>
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
