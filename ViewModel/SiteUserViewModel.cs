using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginRegistrationWebApp.ViewModel
{
    public class SiteUserViewModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IsValid { get; set; }

    }
}