using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HkGuest;

public partial class Admin_Attendance : BasePageAdmin
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        Page.Title = "Attendance";
        if (!IsPostBack)
        {
            foreach (Event evt in Global.EventList.Values)
            {
                ddEvent.Items.Add(new ListItem(evt.EventM == null ? "" : evt.EventM.Name + " " + evt.fromDate.ToString("dd-MMM") + " - " + evt.toDate.ToString("dd-MMM"), evt.ID.ToString()));
            }
        }
    }
}