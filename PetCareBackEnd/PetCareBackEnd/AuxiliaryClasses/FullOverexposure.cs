using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareBackEnd.AuxiliaryClasses
{
    public class FullOverexposure
    {
        public int overexposure_id { get; set; }
        public int user_id { get; set; }
        public string animal { get; set; }
        public string overexposure_note { get; set; }
        public int? cost { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string district { get; set; }
        public string email { get; set; }

        public FullOverexposure(int o_id, int u_id, string a, string o_note, int? c, string fn, string ln, string d, string e)
        {
            overexposure_id = o_id;
            user_id = u_id;
            animal = a;
            overexposure_note = o_note;
            cost = c;
            first_name = fn;
            last_name = ln;
            district = d;
            email = e;
        }
    }
}
