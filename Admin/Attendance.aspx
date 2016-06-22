<%@ Page Title="Attendance" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Attendance.aspx.cs" Inherits="Admin_Attendance" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        var error = "Not logged in";
        $(function ()
        {
            $('input[type="text"]').keydown(function (event)
            {
                if (event.keyCode == 13)
                {
                    event.preventDefault();
                    return false;
                }
            });
            ShowAttendance();
        });
        function SearchMobile(obj)
        {
            var txt = $("#txtMobile").val();
            $.ajax({
                url: "/Data.aspx?Action=GetMobile", datatype: "json", cache: false, success: function (data)
                {
                    if (data.Error == "")
                    {
                        var str = "<table class='border'>";
                        var list = JSON.parse(data.Data);
                        var ctr = 0;
                        $(list).each(function ()
                        {
                            str += "<tr><td><a href='#' onclick=\"return Select(" + this.Mobile + ");\">" + this.Mobile + "</a>";
                        });

                        str += "</table>";
                        $("#spanMobile").html(str);
                    }
                    else
                        alert(data.Error);
                }
            });
            return false
        }
        function Select(Mobile)
        {
            $("#txtMobile").val(Mobile);
            return false;
        }
        function showPersonList()
        {
            var txt = $("#txtMobile").val();
            $.ajax({
                url: "/Data.aspx?Action=GetPersonList&Data1=" + txt + "&Data2=" + $("#ddEvent").val() + "", datatype: "json", cache: false, success: function (data)
                {
                    if (data.Error == "")
                    {
                        if (data.Data != "Done")
                        {
                            var list = JSON.parse(data.Data);
                            var str = "<table class='border'>";
                            $(list).each(function ()
                            {
                                str += "<tr><td><a href='#' onclick=\" return AddAttendance(" + this.ID + ")\">" + this.Name + "</a>";
                            });

                            str += "</table>";
                            $("#spanMobile").html(str);
                        }
                        else
                        {
                            alert("Done");
                            ShowAttendance();
                        }
                    }
                    else
                    {
                        alert(data.Error);
                        if (data.Error == error)
                            location.href = "/admin/Login.aspx";
                    }
                }
            });
            return false;
        }
        function AddAttendance(PersonID)
        {
            $.ajax({
                url: "/Data.aspx?Action=AddAttendance&Data1=" + PersonID + "&Data2=" + $("#ddEvent").val() + "", datatype: "json", cache: false, success: function (data)
                {
                    if (data.Error == "")
                    {
                        alert("Done");
                        ShowAttendance();
                    }
                    else
                    {
                        alert(data.Error);
                        if (data.Error == error)
                            location.href = "/admin/Login.aspx";
                    }
                }
            });
            return false;
        }
        function ShowAttendance(SortBy)
        {
            if (SortBy === undefined)
                SortBy = "Name"
            $.ajax({
                url: "/Data.aspx?Action=ShowAttendance&Data1=" + $("#ddEvent").val() + "&Data2=" + SortBy + "", datatype: "json", cache: false, success: function (data)
                {
                    if (data.Error == "")
                    {
                        if (data.Data != "")
                        {
                            var list = data.Data;
                            var str = "<h3>" + $("#ddEvent option:selected").text() + "</h3>";
                            var ctr = 0;
                            str += "<table class='table table-bordered table-condensed table-striped' style='width:60%;'><th>#<th><a href='#' onclick=\" ShowAttendance('Name') \">Name</a><th><a href='#' onclick=\" ShowAttendance('Mobile') \">Mobile</a><th><a href='#' onclick=\" ShowAttendance('Date') \">Updated Date</a>";
                            $(list).each(function ()
                            {
                                str += "<tr><td>" + (++ctr) + "<td>" + this.Name + "<td>" + this.Contact + "<td>" + this.Date;
                            });

                            str += "</table>";
                            $("#tdPerson").html(str);
                        }
                        else
                            $("#tdPerson").html("No record found!");

                    }
                    else
                    {
                        alert(data.Error);
                        if (data.Error == error)
                            location.href = "/admin/Login.aspx";
                    }
                }
            });
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td style="width:200px;">
                <asp:DropDownList ID="ddEvent" runat="server" Style="width: 100%" onchange="ShowAttendance();"></asp:DropDownList>
                <br />
                <span id="spanMobile"></span>
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="2" id="tdPerson" style="vertical-align: top"></td>
        </tr>
    </table>
</asp:Content>

