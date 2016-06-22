using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HkGuest
{   

    public class Department
    {
        public int ID;
        public string Name = "";
        
        public string Error = "";

        static Department GetRecord(IDataReader dr)
        {
            return new Department()
            {
                ID = Cmn.ToInt(dr["ID"]),
                Name = dr["Name"].ToString()
            };
        }

        public static List<Department> GetAll()
        {
            List<Department> list = new List<Department>();
            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";

                IDataReader dr = db.GetDataReader("select * from Department", ref Error);

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
            Department d = Get(ID);
            if (d == null)
                d = new Department();

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
                TableID.Save("Department", data, new[] { "ID" }, db);
                if (!Global.DepartmentList.ContainsKey(ID))
                {
                    Department d = Department.Get(ID);
                    Global.DepartmentList.Add(ID, d);
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

        public static Department Get(int ID)
        {

            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";
                IDataReader dr = db.GetDataReader("select * from Department where ID=" + ID, ref Error);
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
