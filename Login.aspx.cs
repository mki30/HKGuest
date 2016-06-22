using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string script = string.Empty;
            if (Request.QueryString["as"] != null)
            {
                script = ";Impersonate='" + Request.QueryString["as"] + "';";
            }
            if (Request.QueryString["logout"] != null)
            {
                string Email = Request.Cookies["email"] != null ? Request.Cookies["email"].Value.ToString() : "";

                if (Global.GlobalUser.ContainsKey(Email))
                    Global.GlobalUser.Remove(Email);

                script += "DoSignOut=true;";
            }
            if (script != string.Empty)
                Cmn.WriteClientScript(this, script);
        }
    }
}