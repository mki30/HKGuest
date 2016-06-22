using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Master : BasePageAdmin
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        Title = "Master";
    }
}