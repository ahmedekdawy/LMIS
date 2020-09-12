<div class="col-md-12" style="padding-top: 40px; padding-bottom: 40px;">
    <div class="col-md-4"></div>
    <div class="col-md-4">
        <div class="control-group">
            <label style="display: block;">
                <%=GetGlobalResourceObject("CommonControls", "X_Email")%>  *
                <input type="email" class="form-control validationElement" data-bind="value: UserName, css: { validatingElement: UserName.isValidating() }">
            </label>
       </div>
        <div class="control-group">
            <label style="display: block;">
                <%=GetGlobalResourceObject("CommonControls", "X_Password")%>  *
                <input type="password" class="form-control validationElement" data-bind="value: Password">
            </label>
        </div>
        <div class="control-group">
            <label style="display: block;">
                <%=GetGlobalResourceObject("CommonControls", "X_ConfirmPassword")%>  *
                <input type="password" class="form-control validationElement" data-bind="textInput: PasswordConfirmation">
            </label>
        </div>
        <div class="control-group" data-bind="visible: ReqAuthLetter(), validationOptions: { insertMessages: false }">
            <label>
                <%=GetGlobalResourceObject("CommonControls", "W001_AuthLetter")%>  *
            </label>
            <div class="input-group">
                <input type="text" class="form-control always-white" data-bind="value: AuthLetterFileName" disabled="disabled" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectFile")%>">
                <span class="input-group-btn">
                    <button class="btn btn-default" data-bind="validationElement: AuthLetterFileName" onclick="$('#hdnAuthLetterBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                </span>
            </div>
            <input type="file" id="hdnAuthLetterBrowser" data-bind="attr: { accept: docExtensions }, event: { change: ValidateAuthLetter }" style="height: 0; visibility: hidden;"/>
            <p class="form-description">
                <span style="padding-right: 10px;">PDF , DOC </span>
                <a class="text-center" href="#" target="_blank" data-bind="visible: AuthLetterUploaded(), attr: { href: lmis.x.downloadURL + AuthLetterFileName() }">
                    <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                </a>
            </p>
        </div>
    </div>
    <div class="col-md-4"></div>
    <div class="col-md-12" style="display: none">
        <pre data-bind="text: ko.toJSON(dto, null, 4)"></pre>
        <pre data-bind="text: ko.toJSON(errors, null, 4)"></pre>    
    </div>
    <div style="display: none">
        <span id="msgRegisteredEmail"><%=GetGlobalResourceObject("MessagesResource", "W001_RegisteredEmail")%></span>
        <span id="msgInvalidPassword"><%=GetGlobalResourceObject("MessagesResource", "W001_InvalidPassword")%></span>
        <span id="msgPasswordsMismatch"><%=GetGlobalResourceObject("MessagesResource", "W001_PasswordsMismatch")%></span>
    </div>
</div>