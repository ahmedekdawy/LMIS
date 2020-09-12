function Get() {

    return lmis.ajax("../BackEnd/ConfigCenter.aspx/Get", null, 0, "",
        function (data) {
            $(".txtEmail").val(data.d.filter(function (x) { return x.Key == "Email.UserName" })[0].Value);
            $(".txtEmailDisplayName").val(data.d.filter(function (x) { return x.Key == "Email.DisplayName" })[0].Value);
            $(".txtEmailHost").val(data.d.filter(function (x) { return x.Key == "Email.Host" })[0].Value);
            $(".txtEmailPort").val(data.d.filter(function (x) { return x.Key == "Email.Port" })[0].Value);
            $(".txtDomain").val(data.d.filter(function (x) { return x.Key == "Email.Domain" })[0].Value);
            $(".txtPassword").val(data.d.filter(function (x) { return x.Key == "Email.Password" })[0].Value);
            data.d.filter(function (x) { return x.Key == "Email.SSL" })[0].Value == "True" ? $(".chkEnableSSl").prop('checked', true) : $(".chkEnableSSl").removeAttr('checked');
            data.d.filter(function (x) { return x.Key == "Email.UseDefaultCredentials" })[0].Value == "True" ? $(".chkUseDefaultCredentials").prop('checked', true) : $(".chkUseDefaultCredentials").removeAttr('checked');
         
            $(".txtFaceBook").val(data.d.filter(function (x) { return x.Key == "facebook" })[0].Value);
            $(".txtgoogleplus").val(data.d.filter(function (x) { return x.Key == "google-plus" })[0].Value);
            $(".txtlinkedin").val(data.d.filter(function (x) { return x.Key == "linkedin" })[0].Value);
            $(".txttwitter").val(data.d.filter(function (x) { return x.Key == "twitter" })[0].Value);
            $(".txtyoutube").val(data.d.filter(function (x) { return x.Key == "youtube" })[0].Value);
            $(".txtyoutube").val(data.d.filter(function (x) { return x.Key == "youtube" })[0].Value);
            $(".txteblog").val(data.d.filter(function (x) { return x.Key == "eblog" })[0].Value);
            $(".txtContactUsMap").val(data.d.filter(function (x) { return x.Key == "ContactUsMap" })[0].Value);
            
            $("#hdfBecomePartner").val(data.d.filter(function (x) { return x.Key == "BecomePartner" })[0].Value);
            $("#hdfHomeWelcomeMessageAr").val(data.d.filter(function (x) { return x.Key == "Home.WelcomeMessage" && x.SexCode == 3 })[0].Value);
            $("#hdfHomeWelcomeMessageEn").val(data.d.filter(function (x) { return x.Key == "Home.WelcomeMessage" && x.SexCode == 1 })[0].Value);
            $("#hdfHomeWelcomeMessageFr").val(data.d.filter(function (x) { return x.Key == "Home.WelcomeMessage" && x.SexCode == 2 })[0].Value);
            $("#hdfNewsLetter").val(data.d.filter(function (x) { return x.Key == "NewsLetterTemplate" })[0].Value);
            $("#hdfOffices").val(data.d.filter(function (x) { return x.Key == "Offices" })[0].Value);
            $("#hdfOrgRegDisclaimer").val(data.d.filter(function (x) { return x.Key == "OrgReg.Disclaimer" })[0].Value);
            $("#txtNewsLetterSetting").val(data.d.filter(function (x) { return x.Key == "NewsLetterSetting" })[0].Value);
            data.d.filter(function (x) { return x.Key == "LiveChatDays" })[0].Value.includes("1") ? $("#chkSaturday").prop('checked', true) : $("#chkSaturday").removeAttr('checked');
            data.d.filter(function (x) { return x.Key == "LiveChatDays" })[0].Value.includes("2") ? $("#chkSunday").prop('checked', true) : $("#chkSunday").removeAttr('checked');
            data.d.filter(function (x) { return x.Key == "LiveChatDays" })[0].Value.includes("3") ? $("#chkMonday").prop('checked', true) : $("#chkMonday").removeAttr('checked');
            data.d.filter(function (x) { return x.Key == "LiveChatDays" })[0].Value.includes("4") ? $("#chktuesday").prop('checked', true) : $("#chktuesday").removeAttr('checked');
            data.d.filter(function (x) { return x.Key == "LiveChatDays" })[0].Value.includes("5") ? $("#chkWednesday").prop('checked', true) : $("#chkWednesday").removeAttr('checked');
            data.d.filter(function (x) { return x.Key == "LiveChatDays" })[0].Value.includes("6") ? $("#chkThursday").prop('checked', true) : $("#chkThursday").removeAttr('checked');
            data.d.filter(function (x) { return x.Key == "LiveChatDays" })[0].Value.includes("7") ? $("#chkFriday").prop('checked', true) : $("#chkFriday").removeAttr('checked');
            $(".txtLiveChatFrom").val(data.d.filter(function (x) { return x.Key == "LiveChatFrom" })[0].Value);
            $(".txtLiveChatTo").val(data.d.filter(function (x) { return x.Key == "LiveChatTo" })[0].Value);
            $(".txtReportsWindowYearFrom").val(data.d.filter(function (x) { return x.Key == "ReportsWindowYearFrom" })[0].Value);
            $(".txtReportsWindowYearTo").val(data.d.filter(function (x) { return x.Key == "ReportsWindowYearTo" })[0].Value);
     
        });

}

