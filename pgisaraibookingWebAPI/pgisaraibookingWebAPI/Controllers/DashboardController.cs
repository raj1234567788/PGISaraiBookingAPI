using DLL;
using pgisaraibookingWebAPI.Models;
using pgisaraibookingWebAPI.Repository;
using pgisaraibookingWebAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace pgisaraibookingWebAPI.Controllers
{
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : ApiController
    {
        private String constr = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
        #region Global Declaration
        admin objadmin = new admin();
        DataSet ds = null;
        String sqltext = "";
        #endregion
        [Route("OccupancyReport")]
        [HttpGet]
        public HttpResponseMessage OccupancyReport()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            AccommodationViewModel item = new AccommodationViewModel();

            var okresp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                ReasonPhrase = "Success"
            };
            item.Message = okresp.ReasonPhrase;
            item.AccommodationMasterTwoBad = new List<AccommodationMasterDetails>();
            item.AccommodationMasterDotmetory = new List<AccommodationMasterDetails>();
            item.AccommodationMasterThreeBad = new List<AccommodationMasterDetails>();
            item.AccommodationMasterSixBad = new List<AccommodationMasterDetails>();


            SqlParameter[] _Spr = { };
            DataSet DsCat = objadmin.binddatasetsP("SP_Fetch_AccommodationMaster", _Spr);
            if (DsCat.Tables[0].Rows.Count > 0)
            {
                bool status;
                foreach (DataRow row in DsCat.Tables[0].Rows)
                {
                    if (row["AccmStatus"].ToString() == "A")
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    item.AccommodationMasterTwoBad.Add(new AccommodationMasterDetails
                    {
                        AccmNumber = row["AccmNumber"].ToString(),
                        AccmStatus = status,
                        isActive = row["isActive"].ToString(),
                        rowid = Convert.ToInt32(row["rowid"].ToString())
                    });
                }

                foreach (DataRow row in DsCat.Tables[1].Rows)
                {
                    if (row["AccmStatus"].ToString() == "A")
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    item.AccommodationMasterDotmetory.Add(new AccommodationMasterDetails
                    {
                        AccmNumber = row["AccmNumber"].ToString(),
                        AccmStatus = status,
                        isActive = row["isActive"].ToString(),
                        rowid = Convert.ToInt32(row["rowid"].ToString())
                    });
                }
                foreach (DataRow row in DsCat.Tables[2].Rows)
                {
                    if (row["AccmStatus"].ToString() == "A")
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    item.AccommodationMasterThreeBad.Add(new AccommodationMasterDetails
                    {
                        AccmNumber = row["AccmNumber"].ToString(),
                        AccmStatus = status,
                        isActive = row["isActive"].ToString(),
                        rowid = Convert.ToInt32(row["rowid"].ToString())
                    });
                }
                foreach (DataRow row in DsCat.Tables[3].Rows)
                {
                    if (row["AccmStatus"].ToString() == "A")
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    item.AccommodationMasterSixBad.Add(new AccommodationMasterDetails
                    {
                        AccmNumber = row["AccmNumber"].ToString(),
                        AccmStatus = status,
                        isActive = row["isActive"].ToString(),
                        rowid = Convert.ToInt32(row["rowid"].ToString())
                    });
                }
            }

            return response = Request.CreateResponse(HttpStatusCode.OK, item);
        }

        [Route("UserProfile")]
        [HttpPost]
        public HttpResponseMessage UserProfile()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            RegisterModel model = new RegisterModel();
            var httpcontext = HttpContext.Current.Request;

            var httpRequest = HttpContext.Current.Request;
            var postedImage = httpRequest.Files["ProfileImage"];
            //var postedFile = httpRequest.Files["FileUpload"];


            byte[] imgbyte = null;
            using (Stream fs = postedImage.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    imgbyte = br.ReadBytes((Int32)fs.Length);
                }
            }

            string name = httpRequest.Form["UserName"];

            //string name = httpcontext["UserName"];
            //string Email = httpcontext["Email"];
            //string Password = httpcontext["Password"];
            //string Mobile_Number = httpcontext["Mobile_Number"];
            //string Gender = httpcontext["Gender"];
            //string DeptID = httpcontext["DeptID"];
            //string ProfileImage = httpcontext["ProfileImage"];

            try
            {
                SqlParameter[] _Spr = {
                ( new SqlParameter("@RETURNVAL",SqlDbType.Int)),
                                                   (new SqlParameter("@UserName",name)),
                                                    //(new SqlParameter("@Email",model.Email)),
                                                    //(new SqlParameter("@Password",model.Password)),
                                                    //(new SqlParameter("@Mobile_Number",model.Mobile_Number)),
                                                    //(new SqlParameter("@Gender",model.Gender)),
                                                   ( new SqlParameter("@ProfileImage",imgbyte))
                                                };
                _Spr[0].Direction = ParameterDirection.Output;
                DataSet DsCat = objadmin.binddatasetsP("USP_Insert_FileUploadAPI", _Spr);
                Int32 BookingID = Convert.ToInt32(_Spr[0].Value.ToString());
                if (BookingID == 1)
                {
                    return response = Request.CreateResponse(HttpStatusCode.OK, "success");
                }
                else
                {
                    return response = Request.CreateResponse(HttpStatusCode.OK, "error");
                }
            }
            catch (Exception ex)
            {
                return response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }

        [Route("UserProfile1")]
        [HttpPost]
        public HttpResponseMessage UserProfile1(RegisterModel model)
        {
            string filename = "";
            byte[] imgbyte = null;
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                if (model.ProfileImage != null)
                {
                    if (model.ProfileImage.ContentLength > 0)
                    {
                        string extension = System.IO.Path.GetExtension(model.ProfileImage.FileName);
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
                    return response = Request.CreateResponse(HttpStatusCode.OK, "success");
                }
                else
                {
                    return response = Request.CreateResponse(HttpStatusCode.OK, "error");
                }
            }
            catch (Exception ex)
            {
                return response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }

        [Route("AddAccommodation")]
        [HttpPost]
        public HttpResponseMessage AddAccommodation(AccommodationMasterModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                SqlParameter[] _Spr = {
                                   (new SqlParameter("@AccmType",model.ID)),
                                   (new SqlParameter("@AccmNumber",model.AccmNumber)),
                                   (new SqlParameter("@FloorType",0)),
                                   ( new SqlParameter("@RETURNVAL",SqlDbType.Int))};
                _Spr[3].Direction = ParameterDirection.Output;
                objadmin.insertrecordbysp("USP_InsertAccommodation", _Spr);
                Int32 DsCat = Convert.ToInt32(_Spr[3].Value.ToString());
                if (DsCat == 1)
                {
                    return response = Request.CreateResponse(HttpStatusCode.OK, model.Message = "success");

                }
                if (DsCat == 2)
                {
                    return response = Request.CreateResponse(HttpStatusCode.OK, model.Message = "Error");
                }
                if (DsCat == 3)
                {
                    return response = Request.CreateResponse(HttpStatusCode.OK, model.Message = "Record already exist.");
                }
            }
            catch (Exception ex)
            {
                return response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
            return response;
        }

        [Route("AccommondationType")]
        [HttpGet]
        public HttpResponseMessage AccommodationType()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            AccommodationTypeViewModel model = new AccommodationTypeViewModel();
            model.AccommodationTypeList = new List<AccommodationType>();
            sqltext = "SELECT ID,BookingType FROM TariffUTGuestHouse where active = 'Y' order by BookingType";
            ds = null;
            try
            {
                DataSet DsCat = objadmin.binddatasetGeneral(sqltext);
                if (DsCat.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in DsCat.Tables[0].Rows)
                    {
                        model.AccommodationTypeList.Add(new AccommodationType
                        {
                            ID = Convert.ToInt32(row["ID"].ToString()),
                            BookingType = row["BookingType"].ToString()
                        });
                    }
                    model.message = "Success";
                    return response = Request.CreateResponse(HttpStatusCode.OK, model);
                }
                else
                {
                    return response = Request.CreateResponse(HttpStatusCode.OK, model.message = "No Record Found");
                }
            }
            catch (Exception ex)
            {
                return response = Request.CreateResponse(HttpStatusCode.OK, model.message = ex.Message);
            }
        }


    }
}
