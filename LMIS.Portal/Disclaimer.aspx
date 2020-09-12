<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Disclaimer.aspx.cs" Inherits="LMIS.Portal.Disclaimer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div>
    <span id="tabDisclaimer"><%=GetGlobalResourceObject("CommonControls", "R001_Disclaimer")%></span>
     <div data-bind="html: disclaimer"></div>

        </div>
    <script src="Scripts/Disclaimer.js"></script>      
</asp:Content>
