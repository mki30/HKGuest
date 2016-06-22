using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HkGuest;

public partial class Admin_Location : BasePageAdmin
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            ddParent.Items.Add(new ListItem("-Select-", "0"));
            foreach (Location l in Global.LocationList.Values)
            {
                ddParent.Items.Add(new ListItem(l.Name.ToString(), l.ID.ToString()));
            }
            int ctr = 0;
            foreach (var lt in Enum.GetValues(typeof(LocationType)))
            {
                ddType.Items.Add(new ListItem(lt.ToString(), (ctr++).ToString()));
            }
        }
    }
}