using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pgisaraibookingWebAPI.Models
{
    public class RegisterModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long Mobile_Number { get; set; }
        public string Gender { get; set; }
        public int DeptID { get; set; }
        public HttpPostedFileBase ProfileImage { get; set; }
        public bool IsActive { get; set; }
        public byte[] Image3 { get; set; }
    }
}