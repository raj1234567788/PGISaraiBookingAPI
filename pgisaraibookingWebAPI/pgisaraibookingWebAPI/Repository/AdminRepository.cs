using DLL;
using pgisaraibookingWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace pgisaraibookingWebAPI.Repository
{

    public class AdminRepository : IAdminRepository
    {
        #region Global Declaration
        private String constr = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
        admin objadmin = new admin();
        DataSet ds = null;
        String sqltext = "";
        string mesaage = "";
        #endregion
        public AccommodationMasterModel GetAccommodationMaster()
        {
            var model = new AccommodationMasterModel();
            SqlParameter[] _Spr = {  };
            DataSet DsCat = objadmin.binddatasetsP("SP_Fetch_AccommodationMaster", _Spr);
            //if (DsCat.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow row in DsCat.Tables[0].Rows)
            //    {
            //        model.AccommodationMasterDotmetory.Add(new AccommodationMasterModel
            //        {
            //            AccmNumber = row["AccmNumber"].ToString(),
            //            AccmStatus=row["AccmStatus"].ToString(),
            //            isActive=Convert.ToBoolean(row["isActive"].ToString()),
            //            rowid=Convert.ToInt32(row["rowid"].ToString())
            //        });
            //    }
            //    foreach (DataRow row in DsCat.Tables[1].Rows)
            //    {
            //        model.AccommodationMasterDotmetory.Add(new AccommodationMasterModel
            //        {
            //            AccmNumber = row["AccmNumber"].ToString(),
            //            AccmStatus = row["AccmStatus"].ToString(),
            //            isActive = Convert.ToBoolean(row["isActive"].ToString()),
            //            rowid = Convert.ToInt32(row["rowid"].ToString())
            //        });
            //    }

            //    foreach (DataRow row in DsCat.Tables[2].Rows)
            //    {
            //        model.AccommodationMasterDotmetory.Add(new AccommodationMasterModel
            //        {
            //            AccmNumber = row["AccmNumber"].ToString(),
            //            AccmStatus = row["AccmStatus"].ToString(),
            //            isActive = Convert.ToBoolean(row["isActive"].ToString()),
            //            rowid = Convert.ToInt32(row["rowid"].ToString())
            //        });
            //    }

            //    foreach (DataRow row in DsCat.Tables[3].Rows)
            //    {
            //        model.AccommodationMasterDotmetory.Add(new AccommodationMasterModel
            //        {
            //            AccmNumber = row["AccmNumber"].ToString(),
            //            AccmStatus = row["AccmStatus"].ToString(),
            //            isActive = Convert.ToBoolean(row["isActive"].ToString()),
            //            rowid = Convert.ToInt32(row["rowid"].ToString())
            //        });
            //    }
            //}
            return model;
        }
    }
}