using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using HkGuest;
public partial class Data : System.Web.UI.Page
{

    string Action, Data1, Data2, Data3, Data4, Data5, Data6, Data7, Data8;
    StringBuilder sb = new StringBuilder("");
    string Error = "";
    User user = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        string encode = "no";// Cmn.GetEncode(this);

        Action = Request.QueryString["Action"];
        Data1 = Request.QueryString["Data1"];
        Data2 = Request.QueryString["Data2"];
        Data3 = Request.QueryString["Data3"];
        Data4 = Request.QueryString["Data4"];
        Data5 = Request.QueryString["Data5"];
        Data6 = Request.QueryString["Data6"];
        Data7 = Request.QueryString["Data7"];
        Data8 = Request.QueryString["Data8"];
        string term = Request.QueryString["term"];
        //Error = "OK";
        switch (Action)
        {
            case "AddAttendanceByMobile": AddAttendanceByMobile(Data1, Cmn.ToInt(Data2), Data3 == "1"); break;
            case "AddPersonAndAttendance": AddPersonAndAttendance(Data1, Cmn.ToInt(Data2), Data3, Data4, Cmn.ToInt(Data5)); break;
            //case "ShowAttendance": ShowAttendance(Cmn.ToInt(Data1), Data2, Data3); break;
            //case "GetEventList": GetEventList(Data1, Cmn.ToInt(Data2)); break;
            case "AddAttendance": AddAttendance(Cmn.ToInt(Data1), Cmn.ToInt(Data2)); break;
            case "RegisterEvent": RegisterEvent(Data1, Cmn.ToInt(Data2), Data3); break;
            case "SignIn": sb.Append(SignIn(Data1)); break;
        }

        if (Global.GlobalUser.Any())
            Global.GlobalUser.TryGetValue(Request.Cookies["email"].Value, out user);

        object d = null;
        if (user != null)
        {
            //Error = "";
            try
            {
                switch (Action)
                {
                    case "GetLocations": GetLocations(Data1); break;
                    case "AddLocation": AddLocation(Cmn.ToInt(Data1), Data2, (LocationType)Cmn.ToInt(Data3), Cmn.ToInt(Data4), Data5, Data6, Data7, Data8); break;
                    case "GetDepartments": GetDepartments(); break;
                    case "AddDepartment": AddDepartment(Cmn.ToInt(Data1), Data2); break;
                    case "GetEvents": GetEvents(); break;
                    case "AddEvent": AddEvent(Cmn.ToInt(Data1), Data2); break;
                    case "AddEventDetail": AddEventDetail(); break;
                    case "GetEventDetail": GetEventDetail(Cmn.ToInt(Data1)); break;
                    case "GetEventList": GetEventList(Data1, Cmn.ToInt(Data2)); break;
                    case "GetMobile": GetMobile(); break;
                    case "GetPersonList": GetPersonList(Data1, Cmn.ToInt(Data2)); break;
                    case "AddAttendance": AddAttendance(Cmn.ToInt(Data1), Cmn.ToInt(Data2)); break;
                    case "ShowAttendance": d = ShowAttendance(Cmn.ToInt(Data1), Data2, Data3); break;
                }

            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            finally
            {
            }
        }
        else
        {
            Error = Error == "" ? "Not logged in" : Error;
        }
        Response.AddHeader("Content-Type", "application/json");
        var obj = new { Error = Error, Data = d != null ? d : sb.ToString() };
        Cmn.WriteResponse(this, new JavaScriptSerializer().Serialize(obj), encode);
    }
    string SignIn(string Impersonate)
    {
        //https://developers.google.com/identity/protocols/OpenIDConnect#server-flow
        //IP address should also be tracked to avoid multiple login and duplicate logins
        NameValueCollection nvc = Request.Form;

        try
        {
            using (WebClient w = new WebClient())
            {
                string data = w.DownloadString("https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=" + nvc["token"]);
                Dictionary<string, object> singInData = (Dictionary<string, object>)new JavaScriptSerializer().DeserializeObject(data);

                User u = new User();

                foreach (string key in singInData.Keys)
                {
                    switch (key)
                    {
                        case "email": u.EmailID = singInData[key].ToString(); break;
                        case "name": u.Name = singInData[key].ToString(); break;
                        case "picture": u.PictureURl = singInData[key].ToString(); break;
                    }
                }

                if (u.EmailID == "indusharma.kaushik2@gmail.com" || u.EmailID == "vimalkmail@gmail.com" || u.EmailID == "ky.himanshu@gmail.com" || u.EmailID == "psu.singh@gmail.com")
                {

                    if (!Global.GlobalUser.ContainsKey(u.EmailID))
                        Global.GlobalUser.Add(u.EmailID, u);

                    u.IPAddress = Request.UserHostAddress;

                    if (Impersonate != "")
                        u.IsAdmin = false;
                    else
                        u.IsAdmin = true;

                    Global.GlobalUser[u.EmailID] = u;
                    HttpCookie c = new HttpCookie("email", u.EmailID);
                    Response.Cookies.Add(c);
                    return "OK";
                }
            }

        }
        catch
        {
            return "Authentication server not accessible";
        }

        return "Account Not found";
    }

