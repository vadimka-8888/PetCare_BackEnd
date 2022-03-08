using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareDB
{
    public class QueryInformation
    {
        public string action { get; set; }
        public string data { get; set; }
    }

    public class UserInformation
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string district { get; set; }
        public bool confirmation { get; set; }
    }
}
