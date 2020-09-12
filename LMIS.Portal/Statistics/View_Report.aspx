<%@ Page Language="C#" Title="<%$ Resources:CommonControls,F044_Title %>" AutoEventWireup="true" CodeBehind="View_Report.aspx.cs" Inherits="LMIS.Portal.View_Report" MasterPageFile="~/MasterPages/FrontEnd.Master"%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <form id="form1" runat="server">
                
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:Label ID="NotificationLbl" runat="server" ></asp:Label>
                            
                        
                            <asp:Table ID="Table1" runat="server">
                            </asp:Table>
                          
                <asp:RadioButtonList ID="chkreportType" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="chkreportType_SelectedIndexChanged">
                    <asp:ListItem Value="2" Selected="True" Text="<%$ Resources:CommonControls,F044_All %>" > </asp:ListItem>
                    <asp:ListItem Value="1" Text="<%$ Resources:CommonControls,F044_Tabular %>"></asp:ListItem>
                    <asp:ListItem Value="0" Text="<%$ Resources:CommonControls,F044_Graph %>"></asp:ListItem>
                            </asp:RadioButtonList>
                       <div style="padding-top:10px">
                           
         <rsweb:reportviewer id="Migration_Rport" runat="server"  Width="90%" font-names="Verdana" font-size="8pt"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt"  ShowPromptAreaButton="True">
                                
                            </rsweb:reportviewer>
                          
                       </div>
             
                        
                       
        
        
        
    
  
        </form>
    
   </asp:Content>