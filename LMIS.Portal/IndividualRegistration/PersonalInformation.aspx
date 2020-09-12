<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="PersonalInformation.aspx.cs" Inherits="LMIS.Portal.IndividualRegistration.PersonalInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row form-divider col-sm-12">
        <div class="col-lg-4">
            <hr>
        </div>
        <div class="col-lg-4 form-divider-title">
            <%=GetGlobalResourceObject("CommonControls", "F009")%>
        </div>
        <div class="col-lg-4">
            <hr>
        </div>
    </div>
    <section id="user-registration22">
        <div class="container white-bg padding20">
            <div class="col-md-12">
                <div class="panel with-nav-tabs panel-default">
                    <div class="panel-heading">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab1default" data-toggle="tab">My Account</a></li>
                            <li><a href="#tab2default" data-toggle="tab"><%=GetGlobalResourceObject("CommonControls", "F009_CV")%></a></li>
                            <li><a href="#tab3default" data-toggle="tab"><%=GetGlobalResourceObject("CommonControls", "F009_JobSearch")%></a></li>
                            <!--<li><a href="#tab4default" data-toggle="tab">Recommendations</a></li>-->
                        </ul>
                    </div>
                    <div class="panel-body">
                        <div class="tab-content">
                            <div class="tab-pane fade in active" id="tab1default">
                                <div class="col-lg-12">
                                    <div class="progress-wrap">
                                        <h3>Total Points: 100
                                            <img src="../images/star-on.png" /></h3>
                                        <div class="progress">
                                            <div class="progress-bar  color1" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 85%">
                                                <span class="bar-width">85% Complete</span>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <!--Done-->
                                    <div class="stepwizard">
                                        <div class="stepwizard-row setup-panel">
                                            <div class="stepwizard-step">
                                                <a href="#step-1" type="button" class=" btn btn-primary btn-circle"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%> </span>1 </a>
                                                <p><%=GetGlobalResourceObject("CommonControls", "F009_PersonalInfo")%></p>
                                            </div>
                                            <div class="stepwizard-step">
                                                <a href="#step-2" type="button" class="nextBtn btn btn-default btn-circle" disabled="disabled"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%></span>2</a>
                                                <p><%=GetGlobalResourceObject("CommonControls", "F009_Education")%></p>
                                            </div>
                                            <div class="stepwizard-step">
                                                <a href="#step-3" type="button" class="btn btn-default btn-circle" disabled="disabled"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%></span>3</a>
                                                <p><%=GetGlobalResourceObject("CommonControls", "F009_Experience")%></p>
                                            </div>
                                            <div class="stepwizard-step">
                                                <a href="#step-4" type="button" class="btn btn-default btn-circle" disabled="disabled"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%> </span>4</a>
                                                <p><%=GetGlobalResourceObject("CommonControls", "F009_Skills")%></p>
                                            </div>
                                            <div class="stepwizard-step">
                                                <a href="#step-5" type="button" class="btn btn-default btn-circle" disabled="disabled"><span><%=GetGlobalResourceObject("CommonControls", "F009_Step")%> </span>5</a>
                                                <p><%=GetGlobalResourceObject("CommonControls", "F009_TrainingandCertifications")%></p>
                                            </div>
                                        </div>
                                    </div>
                                    <!--Done-->
                                    <form role="form">
                                        <div class="row setup-content" id="step-1">
                                            <div class="col-xs-12">
                                                <div class="col-md-12">
                                                    <h3><%=GetGlobalResourceObject("CommonControls", "F009_Step")%> 1</h3>
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
                                                            <label><%=GetGlobalResourceObject("CommonControls", "F009_Email")%>  *</label>
                                                            <input id="txtemail" type="email" class="form-control validationElement" data-bind="value: email">
                                                        </div>

                                                        <div class="form-group">
                                                            <label><%=GetGlobalResourceObject("CommonControls", "X_Password")%>  *</label>
                                                            <input id="txtpassword" type="password" class="form-control validationElement" data-bind="value: Password">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <div class="form-group">
                                                            <label><%=GetGlobalResourceObject("CommonControls", "F009_DateofBirth")%>  *</label>
                                                            <input id="dtdateofbirth" type="text" class="form-control validationElement" placeholder="mm/dd/yyyy" data-bind="value: DateofBirth">
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
                                                            <label><%=GetGlobalResourceObject("CommonControls", "F009_Maritalstatus")%></label>
                                                            <select class="form-control" id="ddlmarital" name="Marital" data-bind="value:Maritalstatus, optionsText: 'desc', optionsValue: 'id', options: optionsMaritalstatus, valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                                                            </select>
                                                        </div>
                                                        <div class="form-group">
                                                            <label><%=GetGlobalResourceObject("CommonControls", "F009_RetypePassword")%>  *</label>
                                                            <input id="txtretype " type="password" class="form-control validationElement" data-bind="value: RetypePassword">
                                                        </div>

                                                    </div>
                                                    <div class="row form-divider ">
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
                                                            <select class="form-control validationElement" id="ddlcountry" name="Country" data-bind="value:Country,optionsText: 'desc', optionsValue: 'id', options: optionsCountry,valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
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
                                                            <select class="form-control validationElement" id="ddlcity" name="Country" data-bind="value:City,optionsText: 'desc', optionsValue: 'id', options: optionsCity,valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                                                            </select>
                                                        </div>
                                                        <div class="form-group">
                                                            <label><%=GetGlobalResourceObject("CommonControls", "F009_MobileNo")%></label>
                                                            <input type="text" name="mobileno" id="txtmobileno" class="form-control" data-bind="value: MobileNumber">
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
                                                            <label><%=GetGlobalResourceObject("CommonControls", "F009_NationailtyIDorPassportID")%> *</label>
                                                            <input type="text" class="form-control validationElement" id="txtnationalidorpassportid" data-bind="value: NationailtyIDorPassportID">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-5 col-sm-offset-1">
                                                        <div class="form-group">
                                                            <label><%=GetGlobalResourceObject("CommonControls", "F009_Type")%> *</label>
                                                            <select class="form-control validationElement" id="ddlTypeID" name="TypeID" data-bind="value:TypeID, optionsText: 'desc', optionsValue: 'id', options: optionsTypeID,  valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
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
                                                        <div class="input-group">
                                                            <input type="text" id="txtFileName" class="form-control" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectAnImage")%>">
                                                            <span class="input-group-btn">
                                                                <button class="btn btn-default" onclick="$('#hdnFileBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                                                            </span>
                                                        </div>
                                                        <input type="file" id="hdnFileBrowser" data-bind="attr: { accept: AcceptedFiles }, event: { change: ValidateFile }" style="height: 0; visibility: hidden;" />
                                                        <p class="form-description">JPG , PNG , GIF</p>
                                                        <%--                                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                                            <div class="fileinput-new thumbnail" style="width: 80px; height: 80px;">
                                                                <img src="../images/projects/avatar3.png" alt="...">
                                                            </div>
                                                            <div>
                                                                <span class="btn btn-default btn-file">
                                                                    <input type="file" name="..."></span>
                                                                <a href="#" class="btn btn-default fileinput-exists" data-dismiss="fileinput">Upload</a>
                                                            </div>
                                                        </div>--%>
                                                    </div>

                                                    <div class="col-sm-5 ">
                                                        <div class="form-group">
                                                            <label class="checkbox-inline">
                                                                <%=GetGlobalResourceObject("CommonControls", "F009_Allowemployertoviewcontactinformation")%>
                                                                <input type="checkbox" id="cballowemployee" class="" data-bind="value: AllowtoViewMyInfo">
                                                            </label>
                                                        </div>

                                                        <div class="form-group">
                                                            <label><%=GetGlobalResourceObject("CommonControls", "F009_Medicalconditions")%> *</label>
                                                            <select class="form-control validationElement" id="ddlMedicalconditions" name="TypeID" data-bind="value:Medicalconditions, optionsText: 'desc', optionsValue: 'id', options: optionsMedicalconditions, valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                                                            </select>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12">

                                                        <button class="btn btn-primary nextBtn btn-lg pull-right" type="button" data-bind="click: StartWorkflow">Next</button>


                                                        <%--<button class="btn btn-primary nextBtn btn-lg pull-left" type="button">Back</button>--%>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>


                            <!-- <div class="tab-pane fade" id="tab4default">Default 4</div>-->
                        </div>
                    </div>
                </div>
            </div>
            <div style="display: none;">
                <div id="RequiredFieldsstep1">
                    <p><%=GetGlobalResourceObject("MessagesResource", "X_RequiredFieldErrors")%></p>
                    <p><%=GetGlobalResourceObject("MessagesResource", "F009_RequiredFieldsstep1")%></p>
                </div>
                <div id="Confirmation_Password_and_ConfirmationPassword">
                    <p><%=GetGlobalResourceObject("MessagesResource", "X_RequiredFieldErrors")%></p>
                    <p><%=GetGlobalResourceObject("MessagesResource", "F009_Confirmation_Password_and_ConfirmationPassword")%></p>
                </div>
                <div id="Step1">
                    <p><%=GetGlobalResourceObject("MessagesResource", "F005_Step1")%></p>
                </div>
                <div id="Step3">
                    <p><%=GetGlobalResourceObject("MessagesResource", "F005_Step3")%></p>
                </div>
            </div>
        </div>
    </section>
    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../IndividualRegistration/Scripts/PersonalInformation.js" async="async"></script>
    <script src="../Scripts/Extensions/strength.js"></script>
    <script src="Scripts/js.js"></script>

    <script>
        $(document).ready(function () {


            var navListItems = $('div.setup-panel div a'),
    allWells = $('.setup-content');
            allWells.hide();

            navListItems.click(function (e) {
                e.preventDefault();
                var $target = $($(this).attr('href')),
                        $item = $(this);

                if (!$item.hasClass('disabled')) {
                    navListItems.removeClass('btn-primary').addClass('btn-default');
                    $item.addClass('btn-primary');
                    allWells.hide();
                    $target.show();
                    $target.find('input:eq(0)').focus();
                }
            });
            $('#live-chat').click(function () { $('#live-chat-panel').show(300); });
            $('#live-chat-hide').click(function () { $('#live-chat-panel').hide(300); });
            $('#pie-chart').on({
                'click': function () {
                    $('#my-graph').attr('src', '../images/tab03/pie-full.png');
                }
            });

            $('#bar-chart').on({
                'click': function () {
                    $('#my-graph').attr('src', '../images/tab03/bar-chart.png');
                }
            });


            $('#line-chart').on({
                'click': function () {
                    $('#my-graph').attr('src', '../images/tab03/line-full.png');
                }
            });


            $('#time-chart').on({
                'click': function () {
                    $('#my-graph').attr('src', '../images/tab03/time-series.png');
                }
            });


            $('#pub-table').on({
                'click': function () {
                    $('#my-pub-graph').attr('src', '../images/tab08/table-graphs.png');
                }
            });

            $('#pub-graph').on({
                'click': function () {
                    $('#my-pub-graph').attr('src', '../images/tab08/graph-graphs.png');
                }
            });

            $('#pub-top10').on({
                'click': function () {
                    $('#my-pub-graph').attr('src', '../images/tab08/top10-graphs.png');
                }
            });

            $('div.setup-panel div a.btn-primary').trigger('click');

        });</script>
</asp:Content>
