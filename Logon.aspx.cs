using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4
{
    public partial class Logon : System.Web.UI.Page
    {
        //Connect to the database
        DataContext dbcon;
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\garret.doty\\Downloads\\KarateSchool(1).mdf;Integrated Security = True; Connect Timeout = 30"
        protected void Page_Load(object sender, EventArgs e)
        {
            dbcon = new DataContext(conn);
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string nUserName = Login1.UserName;
            string nPassword = Login1.Password;


            HttpContext.Current.Session["nUserName"] = nUserName;
            HttpContext.Current.Session["uPass"] = nPassword;



            // Search for the current User, validate UserName and Password
            _AspNetUsers_ myUser = (from x in dbcon._AspNetUsers_s
                                    where x.AspUserName == HttpContext.Current.Session["nUserName"].ToString()
                                    && x.AspUserPassword == HttpContext.Current.Session["uPass"].ToString()
                                    select x).First();

            if (myUser != null)
            {
                //Add UserID and User type to the Session
                HttpContext.Current.Session["userID"] = myUser.AspUserId;
                HttpContext.Current.Session["userType"] = myUser.AspUserType;

            }
            if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim() == "student")
            {

                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(), true);

                Response.Redirect("~/StudentInfo/studentpage.aspx");
            }
            else if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim() == "advisor")
            {

                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(), true);

                Response.Redirect("~/AdvisorInfo/advisorpage.aspx");
            }
            else
                Response.Redirect("Logon.aspx", true);


        }

    }
}
