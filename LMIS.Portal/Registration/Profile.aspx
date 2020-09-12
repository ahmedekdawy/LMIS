<%@ Page Language="C#" MasterPageFile="~/MasterPages/LabourExchange.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="LMIS.Portal.Registration.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../Scripts/Extensions/ko.fileinput.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../Registration/Scripts/Profile.js" async="async"></script>

    <div class="col-md-12" style="background: white; margin-bottom: 10px; display: none;" data-bind="visible: Approval() && !(Approval() === 1 && vmReview)">
        <label class="ui-state-highlight ui-corner-all" style="cursor: pointer; width: 100%; padding: 5px 15px 5px 15px; text-align: center;" data-bind="visible: Approval() === 1 && !vmReview">
            <%= GetGlobalResourceObject("MessagesResource", "X_Pending") %>
        </label>
        <label class="ui-state-default ui-corner-all" style="cursor: pointer; width: 100%; padding: 5px 15px 5px 15px; text-align: center;" data-bind="visible: Approval() === 2">
            <%=GetGlobalResourceObject("MessagesResource", "X_Approved")%>
        </label>
        <label class="ui-state-error ui-corner-all" style="cursor: pointer; width: 100%; padding: 5px 15px 5px 15px;" data-bind="visible: Approval() === 3">
            <%=GetGlobalResourceObject("MessagesResource", "X_Rejected")%>
            <br/><strong><span data-bind="text: RejectReason()"></span></strong>
        </label>
    </div>

    <div data-bind="component: { name: 'profile', params: { root: $root, viewOnly: viewOnly, key: key } }"></div>

    <script type="text/javascript">
        function testvalid(value, id) {
            ///  alert(id);                  
            if ($('#txtName_A').val() == "" && $('#txtName_B').val() == "" && $('#txtName_C').val() == "") {
                $('#txtName_A').addClass("validationElement");
            }
            else {
                $('#txtName_A').removeClass("validationElement");
            }

        }
        $(document).ready(function () {
            $('#txtName_A').removeClass("validationElement");

        });
         </script>
</asp:Content>