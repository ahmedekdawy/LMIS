<%@ Page Title="<%$ Resources:CommonControls, F004 %>" Language="C#" MasterPageFile="~/MasterPages/LabourExchange.master" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="LMIS.Portal.LabourExchange.JobList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/JobList.js" async="async"></script>

    <div class="tab-pane fade in active" id="tab-content">
        <div id="Table" class="col-sm-12">
            <div class="row form-divider col-sm-12 ">
                <div class="col-lg-4"><hr></div>
                <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F004_AllOffers")%></div>
                <div class="col-lg-4"><hr></div>
            </div>
            <div class="col-sm-12 text-center">
                <a id="postNew" href="../LabourExchange/JobPost#anchor" class="btn btn-success nextBtn btn-lg">
                    <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F004_PostOffer")%>
                </a>
            </div>
            <table id="grd" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F004_JobTitle")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "X_PostDate")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "X_Status")%></th>
                        <th></th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody data-bind="foreach: VmList">
                    <tr data-bind="attr: { id: RowId }">
                        <td data-bind="text: Title"></td>
                        <td data-bind="text: lmis.format.dateToString(PostDate)"></td>
                        <td data-bind="text: lmis.format.approvalStatus(Approval)"></td>
                        <td>
                            <a href="#" title=" View " data-bind="click: $root.View" class="editBtn"><i class="fa fa-eye"></i></a>
                            <a href="#" title=" Edit " data-bind="click: $root.Edit" class="editBtn"><i class="fa fa-edit"></i></a>
                            <a href="#" title=" Delete " data-bind="click: $root.Delete"><i class="fa fa-trash-o"></i></a>
                        </td>
                        <td>
                            <a class="actionLabel" data-bind="click: $root.RecommendCVs, visible: Approval === 2"><%=GetGlobalResourceObject("CommonControls", "F004_RecommendedCVs")%></a>
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
                            <th><%=GetGlobalResourceObject("CommonControls", "F004_ApplicantsFor")%> <span data-bind="html: DetailsFor"></span></th>
                            <th><%=GetGlobalResourceObject("CommonControls", "F004_AppDate")%></th>
                            <th><%=GetGlobalResourceObject("CommonControls", "X_Status")%></th>
                            <th>...</th>
                        </tr>
                    </thead>
                    <tbody data-bind="foreach: Applicants">
                        <tr style="min-height: 34px;">
                            <td>
                                <span class="actionLabel" data-bind="click: $root.ApplicantDetails, text: userName"></span>
                            </td>
                            <td data-bind="text: lmis.format.dateToString(date)"></td>
                            <td data-bind="text: statusDesc"></td>
                            <td>
                                <div style="line-height: 30px;">
                                    <a class="actionLabel" style="vertical-align: middle" data-bind="visible: canAccept(), click: $root.ChangeApplicantStatus($data, 3)"><%=GetGlobalResourceObject("CommonControls", "X_Accept")%></a>
                                    <a class="actionLabel" style="vertical-align: middle" data-bind="visible: canReject(), click: $root.ChangeApplicantStatus($data, 4)"><%=GetGlobalResourceObject("CommonControls", "X_Reject")%></a>
                                    <a class="actionLabel" style="vertical-align: middle" data-bind="visible: canHire(), click: $root.ChangeApplicantStatus($data, 5)"><%=GetGlobalResourceObject("CommonControls", "F004_Hire")%></a>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                    <tbody data-bind="visible: Applicants().length < 1">
                        <tr>
                            <td colspan="2">
                                <%=GetGlobalResourceObject("MessagesResource", "F004_NoApplicants")%>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    
    <div style="display: none;">
        <div id="F004_New"><%=GetGlobalResourceObject("CommonControls", "F004_New")%></div>
        <div id="F004_Viewed"><%=GetGlobalResourceObject("CommonControls", "F004_Viewed")%></div>
        <div id="X_Accepted"><%=GetGlobalResourceObject("CommonControls", "X_Accepted")%></div>
        <div id="X_Rejected"><%=GetGlobalResourceObject("CommonControls", "X_Rejected")%></div>
        <div id="F004_Hired"><%=GetGlobalResourceObject("CommonControls", "F004_Hired")%></div>
    </div>

</asp:Content>