using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareBackEnd.DataForPost
{
    public class FieldInformation
    {
        public string what { get; set; }
        public int id { get; set; }
    }

    public class FieldInformationStr : FieldInformation
    {
        public string new_value { get; set; }
    }

    public class FieldInformationFl : FieldInformation
    {
        public float new_value { get; set; }
    }

    public class FieldInformationByte : FieldInformation
    {
        public byte[] new_value { get; set; }
    }

    public class FieldInformationInt : FieldInformation
    {
        public int new_value { get; set; }
    }

    public class FieldInformationBool : FieldInformation
    {
        public bool new_value { get; set; }
    }
}
