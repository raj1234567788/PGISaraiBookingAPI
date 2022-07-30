using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pgisaraibookingWebAPI.Models
{
    public class AccommodationMasterModel
    {
        public string Message { get; set; }
        public int ID { get; set; }
        public int rowid { get; set; }
        public string AccmNumber { get; set; }
        public bool AccmStatus { get; set; }
        public string isActive { get; set; }
    }
}