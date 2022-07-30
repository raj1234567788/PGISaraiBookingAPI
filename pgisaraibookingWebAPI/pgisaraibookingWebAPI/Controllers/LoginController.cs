using DLL;
using pgisaraibookingWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pgisaraibookingWebAPI.Controllers
{
    [RoutePrefix("api/Login")]


    public class LoginController : ApiController
    {
        private String constr = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
        #region Global Declaration
        admin objadmin = new admin();
        #endregion

        [Route("GetImage")]
        [HttpGet]
        public HttpResponseMessage GetImage()
        {
            HttpResponseMessage message = new HttpResponseMessage();
            byte[] image = null;
            SqlParameter[] _Spr = {

                                                   ( new SqlParameter("@Retval",SqlDbType.Int))
                                                };
            _Spr[0].Direction = ParameterDirection.Output;

            DataSet DsCat = objadmin.binddatasetsP("Sp_User_GetUserImage", _Spr);
            Int32 BookingID = Convert.ToInt32(_Spr[0].Value.ToString());
            if (BookingID == 1)
            {
                image = (byte[])DsCat.Tables[0].Rows[0]["ProfileImage"];// MyReader["image1"];
            }
            else if (BookingID == 2)
            {

            }
            var imageBase64 = Convert.ToBase64String(image);

            return message = Request.CreateResponse(HttpStatusCode.OK, imageBase64);
        }

        [Route("Login")]
        [HttpGet]
        public HttpResponseMessage Login(string OTP, string mobileno)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            LoginModel model = new LoginModel();
            model.OTP = OTP;
            try
            {
                if (model.OTP == string.Empty || model.OTP == "")
                {
                    return message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please enter OTP");
                }
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand _cmd = new SqlCommand("login", con);
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@mobile", mobileno);
                _cmd.Parameters.AddWithValue("@otp", model.OTP);

                SqlDataAdapter sqladptr = new SqlDataAdapter(_cmd);

                DataTable dt = new DataTable();
                sqladptr.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    return message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect OTP Entered");
                }
                else
                {
                    return message = Request.CreateResponse(HttpStatusCode.OK, "success");
                }


                dt.Dispose();
                _cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                return message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("GetOTP")]
        [HttpGet]
        public HttpResponseMessage GetOTP(string UserName,string Password)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            LoginModel model = new LoginModel();
            model.Password = Password;
            model.UserName = UserName;
            try
            {
                if (model.UserName == string.Empty || model.UserName == "")
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
                    //return message.StatusCode(StatusCode(HttpStatusCode.BadRequest));
                }
                else if (model.UserName.Trim().ToString().Length>10)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Illegal Characters in Applicant User Name....');", true);
                    
                    //return;
                }
                else if (model.Password == string.Empty || model.Password == "")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Enter Password...!!');", true);
                   
                   // return;
                }
                else
                {

                    SqlConnection con = new SqlConnection(constr);
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("dept_login", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@uname", model.UserName.Trim());
                    cmd1.Parameters.AddWithValue("@pass", model.Password.Trim());

                    SqlDataAdapter sqladptr = new SqlDataAdapter(cmd1);

                    DataSet dt = new DataSet();
                    sqladptr.Fill(dt);
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        model.UserName = model.UserName;
                        model.Password = dt.Tables[0].Rows[0]["mobile_no"].ToString();
                        if (dt.Tables[0].Rows[0]["mobile_no"].ToString() != string.Empty || dt.Tables[0].Rows[0]["mobile_no"].ToString() != "")
                        {
                            Random RndNum = new Random();
                            int RnNum = RndNum.Next(1111, 9999);
                            int a = RnNum;

                            string upqury = "update [otp] set logedin='true' where mobile='" + dt.Tables[0].Rows[0]["mobile_no"].ToString() + "'";

                            SqlCommand cmd = new SqlCommand(upqury, con);
                            cmd.ExecuteNonQuery();
                            string qury = "INSERT INTO [otp]([username],[mobile] ,[otp],[time]) OUTPUT INSERTED.ID  VALUES ('" + dt.Tables[0].Rows[0]["mobile_no"].ToString() + "','" + dt.Tables[0].Rows[0]["mobile_no"].ToString() + "'," + a.ToString().Trim() + ",getdate())";

                            cmd = new SqlCommand(qury, con);
                            int otpid = Convert.ToInt32(cmd.ExecuteScalar());
                            cmd = new SqlCommand("select top 1 otp  FROM dbo.otp where id=" + otpid + "", con);

                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable dtt = new DataTable();
                            adp.Fill(dtt);
                            string otpcode = dtt.Rows[0][0].ToString();
                            String _msg = ("your OTP password is " + otpcode + " You can login only once with this OTP  ");
                            model.OTP = _msg;
                            cmd.Dispose();
                            dtt.Dispose();
                            return message = Request.CreateResponse(HttpStatusCode.OK, model);
                        }
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not Found");
                    }
                    dt.Dispose();
                    cmd1.Dispose();
                    con.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not Found");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not Found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            //return message;
        }

        [Route("ForgetPass")]
        [HttpGet]
        public HttpResponseMessage ForgetPass(string UserName)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            if (UserName == string.Empty || UserName == "")
            {
                return message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please Enter Mobile No");
            }
            else if (UserName.ToString().Length != 10)
            {
                return message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please Enter Mobile No");
            }
            else
            {
                SqlParameter[] _Spr = {
                                                   (new SqlParameter("@emp_mobile",UserName)),
                                                    ( new SqlParameter("@password",SqlDbType.VarChar,50)),
                                                   ( new SqlParameter("@Retval",SqlDbType.Int))
                                                };
                _Spr[1].Direction = ParameterDirection.Output;
                _Spr[2].Direction = ParameterDirection.Output;
                DataSet DsCat = objadmin.binddatasetsP("Sp_User_ForgotPassword", _Spr);
                Int32 BookingID = Convert.ToInt32(_Spr[2].Value.ToString());
                string password = Convert.ToString(_Spr[1].Value.ToString());
                if (BookingID == 1)
                {
                    return message = Request.CreateResponse(HttpStatusCode.OK, "Your Password " + password);
                }
                else if (BookingID == 2)
                {
                    return message = Request.CreateResponse(HttpStatusCode.OK, "THIS MOBILE NO NOT EXISTS IN THE DATABASE");
                }
                else if (BookingID == 3)
                {
                    return message = Request.CreateResponse(HttpStatusCode.OK, "Someting went wrong");
                }
            }
            return message;
        }
    }
}
