<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="AddNewTraining.aspx.cs" Inherits="LMIS.Portal.IndividualRegistration.AddNewTraining" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../IndividualRegistration/Scripts/AddNewTraining.js"></script>
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="Scripts/js.js"></script>
    <div id="divtrainingInfo">
        <div class="row form-divider ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("MessagesResource", "X_Training")%> </div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>
        <div class="col-sm-5 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_TrainingName")%> *</label>
                <div class="input-group">
                    <input type="text" id="txttrainingname_A" class="form-control validationElement" maxlength="100" data-bind="value: trainingname.A, attr: { placeholder: trainingname.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btntrainingname" class="btn btn-default always-enabled" data-bind="click: function () { trainingname.LocalizeView(!trainingname.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txttrainingname_B" class="form-control" maxlength="100" data-bind="value: trainingname.B, attr: { placeholder: trainingname.B.ph }, visible: !trainingname.Localized()" />
                <input type="text" id="txttrainingname_C" class="form-control" maxlength="100" data-bind="value: trainingname.C, attr: { placeholder: trainingname.C.ph }, visible: !trainingname.Localized()" />
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_StartDate")%> *</label>
                <input type="text" id="dtStartDate" class="form-control validationElement" data-bind="value: StartDate" />
            </div>
        </div>
        <div class="col-sm-5">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_TrainingProvider")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtTrainingProvider_A" class="form-control validationElement" maxlength="100" data-bind="value: TrainingProvider.A, attr: { placeholder: TrainingProvider.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnTrainingProvider" class="btn btn-default always-enabled" data-bind="click: function () { TrainingProvider.LocalizeView(!TrainingProvider.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtTrainingProvider_B" class="form-control" maxlength="100" data-bind="value: TrainingProvider.B, attr: { placeholder: TrainingProvider.B.ph }, visible: !TrainingProvider.Localized()" />
                <input type="text" id="txtTrainingProvider_C" class="form-control" maxlength="100" data-bind="value: TrainingProvider.C, attr: { placeholder: TrainingProvider.C.ph }, visible: !TrainingProvider.Localized()" />
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_EndDate")%> *</label>
                <input type="text" id="dtEndDate" class="form-control validationElement" data-bind="value: EndDate" />
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
