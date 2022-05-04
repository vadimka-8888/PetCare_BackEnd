using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareBackEnd.DataForPost
{
    public class VaccinationPost
    {
        public int pet_id { get; set; }
        public string type { get; set; }
        public DateTime? date { get; set; }
        public byte[] document { get; set; }
        public bool necessety_of_revaccination { get; set; }
    }
}
