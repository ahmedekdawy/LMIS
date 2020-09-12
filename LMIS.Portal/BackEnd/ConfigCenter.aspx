<%@ Page Title="<%$ Resources:CommonControls,F029_Title %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="ConfigCenter.aspx.cs" Inherits="LMIS.Portal.BackEnd.ConfigCenter" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var IsDigit = (function () {
            var KeyIdentifierMap =
            {
                End: 35,
                Home: 36,
                Left: 37,
                Right: 39,
                'U+00007F': 46		// Delete
            };

            return function (e) {
                if (!e)
                    e = event;

                var iCode = (e.keyCode || e.charCode);

                if (!iCode && e.keyIdentifier && (e.keyIdentifier in KeyIdentifierMap))
                    iCode = KeyIdentifierMap[e.keyIdentifier];

                return (
						(iCode >= 48 && iCode <= 57)		// Numbers
						|| (iCode >= 35 && iCode <= 40)		// Arrows, Home, End
						|| iCode == 8						// Backspace
						|| iCode == 46						// Delete
						|| iCode == 9						// Tab
				);
            }
        })();
    </script>
    <input id="hdfBecomePartner" type="hidden" />
    <input id="hdfHomeWelcomeMessageAr" type="hidden" />
    <input id="hdfHomeWelcomeMessageEn" type="hidden" />
    <input id="hdfHomeWelcomeMessageFr" type="hidden" />
    <input id="hdfNewsLetter" type="hidden" />
    <input id="hdfOffices" type="hidden" />
    <input id="hdfOrgRegDisclaimer" type="hidden" />
            <div class="row">
            <div class="col-sm-2">
             <strong>  <label class="Bold"><%=GetGlobalResourceObject("CommonControls", "F029_Category")%>*</label></strong> </div>
            <div class="col-sm-10">
                <select id="ddlLanguage" class="form-control validationElement ddlKey" onchange="return CategoryChage();">
                    <option value="" selected="selected"><%=GetGlobalResourceObject("CommonControls", "X_Select")%></option>
                    <option value="EmailSettings"><%=GetGlobalResourceObject("CommonControls", "F029_EmailSettings")%></option>
                    <option value="SocialLinks"><%=GetGlobalResourceObject("CommonControls", "F029_SocialLinks")%></option>
                    <option value="HomeWelcomeMessageEn"><%=GetGlobalResourceObject("CommonControls", "F029_HomeWelcomeMessageEn")%></option>
                    <option value="HomeWelcomeMessageAr"><%=GetGlobalResourceObject("CommonControls", "F029_HomeWelcomeMessageAr")%></option>
                    <option value="HomeWelcomeMessageFr"><%=GetGlobalResourceObject("CommonControls", "F029_HomeWelcomeMessageFr")%></option>
                    <option value="LiveChat"><%=GetGlobalResourceObject("CommonControls", "F029LiveChat")%></option>
                    <option value="NewsLetter"><%=GetGlobalResourceObject("CommonControls", "F029_NewsLetter")%></option>
                    <option value="Offices"><%=GetGlobalResourceObject("CommonControls", "F029_Offices")%></option>
                    <option value="OrgRegDisclaimer"><%=GetGlobalResourceObject("CommonControls", "F029_OrgRegDisclaimer")%></option>
                      <option value="BecomePartner"><%=GetGlobalResourceObject("CommonControls", "F042_Title")%></option>
                     <option value="ReportsWindow"><%=GetGlobalResourceObject("CommonControls", "Menu_LabourData")%></option>
                </select>
            </div>
        </div>
        <br />
     
    <div id="EmailSettings" style="display:none">
        <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F009_Email")%>*</div> 
        <div class="col-lg-10"><input type="email" id="txtEmail" class="form-control input-group-lg full-width txtEmail validationElement" maxlength="100" /></div> 
        <br/> <br/>
        <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029_EmailDisplayName")%>*</div> 
        <div class="col-lg-10"><input type="text" id="txtEmailDisplayName" class="form-control input-group-lg full-width txtEmailDisplayName validationElement" maxlength="100" /></div> 
        <br/> <br />
             <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029_EmailHost")%>*</div> 
        <div class="col-lg-10"><input type="text" id="txtEmailHost" class="form-control input-group-lg full-width txtEmailHost validationElement" maxlength="100" /></div> 
        <br/> <br />
                     <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029_Domain")%></div> 
        <div class="col-lg-10"><input type="text" id="txtDomain" class="form-control input-group-lg full-width txtDomain " maxlength="100" /></div> 
        <br/> <br />
           <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "X_Password")%>*</div> 
        <div class="col-lg-10"><input type="password" id="txtPassword" class="form-control input-group-lg full-width txtPassword validationElement" maxlength="100" /></div> 
        <br/> <br />
          <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029_EmailPort")%>*</div> 
        <div class="col-lg-10"><input type="number" id="txtEmailPort" class="number txtEmailPort validationElement" maxlength="5" /></div> 
         <br/> <br/>
          <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029_EmailSSL")%>*</div> 
        <div class="col-lg-10 ">
   <input type="checkbox" name="chkEnableSSl" class="checkbox chkEnableSSl validationElement">
        </div> 
              <br/> <br/>
          <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029_UseDefaultCredentials")%>*</div> 
        <div class="col-lg-10 ">
   <input type="checkbox" name="chkUseDefaultCredentials" class="checkbox chkUseDefaultCredentials validationElement">
        </div> 
              <br/> <br/>
   
      
     </div>
     <div id="NewsLetterSetting" style="display:none">
            <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029_NewsLetterSetting")%>*</div> 
        <div class="col-lg-10"><input type="number" id="txtNewsLetterSetting" class="number form-control input-group-lg full-width txtNewsLetterSetting validationElement" maxlength="100" /></div> 
        <br/> <br/>
     </div>
     <div id="FCKDiv" style="display:none">
         <FCKeditorV2:FCKeditor ID="FCKeditor" Height="500px" runat="server" BasePath="~/FCKeditor/" ImageBrowserURL="~/FCKeditor/" LinkBrowserURL="~/FCKeditor/"></FCKeditorV2:FCKeditor>
        
     </div>
     <br/>
         <br/>
       <div id="SocialLinks" style="display:none">
        <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029_FaceBook")%></div> 
        <div class="col-lg-10"><input type="text" id="txtFaceBook" class="form-control input-group-lg full-width txtFaceBook" maxlength="100" /></div> 
        <br/> <br/>
        <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029googleplus")%></div> 
        <div class="col-lg-10"><input type="text" id="txtgoogleplus" class="form-control input-group-lg full-width txtgoogleplus" maxlength="100" /></div> 
        <br/> <br />
           <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029linkedin")%></div> 
        <div class="col-lg-10"><input type="text" id="txtlinkedin" class="form-control input-group-lg full-width txtlinkedin" maxlength="100" /></div> 
        <br/> <br />
          <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029twitter")%></div> 
        <div class="col-lg-10"><input type="text" id="txttwitter" class="form-control input-group-lg full-width  txttwitter" maxlength="100" /></div> 
         <br/> <br/>
     <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029youtube")%></div> 
        <div class="col-lg-10"><input type="text" id="txtyoutube" class="form-control input-group-lg full-width  txtyoutube" maxlength="100" /></div> 
         <br/> <br/>
              <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029eblog")%></div> 
        <div class="col-lg-10"><input type="text" id="txteblog" class="form-control input-group-lg full-width  txteblog" maxlength="100" /></div> 
         <br/> <br/>
              <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029ContactUsMap")%></div> 
        <div class="col-lg-10"><input type="text" id="txtContactUsMap" class="form-control input-group-lg full-width  txtContactUsMap" maxlength="100" /></div> 
         <br/> <br/>
      
     </div>
    <div id="LiveChat" style="display:none">
        <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029LiveChatDays")%></div> 
        <div class="col-lg-10">
            <label class="checkbox-inline" for="chkSaturday">
      <input type="checkbox" name="chkSunday" id="chkSaturday" value="Saturday">
     <%=GetGlobalResourceObject("CommonControls", "X_Saturday")%>
    </label>
                      <label class="checkbox-inline" for="chkSunday">
      <input type="checkbox" name="chkSunday" id="chkSunday" value="Sunday">
     <%=GetGlobalResourceObject("CommonControls", "X_Sunday")%>
    </label>
                      <label class="checkbox-inline" for="chkMonday">
      <input type="checkbox" name="chkSunday" id="chkMonday" value="Monday">
     <%=GetGlobalResourceObject("CommonControls", "X_Monday")%>
    </label>
                                <label class="checkbox-inline" for="chktuesday">
      <input type="checkbox" name="chkSunday" id="chktuesday" value="tuesday">
     <%=GetGlobalResourceObject("CommonControls", "X_tuesday")%>
    </label>
                      <label class="checkbox-inline" for="chkWednesday">
      <input type="checkbox" name="chkSunday" id="chkWednesday" value="Wednesday">
     <%=GetGlobalResourceObject("CommonControls", "X_Wednesday")%>
    </label>
                      <label class="checkbox-inline" for="chkThursday">
      <input type="checkbox" name="chkSunday" id="chkThursday" value="Thursday">
     <%=GetGlobalResourceObject("CommonControls", "X_Thursday")%>
    </label>
                      <label class="checkbox-inline" for="chkFriday">
      <input type="checkbox" name="chkSunday" id="chkFriday" value="Friday">
     <%=GetGlobalResourceObject("CommonControls", "X_Friday")%>
    </label>

         
        </div> 
        <br/> <br/>
        <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029LiveChatFrom")%></div> 
        <div class="col-lg-2"><input type="text" id="txtLiveChatFrom" class="timepicker form-control hasWickedpicker timepiker txtLiveChatFrom" maxlength="100" /></div> 
       
           <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F029LiveChatTo")%></div> 
        <div class="col-lg-2"><input type="text" id="txtLiveChatTo" class="timepicker form-control hasWickedpicker timepiker txtLiveChatTo" maxlength="100" /></div> 
        <br/> <br />
          
      
     </div>
    <br/><br/>
    <div id="ReportsWindow" style="display:none">
        <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F017_YearFrom")%>*</div> 
        <div class="col-lg-2"><input  id="txtReportsWindowYearFrom" onkeypress="return IsDigit(event);" class="form-control input-group-lg full-width txtReportsWindowYearFrom validationElement" maxlength="4" /></div> 
      
    <div class="col-lg-2"><%=GetGlobalResourceObject("CommonControls", "F017_YearTo")%>*</div> 
        <div class="col-lg-2"><input  id="txtReportsWindowYearTo" onkeypress="return IsDigit(event);" class="form-control input-group-lg full-width txtReportsWindowYearTo validationElement" maxlength="4" /></div> 
      
       <br/><br/>
     </div>
      <div class="row center submit" style="display:none"><input type="button" id="btnSubmit" onclick="return Save();" value="Submit" class="btn btn-success"/></div>
    <script src="../BackEnd/Scripts/ConfigCenter.js"></script>
</asp:Content>
