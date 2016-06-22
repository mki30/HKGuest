using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HkGuest
{

    public class Attendance
    {
        public int ID;
        public int EventID;
        public int PersonID;
        public Event EventDetail = null;
        public Person Person = null;
        public DateTime LUDate;
        public string Error = "";

        static Attendance GetRecord(IDataReader dr)
        {
            return new Attendance()
            {
                ID = Cmn.ToInt(dr["ID"]),
                EventID = Cmn.ToInt(dr["EventID"]),
                PersonID = Cmn.ToInt(dr["PersonID"]),
                LUDate = Cmn.ToDate(dr["LUDate"])
            };
        }

        public static List<Attendance> GetAll()
        {
            List<Attendance> list = new List<Attendance>();
            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";

                IDataReader dr = db.GetDataReader("select * from Attendance", ref Error);

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

        public static void Add(int ID, int EventID, int PersonID,DateTime LUDate=new DateTime())
        {
            Attendance a = Get(ID);
            if (a == null)
                a = new Attendance();

            a.EventID = EventID;
            a.PersonID = PersonID;
            if (LUDate == Cmn.MinDate)
                a.LUDate = LUDate;
            a.Save();

        }
        public void Save()
        {
            DatabaseCE db = new DatabaseCE();
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("ID", ID);
                data.Add("EventID", EventID);
                data.Add("PersonID", PersonID);
                data.Add("LUDate", LUDate);
                TableID.Save("Attendance", data, new[] { "ID" }, db);
                ID = Cmn.ToInt(data["ID"]);
                if (!Global.AttendanceList.ContainsKey(ID))
                {
                    Attendance att = Attendance.Get(ID);
                    Person per = Global.PersonList.Values.FirstOrDefault(m => m.ID == att.PersonID);
                    att.Person = per;
                    Global.AttendanceList.Add(ID, att);
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

        public static Attendance Get(int ID)
        {

            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";
                IDataReader dr = db.GetDataReader("select * from Attendance where ID=" + ID, ref Error);
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
