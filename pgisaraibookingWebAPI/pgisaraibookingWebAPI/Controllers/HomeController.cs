using DLL;
using pgisaraibookingWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pgisaraibookingWebAPI.Controllers
{
    public class HomeController : Controller
    {
        #region Global Declaration
        admin objadmin = new admin();
        #endregion
        [HttpGet]
        public ActionResult Index()
        {
            RegisterModel model = new RegisterModel();
            SqlParameter[] _Spr = {
                                                   
                                                   ( new SqlParameter("@Retval",SqlDbType.Int))
                                                };
            _Spr[0].Direction = ParameterDirection.Output;
           
            DataSet DsCat = objadmin.binddatasetsP("Sp_User_GetUserImage", _Spr);
            Int32 BookingID = Convert.ToInt32(_Spr[0].Value.ToString());
            if (BookingID == 1)
            {
                model.Image3 = (byte[])DsCat.Tables[0].Rows[0]["ProfileImage"];// MyReader["image1"];
            }
            else if (BookingID == 2)
            {
               
            }
            var imageBase64 = Convert.ToBase64String(model.Image3);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(RegisterModel model)
        {

            string filename = "";
            byte[] imgbyte = null;
            string extension = System.IO.Path.GetExtension(model.ProfileImage.FileName);
            if (model.ProfileImage.ContentLength>0)
            {
                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                {
                    filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + model.ProfileImage.FileName;
                    string filename1 = Path.GetFileName(model.ProfileImage.FileName);
                    string contentType = model.ProfileImage.ContentType;
                    using (Stream fs = model.ProfileImage.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            imgbyte = br.ReadBytes((Int32)fs.Length);
                        }
                    }
                }
                else
                {

                }
            }
            SqlParameter[] _Spr = {
                ( new SqlParameter("@RETURNVAL",SqlDbType.Int)),
                                                   (new SqlParameter("@UserName",model.UserName)),
                                                    (new SqlParameter("@Email",model.Email)),
                                                    (new SqlParameter("@Password",model.Password)),
                                                    (new SqlParameter("@Mobile_Number",model.Mobile_Number)),
                                                    (new SqlParameter("@Gender",model.Gender)),
                                                   ( new SqlParameter("@DeptId",model.DeptID)),
                                                   ( new SqlParameter("@ProfileImage",imgbyte))
                                                };
            _Spr[0].Direction = ParameterDirection.Output;
            DataSet DsCat = objadmin.binddatasetsP("USP_Insert_FileUploadAPI", _Spr);
            Int32 BookingID = Convert.ToInt32(_Spr[0].Value.ToString());
            if (BookingID == 1)
            {

            }


                ViewBag.Title = "Home Page";

            return View(model);
        }
    }
}
