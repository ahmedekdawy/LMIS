<%@ Page Title="<%$ Resources:CommonControls,F012_Title %>" Language="C#"  MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="ImportTemplate.aspx.cs" Inherits="LMIS.Portal.BackEnd.ImportTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
            <%= Scripts.Render("~/bundles/noty") %>
    <%= Scripts.Render("~/bundles/inputmask") %>
                      <div class="fileinput fileinput-new" data-provides="fileinput">
                                                         
                                                            <div>
                                                                <span class="btn btn-default btn-file validationElement">
                                                             <asp:FileUpload ID="FileUpload1" runat="server" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"/></span>
                                                            <span>*</span>
                                                                <asp:LinkButton ID="btnUpload" runat="server" class="btn btn-default fileinput-exists" data-dismiss="fileinput" OnClick="btnUpload_Click"><%=GetGlobalResourceObject("CommonControls", "F012_Upload")%></asp:LinkButton>
                                                            </div>
                                                    </div>
  </form>  
</asp:Content>
