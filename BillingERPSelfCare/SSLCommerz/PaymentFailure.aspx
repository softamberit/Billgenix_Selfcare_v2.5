<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentFailure.aspx.cs" Inherits="BillingERPSelfCare.SSLCommerz.PaymentFailure" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .redLebel {
            font-size: 16px;
            color: red;
        }
    </style>

    <asp:UpdatePanel ID="updatePanel1" runat="server">
                <ContentTemplate>
 
                
                  
                    <div class="row-fluid sortable">
				<div class="box span8">
					<div class="box-header well">
						<h2><i class="icon-bell"></i> Payment Failure Info</h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content alerts">
					
						<div class="alert alert-block ">
							<button type="button" class="close" data-dismiss="alert">×</button>
							<h4 class="alert-heading">Warning!</h4>
							<p>Payment failure! Try again! Click 'Go Back' button for back through payment page.</p>

						</div>
					</div>	
                         <div style="text-align:right; float:left;   width:100%">
                                   <%--<input type="button" class="myButton" value="Pay" onclick="SaveData()" />--%>
                         <asp:Button ID="btnGoBack" CssClass="btn btn-info" runat="server" OnClick="GoBack_Click"  Text="Go Back" />
                               </div>
				</div><!--/span-->
				
		
			</div>

                </ContentTemplate>
            </asp:UpdatePanel>

</asp:Content>
