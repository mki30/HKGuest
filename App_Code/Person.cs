using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HkGuest
{

    public class Person
    {
        public int ID;
        public string Name = "";
        public string Contact = "";
        public string OtherContact="";
        public DateTime DOB;
        public DateTime DOJ;
        public string Address = "";
        public string Email = "";
        public string Spouse = "";
        public DateTime SpouseDOB;
        public DateTime Anniversary;
        public string AgeGroup = "";
        public string Gender = "";
        public string Education = "";
        public string Profession = "";
        public string SpecificWork = "";
        public string NatureOfWork = "";
        public string Chanting = "";
        public string Married = "";
        public string Kids = "";
        public string ChildName = "";
        public DateTime ChildDOB;
        public string Language1 = "";
        public string Language2 = "";
        public string Language3 = "";
        public string ForiegnLang = "";
        public string Interest = "";
        public string Category = "";
        public string ConntoISKON = "";
        public int ISKONMonths ;
        public int ISKONYears ;
        public string PreachingGroup = "";
        public string GroupLeaderName = "";
        public string InitiatedName = "";
        public string ConntoOther = "";
        public string OtherSpiritOrg = "";
        public string Locality = "";
        public string PerAddress = "";
        public string RefSrc = "";
        public string RefBy = "";
        public string Relation = "";
        public string ExtID = "";
        public string FamilyID = "";
        public string Error = "";

        static Person GetRecord(IDataReader dr)
        {
            return new Person()
            {
                ID = Cmn.ToInt(dr["ID"]),
                Name = dr["Name"].ToString(),
                Contact = dr["Contact"].ToString(),
                OtherContact = dr["OtherContact"].ToString(),
                DOB = Cmn.ToDate(dr["DOB"]),
                DOJ = Cmn.ToDate(dr["DOJ"]),
                Address = dr["Address"].ToString(),
                Email = dr["Email"].ToString(),
                Spouse = dr["Spouse"].ToString(),
                SpouseDOB = Cmn.ToDate(dr["SpouseDOB"]),
                Anniversary = Cmn.ToDate(dr["Anniversary"]),
                AgeGroup = dr["AgeGroup"].ToString(),
                Gender = dr["Gender"].ToString(),
                Education = dr["Education"].ToString(),
                Profession = dr["Profession"].ToString(),
                SpecificWork = dr["SpecificWork"].ToString(),
                NatureOfWork = dr["NatureOfWork"].ToString(),
                Chanting = dr["Chanting"].ToString(),
                Married = dr["Married"].ToString(),
                Kids = dr["Kids"].ToString(),
                ChildName = dr["ChildName"].ToString(),
                ChildDOB = Cmn.ToDate(dr["ChildDOB"]),
                Language1 = dr["Language1"].ToString(),
                Language2 = dr["Language2"].ToString(),
                Language3 = dr["Language3"].ToString(),
                ForiegnLang = dr["ForiegnLang"].ToString(),
                Interest = dr["Interest"].ToString(),
                Category = dr["Category"].ToString(),
                ConntoISKON = dr["ConntoISKON"].ToString(),
                ISKONMonths = Cmn.ToInt(dr["ISKONMonths"]),
                ISKONYears = Cmn.ToInt(dr["ISKONYears"]),
                PreachingGroup = dr["PreachingGroup"].ToString(),
                GroupLeaderName = dr["GroupLeaderName"].ToString(),
                InitiatedName = dr["InitiatedName"].ToString(),
                ConntoOther = dr["ConntoOther"].ToString(),
                OtherSpiritOrg = dr["OtherSpiritOrg"].ToString(),
                Locality = dr["Locality"].ToString(),
                PerAddress = dr["PerAddress"].ToString(),
                RefSrc = dr["Locality"].ToString(),
                RefBy = dr["PerAddress"].ToString(),
                Relation = dr["Relation"].ToString(),
                ExtID = dr["ExtID"].ToString(),
                FamilyID = dr["FamilyID"].ToString()

            };
        }

        public static List<Person> GetAll()
        {
            List<Person> list = new List<Person>();
            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";

                IDataReader dr = db.GetDataReader("SELECT * FROM Person", ref Error);

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


        public void Save()
        {
            DatabaseCE db = new DatabaseCE();
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("ID", ID);
                data.Add("Name", Name);
                data.Add("Contact", Contact);
                data.Add("OtherContact", OtherContact);
                data.Add("DOB", DOB);
                data.Add("DOJ", DOJ);
                data.Add("Address", Address);
                data.Add("Email", Email);
                data.Add("Spouse", Spouse);
                data.Add("SpouseDOB", SpouseDOB);
                data.Add("Anniversary", Anniversary);
                data.Add("AgeGroup", AgeGroup);
                data.Add("Gender", Gender);
                data.Add("Education", Education);
                data.Add("Profession", Profession);
                data.Add("SpecificWork", SpecificWork);
                data.Add("NatureOfWork", NatureOfWork);
                data.Add("Chanting", Chanting);
                data.Add("Married", Married);
                data.Add("Kids", Kids);
                data.Add("ChildName", ChildName);
                data.Add("ChildDOB", ChildDOB);
                data.Add("Language1", Language1);
                data.Add("Language2", Language2);
                data.Add("Language3", Language3);
                data.Add("ForiegnLang", ForiegnLang);
                data.Add("Interest", Interest);
                data.Add("Category", Category);
                data.Add("ConntoISKON", ConntoISKON);
                data.Add("ISKONMonths", ISKONMonths);
                data.Add("ISKONYears", ISKONYears);
                data.Add("PreachingGroup", PreachingGroup);
                data.Add("GroupLeaderName", GroupLeaderName);
                data.Add("InitiatedName", InitiatedName);
                data.Add("ConntoOther", ConntoOther);
                data.Add("OtherSpiritOrg", OtherSpiritOrg);
                data.Add("Locality", Locality);
                data.Add("PerAddress", PerAddress);
                data.Add("RefSrc", RefSrc);
                data.Add("Relation", Relation);
                data.Add("ExtID", ExtID);
                data.Add("FamilyID", FamilyID);
                TableID.Save("Person", data, new[] { "ID" }, db);
                ID = Cmn.ToInt(data["ID"]);
                if (!Global.PersonList.ContainsKey(ID))
                {
                    Person p = Person.Get(ID);
                    if (p != null)
                        Global.PersonList.Add(ID, p);
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

        public static Person Get(int ID)
        {

            DatabaseCE db = new DatabaseCE();
            try
            {
                string Error = "";
                IDataReader dr = db.GetDataReader("select * from Person where ID=" + ID, ref Error);
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
