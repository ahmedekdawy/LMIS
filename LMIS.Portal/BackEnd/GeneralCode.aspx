<%@ Page Title="<%$ Resources:CommonControls,GeneralCodeTitle %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="GeneralCode.aspx.cs" Inherits="LMIS.Portal.BackEnd.GeneralCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <table class="table-responsive">
        <thead>
            <tr>
                <th> <%=GetGlobalResourceObject("CommonControls", "CodeNo")%></th>
                 <th><%=GetGlobalResourceObject("CommonControls", "Description")%></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td></td>
                <td></td>
            </tr>
        </tbody>
    </table>
 <script src="BackEnd/Scripts/GeneralCodes.js"></script>
</asp:Content>
