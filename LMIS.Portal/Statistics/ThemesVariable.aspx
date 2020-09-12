<%@ Page Language="C#" Title="<%$ Resources:CommonControls,F016_Title %>" AutoEventWireup="true" CodeBehind="ThemesVariable.aspx.cs" Inherits="LMIS.Portal.ThemesVariable"  MasterPageFile="~/MasterPages/BackEnd.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
             
            </td>
        </tr>
        <tr>
            <td>
                <label> <%=GetGlobalResourceObject("CommonControls", "F010_Themes")%> </label></td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlThemeType" runat="server" AppendDataBoundItems="True" CssClass="form-control ddlThemeType validationElement" AutoPostBack="True" OnSelectedIndexChanged="ddlThemeType_SelectedIndexChanged" ></asp:DropDownList>
           
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlThemeType" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td> <label> <%=GetGlobalResourceObject("CommonControls", "F010_Title")%> </label></td>
        </tr>
        <tr>
            <td style="height: 34px">
                <asp:DropDownList ID="ddlThemes" runat="server" CssClass="form-control validationElement" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlThemes_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlThemes" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><label> <%=GetGlobalResourceObject("CommonControls", "F016_Variables")%> </label></td>
            <td><label> <%=GetGlobalResourceObject("CommonControls", "F016_ThemeVariables")%> </label></td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="lstVaiables" runat="server" CssClass="list-group list-group-item" Height="300px" AutoPostBack="True" OnSelectedIndexChanged="lstVaiables_SelectedIndexChanged"></asp:ListBox>
             
            </td>
            <td>
                <asp:ListBox ID="lstThemeVaiables" runat="server" CssClass="list-group list-group-item" Height="300px" AutoPostBack="True" OnSelectedIndexChanged="lstThemeVaiables_SelectedIndexChanged"></asp:ListBox>
             
            </td>
        </tr>
        </table>
    </form>
   
</asp:Content>
