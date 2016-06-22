<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Edit_Default"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HKGuest</title>
    <link href="/BootStrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/css/ui-lightness/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link id="Link1" rel="shortcut icon" type="image/x-icon" href="~/ico/business.png" runat="server" />
</head>
<script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
<script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>

<script type="text/javascript">
    $(window).resize(Resize);
    function Resize()
    {
        var h = $(window).height() - 10;
        $("#tabsProfile").height(h);
        $("#tabsProfile iframe").height(h - 60);
    }
    TabText = new Array("EventMaster", "Location", "Department", "Events", "Devotees", "Attendance", "Volunteer");
    TabLinks = new Array("Master.aspx", "Location.aspx", "Department.aspx", "Event.aspx", "Devotee.aspx", "Attendance.aspx", "Volunteer.aspx");
    $(document).ready(function ()
    {
        var str1 = "<ul>", str2 = "";
        for (var i = 0; i < TabText.length; i++)
        {
            str1 += "<li><a href='#tab-" + i + "' data-index='" + i + "'>" + TabText[i] + "</a></li>";
            str2 += "<div id='tab-" + i + "'></div>";
        }
        str1 += "</ul>";

        $("#tabsProfile").html(str1 + str2);
        $("#tabsProfile").tabs();
        $('#tabsProfile .ui-tabs-anchor').bind('click', function ()
        {
            TabSelect($(this).data("index"));
        });
        TabSelect(0);

        Resize();
    });
    function TabSelect(Index)
    {
        if ($("#tab-" + Index).html() == "")
        {
            $("#tab-" + Index).html("<iframe frameborder='0' style='width:100%;' src='" + TabLinks[Index] + "'></iframe>");
        }
        Resize();
    }
</script>
<body>
    <form id="form1" runat="server">
        <div id='tabsProfile'>
        </div>
    </form>
</body>
</html>


