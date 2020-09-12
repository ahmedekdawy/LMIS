using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System.Web.SessionState;

namespace LMIS.Portal
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(config =>
            {
                config.MapHttpAttributeRoutes();
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{action}/{id}",
                    defaults: new {id = RouteParameter.Optional});
            });

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterRoutes(RouteTable.Routes);
        }
        void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("","ContactUs", "~/FrontEnd/ContactUs.aspx");
            routes.MapPageRoute("", "home", "~/FrontEnd/home.aspx");
            routes.MapPageRoute("", "BackEnd/home", "~/BackEnd/BackEndDefault.aspx");
            routes.MapPageRoute("", "login", "~/Administration/login.aspx");
            routes.MapPageRoute("", "ConfirmPassword", "~/Administration/ResetPasswordConfirmation.aspx");
            routes.MapPageRoute("", "ConceptOfNonFormalTraining", "~/FrontEnd/ConceptOfNonFormalTraining.aspx");
            routes.MapPageRoute("", "BackEnd/ConceptOfNonFormalTraining", "~/BackEnd/ConceptOfNonFormalTrainingBack.aspx");
            routes.MapPageRoute("", "BackEnd/TransactionHistory", "~/BackEnd/TransactionHistory.aspx");
            routes.MapPageRoute("", "BackEnd/ObsceneWords", "~/BackEnd/ObsceneWords.aspx");
            routes.MapPageRoute("", "BackEnd/ContactUsEmails", "~/BackEnd/ListOfEmails.aspx");
            routes.MapPageRoute("", "BackEnd/HelpFullLinks", "~/BackEnd/HelpfulLinks.aspx");
            routes.MapPageRoute("", "HelpFullLinks", "~/FrontEnd/HelpfulLinksFront.aspx");
            routes.MapPageRoute("", "BackEnd/DescriptiveJob", "~/BackEnd/DescriptiveJobBack.aspx");
            routes.MapPageRoute("", "DescriptiveJob", "~/FrontEnd/DescriptiveJob.aspx");
            routes.MapPageRoute("", "BackEnd/Qualifications", "~/BackEnd/QualificationsBack.aspx");
            routes.MapPageRoute("", "Qualifications", "~/FrontEnd/Qualifications.aspx");

            routes.MapPageRoute("", "LabourExchange/Register", "~/LabourExchange/LabourExchange.aspx#anchor");
            routes.MapPageRoute("", "LabourExchange/JobSearch", "~/LabourExchange/JobSearch.aspx#anchor");
            routes.MapPageRoute("", "LabourExchange/TrainingSearch", "~/LabourExchange/TrainingSearch.aspx#anchor");
            routes.MapPageRoute("", "LabourExchange/CVSearch", "~/LabourExchange/CVSearch.aspx#anchor");
            routes.MapPageRoute("", "LabourExchange/JobList", "~/LabourExchange/JobList.aspx#anchor");
            routes.MapPageRoute("", "LabourExchange/EventList", "~/LabourExchange/EventList.aspx#anchor");
            routes.MapPageRoute("", "LabourExchange/TrainingList", "~/LabourExchange/TrainingList.aspx#anchor");
            routes.MapPageRoute("", "LabourExchange/OpportunityList", "~/LabourExchange/OpportunityList.aspx#anchor");
            routes.MapPageRoute("", "LabourExchange/ContactPerson", "~/LabourExchange/ContactPerson.aspx#anchor");
            routes.MapPageRoute("", "LabourExchange/CVSearch", "~/LabourExchange/CVSearch.aspx");
            routes.MapPageRoute("", "LabourExchange/JobPost", "~/LabourExchange/JobPost.aspx");
            routes.MapPageRoute("", "LabourExchange/EventPost", "~/LabourExchange/EventPost.aspx");
            routes.MapPageRoute("", "LabourExchange/TrainingPost", "~/LabourExchange/TrainingPost.aspx");
            routes.MapPageRoute("", "LabourExchange/OpportunityPost", "~/LabourExchange/OpportunityPost.aspx");
            routes.MapPageRoute("", "LabourExchange/EventDetails", "~/LabourExchange/EventDetails.aspx");
            routes.MapPageRoute("", "LabourExchange/OpportunityDetails", "~/LabourExchange/OpportunityDetails.aspx");
            routes.MapPageRoute("", "LabourExchange/TrainingDetails", "~/LabourExchange/TrainingDetails.aspx");
            routes.MapPageRoute("", "LabourExchange/ContactPerson", "~/LabourExchange/ContactPerson.aspx");

            routes.MapPageRoute("", "Organization/SignUp", "~/Registration/SignUp.aspx");
            routes.MapPageRoute("", "Individual/SignUp", "~/Individual/Registeration.aspx");

            routes.MapPageRoute("", "HarmonizedData", "~/Statistics/Report_Entry.aspx");
            routes.MapPageRoute("", "HarmonizedData/View", "~/Statistics/View_Report.aspx");
            routes.MapPageRoute("", "LabourMarket/AverageWages", "~/FrontEnd/AverageWages.aspx");
            routes.MapPageRoute("", "SizeOfInformalSector", "~/FrontEnd/SizeOfInformalSector.aspx");
            routes.MapPageRoute("", "LabourMarket/PastAchievements", "~/FrontEnd/PastAchievements.aspx");
            routes.MapPageRoute("", "LabourMarket/NewsFeed", "~/FrontEnd/NewsFeed.aspx");
            routes.MapPageRoute("", "SiteMap", "~/FrontEnd/SiteMap.aspx");
            routes.MapPageRoute("", "BecomePartner", "~/DashBoard/BecomePartner.aspx");
            routes.MapPageRoute("", "Partners", "~/DashBoard/Partners.aspx");
            routes.MapPageRoute("", "Partners/Details", "~/DashBoard/PartnerDetails.aspx");
            routes.MapPageRoute("", "Disclaimer", "~/Disclaimer.aspx");
            routes.MapPageRoute("", "Individual/Profile", "~/Individual/Profile.aspx");
            routes.MapPageRoute("", "Registration/Profile", "~/Registration/Profile.aspx");
            routes.MapPageRoute("", "Registration/Contacts", "~/Registration/Contacts.aspx");
            routes.MapPageRoute("", "LiveChat", "~/FrontEnd/LiveChat.aspx");
            routes.MapPageRoute("", "ReviewRequests", "~/BackEnd/ReviewRequests.aspx");
            routes.MapPageRoute("", "BackEnd/Review", "~/BackEnd/Review.aspx");
            routes.MapPageRoute("", "NewsDetails", "~/FrontEnd/NewsItem.aspx");
                routes.MapPageRoute("", "News", "~/BackEnd/News.aspx");
            routes.MapPageRoute("", "Reports", "~/Statistics/ReportsList.aspx");
            routes.MapPageRoute("", "CreateReport", "~/Statistics/CreateReport.aspx");
            routes.MapPageRoute("", "Themes", "~/BackEnd/DimThemes.aspx");
            routes.MapPageRoute("", "ThemesVariable", "~/Statistics/ThemesVariable.aspx");
            routes.MapPageRoute("", "ThemesGenerateTemplate", "~/BackEnd/GenerateThemeTemplate.aspx");
            routes.MapPageRoute("", "ThemesImportTemplate", "~/BackEnd/ImportTemplate.aspx");
            routes.MapPageRoute("", "Register", "~/Administration/Register.aspx");
            routes.MapPageRoute("", "Roles", "~/Administration/Roles.aspx");
            routes.MapPageRoute("", "UsersInRoles", "~/Administration/UsersInRoles.aspx");
            routes.MapPageRoute("", "ResetPassword", "~/Administration/ResetPassword.aspx");
            routes.MapPageRoute("", "PagesActions", "~/Administration/PagesActions.aspx");
            routes.MapPageRoute("", "SubCode", "~/BackEnd/SubCode.aspx");
            routes.MapPageRoute("", "ConfigCenter", "~/BackEnd/ConfigCenter.aspx");
            routes.MapPageRoute("", "Faq", "~/FrontEnd/Faq.aspx");
            routes.MapPageRoute("", "FaqDetail", "~/FrontEnd/FaqDetail.aspx");
            routes.MapPageRoute("", "BackEnd/Faq", "~/BackEnd/FaqBack.aspx");
            routes.MapPageRoute("", "ConceptsDefinitions", "~/FrontEnd/ConceptsDefinitions.aspx");
            routes.MapPageRoute("", "BackEnd/ConceptsDefinitions", "~/BackEnd/ConceptsDefinitionsBack.aspx");
            routes.MapPageRoute("", "Employers", "~/FrontEnd/Employers.aspx");
            routes.MapPageRoute("", "TrainingProviders", "~/FrontEnd/TrainingProviders.aspx");
            routes.MapPageRoute("", "BackEnd/EmployersTrainingProviders", "~/BackEnd/EmployersTrainingProviders.aspx");
            routes.MapPageRoute("", "BackEnd/workoffices", "~/BackEnd/Offices.aspx");
            routes.MapPageRoute("", "workoffices", "~/FrontEnd/Offices.aspx");
            routes.MapPageRoute("", "BackEnd/RecruitmentAgencies", "~/BackEnd/RecruitmentAgencies.aspx");
            routes.MapPageRoute("", "RecruitmentAgencies", "~/FrontEnd/RecruitmentAgencies.aspx");
            routes.MapPageRoute("", "BackEnd/LaborUnions", "~/BackEnd/Unions.aspx");
            routes.MapPageRoute("", "LaborUnions", "~/FrontEnd/Unions.aspx");
      

            routes.MapPageRoute("", "AvailableVacancies", "~/FrontEnd/AvailableVacancies.aspx");
            routes.MapPageRoute("", "JobsOffers", "~/FrontEnd/JobsOffers.aspx");
            routes.MapPageRoute("", "Calendar", "~/FrontEnd/Calendar.aspx");

          //  routes.MapPageRoute("", "Individual/Profile/Approve", "~/Individual/Profile.aspx/Approve");
            

            
            
           

            

        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.ReadOnly);
            }
        }

        void Application_BeginRequest(Object sender, EventArgs e)
        {
            var culture = "en";

            if (Request.QueryString["ci"] == null)
            {
                var cookie = Request.Cookies["CultureInfo"];

                if (cookie == null || cookie.Value == null)
                {
                    if (Request.UserLanguages != null && Request.UserLanguages.Length > 0)
                    {
                        var browser = Request.UserLanguages[0].Trim().ToLower();
                        if (browser.StartsWith("ar") || browser.StartsWith("fr")) culture = browser;
                    }
                }
                else
                    culture = cookie.Value;
            }
            else
            {
                var query = Request.QueryString["ci"].Trim().ToLower();
                if (query.StartsWith("ar") || query.StartsWith("fr")) culture = query;
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(culture);

            var updatedCookie = new HttpCookie("CultureInfo");
            updatedCookie.Value = culture;
            Response.Cookies.Add(updatedCookie);
        }
        
        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            var err = "Unknown Error";

            try
            {
                Exception objErr = Server.GetLastError().GetBaseException();
                err = "Date Time: " + DateTime.Now + Environment.NewLine + "Error Caught in Application_Error event" +
                      Environment.NewLine +
                      "Error in: " + Request.Url + Environment.NewLine +
                      "Error Message:" + objErr.Message + Environment.NewLine +
                      "Error Inner Exception:" + objErr.InnerException + Environment.NewLine +
                      "Stack Trace:" + objErr.StackTrace + Environment.NewLine;
                Context.Trace.Write("BookMaster", err, objErr);
            }
            catch
            {
                //Ignore
            }

            try
            {
                FileLogging(err);
            }
            catch
            {
                //Ignore
            }
        }

        private static bool IsWebApiRequest()
        {
            var path = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;

            return path != null && (path.ToLower().StartsWith("api") || path.ToLower().StartsWith("~/api"));
        }

        private void FileLogging(string err)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("~/Logs/") + User.Identity.Name + "_" + DateTime.Now.Day + "_Log.txt", true);
            writer.WriteLine(err + Environment.NewLine + "\t--------------------------------------" + Environment.NewLine);
            writer.Close();
        }  
    }
}