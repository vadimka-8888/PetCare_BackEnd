using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareBackEnd.DataForPost
{
    public class MentionPost
    {
        public int user_id { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }
        public TimeSpan? time { get; set; }
    }
}
