<%@ Page Title="<%$ Resources:CommonControls, F006 %>" Language="C#" MasterPageFile="~/MasterPages/LabourExchange.master" AutoEventWireup="true" CodeBehind="EventPost.aspx.cs" Inherits="LMIS.Portal.LabourExchange.EventPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/EventPost.js" async="async"></script>

    <div class="tab-pane fade in active" id="tab6default">
        
        <div class="col-sm-10 col-sm-offset-1" style="display: none;" data-bind="visible: Mode() !== 'p' && !IsInternal()">
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

        <div class="row form-divider col-sm-12">
            <div class="col-lg-4"><hr></div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F006_EventDetails")%></div>
            <div class="col-lg-4"><hr></div>
        </div>
        
        <div class="col-sm-10 col-sm-offset-1">
            <div class="form-group">
                <label id="lblTitle"><%=GetGlobalResourceObject("CommonControls", "F006_Titles")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtTitle_A" class="form-control validationElement" maxlength="100" data-bind="value: Title.A, attr: { placeholder: Title.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnTitle" class="btn btn-default always-enabled" data-bind="click: function () { Title.LocalizeView(!Title.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtTitle_B" class="form-control" maxlength="100" data-bind="value: Title.B, attr: { placeholder: Title.B.ph }, visible: !Title.Localized()" />
                <input type="text" id="txtTitle_C" class="form-control" maxlength="100" data-bind="value: Title.C, attr: { placeholder: Title.C.ph }, visible: !Title.Localized()" />
            </div>
        </div>

        <div class="col-sm-5 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_StartDate")%> *</label>
                <input type="text" id="txtStartDate" class="form-control validationElement" data-bind="value: StartDate" />
            </div>
        </div>

        <div class="col-sm-5">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_EndDate")%> *</label>
                <input type="text" id="txtEndDate" class="form-control validationElement" data-bind="value: EndDate" />
            </div>
        </div>
        
        <div class="col-sm-10 col-sm-offset-1">
            <div class="form-group">
                <label id="lblEventAddress"><%=GetGlobalResourceObject("CommonControls", "F006_Address")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtAddress_A" class="form-control validationElement" maxlength="200" data-bind="value: Address.A, attr: { placeholder: Address.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnAddress" class="btn btn-default always-enabled" data-bind="click: function () { Address.LocalizeView(!Address.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtAddress_B" class="form-control" maxlength="200" data-bind="value: Address.B, attr: { placeholder: Address.B.ph }, visible: !Address.Localized()" />
                <input type="text" id="txtAddress_C" class="form-control" maxlength="200" data-bind="value: Address.C, attr: { placeholder: Address.C.ph }, visible: !Address.Localized()" />
            </div>
        </div>
        
        <div class="col-sm-5 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F006_Type")%> *</label>
                <select class="form-control always-white validationElement" data-bind="options: TypeOptions, value: Type, optionsValue: 'id', optionsText: 'desc', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
        </div>

        <div class="col-sm-5">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Price")%></label>
                <input type="text" id="txtPrice" class="form-control" data-bind="value: Price" placeholder="<%=GetGlobalResourceObject("CommonControls", "X_Free")%>" />
            </div>
        </div>
        
        <div class="col-sm-10 col-sm-offset-1" data-bind="visible: IsInternal()">
            <div class="form-group">
                <label class="checkbox-inline">
                    <input type="checkbox" data-bind="checked: IsInformal">
                    <span><%=GetGlobalResourceObject("CommonControls", "F006_IsInformal")%></span>
                </label>
            </div>         
        </div>

        <div class="row form-divider col-sm-12">
            <div class="col-lg-4"><hr></div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F006_ContactInfo")%></div>
            <div class="col-lg-4"><hr></div>
        </div>
        
        <div class="col-sm-10 col-sm-offset-1">
            <div class="form-group">
                <label id="lblContactAddress"><%=GetGlobalResourceObject("CommonControls", "X_Address")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtContactAddress_A" class="form-control validationElement" maxlength="200" data-bind="value: ContactAddress.A, attr: { placeholder: ContactAddress.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnContactAddress" class="btn btn-default always-enabled" data-bind="click: function () { ContactAddress.LocalizeView(!ContactAddress.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtContactAddress_B" class="form-control" maxlength="200" data-bind="value: ContactAddress.B, attr: { placeholder: ContactAddress.B.ph }, visible: !ContactAddress.Localized()" />
                <input type="text" id="txtContactAddress_C" class="form-control" maxlength="200" data-bind="value: ContactAddress.C, attr: { placeholder: ContactAddress.C.ph }, visible: !ContactAddress.Localized()" />
            </div>
        </div>
        
        <div class="col-sm-5 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Telephone")%> *</label>
                <input type="text" id="txtContactTelephone" class="form-control validationElement" data-bind="value: ContactTelephone" maxlength="20">
            </div>
        </div>
        
        <div class="col-sm-5">
            <div class="form-group">
                <label id="lblContactWebsite"><%=GetGlobalResourceObject("CommonControls", "X_Website")%> *</label>
                <input type="text" id="txtContactWebsite" class="form-control validationElement" data-bind="value: ContactWebsite" maxlength="200">
            </div>
        </div>

        <div class="col-sm-10 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F006_Upload")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtFileName" class="form-control always-disabled validationElement" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectAnImage")%>">
                    <span class="input-group-btn">
                        <button class="btn btn-default" onclick="$('#hdnFileBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                    </span>
                </div>
                <input type="file" id="hdnFileBrowser" data-bind="attr: { accept: AcceptedFiles }, event: { change: ValidateFile }" style="height: 0; visibility: hidden;"/>
                <p class="form-description">
                    <span style="padding-right: 10px;">JPG , PNG , GIF</span>
                    <a class="text-center" href="#" target="_blank" data-bind="visible: !lmis.string.isNullOrWhiteSpace(ServerFileName()), attr: { href: lmis.x.downloadURL + ServerFileName() }">
                        <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                    </a>
                </p>
            </div>   
        </div>

        <div class="col-sm-12" data-bind="visible: Mode() !== 'v'">
            <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_SaveAndPost")%>" class="btn btn-success nextBtn btn-lg pull-right" type="button" data-bind="click: StartWorkflow" />
        </div>

    </div>
    
    <div style="display: none;">
        <p id="InvalidUrl"><%=GetGlobalResourceObject("MessagesResource", "F006_InvalidUrl")%></p>
        <div id="RequiredFields">
            <p><%=GetGlobalResourceObject("MessagesResource", "X_RequiredFieldErrors")%></p>
            <p><%=GetGlobalResourceObject("MessagesResource", "F006_RequiredFields")%></p>
        </div>
        <div id="Step1">
            <p><%=GetGlobalResourceObject("MessagesResource", "F006_Step1")%></p>
        </div>
        <div id="Step3">
            <p><%=GetGlobalResourceObject("MessagesResource", "F006_Step3")%></p>
        </div>
    </div>

</asp:Content>