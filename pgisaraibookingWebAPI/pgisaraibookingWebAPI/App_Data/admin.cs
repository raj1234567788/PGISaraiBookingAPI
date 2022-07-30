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
    public class admin
    {
        DboperationsDAL objDboperationsDAL = new DboperationsDAL();
        DataSet ds;
        DataTable dt;
        SqlDataAdapter da;
        SqlCommand cmd;
        int returnval = 0;
        public DataSet binddatasetsP(String storedprocname, SqlParameter[] spar)
        {
            SqlConnection conn = objDboperationsDAL.DBConnection();
            conn.Open();
            ds = new DataSet();
            SqlCommand comm = new SqlCommand();
            comm.CommandText = storedprocname;
            comm.Connection = conn;
            comm.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i <= spar.Length - 1; i++)
            {
                comm.Parameters.Add(spar[i]);
            }
            da = new SqlDataAdapter(comm);
            da.Fill(ds,"mynewdataset12345");
            //********Close Connection*************
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            //********Close Connection***************
            return ds;
        }
        public DataSet binddatasetGeneral(String sqltext)
        {
             ds = new DataSet();
             SqlConnection conn = objDboperationsDAL.DBConnection();
             da = new SqlDataAdapter(sqltext, conn);
             conn.Open();
             da.Fill(ds);
             if (conn.State == ConnectionState.Open)
             {
                 conn.Close();
             }
             return ds;
         }
        public int inserrecord(String sqltxt)
        {
            SqlConnection conn = objDboperationsDAL.DBConnection();
            conn.Open();
            SqlCommand comm = new SqlCommand(sqltxt, conn);
            returnval = comm.ExecuteNonQuery();
            //********Close Connection***************
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            return returnval;
        }
        public int insertrecordbysp(String sqltext, SqlParameter[] spar)
        {
            SqlConnection conn = objDboperationsDAL.DBConnection();
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqltext;
            comm.Connection = conn;
            comm.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i <= spar.Length - 1; i++)
            {
                comm.Parameters.Add(spar[i]);
            }
            returnval = comm.ExecuteNonQuery();
            //********Close Connection*************
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            //********Close Connection***************
            return returnval;
        }
        public string GetOTP()
        {
            int length = 4;
            char[] chars = "01234567890123456789098765425841".ToCharArray();
            string password = string.Empty;
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int x = random.Next(1, chars.Length);
                //Don't Allow Repetation of Characters
                if (!password.Contains(chars.GetValue(x).ToString()))
                    password += chars.GetValue(x);
                else
                    i--;
            }
            return password;
        }
        public static string splitdate(string txtdiarydate)
        {
            string[] dateformate = txtdiarydate.Split('/');
            string day = dateformate[0].ToString();
            string month = dateformate[1].ToString();
            string year = dateformate[2].ToString();
            string formateddate = year + '-' + month + '-' + day;
            return formateddate;
        }
    }
}