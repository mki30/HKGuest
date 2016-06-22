using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Linq;
using System.Collections.Specialized;
using System.Text.RegularExpressions;


public class Cmn
{
    const Boolean UseCache = true;
    public Cmn()
    {
    }
    public static DateTime MinDate = new DateTime(1900, 1, 1);
    //public static string GetCompanyProjectImageFileName(int ID, string path)
    //{
    //    Business bus = Global.BusniessList.Values.FirstOrDefault(m => m.ID == ID);
    //    string fileName = "";
    //    for (int i = 1; ; i++)
    //    {
    //        if (!File.Exists(HttpContext.Current.Server.MapPath(path + "/" + bus.UrlName + "_" + i) + ".jpg"))
    //        {
    //            fileName = bus.UrlName + "_" + i;
    //            break;
    //        }
    //    }
    //    return fileName;
    //}
    //public static Dictionary<string, ImagesDetail> getImagesSequenced(int businessID, int Type, string folder = "", string SearchPattern = "")
    //{
    //    Dictionary<string, ImagesDetail> ImageFilesDic = new Dictionary<string, ImagesDetail>();
    //    List<ImagesDetail> ImgDetailList = ImagesDetail.GetByID(businessID, Type);
    //    foreach (ImagesDetail imgd in ImgDetailList)
    //    {
    //        if (!ImageFilesDic.ContainsKey(imgd.ImageID))
    //        {
    //            ImageFilesDic.Add(imgd.ImageID, imgd); //temp work please review as project moved to data
    //        }
    //    }
    //    return ImageFilesDic.OrderBy(x => x.Value.Sequence).ToDictionary(a => a.Key, a => a.Value);
    //}
    //public static Dictionary<string, ImagesDetail> getImagesSequenced(int CompanyID, int ReferenceID, int ImageType, string folder = "", string SearchPattern = "")
    //{
    //    //string[] ImageFiles = Directory.GetFiles(folder, SearchPattern);
    //    Dictionary<string, ImagesDetail> ImageFilesDic = new Dictionary<string, ImagesDetail>();
    //    //foreach (string f in ImageFiles)
    //    //{
    //    //    ImageFilesDic.Add(Path.GetFileName(f), new ImagesDetail());
    //    //}
    //    List<ImagesDetail> ImgDetailList = ImagesDetail.GetByIDs(CompanyID, ReferenceID, ImageType);
    //    foreach (ImagesDetail imgd in ImgDetailList)
    //    {
    //        if (!ImageFilesDic.ContainsKey(imgd.ImageID))
    //        {
    //            ImageFilesDic.Add(imgd.ImageID, imgd); //temp work please review as project moved to data
    //            //ImageFilesDic[imgd.ImageID] = imgd;
    //        }
    //    }
    //    return ImageFilesDic.OrderBy(x => x.Value.Sequence).ToDictionary(a => a.Key, a => a.Value);
    //}
    public static int DayByName(string day)
    {
        switch (day.ToUpper())
        {
            case "MON": return 0;
            case "TUE": return 1;
            case "WED": return 2;
            case "THU": return 3;
            case "FRI": return 4;
            case "SAT": return 5;
            case "SUN": return 6;
        }
        return -1;
    }
    public static int BusinessType(string type)
    {
        switch (ProperCase(type))
        {
            case "Shop": return 0;
            case "Service": return 1;
            case "Building": return 2;
            case "Floor": return 3;
            case "Bank": return 4;
            case "ATM": return 5;
            case "School": return 6;
            case "Restaurant": return 6;
            case "Medical": return 6;
        }
        return 0;
    }
    public static void DeleteExistingFile(string folder, string id)
    {
        string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath(folder), "" + id + "*");
        if (files.Length > 0)
        {
            foreach (string s in files)
            {
                try
                {
                    string fileame = s.Split('\\').Last();
                    FileInfo fi = new FileInfo(HttpContext.Current.Server.MapPath(folder + "\\" + fileame + ""));
                    if (fi != null)
                        fi.Delete();
                }
                catch (Exception ex) { };
            }
        }

    }
    public static string GetEncode(Page pg)
    {
        string encodings = pg.Request.Headers.Get("Accept-Encoding");
        string encode = "no";

        if (encodings != null)
        {
            encodings = encodings.ToLower();
            if (encodings.Contains("gzip"))
            {
                pg.Response.AppendHeader("Content-Encoding", "gzip");
                encode = "gzip";
            }
            else if (encodings.Contains("deflate"))
            {
                pg.Response.AppendHeader("Content-Encoding", "deflate");
                encode = "deflate";
            }
        }
        pg.Response.AppendHeader("Content-Encoding", "gzip");

        return encode;
    }

    public static void WriteFile(string str, string FileName, string CompressionType)
    {
        byte[] buffer = System.Text.Encoding.ASCII.GetBytes(str);

        switch (CompressionType)
        {
            case "gzip":
                {
                    FileStream sw = new FileStream(FileName, FileMode.Create);
                    GZipStream gz = new GZipStream(sw, CompressionMode.Compress);
                    gz.Write(buffer, 0, buffer.Length);
                    gz.Close();
                    sw.Close();
                }
                break;
            case "deflate":
                {
                    FileStream sw = new FileStream(FileName, FileMode.Create);
                    DeflateStream dz = new DeflateStream(sw, CompressionMode.Compress);
                    dz.Write(buffer, 0, buffer.Length);
                    dz.Close();
                    sw.Close();
                }
                break;
            default:
                {
                    StreamWriter sw = new StreamWriter(FileName, false);
                    sw.Write(str);
                    sw.Close();
                }
                break;
        }

        File.SetCreationTime(FileName, DateTime.Now);
    }

    public static void WriteResponse(Page p, string Message, string Compression)
    {
        if (Compression == "no" || string.IsNullOrEmpty(Compression))
            p.Response.Write(Message);
        else
            p.Response.BinaryWrite(GetCompressed(Message, Compression));
    }

    public static byte[] GetCompressed(string str, string CompressionType = "gzip")
    {
        byte[] buffer = System.Text.Encoding.ASCII.GetBytes(str);
        MemoryStream ms = new MemoryStream();

        switch (CompressionType)
        {
            case "gzip":
                {
                    GZipStream gz = new GZipStream(ms, CompressionMode.Compress, true);
                    gz.Write(buffer, 0, buffer.Length);
                    gz.Close();

                }
                break;
            case "deflate":
                {
                    DeflateStream dz = new DeflateStream(ms, CompressionMode.Compress);
                    dz.Write(buffer, 0, buffer.Length);
                    dz.Close();
                }
                break;
        }

        byte[] compressedData = (byte[])ms.ToArray();

        //string result = System.Text.Encoding.Unicode.GetString(compressedData);

        return compressedData;
    }


    public static string GetUnCompressed(byte[] Data, int Size)
    {
        if (Data == null)
            return string.Empty;
        MemoryStream ms = new MemoryStream(Data);
        GZipStream gz = null;
        try
        {

            gz = new GZipStream(ms, CompressionMode.Decompress);
            byte[] decompressedBuffer = new byte[Size];
            int DataLength = gz.Read(decompressedBuffer, 0, Size);
            using (MemoryStream msDec = new MemoryStream())
            {
                msDec.Write(decompressedBuffer, 0, DataLength);
                msDec.Position = 0;
                string s = new StreamReader(msDec).ReadToEnd();
                return s;
            }
        }
        catch
        {
            //return ex.Message;
        }
        finally
        {
            ms.Close();
            gz.Close();
        }

        return string.Empty;
    }

    public static string CheckString(string text)
    {
        return text.Replace("'", "").Replace("\"", "").Replace("@", "").Replace("?", "").Replace("*", "");
    }

    public static void LogException(string ModuleName, string FunctionName, Exception ex)
    {
        if (HttpContext.Current.Application["ERROR_IDS"] == null)
        {
            HttpContext.Current.Application["ERROR_IDS"] = new Dictionary<string, int>();
            HttpContext.Current.Application["ERROR_LIST"] = new Dictionary<int, string>();
        }

        Dictionary<string, int> ErrorLog = (Dictionary<string, int>)HttpContext.Current.Application["ERROR_IDS"];
        if (!ErrorLog.ContainsKey(ModuleName + "_" + FunctionName))
        {
            ErrorLog.Add(ModuleName + "_" + FunctionName, ErrorLog.Count + 1);
        }

        //get the id of the error
        int i = ErrorLog[ModuleName + "_" + FunctionName];
        Dictionary<int, string> ErrorList = (Dictionary<int, string>)HttpContext.Current.Application["ERROR_LIST"];

        if (!ErrorList.ContainsKey(i))
            ErrorList.Add(i, "");

        ErrorList[i] = ModuleName + ":" + FunctionName + ":" + ex.Message + ":" + DateTime.Now.ToString();

    }

    public static string ValidateInput(string Data, int Length, Boolean CheckforValidDate, Boolean ConvertToUpper, Boolean CleanInput)
    {
        if (Length > 0)
            if (Data.Length > Length)
                Data = Data.Substring(0, Length);

        if (ConvertToUpper)
            Data = Data.ToUpper();

        if (CleanInput)
            Data = Data.Replace("'", "").Replace("%", "").Replace("*", "").Replace(" ", "");

        return Data;
    }

    //public static Boolean IsLoggedIn(Page p, Boolean DoAdminCheck = false)
    //{
    //    if (p.Request.Url.AbsoluteUri.Contains("localhost"))
    //    {

    //        //Global.LoginType = Login.LoginType.LT_Admin;
    //    }

    //    if (!Global.LogInDone)
    //    {
    //        WriteClientScript(p, "top.window.location='" + p.ResolveUrl(@"~\Login") + "';");
    //        return false;
    //    }

    //    if (!Global.IsAdmin && DoAdminCheck)
    //    {
    //        p.Response.Redirect("AdminAccess.aspx");
    //        return false;
    //    }

    //    return true;
    //}

    public static DateTime GetIndiaTime()
    {
        return DateTime.Now.ToUniversalTime().AddHours(5).AddMinutes(30);
    }

    public static void WriteClientScript(Page pg, string Client_Script)
    {
        ClientScriptManager cs = pg.ClientScript;
        string csname1 = "S1";
        if (!cs.IsClientScriptBlockRegistered(pg.GetType(), csname1))
        {
            StringBuilder cstext2 = new StringBuilder();
            cstext2.Append("<script language='javascript' type='text/javascript'> \n");
            cstext2.Append(Client_Script);
            cstext2.Append("</script>");
            cs.RegisterClientScriptBlock(pg.GetType(), csname1, cstext2.ToString(), false);
        }
    }

    public static DateTime ToDate(object txt)
    {
        DateTime X;
        if (DateTime.TryParse(txt.ToString(), out X) == false)
            return Cmn.GetIndiaTime();

        return X;
    }

    public static decimal ToDec(TextBox txt)
    {
        decimal X;
        if (decimal.TryParse(txt.Text, out X) == false)
            return 0;

        return X;
    }

    public static decimal ToDec(string txt)
    {
        decimal X;
        if (decimal.TryParse(txt, out X) == false)
            return 0;

        return X;
    }

    public static double ToDbl(object txt)
    {
        if (txt == null)
            return 0;
        double X;
        if (double.TryParse(txt.ToString(), out X) == false)
            return 0;

        return X;
    }

    public static int ToInt(TextBox txt)
    {
        int X;
        if (int.TryParse(txt.Text, out X) == false)
            return 0;

        return X;
    }

    public static int ToInt(string txt, int DefaultValue)
    {
        int X;
        if (int.TryParse(txt, out X) == false)
            return DefaultValue;

        return X;
    }

    public static int ToInt(object txt)
    {
        if (txt == null)
            return 0;
        int X;
        if (int.TryParse(txt.ToString(), out X) == false)
            return 0;

        return X;
    }

    public static string ProperCase(string str)
    {
        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        TextInfo textInfo = cultureInfo.TextInfo;
        return textInfo.ToTitleCase(str.Trim().ToLower());
    }

    public static void ClearTextBoxes(Control parent)
    {
        foreach (Control ctl in parent.Controls)
        {
            if (ctl.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
                ((TextBox)ctl).Text = "";

            if (ctl.GetType().ToString().Equals("System.Web.UI.WebControls.CheckBox"))
                ((CheckBox)ctl).Checked = false;

            if (ctl.GetType().ToString().Equals("System.Web.UI.WebControls.DropDownList"))
                ((DropDownList)ctl).SelectedIndex = 0;

            if (ctl.Controls.Count > 0)
                ClearTextBoxes(ctl);
        }
    }

    public static void LogError(Exception ex = null, string Message = "")
    {
        string FolderPath = HttpContext.Current.Server.MapPath(@"~\Error");
        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        string Filename = FolderPath + @"\Error.txt";
        string Error = DateTime.Now.ToString() + Environment.NewLine;

        if (!string.IsNullOrWhiteSpace(Message))
            Error += Message + Environment.NewLine;

        if (ex != null)
        {
            Error += ex.Message + Environment.NewLine;
        }
        File.AppendAllText(Filename, Error);
    }

    public static void GetAllClientID(Control parent, ref string strCtl)
    {
        foreach (Control ctl in parent.Controls)
        {
            if (ctl.ID != null)
                strCtl += "var " + ctl.ID + "=\"#" + ctl.ClientID + "\";\n";

            try
            {
                if (ctl.Controls.Count > 0)
                    GetAllClientID(ctl, ref strCtl);
            }
            catch (Exception Ex)
            {
                string str = Ex.Message;
            }
        }
    }

    public static string ConvertNumberToWord(Int32 numberVal)
    {
        string[] powers = new string[] { "Thousand ", "Million ", "Billion " };
        string[] ones = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        string wordValue = "";
        if (numberVal == 0) return "Zero";
        if (numberVal < 0)
        {
            wordValue = "Negative ";
            numberVal = -numberVal;
        }
        long[] partStack = new long[] { 0, 0, 0, 0 };
        int partNdx = 0;
        while (numberVal > 0)
        {
            partStack[partNdx++] = numberVal % 1000;
            numberVal /= 1000;
        }
        for (int i = 3; i >= 0; i--)
        {
            long part = partStack[i];
            if (part >= 100)
            {
                wordValue += ones[part / 100 - 1] + " Hundred ";
                part %= 100;
            }
            if (part >= 20)
            {
                if ((part % 10) != 0) wordValue += tens[part / 10 - 2] + " " + ones[part % 10 - 1] + " ";
                else wordValue += tens[part / 10 - 2] + " ";
            }
            else if (part > 0) wordValue += ones[part - 1] + " ";
            if (part != 0 && i > 0) wordValue += powers[i - 1];
        }
        return wordValue;
    }
    public static string GenerateSlug(string phrase)
    {
        string str = RemoveAccent(phrase).Replace("&", "and").ToLower();
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        str = Regex.Replace(str, @"\s+", "-").Trim();
        str = Regex.Replace(str, @"\-+", "-").Trim();

        return str;
    }
    public static string RemoveAccent(string txt)
    {
        byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
        return System.Text.Encoding.ASCII.GetString(bytes);
    }
    //public static string GetFormString(string FieldName)
    //{
    //    NameValueCollection nvc = HttpContext.Current.Request.Form;
    //    if (nvc[FieldName] != null)
    //        return (nvc[FieldName]);

    //    return "";
    //}
}
public class ImageDetailJSC
{
    public string id { get; set; }
    public int seq { get; set; }
}