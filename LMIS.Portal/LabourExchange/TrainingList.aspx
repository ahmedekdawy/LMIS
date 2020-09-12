<%@ Page Title="<%$ Resources:CommonControls, F015 %>" Language="C#" MasterPageFile="~/MasterPages/LabourExchange.master" AutoEventWireup="true" CodeBehind="TrainingList.aspx.cs" Inherits="LMIS.Portal.LabourExchange.TrainingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/TrainingList.js" async="async"></script>

    <div class="tab-pane fade in active" id="tab-content">
        <div id="Table" class="col-sm-12">
            <div class="row form-divider col-sm-12 ">
                <div class="col-lg-4"><hr></div>
                <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F015_AllOffers")%></div>
                <div class="col-lg-4"><hr></div>
            </div>
            <div class="col-sm-12 text-center">
                <a id="postNew" href="../LabourExchange/TrainingPost#anchor" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F015_PostOffer")%>
                </a>
            </div>
            <div class="form-group col-sm-12" style="padding-top: 25px; padding-bottom: 25px;">
                <label style="padding-right: 10px;"><%=GetGlobalResourceObject("CommonControls", "F015_CourseList")%></label>
                <div class="input-group">
                    <input type="text" id="txtFileName" class="form-control" disabled="disabled" style="background-color: white;" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectFile")%>">
                    <span class="input-group-btn">
                        <button class="btn btn-default" onclick="$('#hdnFileBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                    </span>
                </div>
                <input type="file" id="hdnFileBrowser" data-bind="attr: { accept: AcceptedFiles }, event: { change: ValidateFile }" style="height: 0; visibility: hidden;"/>
                <p class="form-description">
                    <span style="padding-right: 10px;">PDF , MS Word , MS PowerPoint</span>
                    <a class="text-center" href="#" target="_blank" data-bind="visible: !lmis.string.isNullOrWhiteSpace(ServerFileName()), attr: { href: lmis.x.downloadURL + ServerFileName() }">
                        <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                    </a>
                </p>
                <div class="text-center">
                    <input value="<%=GetGlobalResourceObject("CommonControls", "X_UploadFile")%>" class="btn btn-success btn-choose-graph btn-sm" style="width: 100px;" type="button" data-bind="click: UploadAndSave" />
                    <input value="<%=GetGlobalResourceObject("CommonControls", "X_DeleteFile")%>" class="btn btn-danger btn-choose-graph btn-sm" style="width: 100px;" type="button" data-bind="click: DeleteFile" />
                </div>
            </div>
            <table id="grd" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F015_CourseTitle")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "X_Status")%></th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody data-bind="foreach: VmList">
                    <tr data-bind="attr: { id: RowId }">
                        <td data-bind="text: Title"></td>
                        <td data-bind="text: lmis.format.approvalStatus(Approval)"></td>
                        <td>
                            <a href="#" title=" View " data-bind="click: $root.View" class="editBtn"><i class="fa fa-eye"></i></a>
                            <a href="#" title=" Edit " data-bind="click: $root.Edit" class="editBtn"><i class="fa fa-edit"></i></a>
                            <a href="#" title=" Delete " data-bind="click: $root.Delete"><i class="fa fa-trash-o"></i></a>
                        </td>
                        <td>
                            <a class="actionLabel" data-bind="click: $root.DetailApplicants"><%=GetGlobalResourceObject("CommonControls", "X_Applicants")%></a>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div id="applicants" class="col-sm-12 grd" style="padding-top: 40px;">
                <table data-bind="visible: !lmis.string.isNullOrWhiteSpace(DetailsFor())" style="border-spacing: 0; width: 100%;">
                    <thead>
                        <tr>
                            <th><%=GetGlobalResourceObject("CommonControls", "F015_ApplicantsFor")%> <span data-bind="html: DetailsFor"></span></th>
                            <th>...</th>
                        </tr>
                    </thead>
                    <tbody data-bind="foreach: Applicants">
                        <tr>
                            <td data-bind="text: desc"></td>
                            <td>
                              <a href="#" target="_blank" data-bind=" attr: { href: '/Individual/Profile?m=v&k=' + id + '#anchor' }">
                                <label class="actionLabel">
                                    <%=GetGlobalResourceObject("CommonControls", "X_Details")%>
                                </label>
                                    </a>
                            </td>
                        </tr>
                    </tbody>
                    <tbody data-bind="visible: Applicants().length < 1">
                        <tr>
                            <td colspan="2">
                                <%=GetGlobalResourceObject("MessagesResource", "F015_NoApplicants")%>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

</asp:Content>