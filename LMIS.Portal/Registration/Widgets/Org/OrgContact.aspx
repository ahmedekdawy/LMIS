<div class="col-md-12" style="padding-top: 40px; padding-bottom: 40px;" data-bind="validationOptions: kovOptions">
    <div class="col-md-2">
    </div>
    <div class="col-md-8">
        <div class="col-md-12">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "W004_FullName")%> *</label>
                <div class="input-group">
                    <input type="text" onchange="testvalid(this.value,this.id)" id="FullName_A" class="form-control validationElement" maxlength="100" data-bind="value: Name.A, attr: { placeholder: Name.A.ph }" />
                    <span class="input-group-btn">
                        <button class="btn btn-default always-enabled" data-bind="click: function () { Name.LocalizeView(!Name.Localized()); }, validationElement: Name.dto">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" class="form-control" onchange="testvalid(this.value,this.id)" id="FullName_B" maxlength="100" data-bind="value: Name.B, attr: { placeholder: Name.B.ph }, visible: !Name.Localized()" />
                <input type="text" class="form-control" onchange="testvalid(this.value,this.id)" id="FullName_C"maxlength="100" data-bind="value: Name.C, attr: { placeholder: Name.C.ph }, visible: !Name.Localized()" />
            </div>
        </div>
        <div class="col-md-12">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "W004_Department")%> *</label>
                <div class="input-group">
                    <input type="text" class="form-control validationElement" onchange="testdeptvalid(this.value,this.id)" id="Dept_A" maxlength="100" data-bind="value: Dept.A, attr: { placeholder: Dept.A.ph }" />
                    <span class="input-group-btn">
                        <button class="btn btn-default always-enabled" data-bind="click: function () { Dept.LocalizeView(!Dept.Localized()); }, validationElement: Dept.dto">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" class="form-control" maxlength="100" onchange="testdeptvalid(this.value,this.id)" id="Dept_B" data-bind="value: Dept.B, attr: { placeholder: Dept.B.ph }, visible: !Dept.Localized()" />
                <input type="text" class="form-control" maxlength="100" onchange="testdeptvalid(this.value,this.id)" id="Dept_C"data-bind="value: Dept.C, attr: { placeholder: Dept.C.ph }, visible: !Dept.Localized()" />
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "W004_JobTitle")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: JobTitleOptions, value: JobTitle, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Mobile")%></label>
                <input type="text" maxlength="20" class="form-control phone" data-bind="textInput: Mobile">
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Telephone")%></label>
                <input type="text" maxlength="20" class="form-control phone" data-bind="textInput: Telephone">
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Fax")%></label>
                <input type="text" maxlength="20" class="form-control phone" data-bind="textInput: Fax">
            </div>
        </div>
    </div>
    <div class="col-md-2">
    </div>
</div>
  