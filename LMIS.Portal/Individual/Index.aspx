<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="LMIS.Portal.Individual.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="stepwizard">
            <div class="stepwizard-row setup-panel">
                <div class="stepwizard-step">
                    <a id="aPersonal" href="#" type="button" class="btn btn-primary btn-circle"><span>Step </span>1 </a>
                    <p><%=GetGlobalResourceObject("CommonControls", "F009_PersonalInfo")%></p>
                </div>
                <div class="stepwizard-step">
                    <a id="aEducation" href="#" type="button" class="btn btn-default btn-circle" disabled="disabled"><span>Step </span>2</a>
                    <p><%=GetGlobalResourceObject("CommonControls", "F038_Education")%></p>
                </div>
                <div class="stepwizard-step">
                    <a href="#" type="button" class="btn btn-default btn-circle" disabled="disabled"><span>Step </span>3</a>
                    <p><%=GetGlobalResourceObject("CommonControls", "F009_Experience")%></p>
                </div>

                <div class="stepwizard-step">
                    <a href="#" type="button" class="btn btn-default btn-circle" disabled="disabled"><span>Step </span>4</a>
                    <p><%=GetGlobalResourceObject("CommonControls", "F009_Skills")%></p>
                </div>

                <div class="stepwizard-step">
                    <a href="#step-5" type="button" class="btn btn-default btn-circle" disabled="disabled"><span>Step </span>5</a>
                    <p><%=GetGlobalResourceObject("CommonControls", "F009_TrainingandCertifications")%></p>
                </div>

            </div>
        </div>
    </div>
    <div data-bind="template: { name: 'currentView', data: currentStep }"></div>

    <div class="col-sm-12">
        <div>
            <button class="btn btn-primary nextBtn btn-lg pull-right" data-bind="click: goNext, visible: canGoNext"><%=GetGlobalResourceObject("CommonControls", "F037_Next")%></button>
        </div>
        <div>
            <input class="btn btn-success nextBtn btn-lg pull-right" type="submit" value="<%=GetGlobalResourceObject("CommonControls", "F037_AcceptandFinishRegisteration")%>" data-bind="visible: canFinish" />
        </div>
        <div>
            <button class="btn btn-primary nextBtn btn-lg pull-right" data-bind="click: goPrevious, visible: canGoPrevious"><%=GetGlobalResourceObject("CommonControls", "F037_Previous")%></button>
        </div>
    </div>

    <script id="currentView" type="text/html">
        <div data-bind="template: { name: getTemplate, data: model, afterRender: afterRenderHandler }"></div>
    </script>

    <script id="personalView" type="text/html">
        <div class="row setup-content" id="step-1">
            <div class="col-md-12">
                <div class="row form-divider ">
                    <div class="col-lg-4">
                        <hr>
                    </div>
                    <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F009_UserDetails")%></div>
                    <div class="col-lg-4">
                        <hr>
                    </div>
                </div>

                <div class="col-sm-5 col-sm-offset-1">
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_FirstName")%>  *</label>
                        <div class="input-group">
                            <input type="text" id="txtfirstname_A" class="form-control validationElement" maxlength="100" data-bind="value: FirstName.A, attr: { placeholder: FirstName.A.ph }" />
                            <span class="input-group-btn">
                                <button id="btnFirstName" class="btn btn-default always-enabled" data-bind="click: function () { FirstName.LocalizeView(!FirstName.Localized()); }">
                                    <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                                </button>
                            </span>
                        </div>
                        <input type="text" id="txtfirstname_B" class="form-control" maxlength="100" data-bind="value: FirstName.B, attr: { placeholder: FirstName.B.ph }, visible: !FirstName.Localized()" />
                        <input type="text" id="txtfirstname_C" class="form-control" maxlength="100" data-bind="value: FirstName.C, attr: { placeholder: FirstName.C.ph }, visible: !FirstName.Localized()" />
                    </div>
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_DateofBirth")%>  *</label>
                        <input id="dtdateofbirth" type="date" class="form-control validationElement" data-bind="value: DateofBirth">
                    </div>
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_Email")%>  *</label>
                        <input id="txtEmail" type="email" class="form-control validationElement" data-bind="value: Email">
                    </div>

                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "X_Password")%>  *</label>
                        <input id="txtPassword" type="password" class="form-control validationElement" data-bind="value: Password">
                    </div>
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_Maritalstatus")%></label>
                        <select class="form-control" id="ddlmarital" name="Marital" data-bind="value:Maritalstatus, optionsText: 'desc', optionsValue: 'id', options: optionsMaritalstatus, valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                        </select>
                    </div>
                </div>
                <div class="col-sm-5">
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_LastName")%>  *</label>
                        <div class="input-group">
                            <input type="text" id="txtlastname_A" class="form-control validationElement" maxlength="100" data-bind="value: LastName.A, attr: { placeholder: LastName.A.ph }" />
                            <span class="input-group-btn">
                                <button id="btnLastName" class="btn btn-default always-enabled" data-bind="click: function () { LastName.LocalizeView(!LastName.Localized()); }">
                                    <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                                </button>
                            </span>
                        </div>
                        <input type="text" id="txtlastname_B" class="form-control" maxlength="100" data-bind="value: LastName.B, attr: { placeholder: LastName.B.ph }, visible: !LastName.Localized()" />
                        <input type="text" id="txtlastname_C" class="form-control" maxlength="100" data-bind="value: LastName.C, attr: { placeholder: LastName.C.ph }, visible: !LastName.Localized()" />
                    </div>

                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_Gender")%> *</label>
                        <select class="form-control validationElement" id="ddGender" name="Gender" data-bind="value: Gender, optionsText: 'desc', options: optionsGender,  valueAllowUnset: true,optionsValue: 'id', optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                        </select>
                    </div>
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_Militarystatus")%></label>
                        <select class="form-control" id="ddlmilitary" name="Military" data-bind="value:Militarystatus, optionsText: 'desc', optionsValue: 'id', options: optionsMilitarystatus, valueAllowUnset: true ,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                        </select>
                    </div>

                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_RetypePassword")%>  *</label>
                        <input id="txtretype " type="password" class="form-control validationElement" data-bind="value: RetypePassword">
                    </div>

                </div>
                <div class="col-sm-12 row form-divider ">
                    <div class="col-lg-4">
                        <hr />
                    </div>
                    <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F009_ContactsInformation")%></div>
                    <div class="col-lg-4">
                        <hr />
                    </div>
                </div>
                <div class="col-sm-5 col-sm-offset-1">
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_Country")%> *</label>
                        <select class="form-control validationElement" id="ddlcountry" name="Country" data-bind="value:Country,optionsText: 'desc', optionsValue: 'id', options: CountryOptions,valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                        </select>
                    </div>
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_Address")%></label>
                        <div class="trilingual-textarea">
                            <label lang="en" data-bind="css: lmis.string.isNullOrWhiteSpace(Address.A()) ? 'unedited' : 'edited'">
                                <input type="radio" value="en" class="always-enabled" data-bind="checked: Address.ActiveLang, click: function () { $('#txtaddress_En').focus(); return true; }">
                                <%=GetGlobalResourceObject("CommonControls", "X_English")%>
                            </label>
                            <label lang="fr" data-bind="css: lmis.string.isNullOrWhiteSpace(Address.B()) ? 'unedited' : 'edited'">
                                <input type="radio" value="fr" class="always-enabled" data-bind="checked: Address.ActiveLang, click: function () { $('#txtaddress_Fr').focus(); return true; }">
                                <%=GetGlobalResourceObject("CommonControls", "X_French")%>
                            </label>
                            <label lang="ar" data-bind="css: lmis.string.isNullOrWhiteSpace(Address.C()) ? 'unedited' : 'edited'">
                                <input type="radio" value="ar" class="always-enabled" data-bind="checked: Address.ActiveLang, click: function () { $('#txtaddress_Ar').focus(); return true; }">
                                <%=GetGlobalResourceObject("CommonControls", "X_Arabic")%>
                            </label>
                        </div>
                        <textarea id="txtaddress_En" maxlength="1000" data-bind="textInput: Address.A, visible: (Address.ActiveLang() == 'en')" class="form-control always-white" rows="8"></textarea>
                        <textarea id="txtaddress_Fr" maxlength="1000" data-bind="textInput: Address.B, visible: (Address.ActiveLang() == 'fr')" class="form-control always-white" rows="8"></textarea>
                        <textarea id="txtaddress_Ar" maxlength="1000" data-bind="textInput: Address.C, visible: (Address.ActiveLang() == 'ar')" class="form-control always-white" rows="8"></textarea>
                    </div>

                </div>
                <div class="col-sm-5">
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_City")%> *</label>
                        <select class="form-control validationElement" id="ddlcity" name="Country" data-bind="value:City,optionsText: 'desc', optionsValue: 'id', options: CityOptions,valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                        </select>
                    </div>
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_MobileNo")%></label>
                        <input type="text" name="mobileno" id="txtmobileno" class="form-control" data-bind="value: MobileNo">
                    </div>
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_TelephoneNo")%></label>
                        <input type="text" name="telephonenumber" id="txttelephonenumber" class="form-control" data-bind="value: TelephoneNo">
                    </div>
                </div>
                <div class="row form-divider col-sm-12 ">
                    <div class="col-lg-4">
                        <hr>
                    </div>
                    <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F009_NationailtyInformation")%></div>
                    <div class="col-lg-4">
                        <hr>
                    </div>
                </div>
                <div class="col-sm-5 col-sm-offset-1">
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_Nationailty")%> *</label>
                        <select class="form-control validationElement" id="ddlnationailty" name="Nationailty" data-bind="value:Nationailty,optionsText: 'desc', optionsValue: 'id', options: optionsNationailty, valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                        </select>
                    </div>
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_IDNumber")%> *</label>
                        <input type="text" class="form-control validationElement" id="txtnationalidorpassportid" data-bind="value: IDNumber">
                    </div>
                </div>
                <div class="col-sm-5 col-sm-offset-1">
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_Type")%> *</label>
                        <select class="form-control validationElement" id="ddlIDType" name="IDType" data-bind="value:IDType, optionsText: 'desc', optionsValue: 'id', options: optionsIDType,  valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                        </select>
                    </div>
                </div>
                <div class="row form-divider col-sm-12 ">
                    <div class="col-lg-4">
                        <hr>
                    </div>
                    <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F009_PhotoandotherOptions")%></div>
                    <div class="col-lg-4">
                        <hr>
                    </div>
                </div>

                <div class="col-sm-5 col-sm-offset-1">
                    <div class="form-group ">
                        <label>Organization Image</label>
                        <div class="input-group">
                            <input type="text" id="txtImageName" name="txtImageName" class="form-control always-disabled" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectFile")%>">
                            <span class="input-group-btn">
                                <button class="btn btn-default" onclick="$('#hdnImageBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                            </span>
                        </div>
                        <input type="file" id="hdnImageBrowser" name="hdnImageBrowser" data-bind="attr: { accept: AcceptedImageFiles }, event: { change: ValidateImage }" style="height: 0; visibility: hidden;" />

                        <p class="form-Address">
                            <span style="padding-right: 10px;">JPG , PNG , GIF</span>
                        </p>
                        <div class="form-group text-center">
                            <input value="<%=GetGlobalResourceObject("CommonControls", "X_ClearFile")%>" class="btn btn-choose-graph btn-sm btn-danger" style="width: 100px;" type="button" data-bind="click: ClearImage" />
                        </div>
                    </div>

                </div>

                <div class="col-sm-5 ">
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F009_Medicalconditions")%> *</label>
                        <select class="form-control validationElement" id="ddlMedicalconditions" name="IDType" data-bind="value:Medicalconditions, optionsText: 'desc', optionsValue: 'id', options: optionsMedicalconditions, valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                        </select>
                    </div>
                    <div class="form-group">
                        <label class="checkbox-inline">
                            <%=GetGlobalResourceObject("CommonControls", "F009_Allowemployertoviewcontactinformation")%>
                            <input type="checkbox" id="cballowemployee" class="" data-bind="value: AllowtoViewMyInfo">
                        </label>
                    </div>


                </div>

            </div>
        </div>
    </script>
    <script id="educationView" type="text/html">
        <div class="row setup-content" id="step-2">
            <div class="col-xs-12">
                <div class="col-md-12">
                    <h3><%=GetGlobalResourceObject("CommonControls", "F009_Step")%> 2</h3>
                    <div class="form-group">
                        <label><%=GetGlobalResourceObject("CommonControls", "F014_LevelofEducation")%>  *</label>
                        <select class="form-control validationElement" id="ddlevelofeducation" name="Levelofeducation" data-bind="value: leveleducation, optionsText: 'desc', options: optionsleveleducation, optionsValue: 'id', valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                        </select>
                    </div>
                    <div class="col-sm-12 text-center">
                        <%--<a id="postNew" href="EventPost.aspx#anchor" class="btn btn-success nextBtn btn-lg" data-bind="">--%>
                        <a id="postNew" class="btn btn-success btn-lg">
                            <i class="fa fa-plus-circle"></i><%=GetGlobalResourceObject("CommonControls", "F009_Addneweducation")%>
                        </a>
                    </div>
                    <table id="grdEducation" class="display" style="border-spacing: 0; width: 100%;">
                        <thead>
                            <tr>
                                <th>Education Level</th>
                                <th>Type</th>
                                <th>Name</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody data-bind="foreach: EducationList">
                            <tr data-bind="attr: { id: RowId }">
                                <td data-bind="value: EducationalLevelId,text: EducationalLevelName"></td>
                                <td data-bind="text: InstitutionType"></td>
                                <td data-bind="text: Title"></td>
                                <td>
                                    <a href="#" title=" Edit " data-bind="click: $root.Edit" class="editBtn"><i class="fa fa-edit"></i></a>
                                    <a href="#" title=" Delete " data-bind="click: $root.Delete"><i class="fa fa-trash-o"></i></a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
              
            </div>
        </div>
    </script>
    <script id="confirmView" type="text/html">
    </script>
    <script async="async" src="../Scripts/Extensions/ko.trilingualtext.js"></script>
    <script async="async" src="../Scripts/Extensions/ko.date.js"></script>
    <script async="async" src="../Scripts/Extensions/lmis.js"></script>
    <script src="../Scripts/Extensions/strength.js"></script>
    <script src="../Scripts/knockout.validation.min.js"></script>
    <script async="async" src="Scripts/individual-controller.js"></script>
    <script src="../Individual/Scripts/personal-vm.js"></script>
    <script src="../Individual/Scripts/education-vm.js"></script>

</asp:Content>