    void RegisterEvent(string Mobile, int EventID, string Name)
    {
        Person p = Global.PersonList.Values.FirstOrDefault(m => m.Contact == Mobile);
        if (p == null)
        {
            p = new Person();
            p.Contact = Mobile;
            p.Name = Name;
            p.Save();
        }
        Attendance att = Global.AttendanceList.Values.FirstOrDefault(m => m.PersonID == p.ID && m.EventID == EventID);
        if (att == null)
            Attendance.Add(0, EventID, p.ID, Cmn.MinDate);
        else
        {
            att.LUDate = Cmn.MinDate;
            att.Save();
        }

    }

    object ShowAttendance(int EventID, string SortBy, string Register)
    {
        Event evt = Global.EventList.Values.FirstOrDefault(m => m.ID == EventID);
        List<Attendance> AttendenceList = Global.AttendanceList.Values.Where(m => m.EventID == evt.ID && m.Person != null).ToList();
        switch (SortBy)
        {
            case "Name": AttendenceList = AttendenceList.OrderBy(m => m.Person.Name).ToList(); break;
            case "Mobile": AttendenceList = AttendenceList.OrderBy(m => m.Person.Contact).ToList(); break;
            case "Date": AttendenceList = AttendenceList.OrderBy(m => m.LUDate).ToList(); break;
        }
        var newList = AttendenceList.Select(a => new
        {
            ID = a.Person.ID,
            Contact = a.Person.Contact,
            Name = a.Person.Name,
            Date = a.LUDate == Cmn.MinDate ? "" : a.LUDate.ToString("hh:mm tt")
        });

