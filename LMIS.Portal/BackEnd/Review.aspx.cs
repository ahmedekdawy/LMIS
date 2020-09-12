using LMIS.Infrastructure.Data.DTOs;
using LMIS.Portal.Helpers;

using System;
using System.Web.Http;
using System.Web.Services;

namespace LMIS.Portal.BackEnd
{
    public partial class Review : System.Web.UI.Page
    {
        public const string PageCode = "F033";

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object LoadRecord(string query, long key, string langCode)
        {
            object res;
            ModelResponse mr;
            var user = Utils.LoggedUser;
            var lang = (int) Utils.GetLanguage(langCode);

            switch (query.ToLower().Trim())
            {
                case "feedback":

                    mr = BllFactory.Singleton.FeedbackManager.Review(user, key, lang);

                    res = new
                    {
                        RecTypeName = Utils.GetResource(false, "F034_feeddBack"),
                        Type = Utils.GetResource(false, "F034_TypeFeedBack"),
                        Title = Utils.GetResource(false, "F034_Titles"),
                        Desc = Utils.GetResource(false, "F034_Message"),
                    };

                    return Utils.ServiceResponse(PageCode, mr, res);

                case "testimonial":

                    mr = BllFactory.Singleton.TestimonialsManager.Review(user, key, lang);

                    res = new
                    {
                        RecTypeName = Utils.GetResource(false, "F034_Testimonial"),
                        Rating = Utils.GetResource(false, "F034_SiteRating"),
                        Comment = Utils.GetResource(false, "F034_Message")
                    };

                    return Utils.ServiceResponse(PageCode, mr, res);

                case "partner":

                    mr = BllFactory.Singleton.PartnersManager.Review(user, key, lang);

                    res = new
                    {
                        RecTypeName = Utils.GetResource(false, "F042_Title"),
                        CEOFirstName = Utils.GetResource(false, "F042_CEOFirstName"),
                        CEOLastName = Utils.GetResource(false, "F042_CEOLastName"),
                        CEOEmail = Utils.GetResource(false, "F042_CEOEmail"),
                        Year = Utils.GetResource(false, "F042_YearFounded"),
                        Business = Utils.GetResource(false, "F042_GeneralDescriptionCoreBusiness"),
                        AOC = Utils.GetResource(false, "F042_PossibleAreaCooperation")
                    };

                    return Utils.ServiceResponse(PageCode, mr, res);
            }

            return null;
        }

        [WebMethod]
       
        public static object Approve(string query, long reqKey, long id, bool approved, string reason)
        {
            ModelResponse mr = null;

            switch (query.ToLower().Trim())
            {
                case "feedback":
                    mr = BllFactory.Singleton.FeedbackManager.Approve(Utils.LoggedUser, reqKey, id, approved, reason);
                    break;
                case "testimonial":
                    mr = BllFactory.Singleton.TestimonialsManager.Approve(Utils.LoggedUser, reqKey, id, approved, reason);
                    break;
                case "partner":
                    mr = BllFactory.Singleton.PartnersManager.Approve(Utils.LoggedUser, reqKey, id, approved, reason);
                    break;
            }

            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}