<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Location.aspx.cs" Inherits="Admin_Location" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        var error = "Not logged in";
        $(function ()
        {
            ShowLocations();
        });
        function ShowLocations(SortBy)
        {
            if (SortBy === undefined)
                SortBy = "Location"
            $.ajax({
                url: "/Data.aspx?Action=GetLocations&Data1=" + SortBy + "&Data2=" + $("#ddLocationFilter").val() + "", datatype: "json", cache: false, success: function (data)
                {
                    if (data.Error == "")
                    {
                        var str = "<table class='table table-condensed table-bordered table-striped'><tr><th>#<th><a href='#' onclick=\"ShowLocations('Location');\">Locations</a><th><a href='#' onclick=\"ShowLocations('Type');\">Type</a><th><a href='#' onclick=\"ShowLocations('Address');\">Address</a><th><a href='#' onclick=\"ShowLocations('City');\">City</a><th><a href='#' onclick=\"ShowLocations('State');\">State</a><th><a href='#' onclick=\"ShowLocations('PinCode');\">PinCode</a>";
                        var list = JSON.parse(data.Data);
                        var ctr = 0;
                        $(list).each(function ()
                        {
                            str += "<tr><td>" + (++ctr) + "<td><a href='#' onclick=\"ShowLocation(" + this.ID + ",'" + this.Name + "'," + this.LocationType + "," + this.ParentID + ",'" + this.Address + "','" + this.City + "','" + this.State + "','" + this.PinCode + "')\">" + this.Name + "</a><td>" + this.LocationTypeName
                            + "<td>" + this.Address + "<td>" + this.City + "<td>" + this.State + "<td>" + this.PinCode;
                        });

                        str += "</table>";
                        $("#tdLocations").html(str);
                    }
                    else
                    {
                        alert(data.Error);
                        if (data.Error == error)
                            location.href="/admin/Login.aspx";
                    }

                }
            });
        }
        function AddLocation(ID, Name)
        {
            if (ID == undefined)
                ID = 0;
            if (Type == undefined)
                Type = 0;

            var person = prompt("Please enter the name of the location", Name);

            if (person != null && person != "")
            {

                $.ajax({
                    url: "/Data.aspx?Action=AddLocation&Data1=" + ID + "&Data2=" + person + "&Data3=" + Type, cache: false, success: function (data)
                    {
                        ShowLocations();
                    }
                });
            }
        }
        function ShowLocation(ID, Name, Type, ParentID, Address, City, State, PinCode)
        {
            $("#lblID").text(ID);
            $("#ddParent").val(ParentID);
            $("#ddType").val(Type);
            $("#txtName").val(Name);
            $("#txtAddress").val(Address);
            $("#txtCity").val(City);
            $("#txtState").val(State);
        }
        function AddLocation()
        {
            $.ajax({
                url: "/Data.aspx?Action=AddLocation&Data1=" + $("#lblID").text() + "&Data2=" + $("#txtName").val() + "&Data3=" + $("#ddType").val() + "&Data4=" + $("#ddParent").val() + "&Data5=" + $("#txtAddress").val() + "&Data6=" + $("#txtCity").val() + "&Data7=" + $("#txtState").val() + "&Data8=" + $("#txtPincode").val(), cache: false, success: function (data)
                {
                    if (data.Error == "")
                    {
                        alert("Done");
                        ShowLocations();
                    }
                    else
                    {
                        alert("Error");
                    }
                }
            });
        }
        function ClearForm()
        {
            $("#lblID").text(0);
            $("#ddParent").val(0);
            $("#ddType").val(0);
            $("#txtName").val("");
            $("#txtAddress").val("");
            $("#txtCity").val("");
            $("#txtState").val("");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="vertical-align: top; width: 50%;" id="tdLocations"></td>
            <td style="vertical-align: top;width: 50%;">
                <table class="table-condensed" style='margin-left: 2%;background-color:#e5e5e5;'>
                    <tr>
                        <td>ID</td>
                        <td>
                            <asp:Label ID="lblID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Parent</td>
                        <td>
                            <asp:DropDownList ID="ddParent" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Type</td>
                        <td>
                            <asp:DropDownList ID="ddType" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Name</td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" placeholder="Name" />
                        </td>
                    </tr>
                    <tr>
                        <td>Address</td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" placeholder="State" />
                        </td>
                    </tr>
                    <tr>
                        <td>City</td>
                        <td>
                            <asp:TextBox ID="txtCity" runat="server" placeholder="City" />
                        </td>
                    </tr>
                    <tr>
                        <td>State</td>
                        <td>
                            <asp:TextBox ID="txtState" runat="server" placeholder="State" />
                        </td>
                    </tr>
                    <tr>
                        <td>PinCode</td>
                        <td>
                            <asp:TextBox ID="txtPincode" runat="server" placeholder="PinCode" MaxLength="6" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <input type="button" id="btnSave" value="Save" onclick="AddLocation();"  class="btn btn-success btn-sm"/>
                            <input type="button" id="btnNew" onclick="ClearForm();" value="New" class="btn btn-primary btn-sm"/>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top;width: 40%;"></td>
        </tr>
    </table>
</asp:Content>

