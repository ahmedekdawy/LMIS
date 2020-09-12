<%@ Page Title="<%$ Resources:CommonControls,F026_Title %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="LMIS.Portal.Administration.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
           <%= Scripts.Render("~/bundles/noty") %>
    <%= Scripts.Render("~/bundles/inputmask") %>
         <p class="text-danger"><asp:Literal runat="server" ID="ErrorMessage" /></p>
        <div class="form-horizontal">
   
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label"><%=GetGlobalResourceObject("CommonControls", "F009_Email")%></asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control validationElement" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="*" />
            </div>
        </div>
    
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="Reset_Click" Text="<%$ Resources:CommonControls,F026_Reset %>" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
    </form>
</asp:Content>