        return newList;
        //sb.Append(new JavaScriptSerializer().Serialize(newList));
    }
    void GetEventList(string SortBy, int LocationID)
    {
        List<Event> Events = Global.EventList.Values.Where(m => m.EventM != null && m.Location != null && m.Department != null).ToList();

        Events = LocationID == 0 ? Events : Events.Where(m => m.LocationID == LocationID).ToList();
        switch (SortBy)
        {
            case "Event": Events = Events.OrderBy(m => m.EventM.Name).ToList(); break;
            case "Location": Events = Events.OrderBy(m => m.Location.Name).ToList(); break;
            case "Department": Events = Events.OrderBy(m => m.Department.Name).ToList(); break;
        }
        var newList = Events.Select(a => new
                {
                    ID = a.ID,
                    EventID = a.EventID,
                    EventName = a.EventM == null ? "" : a.EventM.Name,
                    Location = a.Location == null ? "" : a.Location.Name,
                    Department = a.Department == null ? "" : a.Department.Name,
                    LocationID = a.LocationID,
                    DepartmentID = a.DepartmentID,
                    ResponsiblePersonID = a.ResponsiblePersonID,
                    fromDate = a.fromDate.ToString("dd-MMM-yy hh:mm tt"),
                    toDate = a.toDate.ToString("dd-MMM-yy hh:mm tt")
                }).ToList();
        sb.Append(new JavaScriptSerializer().Serialize(newList));
    }
    void AddPerson()
    {
        NameValueCollection nvc = Request.Form;
        Dictionary<string, object> list = (Dictionary<string, object>)new JavaScriptSerializer().DeserializeObject(nvc["Data"].ToString());
    }
    void GetDepartments()
    {
        sb.Append(new JavaScriptSerializer().Serialize(Department.GetAll()));
    }
    void AddDepartment(int ID, string Name)
    {
        Department.Add(ID, Name);
    }
    void AddEventDetail()
    {
        NameValueCollection nvc = Request.Form;
        Dictionary<string, object> list = (Dictionary<string, object>)new JavaScriptSerializer().DeserializeObject(nvc["Data"].ToString());
        //Event e = Event.Add(GetValueInt(list, "lblID"), GetValueInt(list, "ddEvent"), GetValueInt(list, "ddLocation"), GetValueInt(list, "ddDepartment"), GetValueInt(list, "ddResponsiblePerson"), Cmn.ToDate(GetValue(list, "txtFromDate") + " " + GetValue(list, "ddFromtime")), Cmn.ToDate(GetValue(list, "txtToDate") + " " + GetValue(list, "ddTotime")));
        Event e = Event.Get(GetValueInt(list, "lblID"));
        if (e == null)
            e = new Event();
        try
        {
            e.EventID = GetValueInt(list, "ddEvent");
            e.LocationID = GetValueInt(list, "ddLocation");
            e.DepartmentID = GetValueInt(list, "ddDepartment");
            e.ResponsiblePersonID = GetValueInt(list, "ddResponsiblePerson");
            e.fromDate = Cmn.ToDate(GetValue(list, "txtFromDate") + " " + GetValue(list, "ddFromtime"));
            e.toDate = Cmn.ToDate(GetValue(list, "txtToDate") + " " + GetValue(list, "ddTotime"));
            e.Description = GetValue(list, "txtDescription");
            e.EntryType = GetValue(list, "ddEntryType");
            e.Preacher1 = GetValue(list, "txtPreacher1");
            e.Preacher2 = GetValue(list, "txtPreacher2");
            e.Organizer1 = GetValue(list, "txtOrganizer1");
            e.Organizer2 = GetValue(list, "txtOrganizer2");
            e.Volunteer1 = Volunteer.Get(GetValueInt(list, "ddVolunteer1"));
            e.Volunteer2 = Volunteer.Get(GetValueInt(list, "ddVolunteer2"));
            e.Topic = GetValue(list, "txtTopic");
            e.Collection = GetValue(list, "txtCollection");
            e.Comments = GetValue(list, "txtComments");
            e.BudgetedCost = GetValueInt(list, "txtBudgetedCost");
            e.ActualExpense = GetValueInt(list, "txtActualExpense");
            e.Save();
        }
        catch {
            Error = "Error";
        }
        sb.Append(e.ID);
    }
    int GetValueInt(Dictionary<string, object> list, string key)
    {
        return Cmn.ToInt(GetValue(list, key));
    }

    string GetValue(Dictionary<string, object> list, string key)
    {
        object val;
        list.TryGetValue(key, out val);
        return val == null ? "" : val.ToString();
    }
    void GetEventDetail(int ID)
    {
        Event evnt = Event.Get(ID);
        var obj = new
        {
            ID = evnt.ID,
            EventID = evnt.EventID,
            EventName = evnt.EventM == null ? "" : evnt.EventM.Name,
            Location = evnt.Location == null ? "" : evnt.Location.Name,
            Department = evnt.Department == null ? "" : evnt.Department.Name,
            LocationID = evnt.LocationID,
            DepartmentID = evnt.DepartmentID,
            ResponsiblePersonID = evnt.ResponsiblePersonID,
            fromDate = evnt.fromDate.ToString("dd-MMM-yy hh:mm tt"),
            toDate = evnt.toDate.ToString("dd-MMM-yy hh:mm tt"),
            Description = evnt.Description,
            EntryType = evnt.EntryType,
            Preacher1 = evnt.Preacher1,
            Preacher2 = evnt.Preacher2,
            Organizer1 = evnt.Organizer1,
            Organizer2 = evnt.Organizer2,
            Volunteer1 = evnt.Volunteer1 != null ? evnt.Volunteer1.ID : 0,
            Volunteer2 = evnt.Volunteer2 != null ? evnt.Volunteer2.ID : 0,
            Comments = evnt.Comments,
            Topic = evnt.Topic,
            Collection = evnt.Collection,
            BudgetedCost = evnt.BudgetedCost,
            ActualExpense = evnt.ActualExpense

        };
        sb.Append(new JavaScriptSerializer().Serialize(obj));
    }
    void GetLocations(string SortBy)
    {
        List<Location> Locations = Location.GetAll();
        switch (SortBy)
        {
            case "Location": Locations = Locations.OrderBy(m => m.Name).ToList(); break;
            case "Type": Locations = Locations.OrderBy(m => m.LocationTypeName).ToList(); break;
            case "Address": Locations = Locations.OrderBy(m => m.Address).ToList(); break;
            case "City": Locations = Locations.OrderBy(m => m.City).ToList(); break;
            case "State": Locations = Locations.OrderBy(m => m.State).ToList(); break;
            case "PinCode": Locations = Locations.OrderBy(m => m.PinCode).ToList(); break;
        }
        sb.Append(new JavaScriptSerializer().Serialize(Locations));
    }

