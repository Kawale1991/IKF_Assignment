using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using WebGrease;
using IKF.BAIKF;
using System.Configuration;
using System.Xml.Linq;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Web.UI;

namespace IKF.DAIKF
{
    public class DOIKF
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter da;
        DataTable dt;
        public BOIKF fninsertdata(BOIKF objBOIKF)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NAME", objBOIKF.Name);
                    command.Parameters.AddWithValue("@DOB", objBOIKF.Dob);
                    command.Parameters.AddWithValue("@DESIGNATION", objBOIKF.Designation);
                    command.Parameters.AddWithValue("@SKILLS", objBOIKF.Skills);
                    command.Parameters.AddWithValue("@ACTION", "INSERT");
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return objBOIKF;
        }

        public BOIKF fnupdatedata(BOIKF objBOIKF)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", objBOIKF.ID);
                    command.Parameters.AddWithValue("@NAME", objBOIKF.Name);
                    command.Parameters.AddWithValue("@DOB", objBOIKF.Dob);
                    command.Parameters.AddWithValue("@DESIGNATION", objBOIKF.Designation);
                    command.Parameters.AddWithValue("@SKILLS", objBOIKF.Skills);
                    command.Parameters.AddWithValue("@ACTION", objBOIKF.Action);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return objBOIKF;
        }

        public Boolean fndeletedata(BOIKF objBOIKF)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", objBOIKF.ID);
                command.Parameters.AddWithValue("@ACTION", objBOIKF.Action);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    objBOIKF.Rcount = true;
                }
                else
                {
                    objBOIKF.Rcount = false;
                }
                return objBOIKF.Rcount;
            }
        }

        public BOIKF fneditdata(BOIKF objBOIKF)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", objBOIKF.ID);
                command.Parameters.AddWithValue("@ACTION", objBOIKF.Action);
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                objBOIKF.rdt = dt;
                if (objBOIKF.rdt.Rows.Count > 0)
                {
                    objBOIKF.Name = Convert.ToString(dt.Rows[0]["Name"]);
                    objBOIKF.Dob = Convert.ToDateTime(dt.Rows[0]["Dob"]);
                    objBOIKF.Designation = Convert.ToString(dt.Rows[0]["Designation"]);
                    objBOIKF.Skills = Convert.ToString(dt.Rows[0]["Skills"]);
                }
                connection.Close();
            }
            return objBOIKF;
        }

        public BOIKF Getalldata(BOIKF objBOIKF)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ACTION", objBOIKF.Action);
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                objBOIKF.rdt = dt;
                connection.Close();
            }
            return objBOIKF;
        }

        public BOIKF LoadSkills(BOIKF objBOIKF)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ACTION", objBOIKF.Action);
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                objBOIKF.rdt = dt;
                connection.Close();
            }
            return objBOIKF;
        }

        public BOIKF LoadDesignation(BOIKF objBOIKF)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("IKFCRUD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ACTION", objBOIKF.Action);
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                objBOIKF.rdt = dt;
                connection.Close();
            }
            return objBOIKF;
        }
    }
}