<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MarkAttendance.aspx.cs" Inherits="MarkAttendance" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<style>
        body { padding-top: 40px; padding-bottom: 40px; background-color: #eee; }
    </style>--%>

    <style>
        body { background-color: #dedbdb!important; margin: 0px; font-family: 'Roboto', sans-serif; color: rgb(51,51,51); }
        .txt { position: relative; /*border: 1px solid black;*/ }
        .lbl { /*border: 1px solid black;*/ float: left; width: 80px; font-size: .8em; margin: 3px; padding: 3px; }
        .borderTB { border-top: 1px solid #c0c0c0; border-bottom: 1px solid #c0c0c0; }
        a { text-decoration: none; color: black; cursor: pointer; }
        input, select, textarea { margin: 3px; padding: 3px; }
        .table tr td { border: 1px solid grey; }
        .table { border: 0px; width: 100%; border-collapse: collapse; width: 90%; margin-left: 10px; }
            .table th { background-color: #f9f9f9; font-weight: bold; }
            .table tr td, .table tr th { text-align: left; font-size: .8em;min-height:25px; padding-left: 2px;padding-right: 2px;padding-top:5px;padding-bottom:5px; }
            .table tr:nth-of-type(odd) { background-color: #f9f9f9; }
            .table tr:hover { background-color: #eee; }
        .highlight { background-color: rgba(166, 221, 166, 0.54)!important; }
        .form-control { display: block; width: 90%; height: 34px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; border-radius: 4px; -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075); box-shadow: inset 0 1px 1px rgba(0,0,0,.075); -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s; -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s; transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s; margin: 5px; }
        .btn { display: inline-block; padding: 6px 12px; margin-bottom: 0; font-size: 14px; font-weight: 400; line-height: 1.42857143; text-align: center; white-space: nowrap; vertical-align: middle; -ms-touch-action: manipulation; touch-action: manipulation; cursor: pointer; -webkit-user-select: none; -moz-user-select: none; -ms-user-select: none; user-select: none; background-image: none; border: 1px solid transparent; border-radius: 4px; }
        .success { color: #fff; background-color: #5cb85c; border-color: #4cae4c; }
        .primary { color: #fff; background-color: #286090; border-color: #204d74; }

        @media screen and (max-width: 480px) {
            input, select { width: 95%; margin: 0px; margin-left: 3px; }
            select { width: 97%; }
            .lbl { margin: 3px 0px 0px 3px; padding: 0px; }
        }
    </style>
    <script>
        var Register = "";
        var error = "Not logged in";
        $(document).ready(function ()
        {
            ShowAttendance();
            $("#txtMobile").keyup(function ()
            {
                if (isNumberKey && $("#txtMobile").val().length == 10)
                    Save();
            });
        });
        //function RegisterPerson()
        //{
        //    Register = "1";
        //    Save();
        //}
        function CheckIn()
        {
            debugger;
            Register = "0";
            Save();
        }
        function Save()
        {
            debugger;
            var Mobile = $("#txtMobile").val().length == 10;
            if (Mobile)
            {
                $.ajax({
                    url: "/Data.aspx?Action=AddAttendanceByMobile&Data1=" + $("#txtMobile").val() + "&Data2=" + $("#lblEventID").text() + "&Data3=" + Register + "", cache: false, async: false, success: function (data)
                    {
                        if (data.Error == "")
                        {
                            alert("Done");
                            ShowAttendance();
                        }
                        else
                        {   
                            alert(data.Error);
                            $("#trSave").hide();
                            $(".trAddPerson").show();
                            if (data.Error == error)
                                location.href = "/admin/Login.aspx";
                        }
                    }
                });
            }
            else
            {
                alert("Please enter 10 digits Mobile Number");
                $("#txtMobile").focus();
            }
        }
        function Add(confirm)
        {
            var validate = validateEmailAndMobile();
            if (validate)
            {
                $.ajax({
                    url: "/Data.aspx?Action=AddPersonAndAttendance&Data1=" + $("#txtMobile").val() + "&Data2=" + $("#lblEventID").text() + "&Data3=" + $("#txtName").val() + "&Data4=" + $("#txtEmail").val() + "&Data5=" + (confirm === undefined ? "0" : "1") + "", cache: false, async: false, success: function (data)
                    {
                        if (data.Error == "")
                        {
                            alert("Done");
                            ShowAttendance();
                        }
                        else
                        {
                            var result = window.confirm(data.Error);
                            if (result);
                            Add(1);
                        }
                    }
                });
            }
        }

        function validateEmailAndMobile()
        {
            var Email = IsEmail($("#txtEmail").val());
            var Mobile = $("#txtMobile").val().length == 10;
            var Name = $("#txtName").val()!= ""?true:false;
            if (!Mobile)
            {
                alert("Please enter 10 digits Mobile Number.");
                $("#txtMobile").focus();
                return false;
            }
            else if (!Name)
            {
                alert("Please enter Name");
                $("#txtName").focus();
                return false;
            }
            else if (!Email)
            {
                alert("Please enter correct Email Address.");
                $("#txtEmail").focus();
                return false;
           }
            else
                return true;
        }
        function IsEmail(email)
        {
            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(email))
            {
                return false;
            } else
            {
                return true;
            }
        }
        function ShowAttendance(SortBy)
        {
            
            if (SortBy === undefined)
                SortBy = "Name"
            $.ajax({
                url: "/Data.aspx?Action=ShowAttendance&Data1=" + $("#lblEventID").text() + "&Data2=" + SortBy + "", datatype: "json", cache: false, success: function (data)
                {
                    console.log(data);
                    if (data.Error == "")
                    {
                        if (data.Data != "")
                        {
                            var list = data.Data;
                            var str = "";
                            var ctr = 0;
                            str += "<table class='table'><th>#<th><a href='#' onclick=\" ShowAttendance('Name') \">Name</a><th><a href='#' onclick=\" ShowAttendance('Mobile') \">Mobile</a><th><a href='#' onclick=\" ShowAttendance('Date') \">Updated Date</a>";
                            $(list).each(function ()
                            {
                                str += "<tr " + (this.Date == "" ? "" : "class='highlight'") + " id='tr" + (ctr + 1) + "' onclick='ShowPopUp(" + this.ID + "," + ctr + ");'><td>" + (++ctr) + "<td>" + this.Name + "<td>" + this.Contact + "<td>" + this.Date;
                            });

                            str += "</table>";
                            $("#tdPerson").html(str);

                        }
                        //else
                        //    alert("Done");
                    }
                    else
                    {
                        alert(data.Error);

                    }
                }
            });
            return false;
        }
        function ShowPopUp(PersonID, id)
        {
            var confrm = window.confirm("Do you want mark the attendance ?");
            if (confrm)
            {
                $.ajax({
                    url: "/Data.aspx?Action=AddAttendance&Data1=" + PersonID + "&Data2=" + $("#lblEventID").text() + "", cache: false, async: false, success: function (data)
                    {
                        if (data.Error == "")
                        {
                            alert("Done");
                            ShowAttendance();
                        }
                        else
                            alert(data.Error);
                    }
                });
            }
        }
        function isNumberKey(evt)
        {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 45 || charCode > 57))
                return false;
            return true;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblEventID" runat="server" Style="display: none" />
    
        <%--<h1 style="margin: 0px; padding-top: 15px; font-size: 1.1em; text-align: center">Mark Attendance</h1>--%>
        <h3 style="margin: 0px; padding-top: 10px;padding-bottom: 10px; font-size: 0.9em; text-align: center">
            <br />
            <asp:Label ID="lblEvent" runat="server" /></h3>
        <div class="lbl">Mobile</div>
        <div class="txt">
            <input type="text" placeholder="Mobile" id="txtMobile" class="form-control" style="height: 25px; width: 83%" maxlength="10" />
        </div>

        <table id="trSave">
            <tr>
                <td><%--<input type="button" id="btnSave" value="Register" onclick="RegisterPerson();" class="btn primary" />--%></td>
                <td><input type="button" id="btnSave2" value="CheckIn" onclick="CheckIn()" class="btn success" disabled="disabled" runat="server" /></td>
            </tr>
        </table>

        <div class="lbl trAddPerson" style="display: none">Name</div>
        <div class="txt trAddPerson" style="display: none">
            <input type="text" id="txtName" placeholder="Name" class="form-control" style="height: 25px; width: 83%" /></div>

        <div class="lbl trAddPerson" style="display: none">Email ID</div>
        <div class="txt trAddPerson" style="display: none">
            <input type="text" id="txtEmail" placeholder="Email" class="form-control" style="height: 25px; width: 83%" /></div>

        <div class="txt trAddPerson" style="display: none">
            <input type="button" id="btnAdd" value="Add" onclick="Add()" class="btn" />
        </div>
        <br />
        <div id="tdPerson"></div>
  
</asp:Content>


