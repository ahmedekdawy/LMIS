<!-- ko foreach: cards -->
<div class="panel" data-bind="css: css()">
    <div class="panel-heading">
        <table style="border: none; margin: 0; padding: 0; width: 100%">
            <tr>
                <td><strong data-bind="text: name"></strong></td>
                <td class="align-end">
                    <button class="btn btn-default" data-bind="visible: mode() === 'v' && isEditable(), click: onEdit">
                        <%=GetGlobalResourceObject("CommonControls", "X_Edit")%>
                    </button>
                    <button class="btn btn-default" data-bind="visible: mode() === 'e' && isDirty(), click: onReset">
                        <%=GetGlobalResourceObject("CommonControls", "X_Reset")%>
                    </button>
                    <button class="btn btn-warning" data-bind="visible: mode() === 'e' && isValidated(), click: onAccept">
                        <%=GetGlobalResourceObject("CommonControls", "X_OK")%>
                    </button>
                </td>
            </tr>
        </table>
    </div>
    <div class="panel-body focus-group" data-bind="attr: { id: template + id }">
        <div data-bind="template: { name: template, data: viewModel }"></div>
    </div>
</div>
<!-- /ko -->
<div class="row text-center" data-bind="visible: !viewOnly">
    <input value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-default btn-lg btn-primary" type="button" data-bind="enable: isDirty(), click: UpdateProfile" />
</div> 
<div style="display: none">
    <span id="tabOrgDets"><%=GetGlobalResourceObject("CommonControls", "R001_OrgDets")%></span>
    <span id="tabContactInfo"><%=GetGlobalResourceObject("CommonControls", "R001_ContactInfo")%></span>
    <span id="tabServices"><%=GetGlobalResourceObject("CommonControls", "R001_Services")%></span>
</div>
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
