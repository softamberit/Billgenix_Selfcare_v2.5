<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="BillingERPSelfCare.SSLCommerz.Payment" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--  <script src="../../Scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tabletojson.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tabletojson.min.js" type="text/javascript"></script>    
    <link href="../../Styles/Control.css" rel="stylesheet" />--%>

    <script type="text/javascript">

        $(document).ready(function () {

            // LoadCollectionList()
            $("#btnReset").click(function () {
                $(this).closest('form').find("input[type=text], textarea").val("");
                $(this).closest('form').find("span").text();
            });

        });


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

    <asp:UpdatePanel ID="updatePanel1" runat="server">
        <ContentTemplate>

            <div class="col-md-12 col-sm-12 col-lg-12">


                <div class="card">
                    <div class="card-header" style="font-weight: 700">
                        SSL Payment
                    </div>
                    <div class="card-body" style="height: 100px">
                        <div class="form-row">
                            <div class="col-md-5 col-sm-5">
                                <div class="form-group row gutters">
                                    <label class="col-sm-4 col-md-4 col-form-label" style="font-weight: 500; color:black">Invoice No</label>
                                    <div class="col-sm-8 col-md-8">
                                        <telerik:RadTextBox runat="server" ID="txtInvoiceNo" Skin="Silk" AutoCompleteType="Disabled" Width="90%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvInvoiceNo" runat="server" ControlToValidate="txtInvoiceNo"
                                            ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-md-5 col-sm-5">
                                <div class="form-group row gutters">
                                    <label class="col-sm-4 col-md-4 col-form-label" style="font-weight: 500; color:black">Invoice Amount</label>
                                    <div class="col-sm-7 col-md-7">
                                        <telerik:RadTextBox runat="server" ID="txtInvoiceAmount" Skin="Silk" AutoCompleteType="Disabled" Width="90%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvInvoiceAmount" runat="server" ControlToValidate="txtInvoiceAmount"
                                            ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-1 col-md-1">
                                    <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" OnClick="Pay_Click" ValidationGroup="Save" Text="PAY" Style="line-height: .85" />
                                    </div>
                                </div>
                                
                            </div>
                        </div>

                    </div>

                </div>


            </div>


            <%--<div class="row-fluid sortable">
				<div class="box span12">
					<div class="box-header well" data-original-title>
                        <h2><i class="icon-shopping-cart"></i>Payment</h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<div class="form-horizontal">
						  <fieldset>
							<legend>Details Entry</legend>
							
							<div class="control-group">
								<label class="control-label" for="txtInvoiceId">Invoice No</label>
								<div class="controls">
								     <asp:TextBox ID="txtInvoiceId" ClientIDMode="Static" placeholder="Invoice No" runat="server" AutoCompleteType="Disabled" Width="250px"></asp:TextBox>

								
								</div>
							  </div>
                              <div class="control-group">
								<label class="control-label" for="txtAmount">Invoice Amount</label>
								<div class="controls">
								<asp:TextBox ID="txtAmount" ClientIDMode="Static" placeholder="Invoice Amount" runat="server" AutoCompleteType="Disabled" Width="250px"></asp:TextBox>
                                 
                                      <asp:RegularExpressionValidator ID="revNumber" runat="server" ControlToValidate="txtAmount" CssClass="help-inline text text-info redLebel"
           ErrorMessage="Please enter only numbers like 100 or 100.00" ValidationExpression="^\d+(\.\d\d)?$"></asp:RegularExpressionValidator>
  
								</div>
							  </div>
                            <asp:Label ID="lblStatus"  CssClass="redLebel help-block" ClientIDMode="Static" runat="server" Text=""></asp:Label>

							<div class="form-actions">
                         <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Width="90px" OnClick="Pay_Click"  Text="Pay" />
                                <input id="btnReset" type="reset" class="btn" style="width: 90px" value="Reset"/>
							
							</div>
						  </fieldset>
						</div>   

					</div>
				</div><!--/span-->

			</div>--%>


            <div class="container" runat="server">
                <div id="msgModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header " style="float: right">
                                <p class="modal-title" style="font-weight: 600; padding-left: 320px; color: #b90707">billGENIX FeedBack</p>
                            </div>
                            <div class="modal-body">
                                <p id="msgModalBodyParagraph" style="text-align: center; color: #e50d0d"></p>
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
