<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        var error = "Not logged in";
        $(function ()
        {
            ShowEvents();
        });
        function ShowEvents(SortBy)
        {
            if (SortBy === undefined)
                SortBy = "Event";
            $.ajax({
                url: "/Data.aspx?Action=GetEventList&Data1=" + SortBy + "", datatype: "json", cache: false, success: function (data)
                {
                    if (data.Error == "")
                    {
                        //<th><a href='#' onclick=\"ShowEvents('Location')\">Location</a><th><a href='#' onclick=\"ShowEvents('Department')\">Department</a><th>
                        var str = "<table class='table'><tr><th>#<th><a href='#' onclick=\"ShowEvents('Event')\">Event</a>";
                        var list = JSON.parse(data.Data);
                        var ctr = 0;
                        $(list).each(function ()
                        {
                            str += "<tr style='cursor:pointer' onclick=\"ShowEvent(" + this.ID + ");\"><td>" + (++ctr);
                            str += "<td style='text-align:center;'>" + this.EventName + "<br/>" + this.fromDate + " to " + (this.fromDate.split(' ')[0] == this.toDate.split(' ')[0] ? this.toDate.replace(this.toDate.split(' ')[0], "").trim() : this.toDate) + "<br/>";
                            str += this.Location + "<br/>";
                            str += this.Department + "<br/>";

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
        function ShowEvent(ID)
        {
            location.href = "/MarkAttendance.aspx?ID=" + ID;
        }

    </script>
    <style>
        body { background-color: #dedbdb!important; margin: 0px; font-family: 'Roboto', sans-serif; color: rgb(51,51,51); }
        a { text-decoration: none; color: black; cursor: pointer; }
        input, select, textarea { margin: 3px; padding: 3px; }
        .table tr td { border: 1px solid grey; }
        .table { border: 0px; width: 100%; border-collapse: collapse; width: 90%; margin-left: 10px; }
            .table th { background-color: #f9f9f9; font-weight: bold; }
            .table tr td, .table tr th { padding: 2px; text-align: left; white-space: nowrap; font-size: .8em; }
            .table tr:nth-of-type(odd) { background-color: #f9f9f9; }
            .table tr:hover { background-color: #eee; }

        @media screen and (max-width: 480px) {
            input, select { width: 95%; margin: 0px; margin-left: 3px; }
            select { width: 97%; }
            .lbl { margin: 3px 0px 0px 3px; padding: 0px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h1 style="margin: 0px; padding-top: 20px; font-size: 1.2em; text-align: center">Events List</h1>
    <div id="tdEvents"></div>

</asp:Content>