function CategoryChage() {
    $(".wickedpicker").hide();
    switch($(".ddlKey").val()) {
        case 'EmailSettings':
           
            $("#EmailSettings").show();
            $("#SocialLinks").hide();
            $("#HomeWelcomeMessageEn").hide();
            $("#HomeWelcomeMessageAr").hide();
            $("#HomeWelcomeMessageFr").hide();
            $("#LiveChat").hide();
            $("#FCKDiv").hide();
            $("#Offices").hide();
            $("#OrgRegDisclaimer").hide();
            $("#NewsLetterSetting").hide();
            $("#ReportsWindow").hide();
            $(".submit").show();
            
            break;
        case 'SocialLinks':
            $("#EmailSettings").hide();
            $("#SocialLinks").show();
            $("#HomeWelcomeMessageEn").hide();
            $("#HomeWelcomeMessageAr").hide();
            $("#HomeWelcomeMessageFr").hide();
            $("#LiveChat").hide();
            $("#FCKDiv").hide();
            $("#Offices").hide();
            $("#OrgRegDisclaimer").hide();
            $("#NewsLetterSetting").hide();
            $("#ReportsWindow").hide();
            $(".submit").show();
            break;
        case 'NewsLetter':
            $("#EmailSettings").hide();
            $("#SocialLinks").hide();
            $("#HomeWelcomeMessageEn").hide();
            $("#HomeWelcomeMessageAr").hide();
            $("#HomeWelcomeMessageFr").hide();
            $("#LiveChat").hide();
            $("#FCKDiv").show();
            $("#Offices").hide();
            $("#OrgRegDisclaimer").hide();
            $("#NewsLetterSetting").show();
            $("#ReportsWindow").hide();
            $(".submit").show();
            FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor').SetHTML($("#hdfNewsLetter").val());
            break;
        case 'BecomePartner':
            $("#EmailSettings").hide();
            $("#SocialLinks").hide();
            $("#HomeWelcomeMessageEn").hide();
            $("#HomeWelcomeMessageAr").hide();
            $("#HomeWelcomeMessageFr").hide();
            $("#LiveChat").hide();
            $("#FCKDiv").show();
            $("#Offices").hide();
            $("#OrgRegDisclaimer").hide();
            $("#NewsLetterSetting").hide();
            $("#ReportsWindow").hide();
            $(".submit").show();
            FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor').SetHTML($("#hdfBecomePartner").val());
            break;
        case 'HomeWelcomeMessageEn': 
            $("#EmailSettings").hide();
            $("#SocialLinks").hide();
            $("#HomeWelcomeMessageEn").hide();
            $("#HomeWelcomeMessageAr").hide();
            $("#HomeWelcomeMessageFr").hide();
            $("#LiveChat").hide();
            $("#FCKDiv").show();
            $("#Offices").hide();
            $("#OrgRegDisclaimer").hide();
            $("#NewsLetterSetting").hide();
            $("#ReportsWindow").hide();
            $(".submit").show();
            FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor').SetHTML($("#hdfHomeWelcomeMessageEn").val());
            break;
        case 'HomeWelcomeMessageAr':
            $("#EmailSettings").hide();
            $("#SocialLinks").hide();
            $("#HomeWelcomeMessageEn").hide();
            $("#HomeWelcomeMessageAr").hide();
            $("#HomeWelcomeMessageFr").hide();
            $("#LiveChat").hide();
            $("#FCKDiv").show();
            $("#Offices").hide();
            $("#OrgRegDisclaimer").hide();
            $("#NewsLetterSetting").hide();
            $("#ReportsWindow").hide();
            $(".submit").show();
            FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor').SetHTML($("#hdfHomeWelcomeMessageAr").val());
            break;
        case 'HomeWelcomeMessageFr':
            $("#EmailSettings").hide();
            $("#SocialLinks").hide();
            $("#HomeWelcomeMessageEn").hide();
            $("#HomeWelcomeMessageAr").hide();
            $("#HomeWelcomeMessageFr").hide();
            $("#LiveChat").hide();
            $("#FCKDiv").show();
            $("#Offices").hide();
            $("#OrgRegDisclaimer").hide();
            $("#NewsLetterSetting").hide();
            $("#ReportsWindow").hide();
            $(".submit").show();
            FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor').SetHTML($("#hdfHomeWelcomeMessageFr").val());
            break;
        case 'Offices':
            $("#EmailSettings").hide();
            $("#SocialLinks").hide();
            $("#HomeWelcomeMessageEn").hide();
            $("#HomeWelcomeMessageAr").hide();
            $("#HomeWelcomeMessageFr").hide();
            $("#LiveChat").hide();
            $("#FCKDiv").show();
            $("#Offices").hide();
            $("#OrgRegDisclaimer").hide();
            $("#NewsLetterSetting").hide();
            $("#ReportsWindow").hide();
            $(".submit").show();
            FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor').SetHTML($("#hdfOffices").val());
            break;
        case 'OrgRegDisclaimer':
            $("#EmailSettings").hide();
            $("#SocialLinks").hide();
            $("#HomeWelcomeMessageEn").hide();
            $("#HomeWelcomeMessageAr").hide();
            $("#HomeWelcomeMessageFr").hide();
            $("#LiveChat").hide();
            $("#FCKDiv").show();
            $("#Offices").hide();
            $("#OrgRegDisclaimer").hide();
            $("#NewsLetterSetting").hide();
            $("#ReportsWindow").hide();

           $(".submit").show();
            FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor').SetHTML($("#hdfOrgRegDisclaimer").val());
            break;
        case 'LiveChat':
            $("#EmailSettings").hide();
            $("#SocialLinks").hide();
            $("#HomeWelcomeMessageEn").hide();
            $("#HomeWelcomeMessageAr").hide();
            $("#HomeWelcomeMessageFr").hide();
            $("#FCKDiv").hide();
            $("#Offices").hide();
            $("#OrgRegDisclaimer").hide();
            $("#NewsLetterSetting").hide();
            $("#LiveChat").show();
            $("#ReportsWindow").hide();
            $(".submit").show();
            break;
        case 'ReportsWindow':
            $("#EmailSettings").hide();
            $("#SocialLinks").hide();
            $("#HomeWelcomeMessageEn").hide();
            $("#HomeWelcomeMessageAr").hide();
            $("#HomeWelcomeMessageFr").hide();
            $("#FCKDiv").hide();
            $("#Offices").hide();
            $("#OrgRegDisclaimer").hide();
            $("#NewsLetterSetting").hide();
            $("#LiveChat").hide();
            $("#ReportsWindow").show();
            $(".submit").show();
            break;
        default:
            $("#EmailSettings").hide();
            $("#SocialLinks").hide(); 
            $("#HomeWelcomeMessageEn").hide();
            $("#HomeWelcomeMessageAr").hide();
            $("#HomeWelcomeMessageFr").hide();
            $("#LiveChat").hide();
            $("#FCKDiv").hide();
            $("#Offices").hide();
            $("#NewsLetterSetting").hide();
            $("#OrgRegDisclaimer").hide();
            $("#ReportsWindow").hide();
           $(".submit").hide();

            
    }
    $("p").hide();
  
}

