<%@ Page Title="<%$ Resources:CommonControls,F050 %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="TransactionHistory.aspx.cs" Inherits="LMIS.Portal.BackEnd.TransactionHistory" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="../css/chosen.css" rel="stylesheet" />
              <form id="form1" runat="server">
               
                <div class="form-group">
                    <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F033_ReqType")%> </div>
                    <div class="col-lg-4">
                                  <asp:DropDownList ID="ddlRequestType" runat="server" CssClass="ddl_box" AppendDataBoundItems="True" DataTextField="RequestTypeDesc" >
                            <asp:ListItem Selected="True" Text="<%$ Resources:CommonControls,X_Select %>" Value="All"></asp:ListItem>
                            
                        </asp:DropDownList></div>
                  
                    <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "X_Status")%> </div>
                    <div class="col-lg-4">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdown">
                            <asp:ListItem Selected="True" Text="<%$ Resources:CommonControls,X_Select %>" Value="All"></asp:ListItem>
                            <asp:ListItem Value="Pending" Text="<%$ Resources:CommonControls,X_Pending %>"></asp:ListItem>
                            <asp:ListItem Value="Approved" Text="<%$ Resources:CommonControls,X_Approve %>"></asp:ListItem>
                            <asp:ListItem Value="Rejected" Text="<%$ Resources:CommonControls,X_Reject %>"></asp:ListItem>
                        </asp:DropDownList></div>
             </div>
                  <br/> <br/>
                      <div class="form-group">
                    <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "X_TransactionType")%> </div>
                    <div class="col-lg-4">
                                <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="ddl_box">
                            <asp:ListItem Selected="True" Text="<%$ Resources:CommonControls,X_Select %>" Value="All"></asp:ListItem>
                            <asp:ListItem Value="New" Text="<%$ Resources:CommonControls,x_add %>"></asp:ListItem>
                            <asp:ListItem Value="Updated" Text="<%$ Resources:CommonControls,X_Update %>"></asp:ListItem>
                            <asp:ListItem Value="Deleted" Text="<%$ Resources:CommonControls,X_Delete %>"></asp:ListItem>
                        </asp:DropDownList></div>
                
                    <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F025_Users")%> </div>
                    <div class="col-lg-4">
                        <asp:DropDownList ID="ddlUsesr" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" DataTextField="PortalUserName" >
                            <asp:ListItem Selected="True" Text="<%$ Resources:CommonControls,X_Select %>" Value="All"></asp:ListItem>
                            
                        </asp:DropDownList>
                    </div>
               </div>
                   <br/> <br/>
                        <div class="form-group">
                    <div class="col-lg-2">From</div>
                    <div class="col-lg-4"><asp:TextBox ID="txtFrom" runat="server" CssClass="datepiker"></asp:TextBox></div>
                    <div class="col-lg-2">To</div>
                    <div class="col-lg-4"><asp:TextBox ID="txtTo" runat="server" CssClass="datepiker"></asp:TextBox></div>
                </div>
                   <div class="form-group center">
                       <asp:Button ID="btnDisplay" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:CommonControls,X_print %>" OnClick="btnDisplay_Click" />
                    
                   </div>
                   <div class="row">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <rsweb:reportviewer id="ReportViewer" runat="server" width="100%"  SizeToReportContent="True" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt"  ShowPromptAreaButton="True">
                                
                            </rsweb:reportviewer>
                       </div>
                             <script src="../js/chosen.jquery.js"  type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
                  </form>
 
</asp:Content>
