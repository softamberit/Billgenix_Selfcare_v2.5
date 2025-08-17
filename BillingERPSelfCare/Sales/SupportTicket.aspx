<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupportTicket.aspx.cs" Inherits="BillingERPSelfCare.Sales.SupportTicket" %>

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
    <link href="../css/DashboardCSS.css" rel="stylesheet" />
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

        function OnClientLoadHandler(sender) {
            sender.get_inputDomElement().readOnly = "readonly";
        }

    </script>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12" runat="server" id="New" style="text-align: right; padding-bottom: 10px">
                <asp:Button ID="btnNew" runat="server" Text="CREATE TICKET" OnClick="btnNew_Click" CssClass="btn btn-secondary" Width="100%" />
            </div>

            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12" runat="server" id="GridBody" style="overflow: scroll">

                <telerik:RadGrid ID="grdShowList" runat="server" GridLines="None" Skin="Metro" Width="100%" PageSize="10" AllowPaging="true"
                    AllowFilteringByColumn="false" OnNeedDataSource="grdShowList_NeedDataSource" AutoGenerateColumns="false">
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView CellPadding="0" CellSpacing="0" AutoGenerateColumns="false">
                        <Columns>

                            <telerik:GridBoundColumn DataField="TicketNo" HeaderText="Ticket No" UniqueName="TicketNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Subject" HeaderText="Subject" UniqueName="Subject" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Message" HeaderText="Message" UniqueName="Message" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="TicketStatus" HeaderText="Status" UniqueName="TicketStatus" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ResolvedDate" HeaderText="Resolve Date" UniqueName="ResolvedDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>

            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12" runat="server" id="EntryBody">
                <div class="card">
                    <div class="card-header">
                        <div class="form-row">
                            <div class="col-sm-2" style="text-align: left">
                                <span style="font-weight: 600">Support Ticket</span>
                            </div>

                            <div class="col-sm-6">
                            </div>
                            <div class="col-sm-4" style="text-align: left">
                                <p style="font-size: 12px; font-weight: bold;">(Fields with<span style="color: #cf3c3f"> * </span>are must required)</p>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div id="DivRadio" runat="server">
                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 10px">
                                    <div class="form-group row gutters">
                                        <span style="font-weight: 600; padding-bottom: 20px">1.  ইন্টারনেট সম্পর্কিত সমস্যার জন্য</span>
                                    </div>
                                </div>
                                <%-- <div class="col-sm-2"></div>--%>
                            </div>
                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 40px">
                                    <div class="form-group row gutters">
                                        <div class="col-sm-10">
                                            <asp:RadioButton ID="RadioButton" runat="server" GroupName="Support" Text="a)  আম্বার আইটি থেকে দেয়া অনু ডিভাইসটিতে লাল বাতি জ্বললে ।" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 40px">
                                    <div class="form-group row gutters">
                                        <div class="col-sm-10">
                                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Support" 
                                                Text="b)  ব্যান্ডউইথ সংক্রান্ত সমস্যার অভিযোগ জানাতে ।" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 40px">
                                    <div class="form-group row gutters">
                                        <div class="col-sm-10">
                                            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="Support" Text="c)  রাউটার কনফিগারেশন সংক্রান্ত সমস্যার অভিযোগ জানাতে ।" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 40px">
                                    <div class="form-group row gutters">
                                        <div class="col-sm-10">
                                            <asp:RadioButton ID="RadioButton3" runat="server" GroupName="Support" Text="d)  পিং হাই, প্যাকেট লস, গেমিং সংক্রান্ত সমস্যার অভিযোগ জানাতে ।" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 40px">
                                    <div class="form-group row gutters">
                                        <div class="col-sm-10">
                                            <asp:RadioButton ID="RadioButton4" runat="server" GroupName="Support" Text="e)  ইউটিউব, ফেসবুক, বিডিআইএক্স, ক্যাশ স্পিড সংক্রান্ত সমস্যার অভিযোগ জানাতে ।" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 40px; padding-bottom: 20px">
                                    <div class="form-group row gutters">
                                        <div class="col-sm-10">
                                            <asp:RadioButton ID="RadioButton5" runat="server" GroupName="Support" Text="f)  লিঙ্ক ডাউন সংক্রান্ত সমস্যার অভিযোগ জানাতে ।" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 10px">
                                    <div class="form-group row gutters">
                                        <span style="font-weight: 600; padding-bottom: 20px">2.  ইন্টারনেট বিল সংক্রান্ত তথ্যের জন্য</span>
                                    </div>
                                </div>
                                <%-- <div class="col-sm-2"></div>--%>
                            </div>
                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 40px; padding-bottom: 20px">
                                    <div class="form-group row gutters">
                                        <div class="col-sm-10">
                                            <asp:RadioButton ID="RadioButton6" runat="server" GroupName="Support" Text="a)  বিল পরিশোধ করেছেন লাইন এখনো একটিভ না হলে ।" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 10px">
                                    <div class="form-group row gutters">
                                        <span style="font-weight: 600; padding-bottom: 20px">3.  মার্কেটিং সংক্রান্ত যে কোন তথ্যের জন্য</span>
                                    </div>
                                </div>
                                <%-- <div class="col-sm-2"></div>--%>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 40px">
                                    <div class="form-group row gutters">
                                        <div class="col-sm-10">
                                            <asp:RadioButton ID="RadioButton7" runat="server" GroupName="Support" Text="a)  নতুন ইন্টারনেট সংযোগ নিতে চাইলে ।" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 40px">
                                    <div class="form-group row gutters">
                                        <div class="col-sm-10">
                                            <asp:RadioButton ID="RadioButton8" runat="server" GroupName="Support" Text="b)  আপনার ইন্টারনেট সংযোগের ঠিকানা পরিবর্তন করতে চাইলে ।"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 40px">
                                    <div class="form-group row gutters">
                                        <div class="col-sm-10">
                                            <asp:RadioButton ID="RadioButton9" runat="server" GroupName="Support" Text="c)  প্যাকেজ আপগ্রেড করতে চাইলে ।" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 40px">
                                    <div class="form-group row gutters">
                                        <div class="col-sm-10">
                                            <asp:RadioButton ID="RadioButton10" runat="server" GroupName="Support" Text="d)  প্যাকেজ ডাউনগ্রেড করতে চাইলে । " />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-sm-12" style="padding-left: 40px">
                                    <div class="form-group row gutters">
                                        <div class="col-sm-10">
                                            <asp:RadioButton ID="RadioButton11" runat="server" GroupName="Support"  Text="e)  আপনার নাম /মোবাইল নম্বর/মেইল পরিবর্তন করতে চাইলে ।" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-12">
                                    <asp:Button ID="btnNext" runat="server" Text="NEXT" ValidationGroup="Save" OnClick="btnNext_Click" CssClass="btn btn-primary" Style="float: right" />
                                </div>
                            </div>

                        </div>

                        <div id="divMessage" runat="server">
                            <div class="form-row">
                                <div class="col-sm-12">
                                    <div class="form-group row gutters">
                                        <label for="txtMessage" class="col-sm-2 col-form-label">Description</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled" Width="80%" TextMode="MultiLine" Rows="5" placeholder="Optional"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-sm-12">

                                    <asp:Button ID="btnSave" runat="server" Text="SUBMIT" ValidationGroup="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" Style="float: right" />
                                </div>

                            </div>
                        </div>

                        <div class="form-row">
                            <div class="col-sm-12">
                                <asp:Button ID="btnShowList" runat="server" Text="BACK" OnClick="btnShowList_Click" CssClass="btn btn-primary" Style="float: left" />
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
                                <%-- <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                                <p class="modal-title" style="font-weight: 600; padding-left: 320px; color: #da1113">billGENIX FeedBack</p>
                            </div>
                            <div class="modal-body">
                                <p id="msgModalBodyParagraph" style="text-align: center; color: #da1113"></p>
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
