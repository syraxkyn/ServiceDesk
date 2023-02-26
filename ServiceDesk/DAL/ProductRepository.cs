using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ServiceDesk.DAL
{
    public class ProductRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ServiceDeskContext"].ToString();
            con = new SqlConnection(constr);

        }
        
        public bool AddProduct(Product obj)
        {

            connection();
            string query = "Insert into Products values(@name, @version , @releasedate)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", obj.Name);
            cmd.Parameters.AddWithValue("@version", obj.Version);
            cmd.Parameters.AddWithValue("@releasedate", obj.ReleaseDate);

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

        public List<Product> GetAllProducts()
        {
            connection();
            List<Product> ProductList = new List<Product>();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Products", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                ProductList.Add(

                    new Product
                    {

                        ProductCode = Convert.ToInt32(dr["ProductCode"]),
                        Name = Convert.ToString(dr["Name"]),
                        Version = Convert.ToDecimal(dr["Version"]),
                        ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"])

                    }
                    );
            }
            return ProductList;
        }

        public bool UpdateProduct(Product obj)
        {

            connection();
            con.Open();
            string query = "Update Products SET name=@name, version=@version , releasedate=@releasedate where productcode=@productcode";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", obj.Name);
            cmd.Parameters.AddWithValue("@version", obj.Version);
            cmd.Parameters.AddWithValue("@releasedate", obj.ReleaseDate);
            cmd.Parameters.AddWithValue("@productcode", obj.ProductCode);
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

        public bool DeleteProduct(int Id)
        {

            connection();
            string query = "Delete from Products where productcode=@productcode";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@productcode", Id);

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