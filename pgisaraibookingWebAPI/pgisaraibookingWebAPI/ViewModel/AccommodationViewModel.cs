using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pgisaraibookingWebAPI.ViewModel
{
    public class AccommodationViewModel
    {
        public string Message { get; set; }
        public List<AccommodationMasterDetails> AccommodationMasterDotmetory { get; set; }
        public List<AccommodationMasterDetails> AccommodationMasterTwoBad { get; set; }
        public List<AccommodationMasterDetails> AccommodationMasterThreeBad { get; set; }
        public List<AccommodationMasterDetails> AccommodationMasterSixBad { get; set; }
    }
    public class AccommodationMasterDetails
    {
        public int rowid { get; set; }
        public string AccmNumber { get; set; }
        public bool AccmStatus { get; set; }
        public string isActive { get; set; }
    }
}