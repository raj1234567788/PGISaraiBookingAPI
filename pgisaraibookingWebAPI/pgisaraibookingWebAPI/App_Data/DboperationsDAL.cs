using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace DLL
{
    public class DboperationsDAL
    {
        public SqlConnection DBConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            return conn;
        }

        public Boolean chekforexisting(String searchvalue, String tablename, String fieldinwhichsearch)
        {
            Boolean srchresult = false;
            SqlConnection con = DBConnection();
            SqlDataReader dr;
            String sqltxt = "";
            sqltxt = "SELECT * FROM " + tablename + " WHERE " + fieldinwhichsearch +" = '"+searchvalue+"'";
            SqlCommand cmd = new SqlCommand(sqltxt);

            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                srchresult = true;
            }
            dr.Close();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return srchresult;
        }

        //****Function return field value depend on the id passed
        //******Parameter details 1==Id of the search value,2==Table in which search to be perform, 3==field to be search,

        public String returnNameById(String searchid, String searchidfieldname, String resultfieldname, String tablename)
        {
            String retval = "";
            SqlConnection con = DBConnection();
            SqlDataReader dr;
            String sqltxt = "";
            sqltxt = "SELECT " + resultfieldname + " FROM " + tablename + " WHERE " + searchidfieldname + " = '" + searchid + "'";
            SqlCommand cmd = new SqlCommand(sqltxt);

            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                retval = dr[0].ToString();
            }
            dr.Close();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return retval;
        }
    }

    
}
