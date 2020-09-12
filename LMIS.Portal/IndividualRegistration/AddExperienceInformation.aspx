<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="AddExperienceInformation.aspx.cs" Inherits="LMIS.Portal.IndividualRegistration.AddExperienceInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../IndividualRegistration/Scripts/AddExperienceInformation.js"></script>
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../IndividualRegistration/Scripts/js.js"></script>
    <div id="divExperince">
        <div class="row form-divider ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("MessagesResource", "F009_Employer")%></div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>
        <div class="col-sm-5 col-sm-offset-1">

            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_Employername")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtemployername_A" class="form-control validationElement" maxlength="100" data-bind="value: employername.A, attr: { placeholder: employername.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnemployername" class="btn btn-default always-enabled" data-bind="click: function () { employername.LocalizeView(!employername.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtemployername_B" class="form-control" maxlength="100" data-bind="value: employername.B, attr: { placeholder: employername.B.ph }, visible: !employername.Localized()" />
                <input type="text" id="txtemployername_C" class="form-control" maxlength="100" data-bind="value: employername.C, attr: { placeholder: employername.C.ph }, visible: !employername.Localized()" />
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_StartDate")%> *</label>
                <input type="text" id="dtStartDate" class="form-control validationElement" data-bind="value: StartDate" />
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_JobDesc")%> *</label>
                <div class="trilingual-textarea">
                    <label lang="en" data-bind="css: lmis.string.isNullOrWhiteSpace(JobDesc.A()) ? 'unedited' : 'edited'">
                        <input type="radio" value="en" class="always-enabled" data-bind="checked: JobDesc.ActiveLang, click: function () { $('#txtJobDesc_En').focus(); return true; }">
                        <%=GetGlobalResourceObject("CommonControls", "X_English")%>
                    </label>
                    <label lang="fr" data-bind="css: lmis.string.isNullOrWhiteSpace(JobDesc.B()) ? 'unedited' : 'edited'">
                        <input type="radio" value="fr" class="always-enabled" data-bind="checked: JobDesc.ActiveLang, click: function () { $('#txtJobDesc_Fr').focus(); return true; }">
                        <%=GetGlobalResourceObject("CommonControls", "X_French")%>
                    </label>
                    <label lang="ar" data-bind="css: lmis.string.isNullOrWhiteSpace(JobDesc.C()) ? 'unedited' : 'edited'">
                        <input type="radio" value="ar" class="always-enabled" data-bind="checked: JobDesc.ActiveLang, click: function () { $('#txtJobDesc_Ar').focus(); return true; }">
                        <%=GetGlobalResourceObject("CommonControls", "X_Arabic")%>
                    </label>
                </div>
                <textarea id="txtJobDesc_En" maxlength="1000" data-bind="textInput: JobDesc.A, visible: (JobDesc.ActiveLang() == 'en')" class="form-control always-white" rows="8"></textarea>
                <textarea id="txtJobDesc_Fr" maxlength="1000" data-bind="textInput: JobDesc.B, visible: (JobDesc.ActiveLang() == 'fr')" class="form-control always-white" rows="8"></textarea>
                <textarea id="txtJobDesc_Ar" maxlength="1000" data-bind="textInput: JobDesc.C, visible: (JobDesc.ActiveLang() == 'ar')" class="form-control always-white" rows="8"></textarea>
            </div>

        </div>
        <div class="col-sm-5">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_JobTitle")%> *</label>
                <input id="txtJobTitle" type="text" name="name" class="form-control validationElement" data-bind="value: JobTitle" />
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_EndDate")%> *</label>
                <input type="text" id="dtEndDate" class="form-control validationElement" data-bind="value: EndDate" />
            </div>

            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_TypeofEmployment")%> *</label>
                <select class="form-control validationElement" id="ddlTypeofEmployment" name="TypeofEmployment" data-bind="value:TypeofEmployment,optionsText: 'desc', optionsValue: 'id', options: optionsTypeofEmployment,valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
            <div class="form-group">
                <label class="checkbox-inline">
                    <%=GetGlobalResourceObject("CommonControls", "F009_Currentemployer")%>
                    <input type="checkbox" id="cbCurrentemployer" class="" data-bind="value: Currentemployer">
                </label>
            </div>

        </div>
        <div class="col-sm-12">
            <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_SaveAndContinue")%>" class="btn btn-success nextBtn btn-lg pull-right" type="button" data-bind="click: StartWorkflow" />
        </div>
        <div style="display: none;">
            <div id="RequiredFields">
                <p><%=GetGlobalResourceObject("MessagesResource", "X_RequiredFieldErrors")%></p>
                <p><%=GetGlobalResourceObject("MessagesResource", "X_MarkedRequiredFields")%></p>
            </div>
             <div id="CompareDate">
                <p><%=GetGlobalResourceObject("MessagesResource", "X_RequiredFieldErrors")%></p>
                 <p><%=GetGlobalResourceObject("CommonControls", "X_CompareDate")%></p>
            </div>
        </div>
    </div>
</asp:Content>
