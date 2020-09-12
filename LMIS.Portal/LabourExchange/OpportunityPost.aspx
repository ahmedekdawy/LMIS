<%@ Page Language="C#" MasterPageFile="~/MasterPages/LabourExchange.master" AutoEventWireup="true" CodeBehind="OpportunityPost.aspx.cs" Inherits="LMIS.Portal.LabourExchange.OpportunityPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/OpportunityPost.js" async="async"></script>
    
    <div class="tab-pane fade in active" id="tab5default">
        
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
            <div class="col-lg-4" >
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title">
                <%=GetGlobalResourceObject("CommonControls", "F005")%>
            </div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>
        <div class="col-sm-10 col-sm-offset-1">
            <div class="form-group">
                <label id="lblTitle"><%=GetGlobalResourceObject("CommonControls", "F005_OppTitle")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtTitle_A" class="form-control validationElement" maxlength="50" data-bind="value: Title.A, attr: { placeholder: Title.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnTitles" class="btn btn-default" data-bind="click: function() { Title.LocalizeView(!Title.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtTitle_B" class="form-control" maxlength="50" data-bind="value: Title.B, attr: { placeholder: Title.B.ph }, visible: !Title.Localized()" />
                <input type="text" id="txtTitle_C" class="form-control" maxlength="50" data-bind="value: Title.C, attr: { placeholder: Title.C.ph }, visible: !Title.Localized()" />
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F005_OppUpload")%> *</label>
                <div class="input-group">
                    <input type="text" id="txtFileName" class="form-control validationElement" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectAnImage")%>">
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
        <div class="col-sm-10 col-sm-offset-1" data-bind="visible: IsInternal()">
            <div class="form-group">
                <label class="checkbox-inline">
                    <input type="checkbox" data-bind="checked: IsInformal">
                    <span><%=GetGlobalResourceObject("CommonControls", "F005_IsInformal")%></span>
                </label>
            </div>         
        </div>
        <div class="row text-center" data-bind="visible: Mode() !== 'v'">
            <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "X_UploadAndSave")%>" class="btn btn-default btn-primary" type="button" data-bind="click: StartWorkflow" />
        </div> 
    </div>

    <div style="display: none;">
        <div id="RequiredFields">
            <p><%=GetGlobalResourceObject("MessagesResource", "X_RequiredFieldErrors")%></p>
            <p><%=GetGlobalResourceObject("MessagesResource", "F005_RequiredFields")%></p>
        </div>
        <div id="Step1">
            <p><%=GetGlobalResourceObject("MessagesResource", "F005_Step1")%></p>
        </div>
        <div id="Step3">
            <p><%=GetGlobalResourceObject("MessagesResource", "F005_Step3")%></p>
        </div>
    </div>

</asp:Content>