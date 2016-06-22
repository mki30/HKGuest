<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Volunteer.aspx.cs" Inherits="Admin_Volunteer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var error = "Not logged in";
        $(function ()
        {
            $("#txtDOB").datepicker();
            $("#txtDOJ").datepicker();
            $("#txtSpouseDOB").datepicker();
            $("#txtAnniversary").datepicker();
            $("#txtChild_DoB").datepicker();
        });
    </script>
    <style>
        .req
        {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;">
        <tr>
            <td style="vertical-align: top; width: 10%">
                <%--<h3>Volunteers</h3>--%>
                <asp:Literal ID="ltVolunteer" runat="server"></asp:Literal>
            </td>
            <td style="vertical-align: top; float: left; width: 70%">
                <%--<h3 id="EditHead" runat="server">Add New Volunteer</h3>--%>
                <asp:Label ID="lblID" runat="server" Style="display: none" />
                    <table id="Table1" class="table-condensed" runat="server" style="width:40%;background-color:#e5e5e5;">
                    <tr>
                        <td>Name<span class="req">*</span></td>
                        <td>
                            <asp:TextBox MaxLength="45" ID="txtVolunteerName" runat="server" value="" required="required" placeholder="NAME" /></td>
                    </tr>
                    <tr>
                        <td>Task<span class="req">*</span></td>
                        <td>
                            <textarea id="txtTask" runat="server" />
                        </td>

                    </tr>
                    <tr>
                        <td>
                            
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Style="float: left"  class="btn btn-success btn-sm" />&nbsp;&nbsp;
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" class="btn btn-primary btn-sm"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


