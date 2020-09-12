using LMIS.Portal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using Microsoft.Ajax.Utilities;

namespace LMIS.Portal.FrontEnd
{
    public partial class ContactUs : System.Web.UI.Page
    {
        private static readonly IFeedbackManager Mgr = BllFactory.Singleton.FeedbackManager;
        private static readonly ITestimonialsManager MgrTestimonial = BllFactory.Singleton.TestimonialsManager;
        private static readonly IConfigCenterManager MgrConfigCenter = BllFactory.Singleton.ConfigCenterManager;
        private static readonly IListOfEmailsManager MgrListOfEmails = BllFactory.Singleton.ListOfEmails;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.LoggedUser != null)
            {
                lblName.Text = Utils.LoggedUser.UserName;
             
            }
       
        }
        [WebMethod]
        public static object GetTestimonials(  )
        {
            
          
            var mr = MgrTestimonial.GetTestimonials(1);
            return mr;

        }
        [WebMethod]
        public static object GetOffices()
        {


            var mr = MgrConfigCenter.GetValue(null,2,-1,6);
            return mr;

        }
        [WebMethod]
        public static object GetMap()
        {


            var mr = MgrConfigCenter.GetValue(null, 1, -1, 7);
            return mr;

        }
        [WebMethod]
        public static object GetEmails()
        {


            var mr = MgrListOfEmails.Get(0);
            return mr;

        }

        [WebMethod]
        public static object InsertFeedback(FeedbackVM item)
        {
            //if (Utils.LoggedUser == null)
            //{
            //    Response.Redirect("~/login");
            //}
            //else
            //{
            //    lblEmail.Text = Utils.LoggedUser.Email;
            //    lblName.Text = Utils.LoggedUser.Name;
            //}
            if (Utils.LoggedUser != null)
            {
                item.PortalUserID = (int)Utils.LoggedUser.PortalUserId;
                item.PostUserID = Utils.LoggedUser.UserId;

            }
            else
            {
                return Utils.ServiceResponse("F034", new ModelResponse(101), null);
            }
           
     
            return Mgr.Insert(item);
        }
        [WebMethod]
        public static object  InsertTestimonials(TestimonialsVM item)
        {
            if (Utils.LoggedUser != null)
            {
                item.PortalUserID = (int) Utils.LoggedUser.PortalUserId;


            }
            else
            {
                 
               ;
               return Utils.ServiceResponse("F034", new ModelResponse(101), null);
            }
        

            return MgrTestimonial.Insert(item);
        }



    }
}