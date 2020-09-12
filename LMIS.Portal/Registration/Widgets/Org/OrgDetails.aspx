<div class="col-md-12" style="padding-top: 40px; padding-bottom: 40px;" data-bind="validationOptions: kovOptions">
    <div class="col-md-4">
        <input type="file" id="hdnLogoBrowser" data-bind="attr: { accept: imgExtensions }, event: { change: ValidateLogo }" style="height: 0; visibility: hidden;"/>
        <div class="logo">
            <div data-bind="validationElement: LogoFileName, css: { enabled: mode() !== 'v' }, style: { backgroundImage: LogoPreview }" onclick="$('#hdnLogoBrowser').trigger('click');"></div>
            <i class="delete fa fa-2x fa-times" data-bind="click: ClearLogo, css: { enabled: mode() !== 'v' }"></i>
        </div>
        <div data-bind="foreach: OrgTypeOptions">
            <br/>
            <label class="checkbox-inline">
                <input type="radio" data-bind="checked: $parent.OrgType, checkedValue: $data.id" />
                <span data-bind="text: $data.desc"></span>
            </label>
        </div>
        <br/>
        <div class="form-group col-md-10" data-bind="visible: IsSelfEmployed">
            <label><%=GetGlobalResourceObject("CommonControls", "W002_YOE")%> *</label>
            <select class="form-control always-white validationElement" data-bind="options: YOEOptions, value: YOE, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
            </select>
        </div>
    </div>
    <div class="col-md-8">
        <div class="col-md-12">
            <div class="form-group">
                <label id="lblOrgName"><%=GetGlobalResourceObject("CommonControls", "W002_OrgName")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtName_A" onchange="testvalid(this.value,this.id)" class="form-control validationElement" maxlength="100" data-bind="value: Name.A, attr: { placeholder: Name.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnNames" class="btn btn-default always-enabled" data-bind="click: function () { Name.LocalizeView(!Name.Localized()); }, validationElement: Name.dto">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtName_B" class="form-control" onchange="testvalid(this.value,this.id)" maxlength="100" data-bind="value: Name.B, attr: { placeholder: Name.B.ph }, visible: !Name.Localized()" />
                <input type="text" id="txtName_C" class="form-control" onchange="testvalid(this.value,this.id)" maxlength="100" data-bind="value: Name.C, attr: { placeholder: Name.C.ph }, visible: !Name.Localized()" />
            </div>
        </div>
        <div class="col-md-12">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "W002_Profile")%></label>
                <div class="input-group">
                    <input type="text" class="form-control always-white always-disabled" data-bind="value: ProfileFileName" disabled="disabled" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectFile")%>">
                    <span class="input-group-btn">
                        <button class="btn btn-default" data-bind="validationElement: ProfileFileName" onclick="$('#hdnProfileBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                    </span>
                </div>
                <input type="file" id="hdnProfileBrowser" data-bind="attr: { accept: docExtensions }, event: { change: ValidateProfile }" style="height: 0; visibility: hidden;"/>
                <p class="form-description">
                    <span style="padding-right: 10px;">PDF , DOC , PPT</span>
                    <a class="text-center" href="#" target="_blank" data-bind="visible: ProfileUploaded(), attr: { href: lmis.x.downloadURL + ProfileFileName() }">
                        <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                    </a>
                </p>
            </div>   
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "W002_OrgSize")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: OrgSizeOptions, value: OrgSize, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "W002_IDType")%> *</label>
                <select class="form-control validationElement always-white " data-bind="options: IDTypeOptions, value: IDType, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label id="lblID"><%=GetGlobalResourceObject("CommonControls", "W002_ID")%>  *</label>
                <input type="text" maxlength="20" class="form-control validationElement" data-bind="textInput: ID">
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "W002_EstDate")%>  *</label>
                <input type="text" class="form-control datepicker validationElement" data-bind="textInput: EstDate">
            </div>
        </div>
        <div class="col-md-8">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "W002_Activity")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: ActivityOptions, value: Activity, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>
        <div class="col-md-12">
            <div class="form-group" data-bind="visible: IsIndustrial">
                <label id="lblIndustry"><%=GetGlobalResourceObject("CommonControls", "W002_Industry")%>  *</label>
                <select class="form-control always-white validationElement" data-bind="options: IndustryOptions, value: Industry, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
                <div data-bind="visible: (Industry() == '+')">
                    <div class="input-group">
                        <input type="text" id="txtNewIndustry_A" class="form-control" maxlength="50" data-bind="value: NewIndustry.A, attr: { placeholder: NewIndustry.A.ph }" />
                        <span class="input-group-btn">
                            <button id="btnNewIndustry" class="btn btn-default always-enabled" data-bind="click: function () { NewIndustry.LocalizeView(!NewIndustry.Localized()); }, validationElement: NewIndustry.dto">
                                <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                            </button>
                        </span>
                    </div>
                    <input type="text" id="txtNewIndustry_B" class="form-control" maxlength="50" data-bind="value: NewIndustry.B, attr: { placeholder: NewIndustry.B.ph }, visible: !NewIndustry.Localized()" />
                    <input type="text" id="txtNewIndustry_C" class="form-control" maxlength="50" data-bind="value: NewIndustry.C, attr: { placeholder: NewIndustry.C.ph }, visible: !NewIndustry.Localized()" />
                </div>
            </div>
        </div>
    </div>
    <div style="display: none">
        <span id="X_InsertNewItem"><%=GetGlobalResourceObject("MessagesResource", "X_InsertNewItem")%></span>
    </div>
</div>