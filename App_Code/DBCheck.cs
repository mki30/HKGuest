using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using System.IO;
using System.Web;

public class DBCheck
{
    //CheckDatabase
    //CheckTable
    public static void RunSQLFile(DatabaseCE db, string data)
    {
        try
        {
            string[] Commands = data.Split(new string[] { "GO\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            IDbCommand cmd = new SqlCeCommand();
            cmd.Connection = db.myconnection;
            foreach (string s in Commands)
            {
                cmd.CommandText = s;
                cmd.ExecuteNonQuery();
            }
        }
        catch
        {
        }
    }

    public static void UpdateDB(string Name)
    {
        if (!Directory.Exists(HttpContext.Current.Server.MapPath(@"~/App_Data/")))
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(@"~/App_Data/"));

        string DBName = @"~/App_Data/" + Name + ".sdf";

        if (!File.Exists(DBName))
            DatabaseCE.CreateDB(HttpContext.Current.Server.MapPath(DBName), DatabaseCE.DBPath);

        DatabaseCE db = new DatabaseCE();   //Update PropertyMap Database Structute
        try
        {
            int ctr = 0;
            while (DBCheck.UpdateDBStructure(db, ++ctr)) ;
        }
        catch
        {

        }
        finally
        {
            db.Close();
        }
    }
    //public static string UpdateDB()
    //{
    //    string DbPath = HttpContext.Current.Server.MapPath(@"~\App_Data\HKGuest.sdf");
    //    string connstr = @"Data Source=|DataDirectory|\HKGuest.sdf";

    //    string response = "";
    //    if (!File.Exists(DbPath))
    //        DatabaseCE.CreateDB(DbPath, connstr);
    //    DatabaseCE db = new DatabaseCE();
    //    try
    //    {
    //        int Counter = 0;
    //        while (DBCheck.UpdateDBStructure(db, ++Counter))
    //        {
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Cmn.LogError(ex, "DBUpdate");
    //        response += ex.Message;
    //    }
    //    finally
    //    {
    //        db.Close();
    //        response += " DB Update Done <br/>" + db.Error;
    //    }
    //    return response;
    //}
    public static void CheckTable(DatabaseCE db, string TableName, Dictionary<string, string> Fields, string[] PrimaryKeys)
    {
        //fields to be added to all table

        Fields.Add("LUDate", "[datetime]");
        Fields.Add("LUBy", "[nvarchar](50)");

        //create table
        string SQL = "CREATE TABLE [" + TableName + "] (";
        string PK = " PRIMARY KEY (";
        foreach (string s in PrimaryKeys)
        {
            SQL += " [" + s + "] " + Fields[s] + ",";
            PK += " [" + s + "] " + ",";
        }

        PK = PK.Substring(0, PK.Length - 1) + ") ";
        SQL = SQL + PK + ") ";

        string err = db.RunQuery(SQL);

        //check for fields
        foreach (string s in Fields.Keys)
        {
            string Err = db.RunQuery("ALTER TABLE [" + TableName + "] ADD [" + s + "] " + Fields[s]);
        }
    }

    public static Boolean UpdateDBStructure(DatabaseCE db, int Counter)
    {
        Dictionary<string, string> fields = new Dictionary<string, string>();

        switch (Counter)
        {
            case 1://Location
                fields.Add("ID", "[int]");
                fields.Add("ParentID", "[int]");
                fields.Add("Name", "[nvarchar](50)");
                fields.Add("Address", "[nvarchar](200)");
                fields.Add("City", "[nvarchar](50)");
                fields.Add("State", "[nvarchar](50)");
                fields.Add("Pincode", "[nvarchar](6)");
                fields.Add("LocationType", "[int]");
                CheckTable(db, "Location", fields, new string[] { "ID" });
                break;
            case 2://Department
                fields.Add("ID", "[int]");
                fields.Add("Name", "[nvarchar](50)");
                CheckTable(db, "Department", fields, new string[] { "ID" });
                break;
            case 3://EventMaster
                fields.Add("ID", "[int]");
                fields.Add("Name", "[nvarchar](50)");
                CheckTable(db, "EventMaster", fields, new string[] { "ID" });
                break;
            case 4://Event
                fields.Add("ID", "[int]");
                fields.Add("EventID", "[int]");
                fields.Add("LocationID", "[nvarchar](50)");
                fields.Add("DepartmentID", "[nvarchar](50)");
                fields.Add("FromDate", "[datetime]");
                fields.Add("ToDate", "[datetime]");
                fields.Add("ReponsiblePersonID", "[int]");
                fields.Add("Description", "[nvarchar](500)");
                fields.Add("EntryType", "[nvarchar](100)");
                fields.Add("Preacher1", "[nvarchar](100)");
                fields.Add("Preacher2", "[nvarchar](100)");
                fields.Add("Topic", "[nvarchar](100)");
                fields.Add("Organizer1", "[nvarchar](100)");
                fields.Add("Organizer2", "[nvarchar](100)");
                fields.Add("Volunteer1", "[int]");
                fields.Add("Volunteer2", "[int]");
                fields.Add("BudgetedCost", "[int]");
                fields.Add("ActualExpense", "[int]");
                fields.Add("Collection", "[nvarchar](100)");
                fields.Add("Comments", "[nvarchar](100)");
                CheckTable(db, "Event", fields, new string[] { "ID" });
                break;
            case 5://Person
                fields.Add("ID", "[int]");
                fields.Add("Name", "[nvarchar](100)");
                fields.Add("Contact", "[nvarchar](15)");
                fields.Add("OtherContact", "[nvarchar](10)");
                fields.Add("DOB", "[datetime]");
                fields.Add("DOJ", "[datetime]");
                fields.Add("Address", "[nvarchar](200)");
                fields.Add("Email", "[nvarchar](50)");
                fields.Add("Spouse", "[nvarchar](100)");
                fields.Add("SpouseDOB", "[datetime]");
                fields.Add("Anniversary", "[datetime]");
                fields.Add("AgeGroup", "[nvarchar](25)");
                fields.Add("Gender", "[nvarchar](10)"); // male/female/not specified
                fields.Add("Education", "[nvarchar](25)");
                fields.Add("Profession", "[nvarchar](25)");
                fields.Add("SpecificWork", "[nvarchar](25)");
                fields.Add("NatureOfWork", "[nvarchar](25)");
                fields.Add("Chanting", "[nvarchar](25)");
                fields.Add("Married", "[nvarchar](5)"); // yes/no
                fields.Add("Kids", "[nvarchar](25)");
                fields.Add("ChildName", "[nvarchar](50)");
                fields.Add("ChildDOB", "[datetime]");
                fields.Add("Language1", "[nvarchar](25)");
                fields.Add("Language2", "[nvarchar](25)");
                fields.Add("Language3", "[nvarchar](25)");
                fields.Add("ForiegnLang", "[nvarchar](25)");
                fields.Add("Interest", "[nvarchar](25)");
                fields.Add("Category", "[nvarchar](10)");//General/VIP
                fields.Add("ConntoISKON", "[nvarchar](5)");// yes/no
                fields.Add("ISKONMonths", "[int]");
                fields.Add("ISKONYears", "[int]");
                fields.Add("PreachingGroup", "[nvarchar](50)");
                fields.Add("GroupLeaderName", "[nvarchar](50)");
                fields.Add("InitiatedName", "[nvarchar](50)");
                fields.Add("ConntoOther", "[nvarchar](5)");//yes/no
                fields.Add("OtherSpiritOrg", "[nvarchar](50)");
                fields.Add("Locality", "[nvarchar](50)");
                fields.Add("PerAddress", "[nvarchar](150)");
                fields.Add("RefSrc", "[nvarchar](50)");
                fields.Add("RefBy", "[nvarchar](50)");
                fields.Add("Relation", "[nvarchar](20)");
                fields.Add("ExtID", "[nvarchar](20)");
                fields.Add("FamilyID", "[nvarchar](20)");
                CheckTable(db, "Person", fields, new string[] { "ID" });
                break;
            case 6://EventPersonLink
                fields.Add("EventID", "[int]");
                fields.Add("PersonID", "[nvarchar](50)");
                CheckTable(db, "EventPersonLink", fields, new string[] { "EventID", "PersonID" });
                break;
            case 7://Attendance
                fields.Add("ID", "[int]");
                fields.Add("EventID", "[int]");
                fields.Add("PersonID", "[nvarchar](50)");
                CheckTable(db, "Attendance", fields, new string[] { "ID" });
                break;
            case 8://Volunteer
                fields.Add("ID", "[int]");
                fields.Add("Name", "[nvarchar](100)");
                fields.Add("Task", "[nvarchar](100)");
                CheckTable(db, "Volunteer", fields, new string[] { "ID" });
                break;
            default:
                return false; 
        }
        return true;
    }
}

