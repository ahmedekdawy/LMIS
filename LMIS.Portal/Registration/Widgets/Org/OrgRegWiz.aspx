<div class="panel">
    <div class="panel-heading-v2">
        <ul class="nav nav-wizard" data-bind="foreach: steps">
            <li data-bind="css: { 'active': isActive(), 'unavailable': !isAvailable() }, click: wizard.go.f(id)">
                <span data-bind="text: name"></span>
            </li>
        </ul>
    </div>
    <div class="panel-body focus-group" data-bind="with: currentStep">
        <div data-bind="template: { name: template, data: viewModel }"></div>
    </div>
    <div class="panel-footer align-end">
        <button class="btn btn-default" data-bind="click: goPrevious, enable: !atFirstStep()">
            <%=GetGlobalResourceObject("CommonControls", "X_Previous")%>
        </button>
        <button class="btn btn-default" data-bind="click: goNext, enable: canGoNext(), visible: !atLastStep()">
            <%=GetGlobalResourceObject("CommonControls", "X_Next")%>
        </button>
        <button class="btn btn-warning" data-bind="click: SignUp, visible: canSubmit()">
            <%=GetGlobalResourceObject("CommonControls", "X_SignUp")%>
        </button>
    </div>
</div>

<div id="divAgreement" style="display: none">
    <span id="tabDisclaimer"><%=GetGlobalResourceObject("CommonControls", "R001_Disclaimer")%></span>
    <span id="tabCreds"><%=GetGlobalResourceObject("CommonControls", "R001_Creds")%></span>
    <span id="tabOrgDets"><%=GetGlobalResourceObject("CommonControls", "R001_OrgDets")%></span>
    <span id="tabContactInfo"><%=GetGlobalResourceObject("CommonControls", "R001_ContactInfo")%></span>
    <span id="tabServices"><%=GetGlobalResourceObject("CommonControls", "R001_Services")%></span>
</div>

<script id="message-template" type="text/html">
    <div data-bind="html: typeof message() === 'string' ? message() : ''"></div>
</script>