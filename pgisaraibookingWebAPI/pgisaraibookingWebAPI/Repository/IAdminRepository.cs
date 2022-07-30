using pgisaraibookingWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pgisaraibookingWebAPI.Repository
{
   public interface IAdminRepository
    {
        AccommodationMasterModel GetAccommodationMaster();
    }
}
