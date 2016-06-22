using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

using System.Web.UI;
using HkGuest;
public class User
{
    public string EmailID = "";
    public string IPAddress = "";
    public string Name = "";
    public string PictureURl = "";
    public bool IsAdmin = false;

}

public class Global
{

    public static Dictionary<int, Location> LocationList = new Dictionary<int, Location>();
    public static Dictionary<int, Department> DepartmentList = new Dictionary<int, Department>();
    public static Dictionary<int, EventMaster> MainEventList = new Dictionary<int, EventMaster>();
    public static Dictionary<int, Event> EventList = new Dictionary<int, Event>();
    public static Dictionary<int, Person> PersonList = new Dictionary<int, Person>();
    public static Dictionary<int, Attendance> AttendanceList = new Dictionary<int, Attendance>();
    public static Dictionary<string, User> GlobalUser = new Dictionary<string, User>();
    public static void LoadGlobal()
    {
        LocationList.Clear();
        DepartmentList.Clear();
        MainEventList.Clear();
        EventList.Clear();
        PersonList.Clear();
        AttendanceList.Clear();
        List<Location> Locations = Location.GetAll();
        foreach (Location l in Locations)
        {
            if (!LocationList.ContainsKey(l.ID))
                LocationList.Add(l.ID, l);
        }
        List<Department> Departments = Department.GetAll();
        foreach (Department d in Departments)
        {
            if (!DepartmentList.ContainsKey(d.ID))
                DepartmentList.Add(d.ID, d);
        }
        List<EventMaster> MainEvnts = EventMaster.GetAll();
        foreach (EventMaster e in MainEvnts)
        {
            if (!MainEventList.ContainsKey(e.ID))
                MainEventList.Add(e.ID, e);
        }
        List<Person> Persons = Person.GetAll();
        foreach (Person p in Persons)
        {
            if (!PersonList.ContainsKey(p.ID))
                PersonList.Add(p.ID, p);
        }
        List<Event> Events = Event.GetAll();
        List<Attendance> AtendanceList = Attendance.GetAll();
        foreach (Event e in Events)
        {
            if (!EventList.ContainsKey(e.ID))
            {
                EventList.Add(e.ID, e);
                e.EventM = Global.MainEventList.Values.FirstOrDefault(m => m.ID == e.EventID);
                e.Location = Global.LocationList.Values.FirstOrDefault(m => m.ID == e.LocationID);
                e.Department = Global.DepartmentList.Values.FirstOrDefault(m => m.ID == e.DepartmentID);
            }
        }
        foreach (Attendance at in AtendanceList)
        {
            if (!AttendanceList.ContainsKey(at.ID))
            {
                AttendanceList.Add(at.ID, at);
                Person p = null;
                if (PersonList.TryGetValue(at.PersonID, out p))
                {
                    at.Person = p;
                }
            }
        }
        //foreach (Event evt in EventList.Values)
        //{
        //    List<Attendance> currentAttendance = AtendanceList.Where(m => m.EventID == evt.ID).ToList();
        //    foreach (Attendance at in currentAttendance)
        //    {
        //        Person p = null;
        //        if (PersonList.TryGetValue(at.PersonID, out p) && !evt.PersonList.Contains(p))
        //        {
        //            evt.PersonList.Add(p);
        //            if (!evt.AttendanceList.Contains(at))
        //                evt.AttendanceList.Add(at);
        //            at.Person = p;
        //        }
        //    }
        //}
    }
}