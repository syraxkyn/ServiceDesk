using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.DAL
{
    public class CustomerRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ServiceDeskContext"].ToString();
            con = new SqlConnection(constr);

        }

        public bool AddCustomer(Customer obj)
        {

            connection();
            string query = "Insert into Customers values(@name, @address , @city,@phone,@email)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", obj.Name);
            cmd.Parameters.AddWithValue("@address", obj.Address);
            cmd.Parameters.AddWithValue("@city", obj.City);
            cmd.Parameters.AddWithValue("@phone", obj.Phone);
            cmd.Parameters.AddWithValue("@email", obj.Email);

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

        public List<Customer> GetAllCustomers()
        {
            connection();
            List<Customer> CustomerList = new List<Customer>();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Customers", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                string dr_phone=null; 
                string dr_email=null;
                if (dr["Phone"] != DBNull.Value)
                {
                    dr_phone = Convert.ToString(dr["Phone"]);
                }
                if (dr["Email"] != DBNull.Value)
                {
                    dr_email = Convert.ToString(dr["Email"]);
                }
                CustomerList.Add(

                    new Customer
                    {
                        CustomerID = Convert.ToInt32(dr["CustomerID"]),
                        Name = Convert.ToString(dr["Name"]),
                        Address = Convert.ToString(dr["Address"]),
                        City = Convert.ToString(dr["City"]),
                        Phone = dr_phone,
                        Email = dr_email
                    }
                    );
            }
            return CustomerList;
        }

        public bool UpdateCustomer(Customer obj)
        {

            connection();
            con.Open();
            string query = "Update Customers SET name=@name, address=@address , city=@city, phone=@phone, email=@email where CustomerID=@CustomerID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", obj.Name);
            cmd.Parameters.AddWithValue("@address", obj.Address);
            cmd.Parameters.AddWithValue("@city", obj.City);
            cmd.Parameters.AddWithValue("@phone", obj.Phone);
            cmd.Parameters.AddWithValue("@email", obj.Email);
            cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
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

        public bool DeleteCustomer(int Id)
        {

            connection();
            string query = "Delete from Customers where CustomerID=@CustomerID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CustomerID", Id);

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