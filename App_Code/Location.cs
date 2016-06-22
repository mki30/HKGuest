using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HkGuest
{
    public enum LocationType
    {
        None= 0,
        Temple = 1,

    }

    public class Location
    {
        public int ID;
        public int ParentID;
        public string Name = "";
        public string Address = "";
        public string City = "";
        public string State = "";
        public string PinCode = "";
        public LocationType LocationType;
        public string LocationTypeName = "";
        public string Error="";

        static Location GetRecord(IDataReader dr)
        {
            return new Location()
            {
                ID = Cmn.ToInt(dr["ID"]),
                Name = dr["Name"].ToString(),
                LocationType = (LocationType)Cmn.ToInt(dr["LocationType"]),
                LocationTypeName = ((LocationType)Cmn.ToInt(dr["LocationType"])).ToString(),
                ParentID = Cmn.ToInt(dr["ParentID"]),
                Address = dr["Address"].ToString(),
                City = dr["City"].ToString(),
                State = dr["State"].ToString(),
                PinCode = dr["PinCode"].ToString()
            };
        }

        public static List<Location> GetAll()
        {
            List<Location> list = new List<Location>();
            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";

                IDataReader dr = db.GetDataReader("select * from Location", ref Error);

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

        public static void Add(int ID, string Name, LocationType LocationType,int ParentID,string Address,string City,string State,string PinCode)
        {
            Location l = Get(ID);
            if (l == null)
                l = new Location();

            l.Name = Name;
            l.LocationType = LocationType;
            l.ParentID = ParentID;
            l.Address = Address;
            l.City = City;
            l.State = State;
            l.PinCode = PinCode;
            l.Save();
        }
        public void Save()
        {
            DatabaseCE db = new DatabaseCE();
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("ID", ID);
                data.Add("Name", Name);
                data.Add("LocationType", (int)LocationType);
                data.Add("Address", Address);
                data.Add("ParentID", ParentID);
                data.Add("City", City);
                data.Add("State", State);
                data.Add("Pincode", PinCode);
                TableID.Save("Location", data, new[] { "ID" }, db);
                if (!Global.LocationList.ContainsKey(ID))
                {
                    Location p = Location.Get(ID);
                    Global.LocationList.Add(ID, p);
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

        public static Location Get(int ID)
        {

            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";
                IDataReader dr = db.GetDataReader("select * from Location where ID=" + ID, ref Error);
                while (dr.Read())
                {
                    return GetRecord(dr);
                }

            }
            catch(Exception ex)
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
