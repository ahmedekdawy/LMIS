<%@ Page Title="<%$ Resources:CommonControls, F015 %>" Language="C#" MasterPageFile="~/MasterPages/LabourExchange.master" AutoEventWireup="true" CodeBehind="TrainingPost.aspx.cs" Inherits="LMIS.Portal.LabourExchange.TrainingPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/TrainingPost.js" async="async"></script>

    <div class="tab-pane fade in active" id="tab-content">
        
        <div class="col-sm-10 col-sm-offset-1" style="display: none;" data-bind="visible: Mode() !== 'p'">
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
        
        <div class="row form-divider col-sm-12 ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F015_CourseTitle")%></div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>
        
        <div class="col-sm-10 col-sm-offset-1">

            <div class="form-group">
                <label id="lblTitle"><%=GetGlobalResourceObject("CommonControls", "F015_CourseTitle")%>  *</label>
                <select id="ddlTitle" class="form-control always-white validationElement" data-bind="options: TitleOptions, value: Title, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
                <div data-bind="visible: (Title() === '+')">
                    <div class="input-group">
                        <input type="text" id="txtNewTitle_A" class="form-control" maxlength="100" data-bind="value: NewTitle.A, attr: { placeholder: NewTitle.A.ph }" />
                        <span class="input-group-btn">
                            <button id="btnNewTitle" class="btn btn-default always-enabled" data-bind="click: function () { NewTitle.LocalizeView(!NewTitle.Localized()); }">
                                <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                            </button>
                        </span>
                    </div>
                    <input type="text" id="txtNewTitle_B" class="form-control" maxlength="100" data-bind="value: NewTitle.B, attr: { placeholder: NewTitle.B.ph }, visible: !NewTitle.Localized()" />
                    <input type="text" id="txtNewTitle_C" class="form-control" maxlength="100" data-bind="value: NewTitle.C, attr: { placeholder: NewTitle.C.ph }, visible: !NewTitle.Localized()" />
                </div>
            </div>

            <div class="form-group">
                <label id="lblDescription"><%=GetGlobalResourceObject("CommonControls", "F015_CourseDescription")%> **</label>
                <div class="trilingual-textarea">
                    <label lang="en" data-bind="css: lmis.string.isNullOrWhiteSpace(Description.A()) ? 'unedited' : 'edited'">
                        <input type="radio" value="en" class="always-enabled" data-bind="checked: Description.ActiveLang, click: function () { $('#txtDescription_En').focus(); return true; }">
                        <%=GetGlobalResourceObject("CommonControls", "X_English")%>
                    </label>
                    <label lang="fr" data-bind="css: lmis.string.isNullOrWhiteSpace(Description.B()) ? 'unedited' : 'edited'">
                        <input type="radio" value="fr" class="always-enabled" data-bind="checked: Description.ActiveLang, click: function () { $('#txtDescription_Fr').focus(); return true; }">
                        <%=GetGlobalResourceObject("CommonControls", "X_French")%>
                    </label>
                    <label lang="ar" data-bind="css: lmis.string.isNullOrWhiteSpace(Description.C()) ? 'unedited' : 'edited'">
                        <input type="radio" value="ar" class="always-enabled" data-bind="checked: Description.ActiveLang, click: function () { $('#txtDescription_Ar').focus(); return true; }">
                        <%=GetGlobalResourceObject("CommonControls", "X_Arabic")%>
                    </label>
                </div>
                <textarea id="txtDescription_En" maxlength="1000" data-bind="textInput: Description.A, visible: (Description.ActiveLang() == 'en')" class="form-control always-white validationElement" rows="8"></textarea>
                <textarea id="txtDescription_Fr" maxlength="1000" data-bind="textInput: Description.B, visible: (Description.ActiveLang() == 'fr')" class="form-control always-white validationElement" rows="8"></textarea>
                <textarea id="txtDescription_Ar" maxlength="1000" data-bind="textInput: Description.C, visible: (Description.ActiveLang() == 'ar')" class="form-control always-white validationElement" rows="8"></textarea>
            </div>
            
            <div class="form-group" data-bind="visible: Mode() !== 'v' || !lmis.string.isNullOrWhiteSpace(ServerFileName())">
                <label><%=GetGlobalResourceObject("CommonControls", "F015_CourseOutline")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtFileName" class="form-control always-disabled validationElement" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectFile")%>">
                    <span class="input-group-btn">
                        <button class="btn btn-default" onclick="$('#hdnFileBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                    </span>
                </div>
                <input type="file" id="hdnFileBrowser" data-bind="attr: { accept: AcceptedFiles }, event: { change: ValidateFile }" style="height: 0; visibility: hidden;"/>
                <p class="form-description">
                    <span style="padding-right: 10px;">PDF , MS Word , MS PowerPoint</span>
                    <a class="text-center" href="#" target="_blank" data-bind="visible: !lmis.string.isNullOrWhiteSpace(ServerFileName()), attr: { href: lmis.x.downloadURL + ServerFileName() }">
                        <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                    </a>
                </p>
            </div>
            
            <div class="form-group text-center" data-bind="visible: Mode() !== 'v'">
                <input value="<%=GetGlobalResourceObject("CommonControls", "X_ClearFile")%>" class="btn btn-choose-graph btn-sm btn-danger" style="width: 100px;" type="button" data-bind="click: ClearFile" />
            </div>

        </div>

        <div class="row form-divider col-sm-12 ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F015_CourseDetails")%></div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>
        
        <div class="col-sm-3 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_StartDate")%> *</label>
                <input type="text" id="txtStartDate" class="form-control validationElement" data-bind="value: StartDate" />
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_EndDate")%> *</label>
                <input type="text" id="txtEndDate" class="form-control validationElement" data-bind="value: EndDate" />
            </div>
        </div>

        <div class="col-sm-4">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F015_CourseSeats")%></label>
                <input type="text" id="txtSeats" class="form-control" data-bind="value: Seats" />
            </div>
        </div>
        
        <div class="col-sm-3 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_TimeFrom")%> *</label>
                <input type="text" id="txtTimeFrom" class="form-control validationElement" data-bind="value: TimeFrom" />
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_TimeTo")%> *</label>
                <input type="text" id="txtTimeTo" class="form-control validationElement" data-bind="value: TimeTo" />
            </div>
        </div>

        <div class="col-sm-4">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_TimeZone")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: TimeZoneOptions, value: TimeZone, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>
        
        <div class="col-sm-3 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Country")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: CountryOptions, value: Country, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_City")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: CityOptions, value: City, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>

        <div class="col-sm-4">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Status")%> *</label>
                <br>
                <label class="checkbox-inline">
                    <input type="checkbox" data-bind="checked: Status" class="validationElement">
                    <span><%=GetGlobalResourceObject("MessagesResource", "F015_CourseIsOpen")%></span>
                </label>
            </div>         
        </div>

        <div class="col-sm-10 col-sm-offset-1">
            <div class="form-group">
                <label id="lblAddress"><%=GetGlobalResourceObject("CommonControls", "X_Address")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtAddress_A" class="form-control validationElement" maxlength="500" data-bind="value: Address.A, attr: { placeholder: Address.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnAddress" class="btn btn-default always-enabled" data-bind="click: function () { Address.LocalizeView(!Address.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtAddress_B" class="form-control" maxlength="500" data-bind="value: Address.B, attr: { placeholder: Address.B.ph }, visible: !Address.Localized()" />
                <input type="text" id="txtAddress_C" class="form-control" maxlength="500" data-bind="value: Address.C, attr: { placeholder: Address.C.ph }, visible: !Address.Localized()" />
            </div>
        </div>
        
        <div class="col-sm-3 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Cost")%></label>
                <input type="text" id="txtCost" class="form-control" data-bind="value: Cost" />
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F015_CourseDuration")%> *</label>
                <input type="text" id="txtDuration" class="form-control validationElement" data-bind="value: Duration" />
            </div>
        </div>

        <div class="col-sm-4">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_WeekDays")%> *</label>
                <select id="Occurrence" class="form-control text-center bsmulti validationElement" multiple="multiple" data-bind="multiselect: lmis.defaults.multiselectNoFilterOptions, selectedOptions: Occurrence, foreach: OccurrenceOptions">
                    <option data-bind="value: $data.id, text: $data.desc"></option>
                </select>
            </div>
        </div>
        
        <div class="row form-divider col-sm-12">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F015_CourseSkills")%> *</div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>
        
        <div class="col-sm-3 col-sm-offset-1" data-bind="visible: Mode() !== 'v'">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Industry")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: IndustryOptions, value: Industry, optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>

        <div class="col-sm-10 col-sm-offset-1" data-bind="visible: Mode() !== 'v'">
            <div class="form-group">
                <label id="lblSkills"><%=GetGlobalResourceObject("CommonControls", "X_Skills")%> *</label>
                <select id="Skills" class="form-control text-center validationElement" multiple="multiple" data-bind="multiselect: lmis.defaults.multiselectOptions, selectedOptions: SkillSelections, foreach: SkillOptions">
                    <optgroup data-bind="attr: { label: $data.desc }, foreach: $data.options" label="">
                        <option data-bind="value: $data.id, text: $data.desc"></option>
                    </optgroup>
                </select>
                <div class="input-group">
                    <input type="text" id="txtNewSkill" class="form-control" maxlength="50" data-bind="value: NewSkill" />
                    <span class="input-group-btn">
                        <button id="btnNewSkill" class="btn btn-default" data-bind="click: AddSkill">
                            <i class="fa fa-plus"></i><%=GetGlobalResourceObject("CommonControls", "X_Add")%>
                        </button>
                    </span>
                </div>
            </div>
        </div>
        
        <div class="col-sm-10 col-sm-offset-1" data-bind="visible: Mode() !== 'v'">
            <div class="text-center">
                <input value="<%=GetGlobalResourceObject("CommonControls", "X_Add")%>" class="btn btn-choose-graph btn-sm btn-success" style="width: 100px;" type="button" data-bind="click: AddSkills" />
                <input value="<%=GetGlobalResourceObject("CommonControls", "X_Clear")%>" class="btn btn-choose-graph btn-sm btn-danger" style="width: 100px;" type="button" data-bind="click: ClearSkills" />
            </div>
        </div>

        <div class="col-sm-10 col-sm-offset-1 grd">
            <p></p>
            <table style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "X_Industry")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "X_Skill")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "X_SkillType")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "X_SkillLevel")%></th>
                        <th data-bind="visible: $root.Mode() !== 'v'">...</th>
                    </tr>
                </thead>
                <tbody data-bind="foreach: Skills">
                    <tr>
                        <td data-bind="text: Industry.desc"></td>
                        <td data-bind="text: Skill.desc"></td>
                        <td data-bind="text: lmis.format.nullableString(Type, 'desc')"></td>
                        <td data-bind="text: Level.desc"></td>
                        <td data-bind="visible: $root.Mode() !== 'v'">
                            <label data-bind="click: $root.RemoveSkill, css: $root.Mode() === 'v' ? 'disabledActionLabel' : 'actionLabel'">
                                <%=GetGlobalResourceObject("CommonControls", "X_Remove")%>
                            </label>
                        </td>
                    </tr>
                </tbody>
                <tbody data-bind="visible: Skills().length < 1">
                    <tr>
                        <td colspan="5">
                            <%=GetGlobalResourceObject("MessagesResource", "X_SelectSkills")%>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="col-sm-10 col-sm-offset-1" data-bind="visible: Mode() !== 'v'">
            <input value="<%=GetGlobalResourceObject("CommonControls", "X_UploadAndSave")%>" class="btn btn-primary nextBtn btn-lg pull-right" type="button" data-bind="click: StartWorkflow" />
        </div>

    </div>
    
    <div style="display: none;">
        <div id="X_InsertNewItem"><%=GetGlobalResourceObject("MessagesResource", "X_InsertNewItem")%></div>
        <div id="RequiredFields">
            <p><%=GetGlobalResourceObject("MessagesResource", "X_RequiredFieldErrors")%></p>
            <p><%=GetGlobalResourceObject("MessagesResource", "X_MarkedRequiredFields")%></p>
        </div>
        <div id="Step1">
            <p><%=GetGlobalResourceObject("MessagesResource", "X_Validating")%></p>
        </div>
        <div id="Step3">
            <p><%=GetGlobalResourceObject("MessagesResource", "X_Saving")%></p>
        </div>
    </div>

</asp:Content>