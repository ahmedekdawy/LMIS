<%@ Page Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="LMIS.Portal.LabourExchange.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container content profile">
        
        <div class="col-sm-3">
            <img class="img-responsive profile-img" style="width: 100%" data-bind="attr: { src: ServerLogoName }" src="" alt="">
            <h2 class="text-center" style="width: 100%"><span data-bind="text: OrganizationName"></span></h2>
            <ul class="list-group sidebar-nav-v1" id="sidebar-nav-1" style="width: 100%" data-bind="visible: Mode() !== 'r'">
                <li class="list-group-item active" id="liProfile">
                    <a href="#" data-bind="click: function () { $('#divProfile').show(); $('#divContactPersons').hide(); $('.list-group-item').removeClass('active'); $('#liProfile').addClass('active'); }"><i class="fa fa-bar-chart-o"></i><%=GetGlobalResourceObject("CommonControls", "F038_Profile")%></a>
                </li>
                <li class="list-group-item" id="liContactPersons">
                    <a href="#" data-bind="click: function () { $('#divProfile').hide(); $('#divContactPersons').show(); $('.list-group-item').removeClass('active'); $('#liAppliedJobs').addClass('active'); }"><i class="fa fa-bar-chart-o"></i>Contact Persons</a>
                </li>
                <li class="list-group-item">
                    <a href="#"><i class="fa fa-bar-chart-o"></i>Jobs</a>
                </li>
                <li class="list-group-item">
                    <a href="#"><i class="fa fa-cog"></i>Settings</a>
                </li>
            </ul>
        </div>
        
        <div class="col-sm-8">
            
            <div class="col-sm-11" style="display: none;" data-bind="visible: Mode() !== 'p'">
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
                <div class="col-lg-3">
                    <hr>
                </div>
                <div class="col-lg-6 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "F031_Title")%></div>
                <div class="col-lg-3">
                    <hr>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F037_OrganizationSize")%>:</label>
                    <label data-bind="text: OrganizationSize"></label>
                </div>
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F037_YearsOfExperince")%>:</label>
                    <label data-bind="text: YearsofExperience"></label>
                </div>
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F037_EconomicActivity")%>:</label>
                    <label data-bind="text: EconomicActivity"></label>
                </div>
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F037_IndustryType")%>:</label>
                    <label data-bind="text: IndustryType "></label>
                </div>
            </div>
            <div class="col-sm-6">
                <div id="divOrganizationType" class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F037_OrganizationType")%>:</label>
                    <label data-bind="text: UserSubCategory"></label>
                </div>
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F039_Type")%>:</label>
                    <label data-bind="text: IDType"></label>
                </div>
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F037_IDNumber")%>:</label>
                    <label data-bind="text: IDNumber"></label>
                </div>
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F037_EstablishmentDate")%> :</label>
                    <label data-bind="text: EstablishmentDate"></label>
                </div>
            </div>
            <div class="row form-divider col-sm-12">
                <div class="col-lg-3">
                    <hr />
                </div>
                <div class="col-lg-6 form-divider-title ">
                    <%=GetGlobalResourceObject("CommonControls", "F037_ContactsInformation")%>
                </div>
                <div class="col-lg-3">
                    <hr />
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "X_Country")%>: </label>
                    <label data-bind="text: Country"></label>

                </div>
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F009_City")%>:</label>
                    <label data-bind="text: City"></label>
                </div>
                <div class="form-group">
                    <label id="lblAddress"><%=GetGlobalResourceObject("CommonControls", "F009_Address")%> :</label>
                    <label data-bind="text: Address"></label>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F037_Zippostalcode")%>:</label>
                    <label data-bind="text: ZipPostalCode"></label>
                </div>
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F009_TelephoneNo")%>:</label>
                    <label data-bind="text: Telephone"></label>
                </div>
                <div class="form-group">
                    <label id="lblWebsite"><%=GetGlobalResourceObject("CommonControls", "X_Website")%>:</label>
                    <label data-bind="text: OrganizationWebsite"></label>
                </div>
            </div>
            <div class="row form-divider col-sm-12">
                <div class="col-lg-3">
                    <hr>
                </div>
                <div class="col-lg-6 form-divider-title">Services Requested</div>
                <div class="col-lg-3">
                    <hr>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="form-group col-sm-6">
                    <label class="checkbox-inline">
                        <input type="checkbox" data-bind="checked: TrainingProvider" disabled="disabled">
                        <span><%=GetGlobalResourceObject("CommonControls", "F015_AllOffers")%>:</span>
                    </label>
                </div>
                <div class="form-group col-sm-6">
                    <label class="checkbox-inline">
                        <input type="checkbox" data-bind="checked: TrainingSeeker " disabled="disabled">
                        <span><%=GetGlobalResourceObject("CommonControls", "F037_Trainingfororganizationemployees")%>:</span>
                    </label>
                </div>
                <div class="form-group col-sm-6">
                    <label class="checkbox-inline">
                        <input type="checkbox" data-bind="checked: Employer" disabled="disabled">
                        <span><%=GetGlobalResourceObject("CommonControls", "F004_AllOffers")%>:</span>
                    </label>
                </div>
                <div class="form-group  col-sm-9" id="divRegistrationNumberWithITC" data-bind="attr: { display: (RegistrationNumberWithITC.length > 0) ? 'inline' : 'none' }">
                    <label><%=GetGlobalResourceObject("CommonControls", "F037_ITCRegistration")%>:</label>
                    <input type="text" class="form-control" data-bind="value: RegistrationNumberWithITC" />
                </div>
            </div>
        </div>
    </div>

    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <link href="../css/shortcode_timeline2.css" rel="stylesheet" />
    <link href="../css/profile.css" rel="stylesheet" />
    <link href="../css/app.css" rel="stylesheet" />
    <link href="../css/page_job.css" rel="stylesheet" />
    <script src="../Scripts/Extensions/config.js"></script>
    <script src="../LabourExchange/Scripts/Profile.js" async="async"></script>
    <script src="../Scripts/Extensions/config.js"></script>

</asp:Content>
