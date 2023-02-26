using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace ServiceDesk.DAL
{
    public class IncidentRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ServiceDeskContext"].ToString();
            con = new SqlConnection(constr);

        }

        public bool AddIncident(Incident obj)
        {

            connection();
            string query = "Insert into Incidents(CustomerID,ProductCode,Title,Description,DateOpened) values(@CustomerID , @ProductCode,@Title,@Description,@DateOpened)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
            cmd.Parameters.AddWithValue("@ProductCode", obj.ProductCode);
            cmd.Parameters.AddWithValue("@Title", obj.Title);
            cmd.Parameters.AddWithValue("@Description", obj.Description);
            cmd.Parameters.AddWithValue("@DateOpened", obj.DateOpened);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Incident> GetAllIncidents()
        {
            connection();
            List<Incident> IncidentList = new List<Incident>();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Incidents", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                int? Tech_Id = null;
                DateTime? Date_Closed = null;
                if (dr["TechID"] != DBNull.Value)
                {
                    Tech_Id = Convert.ToInt32((dr["TechID"]));
                }
                if (dr["DateClosed"] != DBNull.Value)
                {
                    Date_Closed = Convert.ToDateTime((dr["DateClosed"]));
                }

                IncidentList.Add(
                    new Incident
                    {
                        IncidentID = Convert.ToInt32(dr["IncidentID"]),
                        Title = Convert.ToString(dr["Title"]),
                        Description = Convert.ToString(dr["Description"]),
                        TechID = Tech_Id,
                        CustomerID = Convert.ToInt32(dr["CustomerID"]),
                        ProductCode = Convert.ToInt32(dr["ProductCode"]),
                        DateOpened = Convert.ToDateTime(dr["DateOpened"]),
                        DateClosed = Date_Closed
                    }
                    );
            }
            return IncidentList;
        }

        public bool ResolveIncident(Incident obj)
        {

            connection();
            con.Open();
            string query = "Update Incidents SET techid=@techid where incidentid=@incidentid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@techid", obj.TechID);
            cmd.Parameters.AddWithValue("@incidentid", obj.IncidentID);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteIncident(int Id)
        {

            connection();
            string query = "Delete from Incidents where incidentid=@incidentid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@incidentid", Id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}