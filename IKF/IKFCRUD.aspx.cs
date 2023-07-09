using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.DynamicData;
using System.Xml.Linq;
using Microsoft.Ajax.Utilities;
using System.ComponentModel;
using System.Web.UI.HtmlControls;

namespace IKF
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
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ACTION", "GETDATA");

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Grddata.DataSource = reader;
                            Grddata.DataBind();
                        }
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length > 0 && txtDOB.Text.Length > 0  )
            {
                string name = txtName.Text;
                DateTime dob = DateTime.Parse(txtDOB.Text);
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NAME", name);
                        command.Parameters.AddWithValue("@DOB", dob);
                        command.Parameters.AddWithValue("@DESIGNATION", ddlDesignation.SelectedItem.Text);
                        command.Parameters.AddWithValue("@SKILLS", GetSelectedSkills());
                        command.Parameters.AddWithValue("@ACTION", "INSERT");
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
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
            if (txtName.Text.Length > 0 && txtDOB.Text.Length > 0 )
            {
                dynamic HValue = ViewState["HIKFID"];
                string name = txtName.Text;
                DateTime dob = DateTime.Parse(txtDOB.Text);
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID", HValue);
                        command.Parameters.AddWithValue("@NAME", name);
                        command.Parameters.AddWithValue("@DOB", dob);
                        command.Parameters.AddWithValue("@DESIGNATION", ddlDesignation.SelectedItem.Text);
                        command.Parameters.AddWithValue("@SKILLS", GetSelectedSkills());
                        command.Parameters.AddWithValue("@ACTION", "UPDATE");
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                Getalldata();
                Grddata.SelectedIndex = -1;
                ShowAlert("Data Updated successfully!", "success");
                divContainer.Visible = false;
                btnClear_Click(sender, e);
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
            GridViewRow row = Grddata.Rows[e.RowIndex];
            HiddenField HIKFID = row.FindControl("IKF") as HiddenField;
            int FinalDeleteValue = Convert.ToInt32(HIKFID.Value);
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", FinalDeleteValue);
                command.Parameters.AddWithValue("@ACTION", "DELETE");
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
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
        }
        private void LoadSkills()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ACTION", "GETSKILLS");
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ddlSkills.DataSource = dt;
                ddlSkills.DataTextField = "SkillName";
                ddlSkills.DataValueField = "SkillID";
                ddlSkills.DataBind();
                connection.Close();
            }
        }
        private void LoadDesignation()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ACTION", "LOADDESIGNATION");
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ddlDesignation.DataSource = dt;
                ddlDesignation.DataTextField = "DesignationName";
                ddlDesignation.DataValueField = "DesignationID";
                ddlDesignation.DataBind();
                connection.Close();
            }
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
                int RowIndex = Convert.ToInt32(e.CommandArgument);
                HiddenField HIKFID = (HiddenField)Grddata.Rows[RowIndex - 1].FindControl("IKF");
                ViewState["HIKFID"] = HIKFID.Value;
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", HIKFID.Value);
                    command.Parameters.AddWithValue("@ACTION", "EDITDATA");
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        txtName.Text = reader["Name"].ToString();
                        txtDOB.Text = Convert.ToDateTime(reader["Dob"]).ToShortDateString();
                        ddlDesignation.SelectedItem.Text = reader["Designation"].ToString();
                        SetSelectedSkills(reader["Skills"].ToString());
                    }
                    connection.Close();
                }
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