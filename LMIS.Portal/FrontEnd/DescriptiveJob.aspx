<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="DescriptiveJob.aspx.cs" Inherits="LMIS.Portal.FrontEnd.DescriptiveJob" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div data-bind="foreach: FilesList">

        <div class="col-lg-11"><a  href="#" target="_blank" data-bind="attr: { href: lmis.x.downloadURL + 'DescriptiveJob/' + ImagePath() }"><img  width="50px" height="50px" src="../images/pdf.png"/><span data-bind="    text: ImageName"></span></a> </div>
       </div>
      <div data-bind="foreach: FilesList">

        <div class="col-lg-11"><a  href="#" target="_blank" data-bind="attr: { href: lmis.x.downloadURL + 'DescriptiveJob/' + ImagePath() }"><img  width="50px" height="50px" src="../images/pdf.png"/><span data-bind="    text: ImageName"></span></a> </div>
       </div>
    <script src="../FrontEnd/Scripts/DescriptiveJob.js"></script>
</asp:Content>
