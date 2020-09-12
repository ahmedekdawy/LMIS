<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Popup.Master" AutoEventWireup="true" CodeBehind="EditEducationalInfo.aspx.cs" Inherits="LMIS.Portal.Individual.EditEducationalInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="divschoolInfo">
        <div class="col-sm-12">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F014_LevelofEducation")%>  *</label>
                <select class="form-control validationElement" id="ddlevelofeducation" name="Levelofeducation"  data-bind="value: leveleducation,event: { change: LevelofEducationChange }, optionsText: 'desc', options: optionsleveleducation, optionsValue: 'id', valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>
        <div class="col-sm-6">
         
            <div class="form-group" id="divUniversityName" >
                <label><%=GetGlobalResourceObject("CommonControls", "F009_InstitutionName")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtUniversityName_A" class="form-control validationElement" maxlength="100" data-bind="value: UniversityName.A, attr: { placeholder: UniversityName.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnUniversityName" class="btn btn-default always-enabled" data-bind="click: function () { UniversityName.LocalizeView(!UniversityName.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtUniversityName_B" class="form-control" maxlength="100" data-bind="value: UniversityName.B, attr: { placeholder: UniversityName.B.ph }, visible: !UniversityName.Localized()" />
                <input type="text" id="txtUniversityName_C" class="form-control" maxlength="100" data-bind="value: UniversityName.C, attr: { placeholder: UniversityName.C.ph }, visible: !UniversityName.Localized()" />
            </div>
            <div class="form-group" id="divCertificate">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_UniversityCertificate")%> *</label>
              
                <div class="input-group">
                    <input type="text" id="txtUniversityCertificate_A" class="form-control validationElement" maxlength="100" data-bind="value: UniversityCertificate.A, attr: { placeholder: UniversityCertificate.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnUniversityCertificate" class="btn btn-default always-enabled" data-bind="click: function () { UniversityCertificate.LocalizeView(!UniversityCertificate.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtUniversityCertificate_B" class="form-control" maxlength="100" data-bind="value: UniversityCertificate.B, attr: { placeholder: UniversityCertificate.B.ph }, visible: !UniversityCertificate.Localized()" />
                <input type="text" id="txtUniversityCertificate_C" class="form-control" maxlength="100" data-bind="value: UniversityCertificate.C, attr: { placeholder: UniversityCertificate.C.ph }, visible: !UniversityCertificate.Localized()" />
           
               
              
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_UniversityGrade")%> *</label>
                
              <div class="input-group">
                    <input type="text" id="txtUniversityGrade_A" class="form-control validationElement" maxlength="100" data-bind="value: UniversityGrade.A, attr: { placeholder: UniversityGrade.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnUniversityGrade" class="btn btn-default always-enabled" data-bind="click: function () { UniversityGrade.LocalizeView(!UniversityGrade.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtUniversityGrade_B" class="form-control" maxlength="100" data-bind="value: UniversityGrade.B, attr: { placeholder: UniversityGrade.B.ph }, visible: !UniversityGrade.Localized()" />
                <input type="text" id="txtUniversityGrade_C" class="form-control" maxlength="100" data-bind="value: UniversityGrade.C, attr: { placeholder: UniversityGrade.C.ph }, visible: !UniversityGrade.Localized()" />
           
            </div>
        </div>
        <div class="col-sm-6">
            <div class="form-group" id="divFaculty">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_UniversityFacultyName")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtUniversityFacultyName_A" class="form-control validationElement" maxlength="100" data-bind="value: UniversityFacultyName.A, attr: { placeholder: UniversityFacultyName.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnUniversityFacultyName" class="btn btn-default always-enabled" data-bind="click: function () { UniversityFacultyName.LocalizeView(!UniversityFacultyName.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtUniversityFacultyName_B" class="form-control" maxlength="100" data-bind="value: UniversityFacultyName.B, attr: { placeholder: UniversityFacultyName.B.ph }, visible: !UniversityFacultyName.Localized()" />
                <input type="text" id="txtUniversityFacultyName_C" class="form-control" maxlength="100" data-bind="value: UniversityFacultyName.C, attr: { placeholder: UniversityFacultyName.C.ph }, visible: !UniversityFacultyName.Localized()" />
           
               
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
            <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_Add")%>" class="btn btn-primary nextBtn btn-lg pull-right" type="button" data-bind="click: StartWorkflow" />
        </div>
        <div style="display: none;">
            <div id="RequiredFields">
                <p><%=GetGlobalResourceObject("MessagesResource", "X_RequiredFieldErrors")%></p>
                <p><%=GetGlobalResourceObject("MessagesResource", "X_MarkedRequiredFields")%></p>
            </div>
        </div>
    </div>
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <%--<script src="../Scripts/js.js"></script>--%>
    <script src="../Individual/Scripts/edit-education-vm.js" async="async"></script>
</asp:Content>
