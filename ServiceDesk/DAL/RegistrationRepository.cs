using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ServiceDesk.DAL
{
    public class RegistrationRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ServiceDeskContext"].ToString();
            con = new SqlConnection(constr);

        }

        public bool AddRegistration(Registration obj)
        {

            connection();
            string query = "Insert into Registrations values(@name, @productcode , @registrationdate)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", obj.CustomerID);
            cmd.Parameters.AddWithValue("@productcode", obj.ProductCode);
            cmd.Parameters.AddWithValue("@registrationdate", obj.RegistrationDate);

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

        public List<Registration> GetAllRegistrations()
        {
            connection();
            List<Registration> RegistrationList = new List<Registration>();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Registrations", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close(); 
            foreach (DataRow dr in dt.Rows)
            {

                RegistrationList.Add(

                    new Registration
                    {

                        ProductCode = Convert.ToInt32(dr["ProductCode"]),
                        CustomerID = Convert.ToInt32(dr["CustomerID"]),
                        RegistrationDate = Convert.ToDateTime(dr["RegistrationDate"])

                    }
                    );
            }
            return RegistrationList;
        }

        public bool UpdateRegistration(Registration obj)
        {

            connection();
            con.Open();
            string query = "Update Registrations SET customerid=@customerid, productcode=@productcode , registrationdate=@registrationdate where productcode=@productcode and customerid=@customerid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@customerid", obj.CustomerID);
            cmd.Parameters.AddWithValue("@productcode", obj.ProductCode);
            cmd.Parameters.AddWithValue("@releasedate", obj.RegistrationDate);
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

        public bool DeleteRegistration(int CustomerID, int ProductCode)
        {

            connection();
            string query = "Delete from Registrations where productcode=@productcode and customerid=@customerid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@productcode", ProductCode);
            cmd.Parameters.AddWithValue("@customerid", CustomerID);

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