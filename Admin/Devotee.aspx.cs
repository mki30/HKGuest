using HkGuest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Devotee : BasePageAdmin
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        Title = "Devotee";
        lblID.Text = Request.QueryString["ID"] == null ? "0" : Request.QueryString["ID"];
        if (!IsPostBack)
        {
            ShowPersonsList();
            ShowPersonDetails();
        }
    }
    protected void ShowPersonsList()
    {
        List<Person> Persons = Global.PersonList.Values.ToList();
        StringBuilder str = new StringBuilder("<table class='table-codensed table-padded'><tr><th>Devotee");
        foreach (Person p in Persons)
        {
            str.Append("<tr><td><a href='/admin/devotee.aspx?ID=" + p.ID + "'>" + p.Name + "</a>");
        }
        str.Append("</table>");
        ltPerson.Text = str.ToString();
    }
    protected void ShowPersonDetails()
    {
        Person p = Global.PersonList.Values.FirstOrDefault(m => m.ID == Cmn.ToInt(lblID.Text));
        if (p == null)
            return;
        txtDevName.Text = p.Name;
        txtContact.Text = p.Contact;
        txtAltContact.Text = p.OtherContact;
        txtDOB.Text = p.DOB.ToString("dd-MMM-yy");
        txtDOJ.Text = p.DOJ.ToString("dd-MMM-yy");
        txtAddress.Text = p.Address.ToString();
        txteMail.Text = p.Email.ToString();
        txtSpouse.Text = p.Spouse.ToString();
        txtSpouseDOB.Text = p.SpouseDOB.ToString("dd-MMM-yy");
        txtAnniversary.Text = p.Anniversary.ToString("dd-MMM-yy");
        ddAgeGroup.SelectedValue = p.AgeGroup;
        ddGender.SelectedValue = p.Gender;
        ddEducation.SelectedValue = p.Education;
        ddProfession.Text = p.Profession;
        txtSpecific_Work.Text = p.SpecificWork;
        ddNature_of_Work.SelectedValue = p.NatureOfWork;
        ddChanting.SelectedValue = p.Chanting;
        ddKids.SelectedValue = p.Kids;
        txtChildName.Text = p.ChildName;
        txtChild_DoB.Text = p.ChildDOB.ToString("dd-MMM-yy");
        ddLanguage1.SelectedValue = p.Language1;
        ddLanguage2.SelectedValue = p.Language2;
        ddLanguage3.SelectedValue = p.Language2;
        ddForiegnLang.SelectedValue = p.ForiegnLang;
        ddInterest.SelectedValue = p.Interest;
        ddCategory.SelectedValue = p.Category;
        ddConn_to_ISKCON.SelectedValue = p.ConntoISKON;
        txtISKCON_Months.Text = p.ISKONMonths.ToString();
        txtISKCON_Years.Text = p.ISKONYears.ToString();
        txtPreaching_Group.Text = p.PreachingGroup;
        txtGroup_Leader_Name.Text = p.GroupLeaderName;
        txtInitiatedName.Text = p.InitiatedName;
        ddConn_To_Other.SelectedValue = p.ConntoOther;
        txtOther_Spirit__Org_.Text = p.OtherSpiritOrg;
        ddLocality.SelectedValue = p.Locality;
        txtPerm_Address.Text = p.PerAddress;
        ddRef_Src.SelectedValue = p.RefSrc;
        ddRelation.SelectedValue = p.Relation;
        txtExt_Id.Text = p.ExtID;
        txtFamilyId.Text = p.FamilyID;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Person p = Global.PersonList.Values.FirstOrDefault(m => m.ID == Cmn.ToInt(lblID.Text));
        if (p == null)
            p = new Person();
        p.Name = txtDevName.Text;
        p.Contact = txtContact.Text;
        p.OtherContact = txtAltContact.Text;
        p.DOB = Cmn.ToDate(txtDOB.Text);
        p.DOJ = Cmn.ToDate(txtDOJ.Text);
        p.Address = txtAddress.Text;
        p.Email = txteMail.Text;
        p.Spouse = txtSpouse.Text;
        p.SpouseDOB = Cmn.ToDate(txtSpouseDOB.Text);
        p.Anniversary = Cmn.ToDate(txtAnniversary.Text);
        p.AgeGroup = ddAgeGroup.SelectedValue;
        p.Gender = ddGender.SelectedValue;
        p.Education = ddEducation.SelectedValue;
        p.Profession = ddProfession.Text;
        p.SpecificWork = txtSpecific_Work.Text;
        p.NatureOfWork = ddNature_of_Work.SelectedValue;
        p.Chanting = ddChanting.SelectedValue;
        p.Kids = ddKids.SelectedValue;
        p.ChildName = txtChildName.Text;
        p.ChildDOB = Cmn.ToDate(txtChild_DoB.Text);
        p.Language1 = ddLanguage1.SelectedValue;
        p.Language2 = ddLanguage2.SelectedValue;
        p.Language3 = ddLanguage3.SelectedValue;
        p.ForiegnLang = ddForiegnLang.SelectedValue;
        p.Interest = ddInterest.SelectedValue;
        p.Category = ddCategory.SelectedValue;
        p.ConntoISKON = ddConn_to_ISKCON.SelectedValue;
        p.ISKONMonths = Cmn.ToInt(txtISKCON_Months.Text);
        p.ISKONYears = Cmn.ToInt(txtISKCON_Years.Text);
        p.PreachingGroup = txtPreaching_Group.Text;
        p.GroupLeaderName = txtGroup_Leader_Name.Text;
        p.InitiatedName = txtInitiatedName.Text;
        p.ConntoOther = ddConn_To_Other.SelectedValue;
        p.OtherSpiritOrg = txtOther_Spirit__Org_.Text;
        p.Locality = ddLocality.SelectedValue;
        p.PerAddress = txtPerm_Address.Text;
        p.RefSrc = ddRef_Src.SelectedValue;
        p.Relation = ddRelation.SelectedValue;
        p.ExtID = txtExt_Id.Text;
        p.FamilyID = txtFamilyId.Text;
        p.Save();
        lblSave.Text = lblID.Text == "0" ? "Person Added Successfully" : "Updated Successfully";
        ShowPersonsList();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/devotee.aspx");
    }
}