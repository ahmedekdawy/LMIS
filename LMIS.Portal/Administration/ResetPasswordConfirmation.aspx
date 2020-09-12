<%@ Page Title="<%$ Resources:CommonControls,F026_Title %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="ResetPasswordConfirmation.aspx.cs" Inherits="LMIS.Portal.Administration.ResetPasswordConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <form runat="server">
           <%= Scripts.Render("~/bundles/noty") %>
    <%= Scripts.Render("~/bundles/inputmask") %>
         <p class="text-danger"><asp:Literal runat="server" ID="ErrorMessage" /></p>
        <div class="form-horizontal">
   
       
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label"><%=GetGlobalResourceObject("CommonControls", "X_Password")%></asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control validationElement" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="*" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label"><%=GetGlobalResourceObject("CommonControls", "F009_RetypePassword")%></asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control validationElement" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="*" />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="<%$ Resources:CommonControls,F026_passwordConfirmnotMatch %>" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="Password" ErrorMessage="<%$ Resources:MessagesResource,W001_InvalidPassword %>" ValidationExpression="^.*(?=.{6,})(?=.*[\d])(?=.*[\W]).*$"></asp:RegularExpressionValidator>
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
