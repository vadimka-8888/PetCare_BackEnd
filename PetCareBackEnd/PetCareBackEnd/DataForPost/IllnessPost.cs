using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareBackEnd.DataForPost
{
    public class IllnessPost
    {
        public int pet_id { get; set; }
        public string type { get; set; }
        public DateTime date_of_begining { get; set; }
        public DateTime date_of_ending { get; set; }
    }
}
