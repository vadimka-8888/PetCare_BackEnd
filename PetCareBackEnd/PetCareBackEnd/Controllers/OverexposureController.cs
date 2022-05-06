using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareBackEnd.DataForPost;
using PetCareBackEnd.Models;
using PetCareBackEnd.AuxiliaryClasses;

namespace PetCareBackEnd.Controllers
{
    public class OverexposureController : Controller
    {
        private PetCareEntities context;
        public OverexposureController(PetCareEntities db)
        {
            context = db;
        }

        [HttpGet]
        public IActionResult LoadOverexposureDataList(int user_id)
        {
            List<Overexposure> Offers = new List<Overexposure>();
            FiltrationSettings f_settings = new FiltrationSettings();
            Dictionary<string, bool> possible_animals = new Dictionary<string, bool> { { "Кот/Кошка", false }, { "Собака", false }, { "Попугай", false }, { "Рыба", false }, { "Хомяк", false }, { "Морская свинка", false } };
            if (ApproveId(user_id, "user"))
            {
                User user = context.Users.Find(user_id);
                foreach (Pet pet in user.Pets)
                {
                    if (possible_animals[pet.Animal] == false)
                    {
                        possible_animals[pet.Animal] = true;
                        f_settings.accessible_animals[pet.Animal] = true;
                        var PeopleOffer = context.Users.Where(x => x.ReadyForOvereposure == true && x.UserId != user_id).Select(x => new { x.UserId, x.District }).AsEnumerable().Select(c => new Tuple<int, string>(c.UserId, c.District));
                        foreach (Tuple<int, string> id_district in PeopleOffer)
                        {
                            var Offers_of_certain_person = context.Overexposures.Where(x => x.UserId == id_district.Item1 && x.Animal == pet.Animal);
                            f_settings.accessible_districts[id_district.Item2] = true;
                            Offers.AddRange(Offers_of_certain_person);
                        }
                    }
                }
                return Json(Offers, f_settings);
            }
            else return Json("Not successful");
        }

        [HttpGet]
        public IActionResult UpdateOverexposureDataList(int user_id, [FromBody] FiltrationSettings f_settings)
        {
            List<Overexposure> Offers = new List<Overexposure>();
            Dictionary<string, bool> possible_animals = new Dictionary<string, bool> { { "Кот/Кошка", false }, { "Собака", false }, { "Попугай", false }, { "Рыба", false }, { "Хомяк", false }, { "Морская свинка", false } };
            if (ApproveId(user_id, "user"))
            {
                User user = context.Users.Find(user_id);
                foreach (Pet pet in user.Pets)
                {
                    if (possible_animals[pet.Animal] == false && f_settings.accessible_animals[pet.Animal] == true)
                    {
                        possible_animals[pet.Animal] = true;
                        var PeopleOffer = context.Users.Where(x => x.ReadyForOvereposure == true && x.UserId != user_id && f_settings.accessible_districts[x.District] == true).Select(x => x.UserId);
                        foreach (int id in PeopleOffer)
                        {
                            var Offers_of_certain_person = context.Overexposures.Where(x => x.UserId == id && x.Animal == pet.Animal && x.Cost >= f_settings.min_cost && x.Cost <= f_settings.max_cost);
                            Offers.AddRange(Offers_of_certain_person);
                        }
                    }
                }
                return Json(Offers, f_settings);
            }
            else return Json("Not successful");
        }

        private bool ApproveId(int id, string kind)
        {
            bool res = false;
            if (id > 0)
            {
                switch (kind)
                {
                    case "user":
                        res = context.Users.Any(u => u.UserId == id);
                        break;
                    default:
                        break;
                }
            }
            return res;
        }
    }
}
