<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="UniversityInformation.aspx.cs" Inherits="LMIS.Portal.IndividualRegistration.UniversityInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../IndividualRegistration/Scripts/UniversityInformation.js"></script>
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="Scripts/js.js"></script>
    <div id="divschoolInfo">
        <div class="row form-divider ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F009_UniversityInformation")%> </div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>
        <div class="col-sm-5 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_UniversityName")%> *</label>
                <select class="form-control validationElement" id="ddUniversityName" name="UniversityName" data-bind="value:UniversityName, optionsText: 'desc', options: optionsUniversityName, optionsValue: 'id', valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
            <div class="form-group" id="divUniversityName"  style="display: none;">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_UniversityName")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtotherUniversityName_A" class="form-control validationElement" maxlength="100" data-bind="value: otherUniversityName.A, attr: { placeholder: otherUniversityName.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnotherUniversityName" class="btn btn-default always-enabled" data-bind="click: function () { otherUniversityName.LocalizeView(!otherUniversityName.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtotherUniversityName_B" class="form-control" maxlength="100" data-bind="value: otherUniversityName.B, attr: { placeholder: otherUniversityName.B.ph }, visible: !otherUniversityName.Localized()" />
                <input type="text" id="txtotherUniversityName_C" class="form-control" maxlength="100" data-bind="value: otherUniversityName.C, attr: { placeholder: otherUniversityName.C.ph }, visible: !otherUniversityName.Localized()" />
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_UniversityCertificate")%> *</label>
                <select class="form-control validationElement" id="ddUniversityCertificate" name="UniversityCertificate" data-bind="value:UniversityCertificate, optionsText: 'desc', options: optionsUniversityCertificate, optionsValue: 'id', valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_UniversityGrade")%> *</label>
                <select class="form-control validationElement" id="ddUniversityGrade" name="UniversityGrade" data-bind="value:UniversityGrade, optionsText: 'desc', options: optionsUniversityGrade, optionsValue: 'id', valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>
        <div class="col-sm-5">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_UniversityFacultyName")%> *</label>
                <select class="form-control validationElement" id="ddUniversityFacultyName" name="UniversityFacultyName" data-bind="value:UniversityFacultyName, optionsText: 'desc', options: optionsUniversityFacultyName, optionsValue: 'id', valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_UniversityGraduationyear")%> *</label>
                <input id="txtUniversityGraduationyear" type="text" name="name" class="form-control validationElement" placeholder="ex. 2002" data-bind="value: graduationyear" />
            </div>

            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_UniversityGradeGPA")%> *</label>
                <input id="txtUniversityGradeGPA" type="text" name="name" class="form-control validationElement" placeholder="ex. 90%" data-bind="value: GradeGPA" />
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
