<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="ImportVariableData.aspx.cs" Inherits="LMIS.Portal.BackEnd.ImportVariableData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <form runat="server">
             <%= Scripts.Render("~/bundles/noty") %>
    <%= Scripts.Render("~/bundles/inputmask") %>
       <div class="fileinput fileinput-new" data-provides="fileinput">
                                                         
                                                            <div>
                                                                <span class="btn btn-default btn-file">
                                                             <asp:FileUpload ID="fUpload" runat="server" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"/></span>
                                                            
                                                                <asp:LinkButton ID="btnUpload" runat="server" class="btn btn-default fileinput-exists" data-dismiss="fileinput" OnClick="btnUpload_Click"><%=GetGlobalResourceObject("CommonControls", "F12_Upload")%></asp:LinkButton>
                                                            </div>
                                                 </div>
</form>   
</asp:Content>
