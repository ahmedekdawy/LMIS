<%@ Page Title="<%$ Resources:CommonControls,F027_Title %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="PagesActions.aspx.cs" Inherits="LMIS.Portal.Administration.PagesActions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <%= Scripts.Render("~/bundles/noty") %>
    <%= Scripts.Render("~/bundles/inputmask") %>
    <div class="form-group">
        <h2><%=GetGlobalResourceObject("CommonControls", "F027_Pages")%></h2>
           <asp:DropDownList data-placeholder="<%$ Resources:CommonControls,F025_UserSelect %>" runat="server" ID="ddlPages" class="chzn-select" Style="width: 350px;" AppendDataBoundItems="True" >
		</asp:DropDownList>
    </div>
     <div class="form-group">
                 <h2><%=GetGlobalResourceObject("CommonControls", "F023_Role")%></h2>
       <asp:DropDownList data-placeholder="<%$ Resources:CommonControls,F025_UserSelect %>" runat="server" ID="ddlRoles" class="chzn-select" Style="width: 350px;" AppendDataBoundItems="True" >
		</asp:DropDownList> 
    </div>
    <div class="form-group">
           <asp:Button runat="server" ID="btnSearch"  Text="<%$ Resources:CommonControls,X_Search %>" CssClass="btn btn-group-lg search-icon-block" OnClick="btnSearch_Click"  />
    </div>
        <div class="form-group">
           <asp:CheckBoxList ID="chkActions" runat="server"   RepeatColumns="5" RepeatDirection="Horizontal" >
               <asp:ListItem Value="1" Text="<%$ Resources:CommonControls,X_Query %>"></asp:ListItem>
               <asp:ListItem Value="2" Text="<%$ Resources:CommonControls,X_Add %>"></asp:ListItem>
               <asp:ListItem Value="3" Text="<%$ Resources:CommonControls,X_Update %>"></asp:ListItem>
               <asp:ListItem Value="4" Text="<%$ Resources:CommonControls,X_Delete %>"></asp:ListItem>
               <asp:ListItem Value="5" Text="<%$ Resources:CommonControls,X_print %>"></asp:ListItem>
           </asp:CheckBoxList>
    </div>
        <div class="form-group">
       <asp:Button runat="server" ID="btnSave"  Text="<%$ Resources:CommonControls,X_Save1 %>" CssClass="btn btn-success  btn-lg btn-large" OnClick="btnSave_Click"  />
    </div>
        </form>
</asp:Content>
