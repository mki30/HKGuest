<%@ Page Title="Master" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Master.aspx.cs" Inherits="Admin_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        var error = "Not logged in";
        $(function ()
        {
            ShowEvents();
        });
        function ShowEvents()
        {
            $.ajax({
                url: "/Data.aspx?Action=GetEvents", datatype: "json", cache: false, success: function (data)
                {
                    console.log(data);
                    if (data.Error == "")
                    {
                        var str = "<table class='table table-condensed table-bordered table-striped'><tr><th>#<th>Events";
                        var list = JSON.parse(data.Data);
                        $(list).each(function ()
                        {
                            str += "<tr><td>" + this.ID + "<td><a href='#' onclick=\"AddEvent(" + this.ID + ",'" + this.Name + "')\">" + this.Name + "</a>";
                        });
                        str += "</table>";
                        $("#tdEvents").html(str);
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

        function AddEvent(ID, Name)
        {
            if (ID == undefined)
                ID = 0;
            var person = prompt("Please enter the name of the event", Name);
            if (person != null && person != "")
            {
                $.ajax({
                    url: "/Data.aspx?Action=AddEvent&Data1=" + ID + "&Data2=" + person, cache: false, success: function (data)
                    {
                        ShowEvents();
                    }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 30%;">
        <table class="table-condensed">
            <tr>
                <td>
                    <input type="button" value="Add Event" onclick="AddEvent()" class="btn btn-sm btn-success" />
                </td>
            </tr>
            <tr>
                <td id="tdEvents"></td>
            </tr>
        </table>
    </div>
</asp:Content>

