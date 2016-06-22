using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Voluntee
/// </summary>
namespace HkGuest
{
  
    public class Volunteer
    {
        public int ID;
        public string Name = "";
        public string Task = "";
        public string Error = "";

        static Volunteer GetRecord(IDataReader dr)
        {
            return new Volunteer()
            {
                ID = Cmn.ToInt(dr["ID"]),
                Name = dr["Name"].ToString(),
                Task = dr["Task"].ToString()
            };
        }

        public static List<Volunteer> GetAll()
        {
            List<Volunteer> list = new List<Volunteer>();
            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";

                IDataReader dr = db.GetDataReader("select * from Volunteer", ref Error);

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

        public static void Add(int ID, string Name, string Task)
        {
            Volunteer v = Get(ID);
            if (v == null)
                v = new Volunteer();

            v.Name = Name;
            v.Task = Task;
            v.Save();
        }
        public void Save()
        {
            DatabaseCE db = new DatabaseCE();
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("ID", ID);
                data.Add("Name", Name);
                data.Add("Task", Task);
                TableID.Save("Volunteer", data, new[] { "ID" }, db);
                //if (!Global.VolunteerList.ContainsKey(ID))
                //{
                //    Volunteer p = Volunteer.Get(ID);
                //    Global.VolunteerList.Add(ID, p);
                //}
            }
            catch
            {
            }
            finally
            {
                db.Close();
            }
        }

        public static Volunteer Get(int ID)
        {

            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";
                IDataReader dr = db.GetDataReader("select * from Volunteer where ID=" + ID, ref Error);
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