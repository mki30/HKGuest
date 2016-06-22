using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public class BasePage : System.Web.UI.Page
{
    public string Action = "";
    public string Data1 = "";
    public string Data2 = "";
    public string Data3 = "";
    public string Data4 = "";
    public string Data5 = "";
    public string Data6 = "";

    public string MenuSelection = "product";
    public Boolean Loin_Action = true;
    public Boolean CheckAdmin = false;
    public Boolean CheckLogin = true;
    public User u;
    public BasePage()
    {

    }
    public void PostDataToServer(string Data, string postAction, string BrandID, string MenuID, string D1 = "", string D2 = "", string D3 = "", string D4 = "", Boolean IsLocal = true)
    {
    
        //ServerPath = ServerPath + "/Data.aspx?Action=" + postAction;
        //using (var wc = new WebClient())
        //{

        //    byte[] response = wc.UploadValues(ServerPath, new NameValueCollection()
        //                   {
        //                       { "MyData", Data },{"BrandID",BrandID},{"MenuID",MenuID},
        //                       {"Data1",D1},{"Data2",D2},{"Data3",D3},{"Data4",D4}
        //                   });
        //}
    }

    public int GetFormInt(string FieldName)
    {
        NameValueCollection nvc = Request.Form;
        if (nvc[FieldName] != null)
            return Cmn.ToInt(nvc[FieldName]);
        return 0;
    }

    public double GetFormDbl(string FieldName)
    {
        NameValueCollection nvc = Request.Form;
        if (nvc[FieldName] != null)
            return Cmn.ToDbl(nvc[FieldName]);
        return 0;
    }
    public Boolean IsLocalHost()
    {
        return Request.Url.AbsoluteUri.Contains("localhost");
    }

    public string GetFormString(string FieldName)
    {
        NameValueCollection nvc = Request.Form;
        if (nvc[FieldName] != null)
            return nvc[FieldName];

        return "";
    }
    public void WriteClientScript()
    {
        string str = "";
        GetAllClientID(this, ref str);
        WriteClientScript(str);
    }

    public string RouteString(string Key, string Default = "")
    {
        return RouteData.Values[Key] != null ? RouteData.Values[Key].ToString() : Default;
    }

    public string QueryString(string Key, string Default = "")
    {
        return Request.QueryString[Key] != null ? Request.QueryString[Key].ToString() : Default;
    }

    public int QueryInteger(string Key, int Default = 0)
    {
        return Request.QueryString[Key] != null ? Cmn.ToInt(Request.QueryString[Key]) : Default;
    }

    public string RouteValue(string Name)
    {
        return RouteData.Values[Name] != null ? RouteData.Values[Name].ToString() : "";
    }

    public string GetCookie(string Key)
    {
        HttpCookie c = Request.Cookies.Get(Key);
        return c != null ? c.Value : "";
    }

    public void SetCookie(string Key, string Value)
    {
        HttpCookie c = Response.Cookies.Get(Key);
        if (c != null)
            c.Value = Value;
        else
            Response.Cookies.Add(new HttpCookie(Key, Value));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        string Debug = Request.QueryString["Debug"] != null ? Request.QueryString["Debug"].ToString() : "";
        //if (Debug != "")
        //    Global.Debug = true;

        //if (!DoNotCheckLogin && HttpContext.Current.Request.RawUrl.ToLower().Contains("/edit/"))
        if (Request.Cookies["email"] != null)
        {
            Global.GlobalUser.TryGetValue(Request.Cookies["email"].Value, out u);
            if (u == null || u.IPAddress != Request.UserHostAddress)
                Response.Redirect("/Login.aspx");
        }
        else
            Response.Redirect("/Login.aspx");

        Action = RouteData.Values["Action"] != null ? RouteData.Values["Action"].ToString() : "";
        Data1 = RouteData.Values["Data1"] != null ? RouteData.Values["Data1"].ToString() : "";
        Data2 = RouteData.Values["Data2"] != null ? RouteData.Values["Data2"].ToString() : "";
        Data3 = RouteData.Values["Data3"] != null ? RouteData.Values["Data3"].ToString() : "";
        
        if (Debug == "")
        {
            //compression
            if (!string.IsNullOrEmpty(Request.Headers["Accept-Encoding"]))
            {
                string enc = Request.Headers["Accept-Encoding"].ToUpperInvariant();

                //preferred: gzip or wildcard 
                if (enc.Contains("GZIP") || enc.Contains("*"))
                {
                    Response.AppendHeader("Content-encoding", "gzip");
                    Response.Filter = new GZipStream(Response.Filter, CompressionMode.Compress);
                }

                //deflate 
                else if (enc.Contains("DEFLATE"))
                {
                    Response.AppendHeader("Content-encoding", "deflate");
                    Response.Filter = new DeflateStream(Response.Filter, CompressionMode.Compress);
                }
            }
        }
    }
    public void GetAllClientID(Control parent, ref string strCtl)
    {
        foreach (Control ctl in parent.Controls)
        {
            //if (ctl.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
            if (ctl.ID != null)
                strCtl += "var " + ctl.ID + "=\"#" + ctl.ClientID + "\";\n";

            try
            {
                if (ctl.Controls.Count > 0)
                    GetAllClientID(ctl, ref strCtl);
            }
            catch (System.Exception Ex)
            {
                string str = Ex.Message;
            }
        }
    }
    public void WriteClientScript(string Client_Script)
    {
        ClientScriptManager cs = ClientScript;
        string csname1 = "S1";
        if (!cs.IsClientScriptBlockRegistered(GetType(), csname1))
        {
            StringBuilder cstext2 = new StringBuilder();
            cstext2.Append("<script language='javascript' type=text/javascript> \n");
            cstext2.Append(Client_Script);
            cstext2.Append("</script>");
            cs.RegisterClientScriptBlock(GetType(), csname1, cstext2.ToString(), false);
        }
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        string Script = "";// "PageType=" + (int)pageType + ";\n";
        Script += "Action='" + Action + "';\n";
        Script += "ID='" + Data1 + "';\n";
        Script += "ID2='" + Data2 + "';\n";
        Script += "SelectedMenu='" + MenuSelection + "';\n";
        //Script += "Debug=" + (Global.Debug ? "true" : "false") + ";\n";
        WriteClientScript(Script);
    }

    public Bitmap ResizeImage(Bitmap src, int newWidth, int newHeight)
    {
        Bitmap result = new Bitmap(newWidth, newHeight);
        using (Graphics Gr = Graphics.FromImage((System.Drawing.Image)result))
        {
            Gr.DrawImage(src, 0, 0, newWidth, newHeight);
        }
        return result;
    }
    public string FolderCheck(string Path)
    {
        bool IsExists = Directory.Exists(Server.MapPath(Path));
        if (!IsExists)
            Directory.CreateDirectory(Server.MapPath(Path));
        return Path;
    }
}