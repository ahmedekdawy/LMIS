<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Popup.Master" AutoEventWireup="true" CodeBehind="EditPersonalInfo.aspx.cs" Inherits="LMIS.Portal.Individual.EditPersonalInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-12">
        <%-- <div class="row form-divider ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F009_UserDetails")%></div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>--%>

        <div class="col-sm-5 col-sm-offset-1 profile">

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
                <label><%=GetGlobalResourceObject("CommonControls", "F009_MobileNo")%></label>
                <input type="text" name="mobileno" id="txtmobileno" class="form-control" data-bind="value: MobileNo">
            </div>
               <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_Email")%>  *</label>
                <input id="txtEmail" type="email" class="form-control validationElement" data-bind="value: Email">
            </div>
              <div class="form-group" >
                <label><%=GetGlobalResourceObject("CommonControls", "F009_Nationality")%> *</label>
                <select class="form-control validationElement" id="ddlnationailty" name="Nationailty" data-bind="value:Nationailty,optionsText: 'desc', optionsValue: 'id', options: optionsNationailty, valueAllowUnset: true, event: { change: ValidateIDType() },optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
         
            
         
                 <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F009_Gender")%> *</label>
                    <select class="form-control validationElement" id="ddGender" name="Gender" data-bind="value: Gender, optionsText: 'desc', options: optionsGender,  valueAllowUnset: true,optionsValue: 'id', event: { change: ValidateMilitarystatus }, optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                    </select>
                </div>
                    <div class="form-group Militarystatus" >
                <label><%=GetGlobalResourceObject("CommonControls", "F009_Militarystatus")%></label>
                <select class="form-control" id="ddlmilitary" name="Military" data-bind="value:Militarystatus, optionsText: 'desc', optionsValue: 'id', options: optionsMilitarystatus, valueAllowUnset: true ,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
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
        <div class="col-sm-5 profile">
            <%--  <div class="row form-divider col-sm-12 ">
            <div class="col-lg-4">
                <hr>
            </div>
            <div class="col-lg-4 form-divider-title "><%=GetGlobalResourceObject("CommonControls", "F009_NationailtyInformation")%></div>
            <div class="col-lg-4">
                <hr>
            </div>
        </div>--%>
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
                <label><%=GetGlobalResourceObject("CommonControls", "F009_TelephoneNo")%></label>
                <input type="text" name="telephonenumber" id="txttelephonenumber" class="form-control" data-bind="value: TelephoneNo" />
            </div>
         <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_DateofBirth")%></label>
                <input id="dtDateOfBirth" type="text" class="form-control datepiker" data-bind="value: DateOfBirth" />
            </div>
          
               <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F039_Type")%> *</label>
                <select class="form-control ddlIDType validationElement" id="ddlIDType" name="IDType" data-bind="value:IDType, optionsText: 'desc', optionsValue: 'id', options: optionsIDType,  valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F039_IDNumber")%></label>
                <input type="text" class="form-control" id="txtnationalidorpassportid" data-bind="value: IDNumber" />
            </div>
              <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_Maritalstatus")%></label>
                <select class="form-control" id="ddlmarital" name="Marital" data-bind="value:Maritalstatus, optionsText: 'desc', optionsValue: 'id', options: optionsMaritalstatus, valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
              <div class="form-group">
                    <label><%=GetGlobalResourceObject("CommonControls", "F009_City")%> *</label>
                    <select class="form-control validationElement" id="ddlcity" name="Country" data-bind="value:City,optionsText: 'desc', optionsValue: 'id', options: CityOptions,valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                    </select>
                </div>
            
            <div class="form-group">
                <label><%=GetGlobalResourceObject("CommonControls", "F009_Medicalconditions")%> *</label>
                <select class="form-control validationElement" id="ddlMedicalconditions" name="IDType" data-bind="value:Medicalconditions, optionsText: 'desc', optionsValue: 'id', options: optionsMedicalconditions, valueAllowUnset: true,optionsCaption: '<%=GetGlobalResourceObject("MessagesResource", "X_ChooseFromDDL")%>    '">
                </select>
            </div>
            
                <div class="form-group ">
                    <label><%=GetGlobalResourceObject("CommonControls", "F039_PersonalImage")%></label>
                    <div class="input-group">
                        <input type="text" id="txtImageName" name="txtImageName" class="form-control always-disabled" placeholder="<%=GetGlobalResourceObject("MessagesResource", "X_SelectFile")%>">
                        <span class="input-group-btn">
                            <button type="button" class="btn btn-default" onclick="$('#hdnImageBrowser').trigger('click');"><i class="fa fa-search"></i><%=GetGlobalResourceObject("CommonControls", "X_Browse")%></button>
                        </span>
                    </div>
                    <input type="file" id="hdnImageBrowser"  data-bind="attr: { accept: AcceptedImageFiles }, event: { change: ValidateImage }" style="height: 0; visibility: hidden;" />

                    <p class="form-Address">
                        <span style="padding-right: 10px;">JPG , PNG , GIF</span>
                    </p>
                    <div class="form-group text-center">
                        <input value="<%=GetGlobalResourceObject("CommonControls", "X_ClearFile")%>" class="btn btn-choose-graph btn-sm btn-danger" style="width: 100px;" type="button" data-bind="click: ClearImage()" />
                    </div>
                </div>

            
        
            <div class="form-group">
                <label class="checkbox-inline">
                    <%=GetGlobalResourceObject("CommonControls", "F009_Allowemployertoviewcontactinformation")%>
                    <input type="checkbox" id="cballowemployee" class="" data-bind="checked: AllowtoViewMyInfo">
                </label>
            </div>               
            <button class="btn btn-primary nextBtn btn-lg pull-right" data-bind="click: Save"><%=GetGlobalResourceObject("CommonControls", "X_Save")%></button>
        </div>

    </div>
    <div style="display: none;">
        <div id="Step1">
            <p><%=GetGlobalResourceObject("MessagesResource", "X_Validating")%></p>
        </div>
        <div id="Step3">
            <p><%=GetGlobalResourceObject("MessagesResource", "X_Saving")%></p>
        </div>
    </div>
   
      
    <script src="../Scripts/Extensions/ko.trilingualtext.js"  async="async"></script>
    <script src="../Scripts/Extensions/ko.date.js"  async="async"></script>
    <script src="../Scripts/Extensions/lmis.js"  async="async"></script>
    <script src="../Scripts/Extensions/strength.js"  async="async"></script>
    <script src="../Scripts/knockout.validation.min.js"  async="async"></script>
    <script src="../Individual/Scripts/edit-personal-vm.js"  async="async"></script>
    <script src="../Scripts/Extensions/config.js"></script>

</asp:Content>
