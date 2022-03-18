using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareDB
{
    public class QueryInformation //for receiving information from client
    {
        public string action { get; set; }
        public string data { get; set; }
    }

    public class SendQuery //for sending information to client
    {
        public string result { get; set; }
        public List<string> data { get; set; }
    }

    public class UpdateField
    {
        public int id { get; set; }
        public string inf_for_update { get; set; }
    }

    public class EmailPassword
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class UserInformation
    {
        public int user_id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string district { get; set; }
        public bool confirmation { get; set; }
    }

    public class PetInformation
    {
        public string animal { get; set; }
        public string name { get; set; }
        public DateTime? date_of_birth { get; set; }
        public string gender { get; set; }
        public float? weight { get; set; }
        public string color { get; set; }
        public byte[] photo { get; set; }
    }

    public class MentionInformation
    {
        public string m_text { get; set; }
        public DateTime date { get; set; }
        public DateTime time { get; set; }
    }

    public class NoteInformation
    {
        public string n_text { get; set; }
        public DateTime date { get; set; }
    }

    public class OverexposureInformation
    {
        public string animal { get; set; }
        public string o_note { get; set; }
        public int? cost { get; set; }
    }

    public class VaccinationInformation
    {
        public string type { get; set; }
        public DateTime? date { get; set; }
        public byte[] official_doc { get; set; }
        public bool? revaccination { get; set; }
    }

    public class IllnessInformation
    {
        public string type { get; set; }
        public DateTime date_of_begining { get; set; }
        public DateTime date_of_ending { get; set; }
    }

    public class OfferInformation
    {
        public string fname { get; set; }
        public string district { get; set; }
        public string email { get; set; }
        public string animal { get; set; }
        public string o_note { get; set; }
        public int? cost { get; set; }
    }
}
