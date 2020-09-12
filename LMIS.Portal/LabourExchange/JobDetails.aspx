<%@ Page Title="<%$ Resources:CommonControls, F019 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="JobDetails.aspx.cs" Inherits="LMIS.Portal.LabourExchange.JobDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/JobDetails.js" async="async"></script>

    <div class="col-md-9 blog" data-bind="visible: VmList().JobId">
        <div class="blog-item">
            <div class="row">
                <div class="col-xs-12 col-sm-3 text-center">
                    <div class="entry-meta">
                        <span id="publish_date" style="font-weight: bolder" data-bind="text: lmis.format.dateToString(VmList().StartDate) + ' - ' + lmis.format.dateToString(VmList().EndDate)"></span>
                        <span><i class="fa fa-building-o"></i>
                            <a href="javascript:void(0);" data-bind="text: VmList().Extras.OrgName"></a>
                        </span>
                        <span data-bind="visible: VmList().Extras.OrgUrl"><i class="fa fa-globe"></i>
                            <a href="#" target="_blank" data-bind="attr: { href: 'http://' +VmList().Extras.OrgUrl }"><%=GetGlobalResourceObject("CommonControls", "X_Website")%></a>
                        </span>
                        <span data-bind="visible: VmList().City"><i class="fa fa-location-arrow"></i>
                            <a href="javascript:void(0);" data-bind="text: VmList().City + ', ' + VmList().Country"></a>
                        </span>
                        <span data-bind="visible: VmList().Gender"><i class="fa fa-user"></i>
                            <a href="javascript:void(0);" data-bind="text: VmList().Gender"></a>
                        </span>
                        <span data-bind="visible: VmList().EmploymentType"><i class="fa fa-briefcase"></i>
                            <a href="javascript:void(0);" data-bind="text: VmList().EmploymentType"></a>
                        </span>
                        <span data-bind="visible: VmList().ExpTo > 0"><i class="fa fa-bookmark"></i>
                            <a href="javascript:void(0);" data-bind="text: VmList().ExpFrom + ' - ' + VmList().ExpTo + ' <%=GetGlobalResourceObject("CommonControls", "X_Years")%>'"></a>
                        </span>
                        <span data-bind="visible: VmList().EdLevel"><i class="fa fa-laptop"></i>
                            <a href="javascript:void(0);" data-bind="text: VmList().EdLevel"></a>
                        </span>
                        <span data-bind="visible: VmList().Vacancies > 0"><i class="fa fa-users"></i>
                            <a href="javascript:void(0);" data-bind="text: VmList().Vacancies + ' <%=GetGlobalResourceObject("CommonControls", "X_Vacancies")%>    '"></a>
                        </span>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-9 blog-content">
                    <h2 data-bind="text: VmList().Title"></h2>
                    <p data-bind="visible: VmList().EdCert, text: lmis.globalString.toLocal(VmList().EdCert, true)"></p>
                    <div data-bind="visible: VmList().Description">
                        <h3><%=GetGlobalResourceObject("CommonControls", "F019_JobDesc")%></h3>
                        <p data-bind="text: lmis.globalString.toLocal(VmList().Description, true)"></p>
                    </div>
                    <div data-bind="visible: Skills()">
                        <h3><%=GetGlobalResourceObject("CommonControls", "F019_ReqSkills")%></h3>
                        <ul data-bind="foreach: Skills()">
                            <li data-bind="text: $data"></li>
                        </ul>
                    </div>
                    <div data-bind="visible: VmList().MedConditions && VmList().MedConditions.length > 0" class="post-tags">
                        <strong><%=GetGlobalResourceObject("CommonControls", "F019_MedConds")%>: </strong>
                        <span data-bind="text: VmList().MedConditions ? VmList().MedConditions.join(', ') : ''"></span>
                    </div>
                    <div data-bind="visible: CanApply">
                        <input id="btnSave" data-bind="click: Apply" value="<%=GetGlobalResourceObject("CommonControls", "X_Apply")%>" class="btn btn-primary" type="button" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-3">
        <div class="widget categories">
        </div>
    </div>

</asp:Content>