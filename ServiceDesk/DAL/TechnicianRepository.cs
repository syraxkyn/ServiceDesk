using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ServiceDesk.DAL
{
    public class TechnicianRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ServiceDeskContext"].ToString();
            con = new SqlConnection(constr);

        }

        public bool AddTechnician(Technician obj)
        {

            connection();
            string query = "Insert into Technicians values(@name, @email , @phone)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", obj.Name);
            cmd.Parameters.AddWithValue("@email", obj.Email);
            cmd.Parameters.AddWithValue("@phone", obj.Phone);

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

        public List<Technician> GetAllTechnicians()
        {
            connection();
            List<Technician> TechnicianList = new List<Technician>();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Technicians", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {

                TechnicianList.Add(

                    new Technician
                    {

                        TechID = Convert.ToInt32(dr["TechID"]),
                        Name = Convert.ToString(dr["Name"]),
                        Email = Convert.ToString(dr["Email"]),
                        Phone = Convert.ToString(dr["Phone"])

                    }
                    );
            }
            return TechnicianList;
        }

        public bool UpdateTechnician(Technician obj)
        {

            connection();
            con.Open();
            string query = "Update Technicians SET name=@name, email=@email , phone=@phone where techid=@techid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", obj.Name);
            cmd.Parameters.AddWithValue("@email", obj.Email);
            cmd.Parameters.AddWithValue("@phone", obj.Phone);
            cmd.Parameters.AddWithValue("@techid", obj.TechID);
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

        public bool DeleteTechnician(int Id)
        {

            connection();
            string query = "Delete from Technicians where techid=@techid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@techid", Id);

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