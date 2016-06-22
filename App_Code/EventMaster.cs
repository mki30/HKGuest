using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HkGuest
{

    public class EventMaster
    {
        public int ID;
        public string Name = "";

        public string Error = "";

        static EventMaster GetRecord(IDataReader dr)
        {
            return new EventMaster()
            {
                ID = Cmn.ToInt(dr["ID"]),
                Name = dr["Name"].ToString()
            };
        }

        public static List<EventMaster> GetAll()
        {
            List<EventMaster> list = new List<EventMaster>();
            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";

                IDataReader dr = db.GetDataReader("select * from EventMaster", ref Error);

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

        public static void Add(int ID, string Name)
        {
            EventMaster d = Get(ID);
            if (d == null)
                d = new EventMaster();

            d.Name = Name;
            d.Save();

        }
        public void Save()
        {
            DatabaseCE db = new DatabaseCE();
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("ID", ID);
                data.Add("Name", Name);
                TableID.Save("EventMaster", data, new[] { "ID" }, db);
                if (!Global.MainEventList.ContainsKey(ID))
                {
                    EventMaster e = EventMaster.Get(ID);
                    Global.MainEventList.Add(ID, e);
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

        public static EventMaster Get(int ID)
        {

            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";
                IDataReader dr = db.GetDataReader("select * from EventMaster where ID=" + ID, ref Error);
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
