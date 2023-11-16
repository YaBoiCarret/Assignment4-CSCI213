<%@ Page Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="administratorpage.aspx.cs" Inherits="Assignment4.AdminFolder.administratorpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Members</h2>
    <asp:GridView ID="MembersGridView" runat="server" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="FullName" HeaderText="Name" />
        <asp:BoundField DataField="MemberPhoneNumber" HeaderText="Phone Number" />
        <asp:BoundField DataField="MemberEmail" HeaderText="Email" />
        <asp:BoundField DataField="MemberDateJoined" HeaderText="Date Joined" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="DeleteMemberButton" runat="server" CommandName="DeleteMember" CommandArgument='<%# Eval("Member_UserID") %>' Text="Delete" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
    <h2>Instructors</h2>
   <asp:GridView ID="InstructorsGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="InstructorsGridView_RowCommand">
    <Columns>
        <asp:BoundField DataField="InstructorFirstName" HeaderText="First Name" />
        <asp:BoundField DataField="InstructorLastName" HeaderText="Last Name" />
        <asp:BoundField DataField="InstructorPhoneNumber" HeaderText="Phone Number" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="DeleteInstructorButton" runat="server" CommandName="DeleteInstructor" CommandArgument='<%# Eval("InstructorID") %>' Text="Delete" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
    <!-- Form to Add Member -->
<asp:Panel ID="AddMemberPanel" runat="server">
    <h3>Add Member</h3>
    First Name: <asp:TextBox ID="txtMemberFirstName" runat="server"></asp:TextBox><br />
    Last Name: <asp:TextBox ID="txtMemberLastName" runat="server"></asp:TextBox><br />
    Phone Number: <asp:TextBox ID="txtMemberPhoneNumber" runat="server"></asp:TextBox><br />
    Email: <asp:TextBox ID="txtMemberEmail" runat="server"></asp:TextBox><br />
    Date Joined: <asp:TextBox ID="txtMemberDateJoined" runat="server"></asp:TextBox><br />
    <!-- Add any other member-specific fields here -->
        <asp:TextBox ID="txtMemberUserName" runat="server" Text="Set UserName"></asp:TextBox><br />
<asp:TextBox ID="txtMemberPassword" Text="Set Password" runat="server"></asp:TextBox><br />
    <asp:Button ID="AddMemberButton" runat="server" Text="Add Member" OnClick="AddMemberButton_Click" />
</asp:Panel>

<!-- Form to Add Instructor -->
<asp:Panel ID="AddInstructorPanel" runat="server">
    <h3>Add Instructor</h3>
    First Name: <asp:TextBox ID="txtInstructorFirstName" runat="server"></asp:TextBox><br />
    Last Name: <asp:TextBox ID="txtInstructorLastName" runat="server"></asp:TextBox><br />
    Phone Number: <asp:TextBox ID="txtInstructorPhoneNumber" runat="server"></asp:TextBox><br />
   
        <asp:TextBox ID="txtInstructorUserName" runat="server" Text="Set UserName"></asp:TextBox><br />
<asp:TextBox ID="txtInstructorPassword" Text="Set Password" runat="server"></asp:TextBox><br />
    <asp:Button ID="AddInstructorButton" runat="server" Text="Add Instructor" OnClick="AddInstructorButton_Click" />
</asp:Panel>
   
<<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="MembersGridView_RowCommand">
    <Columns>
        
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="DeleteMemberButton" runat="server" CommandName="DeleteMember" CommandArgument='<%# Eval("MemberID") %>' Text="Delete" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<!-- Form to Assign Member to a Section -->
<asp:Panel ID="AssignMemberToSectionPanel" runat="server">
    <h3>Assign Member to Section</h3>
    Member: <asp:DropDownList ID="ddlMembers" runat="server"></asp:DropDownList><br />
    Section: <asp:DropDownList ID="ddlSections" runat="server"></asp:DropDownList><br />
    <asp:Button ID="AssignButton" runat="server" Text="Assign" OnClick="AssignButton_Click" />
</asp:Panel>

</asp:Content>
