using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookMasterUI
{
    public partial class CustomError1 : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {



        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Exception objErr = Server.GetLastError().GetBaseException();
                    string ErrorMesage = "Date Time: " + DateTime.Now.ToString() + Environment.NewLine + "Error Caught in Application_Error event" + Environment.NewLine +
                        "Error in: " + Request.Url.ToString() + Environment.NewLine +
                        "Error Message:" + objErr.Message.ToString() + Environment.NewLine +
                        "Error Inner Exception:" + objErr.InnerException + Environment.NewLine +
                        "Stack Trace:" + objErr.StackTrace.ToString() + Environment.NewLine;
                    this.TextBox_Message.Value = ErrorMesage;
                    Session["errormessag"] = ErrorMesage;
                    // SendMail(ErrorMesage);


                }
                catch { }
                finally
                {
                    Server.ClearError();
                }
                this.HyperLink_back.NavigateUrl = "~/Home.aspx";//Request.QueryString["BU"];
           

            }

        }
    }
}