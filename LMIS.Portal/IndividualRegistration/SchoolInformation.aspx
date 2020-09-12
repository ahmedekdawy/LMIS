<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind=".aspx.cs" Inherits="LMIS.Portal.IndividualRegistration.SchoolInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../IndividualRegistration/Scripts/SchoolInformation.js"></script>
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="Scripts/js.js"></script>
    <div id="divschoolInfo">
        <div class="row form-divider ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F009_SchoolInfo")%> </div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>
        <div class="col-sm-5 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_Schooldegree")%> *</label>
                <select class="form-control validationElement" id="ddlschooldegree" name="schooldegree" data-bind="value:schooldegree, optionsText: 'desc', options: optionsschooldegree, optionsValue: 'id', valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>

            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_SchoolGrade")%> *</label>
                <input id="txtschoolgrade" type="text" max="100" class="form-control validationElement" placeholder="ex. 90%" data-bind="value: schoolgrade" />
            </div>
        </div>
        <div class="col-sm-5">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_SchoolName")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtschoolname_A" class="form-control validationElement" maxlength="100" data-bind="value: schoolname.A, attr: { placeholder: schoolname.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnschoolname" class="btn btn-default always-enabled" data-bind="click: function () { schoolname.LocalizeView(!schoolname.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtschoolname_B" class="form-control" maxlength="100" data-bind="value: schoolname.B, attr: { placeholder: schoolname.B.ph }, visible: !schoolname.Localized()" />
                <input type="text" id="txtschoolname_C" class="form-control" maxlength="100" data-bind="value: schoolname.C, attr: { placeholder: schoolname.C.ph }, visible: !schoolname.Localized()" />
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_GraduationYear")%> *</label>
                <input id="txtgraduationyear" type="text" name="name" class="form-control validationElement" placeholder="ex. 2002" data-bind="value: graduationyear" />
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
        </div>
    </div>
</asp:Content>
