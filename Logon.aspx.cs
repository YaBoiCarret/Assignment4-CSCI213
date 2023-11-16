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
        UserDataDataContext dbcon;
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\garret.doty\\Downloads\\KarateSchool(1).mdf;Integrated Security = True; Connect Timeout = 30";
        protected void Page_Load(object sender, EventArgs e)
        {
            dbcon = new UserDataDataContext(conn);
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string UserName = Login1.UserName;
            string UserPassword = Login1.Password;


            HttpContext.Current.Session["UserName"] = UserName;
            HttpContext.Current.Session["UserPassword"] = UserPassword;



            // Search for the current User, validate UserName and Password
            NetUser myUser = (from x in dbcon.NetUsers
                                    where x.UserName == HttpContext.Current.Session["UserName"].ToString()
                                    && x.UserPassword == HttpContext.Current.Session["UserPassword"].ToString()
                                    select x).First();

            if (myUser != null)
            {
                //Add UserID and User type to the Session
                HttpContext.Current.Session["UserID"] = myUser.UserID;
                HttpContext.Current.Session["UserType"] = myUser.UserType;

            }
            if (myUser != null && HttpContext.Current.Session["UserType"].ToString().Trim() == "Member")
            {

                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["UserName"].ToString(), true);

                Response.Redirect("~/Student/studentpage.aspx");
            }
            else if (myUser != null && HttpContext.Current.Session["UserType"].ToString().Trim() == "Instructor")
            {

                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["UserName"].ToString(), true);

                Response.Redirect("~/InstructorFolder/instructorpage.aspx");
            }
            else if (myUser != null && HttpContext.Current.Session["UserType"].ToString().Trim() == "Administrator")
            {

                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["UserName"].ToString(), true);

                Response.Redirect("~/AdminFolder/administratorpage.aspx");
            }
            else
                Response.Redirect("Logon.aspx", true);


        }

    }
}
