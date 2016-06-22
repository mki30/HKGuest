using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HkGuest
{

    public class Event
    {
        public int ID;
        public int EventID;
        public int LocationID;
        public int DepartmentID;
        public int ResponsiblePersonID;
        public DateTime fromDate;
        public DateTime toDate;
        public EventMaster EventM = null;
        public Location Location = null;
        public Department Department = null;
        public List<Person> PersonList = new List<Person>();
        public string Description = "";
        public string EntryType = "";
        public string Preacher1 = "";
        public string Preacher2 = "";
        public string Organizer1 = "";
        public string Organizer2 = "";
        public Volunteer Volunteer1;
        public Volunteer Volunteer2;
        public string Topic = "";
        public string Collection = "";
        public string Comments = "";
        public int BudgetedCost;
        public int ActualExpense;

        public List<Attendance> AttendanceList = new List<Attendance>();
        public string Error = "";

        static Event GetRecord(IDataReader dr)
        {
            return new Event()
            {
                ID = Cmn.ToInt(dr["ID"]),
                EventID = Cmn.ToInt(dr["EventID"]),
                LocationID = Cmn.ToInt(dr["LocationID"]),
                DepartmentID = Cmn.ToInt(dr["DepartmentID"]),
                ResponsiblePersonID = Cmn.ToInt(dr["ReponsiblePersonID"]),
                fromDate = Cmn.ToDate(dr["FromDate"]),
                toDate = Cmn.ToDate(dr["ToDate"]),
                Description = dr["Description"].ToString(),
                EntryType = dr["EntryType"].ToString(),
                Preacher1 = dr["Preacher1"].ToString(),
                Preacher2 = dr["Preacher2"].ToString(),
                Topic = dr["Topic"].ToString(),
                Organizer1 = dr["Organizer1"].ToString(),
                Organizer2 = dr["Organizer2"].ToString(),
                Volunteer1 = Volunteer.Get(Cmn.ToInt(dr["Volunteer1"])),
                Volunteer2 = Volunteer.Get(Cmn.ToInt(dr["Volunteer2"])),
                Collection = dr["Collection"].ToString(),
                Comments = dr["Comments"].ToString(),
                BudgetedCost = Cmn.ToInt(dr["BudgetedCost"]),
                ActualExpense = Cmn.ToInt(dr["ActualExpense"]),
            };
        }

        public static List<Event> GetAll()
        {
            List<Event> list = new List<Event>();
            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";

                IDataReader dr = db.GetDataReader("select * from Event", ref Error);

                while (dr.Read())
                {
                    list.Add(GetRecord(dr));
                }
            }
            finally
            {
                db.Close();
            }
            return list;
        }

        public static Event Add(int ID, int EventID, int LocationID, int DepartmentID, int PersonID, DateTime FromDate, DateTime ToDate)
        {
            Event e = Get(ID);
            if (e == null)
                e = new Event();

            e.EventID = EventID;
            e.LocationID = LocationID;
            e.DepartmentID = DepartmentID;
            e.ResponsiblePersonID = PersonID;
            e.fromDate = FromDate;
            e.toDate = ToDate;
            e.Save();
            return e;
        }
        public void Save()
        {
            DatabaseCE db = new DatabaseCE();
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("ID", ID);
                data.Add("EventID", EventID);
                data.Add("LocationID", LocationID);
                data.Add("DepartmentID", DepartmentID);
                data.Add("ReponsiblePersonID", ResponsiblePersonID);
                data.Add("FromDate", fromDate);
                data.Add("ToDate", toDate);
                data.Add("Description", Description);
                data.Add("EntryType", EntryType);
                data.Add("Preacher1", Preacher1);
                data.Add("Preacher2", Preacher2);
                data.Add("Topic", Topic);
                data.Add("Organizer1", Organizer1);
                data.Add("Organizer2", Organizer2);
                data.Add("Volunteer1", (Volunteer1==null?0:Volunteer1.ID));
                data.Add("Volunteer2", (Volunteer1 == null?0:Volunteer2.ID));
                data.Add("Collection", Collection);
                data.Add("Comments", Comments);
                data.Add("BudgetedCost", BudgetedCost);
                data.Add("ActualExpense", ActualExpense);
                TableID.Save("Event", data, new[] { "ID" }, db);
                ID = Cmn.ToInt(data["ID"]);
                if (!Global.EventList.ContainsKey(ID))
                {
                    Event evt = Event.Get(ID);
                    Department dpt = Department.Get(evt.DepartmentID);
                    evt.Department = dpt;
                    Location loc = Location.Get(evt.LocationID);
                    evt.Location = loc;
                    EventMaster eventMain = EventMaster.Get(evt.EventID);
                    evt.EventM = eventMain;
                    Global.EventList.Add(ID, evt);

                }
            }
            catch
            {
            }
            finally
            {
                db.Close();
            }
        }

        public static Event Get(int ID)
        {

            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";
                IDataReader dr = db.GetDataReader("select * from Event where ID=" + ID, ref Error);
                while (dr.Read())
                {
                    return GetRecord(dr);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Close();
            }
            return null;
        }
    }
}