Get();
function fnTime() {
    $(".wickedpicker__title").css('display', 'block');

}
function Save() {
   
    var keysvm = new Object() ;
    switch ($(".ddlKey").val()) {
        case 'EmailSettings':
            if ($(".txtEmail").val().toString() == '') {
                $(".txtEmail").focus(); return;
            }
            if ($(".txtEmailDisplayName").val().toString() == '') {
                $(".txtEmailDisplayName").focus(); return;
            }
            if ($(".txtEmailHost").val().toString() == '') {
                $(".txtEmailHost").focus(); return;
            }
            if ($(".txtEmailPort").val().toString() == '') {
                $(".txtEmailPort").focus(); return;
            }
            if ($(".txtPassword").val().toString() == '') {
                $(".txtPassword").focus(); return;
            }
            var validateemail = validateEmail($(".txtEmail").val());
            if (!validateemail) {
                lmis.notification.error($("#X_InvalidEmail").html());
                $(".txtEmail").focus(); return;
            }
            keysvm["Email.UserName"] = $(".txtEmail").val();
            keysvm["Email.DisplayName"] = $(".txtEmailDisplayName").val();
            keysvm["Email.Host"] = $(".txtEmailHost").val();
            keysvm["Email.Port"] = $(".txtEmailPort").val();
            keysvm["Email.SSL"] = $(".chkEnableSSl").prop("checked") ? true : false;
            keysvm["Email.UseDefaultCredentials"] = $(".chkUseDefaultCredentials").prop("checked") ? true : false;
            keysvm["Email.Password"] = $(".txtPassword").val();
            keysvm["Email.Domain"] = $(".txtDomain").val();
            break;
        case 'SocialLinks':
            keysvm["facebook"] = $(".txtFaceBook").val();
            keysvm["google-plus"] = $(".txtgoogleplus").val();
            keysvm["linkedin"] = $(".txtlinkedin").val();
            keysvm["twitter"] = $(".txttwitter").val();
            keysvm["youtube"] = $(".txtyoutube").val();
            keysvm["eblog"] = $(".txteblog").val();
            keysvm["ContactUsMap"] = $(".txtContactUsMap").val();
            break;
        case 'NewsLetter':
            var FCKeditor = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor');
            $("#hdfNewsLetter").val(FCKeditor.GetXHTML(true));
            if ($(".txtNewsLetterSetting").val() == '') {
                $(".txtNewsLetterSetting").focus(); return;
            }
            if (FCKeditor.GetXHTML(true) == '') {
                FCKeditor.focus(); return;
            }
            keysvm['NewsLetterTemplate'] = FCKeditor.GetXHTML(true);
            keysvm['NewsLetterSetting'] = $(".txtNewsLetterSetting").val();;
            break;
        case 'BecomePartner':
            var FCKeditor = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor');
            $("#hdfBecomePartner").val(FCKeditor.GetXHTML(true));
            if (FCKeditor.GetXHTML(true) == '') {
                FCKeditor.focus(); return;
            }
            keysvm['BecomePartner'] = FCKeditor.GetXHTML(true);

            break;
        case 'HomeWelcomeMessageEn':
            var FCKeditor = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor');
            $("#hdfHomeWelcomeMessageEn").val(FCKeditor.GetXHTML(true));
            if (FCKeditor.GetXHTML(true) == '') {
                FCKeditor.focus(); return;
            }
            keysvm['Home.WelcomeMessageEn'] = FCKeditor.GetXHTML(true);

            break;
        case 'HomeWelcomeMessageAr':
            var FCKeditor = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor');
            $("#hdfHomeWelcomeMessageAr").val(FCKeditor.GetXHTML(true));
            if (FCKeditor.GetXHTML(true) == '') {
                FCKeditor.focus(); return;
            }
            keysvm['Home.WelcomeMessageAr'] = FCKeditor.GetXHTML(true);

            break;
        case 'HomeWelcomeMessageFr':
            var FCKeditor = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor');
            $("#hdfHomeWelcomeMessageFr").val(FCKeditor.GetXHTML(true));
            if (FCKeditor.GetXHTML(true) == '') {
                FCKeditor.focus(); return;
            }
            keysvm['Home.WelcomeMessageFr'] = FCKeditor.GetXHTML(true);

            break;
        case 'Offices':
            var FCKeditor = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor');
            $("#hdfOffices").val(FCKeditor.GetXHTML(true));
            if (FCKeditor.GetXHTML(true) == '') {
                FCKeditor.focus(); return;
            }
            keysvm['Offices'] = FCKeditor.GetXHTML(true);

            break;
        case 'OrgRegDisclaimer':
            var FCKeditor = FCKeditorAPI.GetInstance('ContentPlaceHolder1_FCKeditor');
            $("#hdfOrgRegDisclaimer").val(FCKeditor.GetXHTML(true));
            if (FCKeditor.GetXHTML(true) == '') {
                FCKeditor.focus(); return;
            }
            keysvm['OrgReg.Disclaimer'] = FCKeditor.GetXHTML(true);

            break;
        case 'LiveChat':
            if (Date.parse('01/01/2011 ' + $(".txtLiveChatTo").val().replace(/ /g, '')) < Date.parse('01/01/2011 ' + $(".txtLiveChatFrom").val().replace(/ /g, ''))) {
                lmis.notification.error($("#X_FromGreateThanToTime").html());
                return;
            }
            keysvm["LiveChatDays"] = "";
            keysvm["LiveChatDays"] += $("#chkSaturday").prop("checked") ? ",1" : "";
            keysvm["LiveChatDays"] += $("#chkSunday").prop("checked") ? ",2" : "";
            keysvm["LiveChatDays"] += $("#chkMonday").prop("checked") ? ",3" : "";
            keysvm["LiveChatDays"] += $("#chktuesday").prop("checked") ? ",4" : "";
            keysvm["LiveChatDays"] += $("#chkWednesday").prop("checked") ? ",5" : "";
            keysvm["LiveChatDays"] += $("#chkThursday").prop("checked") ? ",6" : "";
            keysvm["LiveChatDays"] += $("#chkFriday").prop("checked") ? ",7" : "";
            keysvm["LiveChatDays"] = keysvm["LiveChatDays"].slice(1);
            keysvm["LiveChatFrom"] = $(".txtLiveChatFrom").val().replace(/ /g, '');
            keysvm["LiveChatTo"] = $(".txtLiveChatTo").val().replace(/ /g, '');

            break;
        case 'ReportsWindow':
            if (parseInt($(".txtReportsWindowYearFrom").val()) > parseInt($(".txtReportsWindowYearTo").val())) {
                lmis.notification.error($("#X_FromGreateThanToTime").html());
                return;
            }
            keysvm["ReportsWindowYearFrom"] = $(".txtReportsWindowYearFrom").val();
            keysvm["ReportsWindowYearTo"] = $(".txtReportsWindowYearTo").val();
            break;
    }

    var dto = { obj: keysvm };
    return lmis.ajax("../BackEnd/ConfigCenter.aspx/Post", dto, 0, "show,close", function (data) {
        if (data.d.Data == null) {
            lmis.notification.success();
        } 
    });

}
function validateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}