<%@ Page Title="<%$ Resources:CommonControls, F004 %>" Language="C#" MasterPageFile="~/MasterPages/LabourExchange.master" AutoEventWireup="true" CodeBehind="JobPost.aspx.cs" Inherits="LMIS.Portal.LabourExchange.JobPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/JobPost.js" async="async"></script>

    <div class="tab-pane fade in active" id="tab-content">
        
        <div class="col-sm-10 col-sm-offset-1" style="display: none; text-align: center;" data-bind="visible: Mode() === 'v' && !vmReview">
            <button class="btn btn-default always-enabled" type="button" data-bind="click: CVSearch">
                <i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "F004_CVSearch")%>
            </button>
            <p></p>
        </div>

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
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F004_JobDetails")%></div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>

        <div class="col-sm-10 col-sm-offset-1">

            <div class="form-group">
                <label id="lblTitle"><%=GetGlobalResourceObject("CommonControls", "F004_JobTitle")%>  *</label>
                <select id="ddlTitle" class="form-control always-white validationElement" data-bind="options: TitleOptions, value: Title, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>'">
                </select>
                <div data-bind="visible: (Title() == '+')">
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
                <label id="lblDescription"><%=GetGlobalResourceObject("CommonControls", "F004_JobDescription")%> *</label>
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
                <label><%=GetGlobalResourceObject("CommonControls", "F004_ApplicationForm")%></label>
                <div class="input-group">
                    <input type="text" id="txtFileName" class="form-control always-disabled" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectFile")%>">
                    <span class="input-group-btn">
                        <button class="btn btn-default" onclick="$('#hdnFileBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                    </span>
                </div>
                <input type="file" id="hdnFileBrowser" data-bind="attr: { accept: AcceptedFiles }, event: { change: ValidateFile }" style="height: 0; visibility: hidden;"/>
                <p class="form-description">
                    <span style="padding-right: 10px;">PDF , MS Word</span>
                    <a class="text-center" href="#" target="_blank" data-bind="visible: !lmis.string.isNullOrWhiteSpace(ServerFileName()), attr: { href: lmis.x.downloadURL + ServerFileName() }">
                        <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                    </a>
                </p>
            </div>
            
            <div class="form-group text-center" data-bind="visible: Mode() !== 'v'">
                <input value="<%=GetGlobalResourceObject("CommonControls", "X_ClearFile")%>" class="btn btn-choose-graph btn-sm btn-danger" style="width: 100px;" type="button" data-bind="click: ClearFile" />
            </div>

        </div>

        <div class="row form-divider col-sm-12">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F004_JobSpecs")%></div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>
        
        <div class="col-sm-3 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F004_ExpFrom")%> *</label>
                <input type="text" id="txtExpFrom" class="form-control validationElement" data-bind="value: ExpFrom" />
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F004_ExpTo")%> *</label>
                <input type="text" id="txtExpTo" class="form-control validationElement" data-bind="value: ExpTo" />
            </div>
        </div>

        <div class="col-sm-4">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F004_VacantPositions")%> *</label>
                <input type="text" id="txtVacancies" class="form-control validationElement" data-bind="value: Vacancies" />
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
                <label><%=GetGlobalResourceObject("CommonControls", "F004_EmploymentType")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: EmploymentTypeOptions, value: EmploymentType, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
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
                <label><%=GetGlobalResourceObject("CommonControls", "X_Gender")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: GenderOptions, value: Gender, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>
        
        <div class="col-sm-3 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_EducationLevel")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: EdLevelOptions, value: EdLevel, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F004_JobStatus")%> *</label>
                <br>
                <label class="checkbox-inline">
                    <input type="checkbox" data-bind="checked: JobStatus" class="validationElement">
                    <span><%=GetGlobalResourceObject("MessagesResource", "F004_JobIsOpen")%></span>
                </label>
            </div>         
        </div>
        
        <div class="col-sm-7">
            <div class="form-group">
                <label id="lblEdCert"><%=GetGlobalResourceObject("CommonControls", "X_EducationCertificate")%> *</label>
                <div>
                    <div class="input-group">
                        <input type="text" id="txtEdCert_A" class="form-control validationElement" maxlength="100" data-bind="value: EdCert.A, attr: { placeholder: EdCert.A.ph }" />
                        <span class="input-group-btn">
                            <button id="btnEdCert" class="btn btn-default always-enabled" data-bind="click: function () { EdCert.LocalizeView(!EdCert.Localized()); }">
                                <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                            </button>
                        </span>
                    </div>
                    <input type="text" id="txtEdCert_B" class="form-control" maxlength="100" data-bind="value: EdCert.B, attr: { placeholder: EdCert.B.ph }, visible: !EdCert.Localized()" />
                    <input type="text" id="txtEdCert_C" class="form-control" maxlength="100" data-bind="value: EdCert.C, attr: { placeholder: EdCert.C.ph }, visible: !EdCert.Localized()" />
                </div>
            </div>
        </div>

        <div class="row form-divider col-sm-12 ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F004_Skills")%> *</div>
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
                <span class="validationElement">
                <select id="Skills" class="form-control text-center validationElement" multiple="multiple" data-bind="multiselect: lmis.defaults.multiselectOptions, selectedOptions: SkillSelections, foreach: SkillOptions">
                    <optgroup data-bind="attr: { label: $data.desc }, foreach: $data.options" label="">
                        <option data-bind="value: $data.id, text: $data.desc"></option>
                    </optgroup>
                </select>
                    </span>
                <div class="input-group">
                    <input type="text" id="txtNewSkill" class="form-control " maxlength="50" data-bind="value: NewSkill" />
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

        <div class="row form-divider col-sm-12 ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F004_SalaryRange")%></div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>

        <div class="col-sm-3 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F004_PerMonth")%> **</label>
                <input type="text" id="txtPerMonth" class="form-control validationElement" data-bind="value: PerMonth" />
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F004_PerHour")%> **</label>
                <input type="text" id="txtPerHour" class="form-control validationElement" data-bind="value: PerHour" />
            </div>
        </div>

        <div class="col-sm-4">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Currency")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: CurrencyOptions, value: Currency, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>

        <div class="row form-divider col-sm-12">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F004_MedicalCondition")%> *</div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>

        <div class="col-sm-10 col-sm-offset-1">
            <div class="form-group" data-bind="foreach: MedConditionOptions">
                <label class="checkbox-inline">
                    <input type="checkbox" data-bind="checked: $root.MedConditions, checkedValue: $data.id">
                    <span data-bind="text: $data.desc"></span>
                </label>
            </div>
        </div>

        <div class="row form-divider col-sm-12">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F004_Documents")%></div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>

        <div class="col-sm-10 col-sm-offset-1">
            <div class="form-group" data-bind="foreach: DocTypeOptions">
                <label class="checkbox-inline">
                    <input type="checkbox" data-bind="checked: $root.DocTypes, checkedValue: $data.id">
                    <span data-bind="text: $data.desc"></span>  
                </label>
            </div>
        </div>

        <div class="col-sm-10 col-sm-offset-1" data-bind="visible: Mode() !== 'v'">
            <input value="<%=GetGlobalResourceObject("CommonControls", "X_UploadAndSave")%>" class="btn btn-primary nextBtn btn-lg pull-right" type="button" data-bind="click: StartWorkflow" />
        </div>

    </div>

    <div style="display: none;">
        <div id="F004_AddJobTitle"><%=GetGlobalResourceObject("MessagesResource", "F004_AddJobTitle")%></div>
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