    void AddLocation(int ID, string Name, LocationType LocationType, int ParentID, string Address, string City, string State, string PinCode)
    {
        Location.Add(ID, Name, LocationType, ParentID, Address, City, State, PinCode);
    }
    void GetEvents()
    {
        sb.Append(new JavaScriptSerializer().Serialize(EventMaster.GetAll()));
    }

    void AddEvent(int ID, string Name)
    {
        EventMaster.Add(ID, Name);
    }
    void GetMobile()
    {
        var newList = Global.PersonList.Values.Select(m => new { ID = m.ID.ToString(), Mobile = m.Contact });
        sb.Append(new JavaScriptSerializer().Serialize(newList));
    }
    void GetPersonList(string Mobile, int EventID)
    {
        List<Person> pList = Global.PersonList.Values.Where(m => m.Contact == Mobile).ToList();
        if (pList.Count > 1)
        {
            var newList = pList.Select(m => new { ID = m.ID.ToString(), Name = m.Name });
            sb.Append(new JavaScriptSerializer().Serialize(newList));
        }
        else
        {
            Person p = pList.FirstOrDefault(m => m.Contact == Mobile);
            if (p != null)
            {
                Attendance.Add(0, EventID, p.ID);
                sb.Append("Done");
            }
            else
                Error = "Not found";
        }
    }
    void AddAttendance(int PersonID, int EventID)
    {
        Attendance att = Global.AttendanceList.Values.FirstOrDefault(m => m.PersonID == PersonID && m.EventID == EventID);
        if (att != null)
        {
            att.LUDate = DateTime.Now;
            att.Save();
        }
        else
            Attendance.Add(0, EventID, PersonID);

    }
    void AddAttendanceByMobile(string Mobile, int EventID, Boolean Register)
    {
        Person person = Global.PersonList.Values.FirstOrDefault(m => m.Contact == Mobile);
        if (person != null)
        {
            Attendance att = Global.AttendanceList.Values.FirstOrDefault(m => m.PersonID == person.ID && m.EventID == EventID);
            if (att != null)
            {
                att.LUDate = Register ? Cmn.MinDate : DateTime.Now;
                att.Save();
            }
            else
                Attendance.Add(0, EventID, person.ID, Register ? Cmn.MinDate : DateTime.Now);
            sb.Append("Done");
        }
        else
        {
            Error = "Not found";
        }
    }
    void AddPersonAndAttendance(string Mobile, int EventID, string Name, string Email, int confirm)
    {
        Person person = Global.PersonList.Values.FirstOrDefault(m => m.Contact == Mobile);
        if ((person == null && confirm == 0) || (person != null && confirm == 1))
        {
            person = new Person();
            person.Contact = Mobile;
            person.Name = Name;
            person.Email = Email;
            person.DOB = Cmn.MinDate;
            person.DOJ = Cmn.MinDate;
            person.ChildDOB = Cmn.MinDate;
            person.SpouseDOB = Cmn.MinDate;
            person.Anniversary = Cmn.MinDate;
            person.Save();
            Attendance att = Global.AttendanceList.Values.FirstOrDefault(m => m.EventID == EventID && m.PersonID == person.ID);
            if (att == null)
                Attendance.Add(0, EventID, person.ID, Cmn.MinDate);
            else
            {
                att.LUDate = Cmn.MinDate;
                att.Save();
            }
            sb.Append("Done");
        }
        else
            Error = "Mobile Number already exists, Do you want to continue?";
    }
}