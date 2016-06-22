using HkGuest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Event : BasePageAdmin
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            ddEvent.Items.Add(new ListItem("Select", ""));
            foreach (EventMaster ev in EventMaster.GetAll())
            {
                ddEvent.Items.Add(new ListItem(ev.Name, ev.ID.ToString()));
            }
            ddLocation.Items.Add(new ListItem("Select", ""));
            ddLocationFilter.Items.Add(new ListItem("Location", ""));
            foreach (Location l in Location.GetAll())
            {
                ddLocation.Items.Add(new ListItem(l.Name, l.ID.ToString()));
                ddLocationFilter.Items.Add(new ListItem(l.Name, l.ID.ToString()));
            }
            ddDepartment.Items.Add(new ListItem("Select", ""));
            foreach (Department d in Department.GetAll())
            {
                ddDepartment.Items.Add(new ListItem(d.Name, d.ID.ToString()));
            }
            ddResponsiblePerson.Items.Add(new ListItem("Select", ""));
            foreach (Person p in Person.GetAll())
            {
                ddResponsiblePerson.Items.Add(new ListItem(p.Name, p.ID.ToString()));
            }
            ddVolunteer1.Items.Add(new ListItem("Select", ""));
            ddVolunteer2.Items.Add(new ListItem("Select", ""));
            foreach (Volunteer v in Volunteer.GetAll())
            {
                ddVolunteer1.Items.Add(new ListItem(v.Name, v.ID.ToString()));
                ddVolunteer2.Items.Add(new ListItem(v.Name, v.ID.ToString()));
            }
            ddFromtime.Items.Add(new ListItem("Select", ""));
            ddTotime.Items.Add(new ListItem("Select", ""));
            DateTime dt = DateTime.Today;
            for (int i = 0; i <= 1440; i += 30)
            {
                ddFromtime.Items.Add(new ListItem(dt.AddMinutes(i).ToString("hh:mm tt"), dt.AddMinutes(i).ToString("hh:mm tt")));
                ddTotime.Items.Add(new ListItem(dt.AddMinutes(i).ToString("hh:mm tt"), dt.AddMinutes(i).ToString("hh:mm tt")));
            }

        }
    }
}