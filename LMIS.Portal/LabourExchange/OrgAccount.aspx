﻿<%@ Page Title="<%$ Resources:CommonControls, F001 %>" Language="C#" MasterPageFile="~/MasterPages/LabourExchange.Master" AutoEventWireup="true" CodeBehind="OrgAccount.aspx.cs" Inherits="LMIS.Portal.LabourExchange.OrgAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-12">

        <div class="row form-divider ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F031_Title")%></div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>
        <div class="col-sm-5 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F037_OrganizationName")%></label>
                <div class="input-group">
                    <input type="text" id="txtOrganizationName_A" class="form-control" maxlength="100" data-bind='value: OrganizationName.A, attr: { placeholder: OrganizationName.A.ph }' />
                    <span class="input-group-btn">
                        <button id="btnNameTranslations" class="btn btn-default always-enabled" data-bind="click: function () { OrganizationName.LocalizeView(!OrganizationName.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtOrganizationName_B" class="form-control" maxlength="100" data-bind="value: OrganizationName.B, attr: { placeholder: OrganizationName.B.ph }, visible: !OrganizationName.Localized()" />
                <input type="text" id="txtOrganizationName_C" class="form-control" maxlength="100" data-bind="value: OrganizationName.C, attr: { placeholder: OrganizationName.C.ph }, visible: !OrganizationName.Localized()" />
                <span id="OrganizationNameValidation" class="validationMessage"><%=GetGlobalResourceObject("CommonControls", "F037_Thisfieldisrequired")%></span>
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F037_OrganizationSize")%></label>
                <p data-bind="css: { error: OrganizationSize.hasError }">
                    <select class="form-control" id="ddlOrganizationSize" name="OrganizationSize" data-bind="value: OrganizationSize, optionsText: 'desc', optionsValue: 'id', options: OrganizationSizeOptions, valueAllowUnset: true, optionsCaption: 'Choose...'">
                    </select>
                </p>
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F037_EconomicActivity")%></label>
                <select class="form-control" id="ddlEconomicActivity" name="EconomicActivity" data-bind="value: EconomicActivity, optionsText: 'desc', optionsValue: 'id', options: EconomicActivityOptions, valueAllowUnset: true, optionsCaption: 'Choose...'">
                </select>

            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F037_IndustryType")%></label>
                <select class="form-control" id="ddlIndustryType" name="IndustryType" data-bind="value: IndustryType, optionsText: 'desc', optionsValue: 'id', options: IndustryTypeOptions, valueAllowUnset: true, optionsCaption: 'Choose...'">
                </select>
            </div>
            <div id="divOtherIndustry" class="form-group" data-bind="visible: isOtherIndustryTypeVisiable">
                <label><%=GetGlobalResourceObject("CommonControls", "F037_Otherindustrytype")%></label>
                <div class="input-group">
                    <input type="text" id="txtOtherIndustryType_A" class="form-control" maxlength="100" data-bind="value: OtherIndustryType.A, attr: { placeholder: OtherIndustryType.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnOtherIndustryTypeTranslations" class="btn btn-default always-enabled" data-bind="click: function () { OtherIndustryType.LocalizeView(!OtherIndustryType.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtOtherIndustryType_B" class="form-control" maxlength="100" data-bind="value: OtherIndustryType.B, attr: { placeholder: OtherIndustryType.B.ph }, visible: !OtherIndustryType.Localized()" />
                <input type="text" id="txtOtherIndustryType_C" class="form-control" maxlength="100" data-bind="value: OtherIndustryType.C, attr: { placeholder: OtherIndustryType.C.ph }, visible: !OtherIndustryType.Localized()" />
            </div>
        </div>
        <div class="col-sm-5">
            <div id="divOrganizationType" class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F037_OrganizationType")%></label>
                <div class="form-group">
                    <input type="radio" name="organizationType" value="10000001" data-bind="checked: UserSubCategory" />
                    <span>Government&nbsp;&nbsp;&nbsp;</span>
                    <input type="radio" name="flavorGroup" value="10000003" data-bind="checked: UserSubCategory" />
                    <span>Private   </span>
                </div>
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_Type")%></label>
                <select class="form-control" id="ddlIDType" name="IDType" data-bind="value: IDType, optionsText: 'desc', optionsValue: 'id', options: IDTypeOptions, valueAllowUnset: true, optionsCaption: 'Choose...'">
                </select>
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F037_IDNumber")%></label>
                <input type="text" name="IDNumber" class="form-control" data-bind="value: IDNumber" required="required">
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F037_YearsOfExperince")%></label>
                <select class="form-control" id="ddlYearsofExperienceID" name="YearsofExperienceID" data-bind="value: YearsofExperienceID, optionsText: 'desc', optionsValue: 'id', options: YearsofExperienceIDOptions, valueAllowUnset: true, optionsCaption: 'Choose...'">
                </select>
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F037_EstablishmentDate")%> </label>
                <input type="text" class="datepiker form-control" data-bind="value: EstablishmentDate" required="required" />
            </div>
        </div>
        <div class="col-sm-12 row form-divider ">
            <div class="col-lg-4">
                <hr />
            </div>
            <div class="col-lg-4 form-divider-title ">
                <%=GetGlobalResourceObject("CommonControls", "F037_ContactsInformation")%>
            </div>
            <div class="col-lg-4">
                <hr />
            </div>
        </div>
        <div class="col-sm-5 col-sm-offset-1">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Country")%> </label>
                <select class="form-control" data-bind="options: CountryOptions, value: Country, optionsValue: 'id', optionsText: 'desc', optionsCaption: 'Choose...'">
                </select>

            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_City")%></label>
                <select class="form-control" id="ddlcity" name="city" data-bind="value: City, optionsText: 'desc', optionsValue: 'id', options: CityOptions, valueAllowUnset: true, optionsCaption: 'Choose...'">
                </select>
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_Address")%> </label>
                <div class="input-group">
                    <input type="text" id="txtAddress_A" class="form-control" maxlength="100" data-bind="value: Address.A, attr: { placeholder: Address.A.ph }" />
                    <span class="input-group-btn">
                        <button id="btnAddressTranslations" class="btn btn-default always-enabled" data-bind="click: function () { Address.LocalizeView(!Address.Localized()); }">
                            <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                        </button>
                    </span>
                </div>
                <input type="text" id="txtAddress_B" class="form-control" maxlength="100" data-bind="value: Address.B, attr: { placeholder: Address.B.ph }, visible: !Address.Localized()" />
                <input type="text" id="txtAddress_C" class="form-control" maxlength="100" data-bind="value: Address.C, attr: { placeholder: Address.C.ph }, visible: !Address.Localized()" />
                <span id="AddressValidation" class="validationMessage"><%=GetGlobalResourceObject("CommonControls", "F037_Thisfieldisrequired")%></span>
            </div>

        </div>
        <div class="col-sm-5">
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F037_Zippostalcode")%></label>
                <input type="text" name="postal" class="form-control" data-bind="value: ZipPostalCode">
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_TelephoneNo")%></label>
                <input type="tel" name="postal" class="form-control" data-bind="value: Telephone, valueUpdate: 'afterkeydown'" required="required">
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "X_Website")%></label>
                <input type="url" class="form-control" data-bind="value: OrganizationWebsite">
            </div>

        </div>
    </div>
    <div class="row form-divider col-sm-12 ">
        <div class="col-lg-4">
            <hr>
        </div>
        <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F037_LogoandProfile")%></div>
        <div class="col-lg-4">
            <hr>
        </div>
    </div>

    <div class="col-sm-5 col-sm-offset-1">
        <div class="form-group ">
            <label><%=GetGlobalResourceObject("CommonControls", "F037_OrganizationLogo")%></label>
            <div class="input-group">
                <input type="text" id="txtLogoName" name="txtLogoName" class="form-control always-disabled" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectFile")%>">
                <span class="input-group-btn">
                    <button class="btn btn-default" onclick="$('#hdnLogoBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                </span>
            </div>
            <input type="file" id="hdnLogoBrowser" name="hdnLogoBrowser" data-bind="attr: { accept: AcceptedLogoFiles }, event: { change: ValidateLogo }" style="height: 0; visibility: hidden;" />

            <p class="form-Address">
                <span style="padding-right: 10px;">JPG , PNG , GIF</span>
            </p>
            <div class="form-group text-center">
                <input value="<%=GetGlobalResourceObject("CommonControls", "X_ClearFile")%>" class="btn btn-choose-graph btn-sm btn-danger" style="width: 100px;" type="button" data-bind="click: ClearLogo" />
            </div>
        </div>

    </div>
    <div class="col-sm-5 ">
        <div class="form-group ">
            <label><%=GetGlobalResourceObject("CommonControls", "F037_OrganizationProfile")%></label>
            <div class="input-group">
                <input type="text" id="txtProfileName" class="form-control always-disabled" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectFile")%>">
                <span class="input-group-btn">
                    <button class="btn btn-default" onclick="$('#hdnProfileBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                </span>
            </div>
            <input type="file" id="hdnProfileBrowser" data-bind="attr: { accept: AcceptedProfileFiles }, event: { change: ValidateProfile }" style="height: 0; visibility: hidden;" />
            <p class="form-Address">
                <span style="padding-right: 10px;">PDF , MS Word , PowerPoint</span>
                <a class="text-center" href="#" target="_blank" data-bind="visible: !lmis.string.isNullOrWhiteSpace(ServerProfileName()), attr: { href: lmis.x.downloadURL + ServerProfileName() }">
                    <%=GetGlobalResourceObject("MessagesResource", "X_DownloadServerFile")%>
                </a>
            </p>
            <div class="form-group text-center">
                <input value="<%=GetGlobalResourceObject("CommonControls", "X_ClearFile")%>" class="btn btn-choose-graph btn-sm btn-danger" style="width: 100px;" type="button" data-bind="click: ClearProfile" />
            </div>
        </div>
    </div>
    <div class="row form-divider col-sm-12 ">
        <div class="col-lg-4">
            <hr>
        </div>
        <div class="col-lg-4 form-divider-title ">Services Request </div>
        <div class="col-lg-4">
            <hr>
        </div>
    </div>

    <div class="col-sm-7 col-sm-offset-1">
        <p class="form-Address"><%=GetGlobalResourceObject("CommonControls", "F037_Choosewhatwillyouuseourportalfor")%></p>
        <br />
    </div>

    <div class="col-sm-10  col-sm-offset-1">
        <div class="form-group col-sm-3">
            <label class="checkbox-inline">
                <input type="checkbox" data-bind="checked: TrainingProvider">
                <span><%=GetGlobalResourceObject("CommonControls", "F015_AllOffers")%></span>
            </label>
        </div>

        <div class="form-group col-sm-5">
            <label class="checkbox-inline">
                <input type="checkbox" data-bind="checked: TrainingSeeker ">
                <span><%=GetGlobalResourceObject("CommonControls", "F037_Trainingfororganizationemployees")%></span>
            </label>
        </div>
        <div class="form-group col-sm-3">
            <label class="checkbox-inline">
                <input type="checkbox" data-bind="checked: Employer">
                <span><%=GetGlobalResourceObject("CommonControls", "F004_AllOffers")%></span>
            </label>
        </div>
        <div class="form-group col-sm-7" id="divRegistrationNumberWithITC">
            <label><%=GetGlobalResourceObject("CommonControls", "F037_ITCRegistration")%></label>
            <input type="text" class="form-control" data-bind="value: RegistrationNumberWithITC" />
        </div>
    </div>
    <div class="col-sm-12">
        <div>
            <input class="btn btn-success nextBtn btn-lg pull-right" type="submit" value="<%=GetGlobalResourceObject("CommonControls", "X_Update")%> " data-bind=" click: StartWorkflow" />
        </div>

    </div>


    <div style="display: none;">
        <div id="RequiredFields">
            <p><%=GetGlobalResourceObject("MessagesResource", "X_RequiredFieldErrors")%></p>
            <p><%=GetGlobalResourceObject("MessagesResource", "X_MarkedRequiredFields")%></p>
        </div>
        <div id="Step1">
            <p><%=GetGlobalResourceObject("MessagesResource", "X_Validating")%></p>
        </div>
        <div id="Step3">
            <p><%=GetGlobalResourceObject("MessagesResource", "X_Saving")%></p>
        </div>
    </div>
    <script src="../Scripts/knockout.validation.min.js"></script>
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../Scripts/Extensions/strength.js"></script>
    <script src="../Scripts/Extensions/config.js"></script>
    <script src="../LabourExchange/Scripts/OrgAccount.js" async="async"></script>
</asp:Content>
