<%@ Page Language="C#" MasterPageFile="~/MasterPages/LabourExchange.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="LMIS.Portal.Registration.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../Registration/Scripts/Contact.js" async="async"></script>
    
    <!-- ko foreach: cards -->
    <div class="row form-divider col-sm-12">
        <div class="col-lg-4" ><hr></div>
        <div class="col-lg-4 form-divider-title">
            <span data-bind="text: name"></span>
        </div>
        <div class="col-lg-4"><hr></div>
    </div>
    <div data-bind="attr: { id: template + id }">
        <div data-bind="template: { name: template, data: viewModel }"></div>
    </div>
    <!-- /ko -->

    <div class="row text-center" style="display: none" data-bind="visible: mode() !== 'v'">
        <input value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-default btn-lg btn-primary" type="button" data-bind="enable: isDirty() && isValidated(), click: Update" />
    </div>

    <div id="tab" style="display: none">
        <span id="tabCredentials"><%=GetGlobalResourceObject("CommonControls", "R003_EmpCreds")%></span>
        <span id="tabOrgContact"><%=GetGlobalResourceObject("CommonControls", "R003_EmpDetails")%></span>
    </div>
    <script type="text/javascript">
        function testvalid(value,id) {
          //  alert(id);
            if ($('#FullName_A').val() == "" && $('#FullName_B').val() == "" && $('#FullName_C').val() == "") {
                $('#FullName_A').addClass("validationElement");
            }
            else {
                $('#FullName_A').removeClass("validationElement");
            }

        }
        function testdeptvalid(value, id) {
           // alert(id);
            if ($('#Dept_A').val() == "" && $('#Dept_B').val() == "" && $('#Dept_C').val() == "") {
                $('#Dept_A').addClass("validationElement");
            }
            else {
                $('#Dept_A').removeClass("validationElement");
            }

        }
         </script>
</asp:Content>