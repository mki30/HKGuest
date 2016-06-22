using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BasePageAdmin
/// </summary>
public class BasePageAdmin:BasePage
{
	public BasePageAdmin()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!u.IsAdmin)
            Response.Redirect("/Login.aspx");
    }
}