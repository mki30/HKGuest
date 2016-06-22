<%@ Page Title="Event" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Event.aspx.cs" Inherits="Admin_Event" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script src="../js/event.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table style="width: 100%">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <asp:DropDownList ID="ddLocationFilter" onchange="ShowEvents();" runat="server"></asp:DropDownList><br />
                <span id="tdEvents"></span>
            </td>
            <td style="vertical-align: top; width: 50%;">
                <table style='margin-left: 2%; width: 100%; background-color: #e5e5e5;' class="table-padded">
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                    <tr style="display:none;">
                        <td>ID</td>
                        <td>
                            <asp:Label ID="lblID" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Event</td>
                        <td>
                            <asp:DropDownList ID="ddEvent" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Location</td>
                        <td>
                            <asp:DropDownList ID="ddLocation" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Department</td>
                        <td>
                            <asp:DropDownList ID="ddDepartment" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>From Date</td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" placeholder="From Date" />
                            <asp:DropDownList ID="ddFromtime" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>To Date</td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date" />
                            <asp:DropDownList ID="ddTotime" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Resp. Person</td>
                        <td>
                            <asp:DropDownList ID="ddResponsiblePerson" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Description</td>
                        <td>
                            <textarea id="txtDescription" runat="server" style="width: 80%;" />
                        </td>
                    </tr>
                    <tr>
                        <td>EntryType</td>
                        <td>
                            <asp:DropDownList ID="ddEntryType" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="Open for all">Open for all</asp:ListItem>
                                <asp:ListItem Value="Prior membership">Prior membership</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Preacher</td>
                        <td>
                            <asp:TextBox ID="txtPreacher1" runat="server" placeholder="Preacher1" />
                            <asp:TextBox ID="txtPreacher2" runat="server" placeholder="Preacher2" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td>Preacher1</td>
                        <td>
                            
                        </td>
                    </tr>--%>
                    <tr>
                        <td>Organizer</td>
                        <td>
                            <asp:TextBox ID="txtOrganizer1" runat="server" placeholder="Organizer1" />
                            <asp:TextBox ID="txtOrganizer2" runat="server" placeholder="Organizer2" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td>Organizer2</td>
                        <td>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>Volunteer</td>
                        <td>
                            <asp:DropDownList ID="ddVolunteer1" runat="server" placeholder="Volunteer1"></asp:DropDownList>
                            <asp:DropDownList ID="ddVolunteer2" runat="server" placeholder="Volunteer2"></asp:DropDownList>
                        </td>
                    </tr>
                    <%--<tr>
                        <td>Volunteer2</td>
                        <td>
                            
                        </td>
                    </tr>--%>
                    <tr>
                        <td>Topic</td>
                        <td>
                            <asp:TextBox ID="txtTopic" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Collection</td>
                        <td>
                            <asp:TextBox ID="txtCollection" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Comments</td>
                        <td>
                            <textarea id="txtComments" runat="server" style="width: 80%;" />
                        </td>
                    </tr>
                    <tr>
                        <td>Budgeted Cost</td>
                        <td>
                            <asp:TextBox ID="txtBudgetedCost" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Actual Expense</td>
                        <td>
                            <asp:TextBox ID="txtActualExpense" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="padding-top: 5px; padding-bottom: 5px;">
                            <input type="button" id="btnSave" value="Save" onclick="AddEvent();" class="btn btn-success btn-sm" />
                            <input type="button" id="btnNew" onclick="ClearForm();" value="New" class="btn btn-primary btn-sm" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

