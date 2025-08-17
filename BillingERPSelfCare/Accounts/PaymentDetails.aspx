<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentDetails.aspx.cs" Inherits="BillingERPSelfCare.Accounts.PaymentDetails" %>

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

            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12" runat="server" style="overflow: scroll" id="DivGrid">
                <div class="card" style="width:100%">
                    <div class="card-body" >
                        
                        <telerik:RadGrid ID="grdRequestList" runat="server" CellSpacing="0" GridLines="None" Height="100%" Skin="Silk" Width="100%"
                            AllowPaging="true"  OnNeedDataSource="grdRequestList_NeedDataSource" AllowFilteringByColumn="true"
                            PageSize="10">
                          
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView HeaderStyle-Font-Bold="false" AutoGenerateColumns="true" HeaderStyle-Font-Size="Small">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="20px">
                                        <ItemTemplate>
                                            <telerik:RadButton runat="server" ID="btnDownload" ForeColor="Red" Skin="Silk" Text="DOWNLOAD" Font-Size="Small" OnClick="btnDownload_Click"></telerik:RadButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>

                        </telerik:RadGrid>
                        <div style="float: right">
                            <div class="form-group row gutters">
                                <label for="txtamount" class="col-sm-4 col-form-label">Total Amount</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtamount" Style="text-align: right" class="form-control" ReadOnly="true" ForeColor="red" runat="server" CssClass="form-control form-control-sm" Width="194px"></asp:TextBox>

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
   <%--     </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>
