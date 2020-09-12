<%@ Page Language="C#" Title="Add Variable" AutoEventWireup="true" CodeBehind="AddVariable.aspx.cs" Inherits="LMIS.Portal.AddVariable" MasterPageFile="~/MasterPages/BackEnd.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <table>
        <tr>
            <td colspan="3">

            
            
               
            </td>
        </tr>
        <tr>
            <td>

                Please enter variable name:

            </td>
            <td>
                
                <asp:TextBox ID="VariableNameTxt" runat="server" Width="259px"></asp:TextBox>
                
            </td>
            <td>
                <asp:Label ID="NotificationLbl" runat="server" ForeColor="#CC0000"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>

                <br />
                <br />

            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">

                <asp:Button ID="CreateVariableBtn" runat="server" OnClick="CreateVariableBtn_Click" style="text-align: center" Text="Create Variable" />

            </td>
        </tr>
    </table>
    </form>
</asp:Content>
