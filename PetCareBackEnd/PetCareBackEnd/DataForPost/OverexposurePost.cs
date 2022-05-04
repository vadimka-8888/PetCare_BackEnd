using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareBackEnd.DataForPost
{
    public class OverexposurePost
    {
        public int user_id { get; set; }
        public string animal { get; set; }
        public string overexposure_note { get; set; }
        public int? cost { get; set; }
    }
}
