<%@ Page Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="LMIS.Portal.Registration.SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript">
         function testvalid(value,id) {
           ///  alert(id);                  
             if ($('#txtName_A').val() == "" && $('#txtName_B').val() == "" && $('#txtName_C').val() == "") {
                 $('#txtName_A').addClass("validationElement");
             }
             else {
                 $('#txtName_A').removeClass("validationElement");
             }

         }
         </script>
    <script src="../Scripts/Extensions/ko.fileinput.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../Registration/Scripts/SignUp.js" async="async"></script>

    <div data-bind="component: { name: 'reg-wizard', params: { root: $root } }"></div>

</asp:Content>