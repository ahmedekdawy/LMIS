<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="InstitutionInformation.aspx.cs" Inherits="LMIS.Portal.IndividualRegistration.InstitutionInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../IndividualRegistration/Scripts/InstitutionInformation.js"></script>
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="Scripts/js.js"></script>
    <div id="divschoolInfo">
        <div class="row form-divider ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F009_InstitutionInformation")%> </div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>
        <div class="col-sm-5 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_Institutiondegree")%> *</label>
                <select class="form-control validationElement" id="ddiInstitutiondegree" name="institutiondegree" data-bind="value:institutiondegree, optionsText: 'desc', options: optionsinstitutiondegree, optionsValue: 'id', valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>

            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_Institutiongrade")%> *</label>
                <select class="form-control validationElement" id="ddinstitutiongrade" name="institutiongrade" data-bind="value:institutiongrade, optionsText: 'desc', options: optionsinstitutiongrade, optionsValue: 'id', valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_InstitutionGraduationyear")%> *</label>
                <input id="txtinstitutionGraduationyear" type="text" name="name" class="form-control validationElement" placeholder="ex. 2002" data-bind="value: graduationyear" />
            </div>
        </div>
        <div class="col-sm-5">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_InstitutionName")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtinstitutionName_A" class="form-control validationElement" maxlength="100" data-bind="value: institutionName.A, attr: { placeholder: institutionName.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btninstitutionName" class="btn btn-default always-enabled" data-bind="click: function () { institutionName.LocalizeView(!institutionName.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtinstitutionName_B" class="form-control" maxlength="100" data-bind="value: institutionName.B, attr: { placeholder: institutionName.B.ph }, visible: !institutionName.Localized()" />
                <input type="text" id="txtinstitutionName_C" class="form-control" maxlength="100" data-bind="value: institutionName.C, attr: { placeholder: institutionName.C.ph }, visible: !institutionName.Localized()" />
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_Institutiongradepercentage")%> *</label>
                <input id="txtinstitutiongradepercentage" type="text" name="name" class="form-control validationElement" placeholder="ex. 90%" data-bind="value: graduationpercentage" />
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
