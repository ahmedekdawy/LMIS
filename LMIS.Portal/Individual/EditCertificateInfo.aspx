﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Popup.Master" AutoEventWireup="true" CodeBehind="EditCertificateInfo.aspx.cs" Inherits="LMIS.Portal.Individual.EditCertificateInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divtrainingInfo">
     <%--   <div class="row form-divider ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "X_Certificate")%> </div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>--%>
          <div class="col-sm-10 col-sm-offset-1">
        <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_CertificationName")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtCertificationName_A" class="form-control validationElement" maxlength="100" data-bind="value: CertificationName.A, attr: { placeholder: CertificationName.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnCertificationName" class="btn btn-default always-enabled" data-bind="click: function () { CertificationName.LocalizeView(!CertificationName.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtCertificationName_B" class="form-control" maxlength="100" data-bind="value: CertificationName.B, attr: { placeholder: CertificationName.B.ph }, visible: !CertificationName.Localized()" />
                <input type="text" id="txtCertificationName_C" class="form-control" maxlength="100" data-bind="value: CertificationName.C, attr: { placeholder: CertificationName.C.ph }, visible: !CertificationName.Localized()" />
            </div>
              </div>
        <div class="col-sm-5 col-sm-offset-1">            
            <div class="form-group">
              <label><%=GetGlobalResourceObject("CommonControls", "X_CertificationIssueDate")%> *</label>
                <input type="text" id="dtCertificationIssueDate" class="datepiker form-control validationElement" data-bind="value: CertificationIssueDate" />
                </div>
        </div>
        <div class="col-sm-5">           
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_CertificationValidUntil")%> *</label>
                <input type="text" id="dtCertificationValidUntil" class="datepiker form-control validationElement" data-bind="value: CertificationValidUntil" />
          </div>

        </div>
        <div class="col-sm-12">
            <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_Save")%>" class="btn btn-primary nextBtn btn-lg pull-right" type="button" data-bind="click: StartWorkflow" />
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

    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../Scripts/js.js"></script>
    <script src="../Individual/Scripts/edit-certificate-vm.js" async="async"></script>
</asp:Content>
