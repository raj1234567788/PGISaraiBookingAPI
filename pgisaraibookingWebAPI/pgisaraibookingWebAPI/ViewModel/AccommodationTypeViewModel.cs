using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pgisaraibookingWebAPI.ViewModel
{
    public class AccommodationTypeViewModel
    {
        public string message { get; set; }
        public List<AccommodationType> AccommodationTypeList { get; set; }
    }
    public class AccommodationType
    {
        public int ID { get; set; }
        public string BookingType { get; set; }
    }
}