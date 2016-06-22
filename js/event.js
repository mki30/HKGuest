var error = "Not logged in";
$(function ()
{
    ShowEvents();
    $("#txtFromDate").datepicker();
    $("#txtToDate").datepicker();

});
function ShowEvents(SortBy)
{
    if (SortBy === undefined)
        SortBy = "Event";
    $.ajax({
        url: "/Data.aspx?Action=GetEventList&Data1=" + SortBy + "&Data2=" + $("#ddLocationFilter").val() + "", datatype: "json", cache: false, success: function (data)
        {
            if (data.Error == "")
            {
                var str = "<table class='table table-condensed table-bordered table-striped'><tr><th>#<th><a href='#' onclick=\"ShowEvents('Event')\">Event</a><th><a href='#' onclick=\"ShowEvents('Location')\">Location</a><th><a href='#' onclick=\"ShowEvents('Department')\">Department</a><th>";
                var list = JSON.parse(data.Data);
                var ctr = 0;
                $(list).each(function ()
                {
                    str += "<tr><td>" + (++ctr);
                    str += "<td><a href='#' onclick=\"ShowEvent(" + this.ID + ")\">" + this.EventName + "</a>";
                    str += "<td>" + this.Location;
                    str += "<td>" + this.Department;
                    str += "<td>" + this.fromDate + " to " + this.toDate;
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
    $.ajax({
        url: "/Data.aspx?Action=GetEventDetail&Data1=" + ID, datatype: "json", cache: false, success: function (data)
        {
            if (data.Error == "")
            {
                var obj = JSON.parse(data.Data);
                console.log(obj);
                $("#lblID").val(obj.ID);
                $("#ddEvent").val(obj.EventID);
                $("#ddLocation").val(obj.LocationID);
                $("#ddDepartment").val(obj.DepartmentID);
                $("#ddResponsiblePerson").val(obj.ResponsiblePersonID == 0 ? "" : obj.ResponsiblePersonID);
                $("#txtFromDate").val(obj.fromDate.split(' ')[0]);
                $("#txtToDate").val(obj.toDate.split(' ')[0]);
                $("#ddFromtime").val(obj.fromDate.replace($("#txtFromDate").val(), "").trim());
                $("#ddTotime").val(obj.toDate.replace($("#txtToDate").val(), "").trim());
                $("#txtDescription").val(obj.Description);
                $("#ddEntryType").val(obj.EntryType);
                $("#txtPreacher1").val(obj.Preacher1);
                $("#txtPreacher2").val(obj.Preacher2);
                $("#txtOrganizer1").val(obj.Organizer1);
                $("#txtOrganizer2").val(obj.Organizer2);
                $("#ddVolunteer1").val(obj.Volunteer1 == 0 ? "" : obj.Volunteer1);
                $("#ddVolunteer2").val(obj.Volunteer2 == 0 ? "" : obj.Volunteer2);
                $("#txtTopic").val(obj.Topic);
                $("#txtCollection").val(obj.Collection);
                $("#txtComments").val(obj.Comments);
                $("#txtBudgetedCost").val(obj.BudgetedCost == 0 ? "" : obj.BudgetedCost);
                $("#txtActualExpense").val(obj.ActualExpense == 0 ? "" : obj.ActualExpense);
            }
        }
    });
}

function AddEvent()
{
    var dateFrom = Date.parse($("#txtFromDate").val());
    var dateto = Date.parse($("#txtToDate").val());
    if (dateto < dateFrom)
        alert("To Date cannot be less than From Date");
    else if (dateto >= dateFrom)
    {
        $.post("/data.aspx?Action=AddEventDetail", { Data: JSON.stringify($("#form1").serializeObject()) }, function (data)
        {
            if (data.Error == "")
            {
                $("#lblID").val(data.Data);
                alert("Done");
                ShowEvents();
            }
            else
            {
                alert("Error");
            }
        });
    }

}
function ClearForm()
{
    //$("#lblID").val(0);
    //$("#ddEvent").val("0");
    //$("#ddLocation").val("0");
    //$("#ddDepartment").val("0");
    //$("#ddResponsiblePerson").val("0");
    //$("#txtFromDate").val("");
    //$("#txtToDate").val("");
    var obj = JSON.stringify($("#form1").serializeObject());
    var FormData = JSON.parse(obj);  //Load form values from local storage
    for (var a in FormData)
    {
        if (a != "ddLocationFilter" || a != "tdEvents")
            $("#" + a).val("");
    }
}

$.fn.serializeObject = function ()
{
    var o = {};
    $("input[type=text],select,textarea,span", this).each(function ()
    {
        var id = $(this).attr("id");
        var val = $.trim($(this).val())
        o[id] = val;
    });
    return o;
};
