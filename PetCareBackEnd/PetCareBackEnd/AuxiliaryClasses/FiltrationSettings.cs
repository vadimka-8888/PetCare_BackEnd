using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareBackEnd.Models;

namespace PetCareBackEnd.AuxiliaryClasses
{
    public class FiltrationSettings
    {
        public Dictionary<string, bool> accessible_districts { get; set; }
        public Dictionary<string, bool> accessible_animals { get; set; }
        public int min_cost { get; set; }
        public int max_cost { get; set; }

        public FiltrationSettings()
        {
            accessible_animals = new Dictionary<string, bool>();
            accessible_districts = new Dictionary<string, bool>();
            min_cost = int.MinValue;
            max_cost = int.MaxValue;
        }
    }

    public class InformationWithFilter
    {
        public List<Overexposure> offers;
        public FiltrationSettings settings;
        public InformationWithFilter(List<Overexposure> o, FiltrationSettings s)
        {
            offers = o;
            settings = s;
        }
    }
}
