using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4.AdminFolder
{
    public partial class administratorpage : System.Web.UI.Page
    {
        
        UserDataDataContext dbcon;
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\garret.doty\\Downloads\\KarateSchool(1).mdf;Integrated Security=True;Connect Timeout=30";

        protected void Page_Load(object sender, EventArgs e)
        {
         
                dbcon = new UserDataDataContext(conn);
                try
                {
                    BindMembersGrid();
                    BindInstructorsGrid();
                    BindMembersDropDown();
                    BindSectionsDropDown();
                }
                catch (Exception ex)
                {
   
                    Response.Write("An error occurred: " + ex.Message);
                }
            
        }

        private void BindMembersGrid()
        {
            if (dbcon != null && dbcon.Members != null)
            {
                var members = dbcon.Members.Select(m => new
                {
                    Member_UserID = m.Member_UserID,
                    FullName = m.MemberFirstName + " " + m.MemberLastName,
                    MemberPhoneNumber = m.MemberPhoneNumber,
                    MemberEmail = m.MemberEmail,
                    MemberDateJoined = m.MemberDateJoined
                }).ToList();

                MembersGridView.DataSource = members;
                MembersGridView.DataBind();
            }
            else
            {
                Response.Write("An error occurred: Unable to bind members grid.");
            }
        }


        private void BindInstructorsGrid()
        {
            if (dbcon != null && dbcon.Instructors != null)
            {
                var instructors = dbcon.Instructors.Select(i => new
                {
                    InstructorID = i.InstructorID,
                    InstructorFirstName = i.InstructorFirstName,
                    InstructorLastName = i.InstructorLastName,
                    InstructorPhoneNumber = i.InstructorPhoneNumber
                }).ToList();

                InstructorsGridView.DataSource = instructors;
                InstructorsGridView.DataBind();
            }
            else
            {
                Response.Write("An error occurred: Unable to bind instructors grid.");
            }
        }

        protected void AddMemberButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get member details from form
                string firstName = txtMemberFirstName.Text;
                string lastName = txtMemberLastName.Text;
                string phoneNumber = txtMemberPhoneNumber.Text;
                string email = txtMemberEmail.Text;
                DateTime dateJoined = DateTime.Parse(txtMemberDateJoined.Text);

                // Get NetUser details from form
                string userName = txtMemberUserName.Text;
                string password = txtMemberPassword.Text;

                // Create NetUser
                var newUser = new NetUser
                {
                    UserName = userName,
                    UserPassword = password,
                    UserType = "Member"
                };
                dbcon.NetUsers.InsertOnSubmit(newUser);
                dbcon.SubmitChanges();

                // Create Member with NetUser's ID
                var newMember = new Member
                {
                    Member_UserID = newUser.UserID, // Linking UserID
                    MemberFirstName = firstName,
                    MemberLastName = lastName,
                    MemberPhoneNumber = phoneNumber,
                    MemberEmail = email,
                    MemberDateJoined = dateJoined
                };
                dbcon.Members.InsertOnSubmit(newMember);
                dbcon.SubmitChanges();

                BindMembersGrid();
            }
            catch (Exception ex)
            {
                Response.Write("An error occurred: " + ex.Message);
            }
        }

        protected void AddInstructorButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get instructor details from form
                string firstName = txtInstructorFirstName.Text;
                string lastName = txtInstructorLastName.Text;
                string phoneNumber = txtInstructorPhoneNumber.Text;

                // Get NetUser details from form
                string userName = txtInstructorUserName.Text;
                string password = txtInstructorPassword.Text;

                // Create NetUser
                var newUser = new NetUser
                {
                    UserName = userName,
                    UserPassword = password,
                    UserType = "Instructor"
                };
                dbcon.NetUsers.InsertOnSubmit(newUser);
                dbcon.SubmitChanges();

                // Create Instructor with NetUser's ID
                var newInstructor = new Instructor
                {
                    InstructorID = newUser.UserID, // Linking UserID
                    InstructorFirstName = firstName,
                    InstructorLastName = lastName,
                    InstructorPhoneNumber = phoneNumber
                };
                dbcon.Instructors.InsertOnSubmit(newInstructor);
                dbcon.SubmitChanges();

                BindInstructorsGrid();
            }
            catch (Exception ex)
            {
                Response.Write("An error occurred: " + ex.Message);
            }
        }




        protected void MembersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteMember")
            {
                try
                {
                    int memberId = Convert.ToInt32(e.CommandArgument);
                    var memberToDelete = dbcon.Members.FirstOrDefault(m => m.Member_UserID == memberId);

                    if (memberToDelete != null)
                    {
                        // Delete Member
                        dbcon.Members.DeleteOnSubmit(memberToDelete);

                        // Delete corresponding NetUser
                        var netUserToDelete = dbcon.NetUsers.FirstOrDefault(n => n.UserID == memberToDelete.Member_UserID);
                        if (netUserToDelete != null)
                        {
                            dbcon.NetUsers.DeleteOnSubmit(netUserToDelete);
                        }

                        dbcon.SubmitChanges();
                        BindMembersGrid();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("An error occurred: " + ex.Message);
                }
            }
        }
        protected void InstructorsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteInstructor")
            {
                try
                {
                    int instructorId = Convert.ToInt32(e.CommandArgument);
                    var instructorToDelete = dbcon.Instructors.FirstOrDefault(i => i.InstructorID == instructorId);

                    if (instructorToDelete != null)
                    {
                        // Delete Instructor
                        dbcon.Instructors.DeleteOnSubmit(instructorToDelete);

                        // Delete corresponding NetUser
                        var netUserToDelete = dbcon.NetUsers.FirstOrDefault(n => n.UserID == instructorToDelete.InstructorID);
                        if (netUserToDelete != null)
                        {
                            dbcon.NetUsers.DeleteOnSubmit(netUserToDelete);
                        }

                        dbcon.SubmitChanges();
                        BindInstructorsGrid();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("An error occurred: " + ex.Message);
                }
            }
        }




        protected void AssignButton_Click(object sender, EventArgs e)
        {
            try
            {
                int memberId = Convert.ToInt32(ddlMembers.SelectedValue);
                int sectionId = Convert.ToInt32(ddlSections.SelectedValue);

                var section = dbcon.Sections.FirstOrDefault(s => s.SectionID == sectionId);
                if (section != null)
                {
                    section.Member_ID = memberId;
                    dbcon.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                Response.Write("An error occurred: " + ex.Message);
            }
        }


        private void BindMembersDropDown()
        {
            ddlMembers.DataSource = dbcon.Members.Select(m => new
            {
                MemberID = m.Member_UserID,  
                FullName = m.MemberFirstName + " " + m.MemberLastName
            }).ToList();
            ddlMembers.DataTextField = "FullName";
            ddlMembers.DataValueField = "MemberID";
            ddlMembers.DataBind();
        }

        private void BindSectionsDropDown()
        {
            ddlSections.DataSource = dbcon.Sections.Select(s => new
            {
                SectionID = s.SectionID,
                SectionName = s.SectionName
            }).ToList();
            ddlSections.DataTextField = "SectionName";
            ddlSections.DataValueField = "SectionID";
            ddlSections.DataBind();
        }





    }
}