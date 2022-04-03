using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareBackEnd.DataForPost
{
    public class UserPost
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string district { get; set; }
        public bool confirmation { get; set; }
    }
}
