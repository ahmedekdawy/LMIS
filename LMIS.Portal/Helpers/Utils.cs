using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.ExceptionServices;
using System.Web;

namespace LMIS.Portal.Helpers
{
    public static class Utils
    {
        public static string GetPageTitle(string pageCode)
        {
            return (String)HttpContext.GetGlobalResourceObject("CommonControls", pageCode);
        }
        public static string GetResource(bool isMessage, string key)
        {
            return (String)HttpContext.GetGlobalResourceObject(isMessage ? "MessagesResource" : "CommonControls", key);
        }
        public static List<PagesVM> GetPages()
        {
            var pages = BllFactory.Singleton.PagesManager.GetPages();
            foreach (PagesVM pag in pages)
            {
                pag.PageDesc = (String)HttpContext.GetGlobalResourceObject("CommonControls", pag.PageCode + "_Title");
                if (string.IsNullOrEmpty(pag.PageDesc))
                {
                    pag.PageDesc = (String)HttpContext.GetGlobalResourceObject("CommonControls", pag.PageCode);
                }
            }

            return pages;
        }
        public static int CheckPermission(int actionid, int pageId, List<string> roleId)
        {
            return BllFactory.Singleton.PagesActionsManager.checkPermission(actionid, pageId, roleId);
        }
        public static Language GetLanguage(string langCode)
        {
            var ret = Language.English;

            switch (langCode.Trim().ToLower())
            {
                case "ar":
                    ret = Language.Arabic;
                    break;
                case "fr":
                    ret = Language.French;
                    break;
            }

            return ret;
        }
        public static string GetReportName(int lang, ReportVM result)
        {
            var value = "";
            switch (lang)
            {
                case 1: value = result.ReportEnName;
                    break;
                case 2: value = result.ReportFrName;
                    break;
                case 3: value = result.ReportArName;
                    break;
            }
            if (string.IsNullOrEmpty(value))
            {
                switch (lang)
                {
                    case 2: value = result.ReportEnName;
                        break;
                    case 3: value = result.ReportFrName;
                        break;
                    case 1: value = result.ReportArName;
                        break;
                }
            }
            if (string.IsNullOrEmpty(value))
            {
                switch (lang)
                {
                    case 3: value = result.ReportEnName;
                        break;
                    case 1: value = result.ReportFrName;
                        break;
                    case 2: value = result.ReportArName;
                        break;
                }
            }
            return value;
        }
        public static string GetReportSource(int lang, ReportVM result)
        {
            var value = "";
            switch (lang)
            {
                case 1: value = result.Source;
                    break;
                case 2: value = result.SourceFr;
                    break;
                case 3: value = result.SourceAr;
                    break;
            }
            if (string.IsNullOrEmpty(value))
            {
                switch (lang)
                {
                    case 2: value = result.Source;
                        break;
                    case 3: value = result.SourceFr;
                        break;
                    case 1: value = result.SourceAr;
                        break;
                }
            }
            if (string.IsNullOrEmpty(value))
            {
                switch (lang)
                {
                    case 3: value = result.Source;
                        break;
                    case 1: value = result.SourceFr;
                        break;
                    case 2: value = result.SourceAr;
                        break;
                }
            }
            return value;
        }
        public static void GetSubCode(ref  string nameEn, ref string nameAr, ref string nameFr)
        {
            if (string.IsNullOrEmpty(nameEn) && string.IsNullOrEmpty(nameAr))
            {
                nameEn = nameFr;
                nameAr = nameFr;
            }
            else if (string.IsNullOrEmpty(nameEn) && string.IsNullOrEmpty(nameFr))
            {
                nameEn = nameAr;
                nameFr = nameAr;
            }
            else if (string.IsNullOrEmpty(nameAr) && string.IsNullOrEmpty(nameFr))
            {
                nameAr = nameEn;
                nameFr = nameEn;
            }

            if (!string.IsNullOrEmpty(nameEn) && !string.IsNullOrEmpty(nameAr))
            {
                nameFr = nameEn;
            }
            else if (!string.IsNullOrEmpty(nameEn) && !string.IsNullOrEmpty(nameFr))
            {
                nameAr = nameEn;
            }
            else if (!string.IsNullOrEmpty(nameAr) && !string.IsNullOrEmpty(nameFr))
            {
                nameEn = nameFr;
            }

        }
        public static Language GetLanguage()
        {
            string langCode;

            if (HttpContext.Current.Request.Cookies["CultureInfo"] == null)
            {
                langCode = "en";
            }
            else
            {
                langCode = HttpContext.Current.Request.Cookies["CultureInfo"].Value;
            }

            var ret = Language.English;

            switch (langCode.Trim().ToLower())
            {
                case "ar":
                    ret = Language.Arabic;
                    break;
                case "fr":
                    ret = Language.French;
                    break;
            }

            return ret;
        }
        public static string GetLanguageStr()
        {
            string langCode;

            if (HttpContext.Current.Request.Cookies["CultureInfo"] == null)
            {
                langCode = "en";
            }
            else if (HttpContext.Current.Request.Cookies["CultureInfo"].Value.ToLower() == "ar-eg")
            {
                langCode = "ar";
            }
            else if (HttpContext.Current.Request.Cookies["CultureInfo"].Value.ToLower() == "en")
            {
                langCode = "en";
            }
            else
            {
                langCode = HttpContext.Current.Request.Cookies["CultureInfo"].Value.ToLower();
            }

            return langCode;
        }
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        public static object ServiceResponse(string pageCode, ModelResponse mr, object resources = null)
        {
            object response = null;

            switch (mr.Status)
            {

                case ResponseStatus.Success:
                    //HTTP Status Code: 200
                    HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.OK;
                    response = resources == null ? mr.Data : new
                    {
                        data = mr.Data,
                        res = resources
                    };
                    break;

                case ResponseStatus.ValidationError:
                    //HTTP Status Code: 400 (will be simulated client-side in [lmis.js] due to production environment issues)
                    HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.OK;
                    var prefix = (mr.ErrorId < 100) ? pageCode : "X";
                    response = new
                    {
                        mr.ErrorId,
                        Message = (String)HttpContext.GetGlobalResourceObject("MessagesResource", prefix + "_Error" + mr.ErrorId)
                    };
                    break;

                case ResponseStatus.InRelation:
                    response = new
                    {
                        mr.ErrorId,
                        Message = (String)HttpContext.GetGlobalResourceObject("MessagesResource", "X_Error103_InRelation")
                    };
                    break;
                case ResponseStatus.Exist:
                    response = new
                    {
                        mr.ErrorId,
                        Message =(string)HttpContext.GetGlobalResourceObject("MessagesResource","W001_RegisteredEmail")
                    };
                    break;

                case ResponseStatus.Exception:
                    //HTTP Status Code: 500
#if DEBUG
                        //ExceptionDispatchInfo.Capture(mr.Exception.GetBaseException()).Throw();
#else
                //    ExceptionDispatchInfo.Capture(mr.Exception).Throw();
#endif
                    break;
            }

            return response;
        }
        public static object ServiceResponse(object data, object resources = null)
        {
            return ServiceResponse("", new ModelResponse(0, data), resources);
        }
        public static string UploadFolder
        {
            get
            {
                return HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["VirtualUploadFolder"]);
            }
        }
        public static UserInfo LoggedUser
        {
            get
            {
                //#if DEBUG
                //return TestUser(2);
                //#else
                //    try
                //    {
                //        return (UserInfo) HttpContext.Current.Session["UserInfo"];
                //    }
                //    catch (Exception)
                //    {
                //        return null;
                //    }
                // #endif
                try
                {
                    //var newuser = (UserInfo)HttpContext.Current.Session["UserInfo"];
                    //if (newuser == null)
                    //{
                    //    return (UserInfo)HttpContext.Current.Session["UserInfo"]; 
                    //}
                    //else
                    //{
                    //HttpContext.Current.Session["UserInfo"] = TestUser(1);
                    //return TestUser(1);
                    return (UserInfo)HttpContext.Current.Session["UserInfo"];
                    //}

                }
                catch (Exception)
                {
                    return null;
                }

            }
        }
        public static UserInfo TestUser(int userSelector)
        {
            var ui = new UserInfo();

            switch (userSelector)
            {
                case 1:
                    ui.UserId = "a2105835-b1b0-4bac-b481-179036d8dd16";
                    ui.PortalUserId = 1;
                    ui.OrgContactId = 2;
                    //ui.Roles = new List<string>() { "Admin" };
                    ui.Roles = new List<string>() { "a2105835-b1b0-4bac-b481-179036d8dd16" };
                    ui.UserName = "ahmed.ekdawy@gmail.com";
                    ui.IsIndividual = false;
                    ui.IsResearcher = true;
                    ui.IsApproved = true;
                    ui.IsTrainingProvider = true;
                    ui.IsEmployer = true;
                    break;
                case 2: //Super Admin
                    ui.UserId = "69060a37-d81c-414e-85e1-bfbc3f2a7f86";
                    ui.PortalUserId = 1;
                    ui.OrgContactId = 2;
                    //ui.Roles = new List<string>() { "SuperAdmin" };
                    ui.Roles = new List<string>() { "a2105835-b1b0-4bac-b481-179036d8dd16", "98756af6-d36f-4d68-8dc5-8ca37eb35112" };
                    ui.UserName = "ahmedekdawy@gmail.com";
                    ui.IsIndividual = false;
                    ui.IsResearcher = true;
                    ui.IsApproved = true;
                    ui.IsTrainingProvider = true;
                    ui.IsEmployer = true;
                    break;
                case 3:
                    ui.UserId = "02948cce-499e-4b18-82a9-28e27257322c";
                    ui.PortalUserId = 20241;
                    ui.OrgContactId = 20085;
                    //ui.Roles = new List<string>() { "OrgAdmin" };
                    //ui.Roles = new List<string>() { "" };
                    ui.IsApproved = true;
                    ui.IsTrainingProvider = true;
                    ui.IsEmployer = true;
                    break;
                case 4:
                    ui.UserId = "609dd60a-e78e-4f17-b467-a9368c6c1f15";
                    ui.PortalUserId = 20296;
                    //ui.Roles = new List<string>() { "Individual" };
                    //ui.Roles = new List<string>() { "" };
                    ui.IsApproved = true;
                    ui.IsIndividual = true;
                    break;
            }

            HttpContext.Current.Session["UserInfo"] = ui;
            return ui;
        }
        public static void SendEmailAttachments(List<string> recipients, string subject, string body, List<string> attachments)
        {
            var keys = new List<string> { "Email.Host", "Email.Port", "Email.SSL", "Email.DisplayName", "Email.UserName", "Email.Password", "Email.UseDefaultCredentials", "Email.Domain" };
            var settings = BllFactory.Singleton.ConfigCenterManager.List(keys);

            if (settings.Values.Count != 8) return;
            //if (settings.Values.Any(string.IsNullOrWhiteSpace)) return;
            if (recipients == null || recipients.All(string.IsNullOrWhiteSpace)) return;

            var mm = new MailMessage();

            recipients.Where(r => !string.IsNullOrWhiteSpace(r))
                .ToList().ForEach(r => mm.To.Add(new MailAddress(r)));

            mm.From = new MailAddress(settings["Email.UserName".ToLower()], settings["Email.DisplayName".ToLower()]);
            mm.Subject = subject;
            mm.IsBodyHtml = true;
            mm.Body = body;

            if (attachments != null)
                attachments.Where(a => !string.IsNullOrWhiteSpace(a))
                    .ToList().ForEach(a => mm.Attachments.Add(new Attachment(a)));


            var smtp = new SmtpClient();
            smtp.UseDefaultCredentials = Convert.ToBoolean(settings["Email.UseDefaultCredentials".ToLower()]);
            smtp.Port = Convert.ToInt16(settings["Email.Port".ToLower()]);
            smtp.Host = settings["Email.Host".ToLower()];
            smtp.EnableSsl = Convert.ToBoolean(settings["Email.SSL".ToLower()]);
            var credentials = new NetworkCredential { UserName = settings["Email.UserName".ToLower()], Password = settings["Email.Password".ToLower()], Domain = (string.IsNullOrEmpty(settings["Email.Domain".ToLower()]) ? null : settings["Email.Domain".ToLower()]) };
            smtp.Credentials = credentials;
        

            try
            {
                smtp.Send(mm);
                smtp.Dispose();
                GC.Collect();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #region Individual Utils
        public static string IndividualUploadPath
        {
            get
            {
                return ConfigurationManager.AppSettings["IndividualUploadPath"];
            }
        }
        public static string IndividualDefaultImage
        {
            get
            {
                return ConfigurationManager.AppSettings["IndividualDefaultImage"];
            }
        }
        #endregion

    }
}