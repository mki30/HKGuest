using HkGuest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MarkAttendance : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Attendance";
        base.Page_Load(sender, e);
        lblEventID.Text = Request.QueryString["ID"] == null ? "" : Request.QueryString["ID"].ToString();
        Event evt = Global.EventList.Values.FirstOrDefault(m => m.ID == Cmn.ToInt(lblEventID.Text));
        if (evt != null)
            lblEvent.Text = evt.EventM.Name + " " + evt.fromDate.ToString("dd-MMM hh:mm tt") + " - " + (evt.fromDate.ToString("dd-MMM") == evt.toDate.ToString("dd-MMM") ? evt.toDate.ToString("hh:mm tt") : evt.toDate.ToString("dd-MMM hh:mm tt"));
        if (DateTime.Now <= evt.fromDate.AddMinutes(15))
            btnSave2.Attributes.Remove("disabled");
    }
}