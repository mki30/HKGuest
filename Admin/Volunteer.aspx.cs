using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HkGuest;
using System.Text;

public partial class Admin_Volunteer : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            int ID=Request.QueryString["ID"]!=null?Cmn.ToInt(Request.QueryString["ID"]):0;
            ShowVolunteer(ID);
            ShowVolunteerList();
        }
    }
    void ShowVolunteerList()
    {
        List<Volunteer> VolunteerList = Volunteer.GetAll();
        StringBuilder str = new StringBuilder("<table><tr><th>Volunteers");
        foreach (Volunteer v in VolunteerList)
        {
            str.Append("<tr><td><a href='?ID=" + v.ID + "'>" + v.Name + "</a>");
        }
        ltVolunteer.Text = str.Append("</table>").ToString();
    }
    void ShowVolunteer(int ID)
    {
        lblID.Text = ID.ToString();
        Volunteer v=Volunteer.Get(ID);
        if(v!=null)
        {
            //EditHead.InnerText = "Edit Volunteer";
            txtVolunteerName.Text = v.Name;
            txtTask.InnerText = v.Task;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Volunteer v = Volunteer.Get(Cmn.ToInt(lblID.Text));
        if (v == null)
            v = new Volunteer();
        v.Name = txtVolunteerName.Text;
        v.Task = txtTask.Value;
        v.Save();
        ShowVolunteerList();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        lblID.Text = "";
        txtTask.Value = "";
        txtVolunteerName.Text = "";
        //EditHead.InnerText = "Add New Volunteer";
    }
}