using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareBackEnd.DataForPost
{
    public class PetPost
    {
        public int user_id { get; set; }
        public string animal { get; set; }
        public string name { get; set; }
        public string breed { get; set; }
        public DateTime? date_of_birth { get; set; }
        public string gender { get; set; }
        public float? weight { get; set; }
        public string color { get; set; }
        public byte[] photo { get; set; }
    }
}
