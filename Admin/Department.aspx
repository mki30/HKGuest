<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Department.aspx.cs" Inherits="Admin_Department" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        var error = "Not logged in";
        $(function ()
        {
            ShowDepartments();
        });
        function ShowDepartments()
        {
            $.ajax({
                url: "/Data.aspx?Action=GetDepartments", datatype: "json", cache: false, success: function (data)
                {
                    if (data.Error == "")
                    {
                        var str = "<table class='table table-condensed table-bordered table-striped'><tr><th>#<th>Departments";
                        var list = JSON.parse(data.Data);
                        $(list).each(function ()
                        {
                            str += "<tr><td>" + this.ID + "<td><a href='#' onclick=\"AddDepartment(" + this.ID + ",'" + this.Name + "')\">" + this.Name + "</a>";
                        });

                        str += "</table>";
                        $("#tdDepartments").html(str);
                    }
                    else
                    {
                        alert(data.Error);
                        if (data.Error == error)
                            location.href = "/admin/Login.aspx";
                    }

                }
            });
        }
        function AddDepartment(ID, Name)
        {

            if (ID == undefined)
                ID = 0;

            var person = prompt("Please enter the name of the department", Name);

            if (person != null && person != "")
            {
                 $.ajax({
                    url: "/Data.aspx?Action=AddDepartment&Data1=" + ID + "&Data2=" + person, cache: false, success: function (data)
                    {
                        ShowDepartments();
                    }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 20%;">
        <table class="table-condensed" >
            <tr>
                <td>
                    <input type="button" value="Add Department" onclick="AddDepartment()" class="btn btn-success btn-sm"/>
                </td>
            </tr>
            <tr>
                <td id="tdDepartments"></td>
            </tr>
        </table>
    </div>
</asp:Content>

