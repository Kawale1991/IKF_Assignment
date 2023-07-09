using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IKF.BAIKF;
using IKF.DAIKF;
using Antlr.Runtime.Tree;
using System.Runtime.Remoting.Messaging;

namespace IKF.IKFWEB
{
    public partial class IKFCRUD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSkills();
                LoadDesignation();
                btnUpdate.Enabled = false;
                divContainer.Visible = false;
                Maintable.Visible = false;
            }
        }

        private void Getalldata()
        {
            BOIKF objBOIKF = new BOIKF();
            objBOIKF.Action = "GETDATA";
            DOIKF objDOIKF = new DOIKF();
            objDOIKF.Getalldata(objBOIKF);
            Grddata.DataSource = objBOIKF.rdt;
            Grddata.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length > 0 && txtDOB.Text.Length > 0)
            {
                BOIKF OBJBOIKF = new BOIKF();
                OBJBOIKF.Name = txtName.Text;
                OBJBOIKF.Dob = DateTime.Parse(txtDOB.Text);
                OBJBOIKF.Designation = ddlDesignation.SelectedItem.Text;
                OBJBOIKF.Skills = GetSelectedSkills();
                OBJBOIKF.Action = "INSERT";
                DOIKF OBJDAIKF = new DOIKF();
                OBJDAIKF.fninsertdata(OBJBOIKF);
                Getalldata();
                Grddata.SelectedIndex = -1;
                ShowAlert("Data saved successfully!", "success");
                divContainer.Visible = false;
                btnClear_Click(sender, e);
            }
            else
            {
                ShowAlert("Please Enter Name,DOB and Skills and proceed further!", "danger");
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length > 0 && txtDOB.Text.Length > 0)
            {
                dynamic HValue = ViewState["HIKFID"];
                BOIKF OBJBOIKF = new BOIKF();
                OBJBOIKF.ID = Convert.ToInt32(HValue);
                OBJBOIKF.Name = txtName.Text;
                OBJBOIKF.Dob = DateTime.Parse(txtDOB.Text);
                OBJBOIKF.Designation = ddlDesignation.SelectedItem.Text;
                OBJBOIKF.Skills = GetSelectedSkills();
                OBJBOIKF.Action = "UPDATE";
                DOIKF OBJDAIKF = new DOIKF();
                OBJDAIKF.fnupdatedata(OBJBOIKF);
                Getalldata();
                Grddata.SelectedIndex = -1;
                ShowAlert("Data Updated successfully!", "success");
                divContainer.Visible = false;
                btnClear_Click(sender, e);
            }
            else
            {
                ShowAlert("Please Enter Name,DOB and Skills and proceed further!", "danger");
            }
        }
        protected void Grddata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDelete = e.Row.FindControl("btnDelete") as LinkButton;
                if (lnkDelete != null)
                {
                    lnkDelete.OnClientClick = "return confirm('Are you sure you want to delete?');";
                }
            }
        }
        protected void Grddata_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BOIKF OBJBOIKF = new BOIKF();
            GridViewRow row = Grddata.Rows[e.RowIndex];
            HiddenField HIKFID = row.FindControl("IKF") as HiddenField;
            int FinalDeleteValue = Convert.ToInt32(HIKFID.Value);
            OBJBOIKF.ID = FinalDeleteValue;
            OBJBOIKF.Action = "DELETE";
            DOIKF OBJDOIKF = new DOIKF();
            OBJDOIKF.fndeletedata(OBJBOIKF);
            if (OBJBOIKF.Rcount == true)
            {
                ShowAlert("User deleted successfully!", "success");
                Getalldata();
                Grddata.SelectedIndex = -1;
                divContainer.Visible = false;
                Page_Load(sender, e);
                btnClear_Click(sender, e);
            }
            else
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }
        private void LoadSkills()
        {
            BOIKF objBOIKF = new BOIKF();
            objBOIKF.Action = "GETSKILLS";
            DOIKF objDOIKF = new DOIKF();
            objDOIKF.LoadSkills(objBOIKF);
            ddlSkills.DataSource = objBOIKF.rdt;
            ddlSkills.DataTextField = "SkillName";
            ddlSkills.DataValueField = "SkillID";
            ddlSkills.DataBind();
        }
        private void LoadDesignation()
        {
            BOIKF objBOIKF = new BOIKF();
            objBOIKF.Action = "LOADDESIGNATION";
            DOIKF objDOIKF = new DOIKF();
            objDOIKF.LoadDesignation(objBOIKF);
            ddlDesignation.DataSource = objBOIKF.rdt;
            ddlDesignation.DataTextField = "DesignationName";
            ddlDesignation.DataValueField = "DesignationID";
            ddlDesignation.DataBind();
        }
        private string GetSelectedSkills()
        {
            string selectedSkills = string.Empty;
            foreach (ListItem item in ddlSkills.Items)
            {
                if (item.Selected)
                {
                    selectedSkills += item.Text + ",";
                }
            }
            return selectedSkills.TrimEnd(',');
        }
        private void SetSelectedSkills(string skills)
        {
            string[] selectedSkills = skills.Split(',');
            foreach (ListItem item in ddlSkills.Items)
            {
                item.Selected = selectedSkills.Contains(item.Text);
            }
        }

        protected void Grddata_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                BOIKF OBJBOIKF = new BOIKF();
                int RowIndex = Convert.ToInt32(e.CommandArgument);
                HiddenField HIKFID = (HiddenField)Grddata.Rows[RowIndex - 1].FindControl("IKF");
                ViewState["HIKFID"] = HIKFID.Value;
                OBJBOIKF.ID = Convert.ToInt32(HIKFID.Value);
                OBJBOIKF.Action = "EDITDATA";
                DOIKF OBJDOIKF = new DOIKF();
                OBJDOIKF.fneditdata(OBJBOIKF);
                txtName.Text = OBJBOIKF.Name;
                txtDOB.Text = Convert.ToDateTime(OBJBOIKF.Dob).ToShortDateString();
                ddlDesignation.SelectedItem.Text = OBJBOIKF.Designation;
                string skill = OBJBOIKF.Skills;
                SetSelectedSkills(skill);
                btnSubmit.Enabled = false;
                btnUpdate.Enabled = true;
                divContainer.Visible = true;
            }
        }

        protected void Grddata_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            Getalldata();
            divContainer.Visible = true;
            Maintable.Visible = true;
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            LoadSkills();
            LoadDesignation();
            Maintable.Visible = true;
            divContainer.Visible = false;
            txtName.Text = "";
            txtDOB.Text = "";
            ddlSkills.ClearSelection();
            ddlDesignation.SelectedItem.Text = "All";
        }

        protected void ShowAlert(string message, string alertType)
        {
            var alertDiv = new HtmlGenericControl("div");
            alertDiv.Attributes["class"] = "alert alert-" + alertType;
            alertDiv.InnerText = message;
            alertContainer.Controls.Add(alertDiv);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtDOB.Text = "";
            ddlSkills.ClearSelection();
            ddlDesignation.SelectedItem.Text = "All";
            divContainer.Visible = false;
        }
    }
}