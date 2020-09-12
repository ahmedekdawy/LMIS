<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Registeration.aspx.cs" Inherits="LMIS.Portal.Individual.Registeration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                            <button id="btnFirstName"  class="btn btn-default always-enabled" data-bind="click: function () { FirstName.LocalizeView(!FirstName.Localized()); }">
                                <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                            </button>
                        </span>
                    </div>
                    <input type="text" id="txtfirstname_B"  class="form-control" maxlength="100" data-bind="value: FirstName.B, attr: { placeholder: FirstName.B.ph }, visible: !FirstName.Localized()" />
                    <input type="text" id="txtfirstname_C"  class="form-control" maxlength="100" data-bind="value: FirstName.C, attr: { placeholder: FirstName.C.ph }, visible: !FirstName.Localized()" />
                <span id="FirstNameValidation" style="display: none;" class="validationMessage"><%=GetGlobalResourceObject("CommonControls", "F037_Thisfieldisrequired")%></span>
                     </div>
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F009_DateofBirth")%>  *</label>
                    <input type="text" class="datepiker form-control validationElement" data-bind="value: DateofBirth" />
                </div>
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F009_Email")%>  *</label>
                    <input id="txtEmail" type="email" class="form-control validationElement" data-bind="value: Email">
                </div>

                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "X_Password")%>  *</label>
                    <input id="txtPassword" type="password" class="form-control validationElement" data-bind="value: Password , event: { keyup : CheckPasswordStrength(this) }">
                     <span class="password_strength"></span> 
                </div>
                  <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F009_RetypePassword")%>  *</label>
                    <input id="txtretype " type="password" class="form-control validationElement" data-bind="value: RetypePassword" >
                    
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
               <span id="lastnameValidation" style="display: none;" class="validationMessage"><%=GetGlobalResourceObject("CommonControls", "F037_Thisfieldisrequired")%> </span>
                      </div>

                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F009_Gender")%> *</label>
                    <select class="form-control validationElement" id="ddGender" name="Gender" data-bind="value: Gender, optionsText: 'desc', options: optionsGender,  valueAllowUnset: true,optionsValue: 'id', event: { change: ValidateMilitarystatus }, optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                    </select>
                </div>
                <div class="form-group Militarystatus" style="display:none">
                    
                    <label><%=GetGlobalResourceObject("CommonControls", "F009_Militarystatus")%>**</label>
                    <select class="form-control validationElement" id="ddlmilitary" name="Military" data-bind="value:Militarystatus, optionsText: 'desc', optionsValue: 'id', options: optionsMilitarystatus, valueAllowUnset: true ,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                    </select>
                </div>
                    <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F009_Maritalstatus")%></label>
                    <select class="form-control" id="ddlmarital" name="Marital" data-bind="value:Maritalstatus, optionsText: 'desc', optionsValue: 'id', options: optionsMaritalstatus, valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                    </select>
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
                    <div class="input-group">
                        <input type="text" id="txtAddress_A" class="form-control" maxlength="100" data-bind="value: Address.A, attr: { placeholder: Address.A.ph }" />
                        <span class="input-group-btn">
                            <button id="btnAddress" class="btn btn-default always-enabled" data-bind="click: function () { Address.LocalizeView(!Address.Localized()); }">
                                <i class="fa fa-th-list"></i><%=GetGlobalResourceObject("CommonControls", "X_Translate")%>
                            </button>
                        </span>
                    </div>
                    <input type="text" id="txtAddress_B" class="form-control" maxlength="100" data-bind="value: Address.B, attr: { placeholder: Address.B.ph }, visible: !Address.Localized()" />
                    <input type="text" id="txtAddress_C" class="form-control" maxlength="100" data-bind="value: Address.C, attr: { placeholder: Address.C.ph }, visible: !Address.Localized()" />
               
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
                    <label><%=GetGlobalResourceObject("CommonControls", "F009_Nationality")%> *</label>
                    <select class="form-control validationElement" id="ddlnationailty" name="Nationality" data-bind="value:Nationailty,optionsText: 'desc', optionsValue: 'id', options: optionsNationailty, valueAllowUnset: true, event: { change: ValidateIDType() },optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                    </select>
                </div>
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F039_IDNumber")%> *</label>
                    <input type="text" class="form-control validationElement" id="txtnationalidorpassportid" data-bind="value: IDNumber">
                </div>
            </div>
            <div class="col-sm-5 col-sm-offset-1">
                <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F039_Type")%> *</label>
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
                    <label><%=GetGlobalResourceObject("CommonControls", "F039_PersonalImage")%></label>
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
                        <input type="checkbox" id="cballowemployee" class="" data-bind="checked: AllowtoViewMyInfo">
                    </label>
                </div>


            </div>

         
        <div class="col-sm-12">
            <input type="checkbox" id="chkAggrement" class="Bold " data-bind="checked: Aggrement">
             <a onclick="return showpopup();">
                        <%=GetGlobalResourceObject("CommonControls", "X_Agreement")%>
              </a>
            <button class="btn btn-primary nextBtn btn-lg pull-right" data-bind="click: Save">Register</button>
        </div>

    </div>
        <div id="agreement" class="pop" style="display: none">
    <span id="tabDisclaimer"><%=GetGlobalResourceObject("CommonControls", "R001_Disclaimer")%></span>
     <div data-bind="html: disclaimer"></div>

        </div>
</div>
    <script async="async" src="../Scripts/Extensions/ko.trilingualtext.js"></script>
    <script async="async" src="../Scripts/Extensions/ko.date.js"></script>
    <script async="async" src="../Scripts/Extensions/lmis.js"></script>
    <script src="../Scripts/Extensions/strength.js"></script>
    <script src="../Scripts/knockout.validation.min.js"></script>
    <script async="async" src="../Individual/Scripts/personal-vm.js"></script>
    <script src="../Scripts/Extensions/config.js"></script> 
</asp:Content>
