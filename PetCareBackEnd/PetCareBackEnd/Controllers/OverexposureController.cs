using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using PetCareBackEnd.Models;
using PetCareBackEnd.AuxiliaryClasses;
using Microsoft.EntityFrameworkCore;

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
            List<FullOverexposure> Offers = new List<FullOverexposure>();
            FiltrationSettings f_settings = new FiltrationSettings();
            Dictionary<string, bool> possible_animals = new Dictionary<string, bool> { { "Кот/Кошка", false }, { "Собака", false }, { "Попугай", false }, { "Рыба", false }, { "Хомяк", false }, { "Морская свинка", false } };
            if (ApproveId(user_id, "user"))
            {
                User user = context.Users.Where(u => u.UserId == user_id).Include(u => u.Pets).First();
                foreach (Pet pet in user.Pets)
                {
                    if (possible_animals[pet.Animal] == false)
                    {
                        possible_animals[pet.Animal] = true;
                        f_settings.accessible_animals[pet.Animal] = true;
                        var PeopleOffer = context.Users.Where(x => x.ReadyForOvereposure == true && x.UserId != user_id).Select(x => new { x.UserId, x.District }).AsEnumerable().Select(c => new Tuple<int, string>(c.UserId, c.District)).ToList();
                        foreach (Tuple<int, string> id_district in PeopleOffer)
                        {
                            var Offers_of_certain_person = context.Overexposures.Where(x => x.UserId == id_district.Item1 && x.Animal == pet.Animal).Include(x => x.User).Select(x => new FullOverexposure(x.OverexposureId, x.UserId, x.Animal, x.ONote, x.Cost, x.User.FirstName, x.User.LastName, x.User.District, x.User.Email));
                            f_settings.accessible_districts[id_district.Item2] = true;
                            Offers.AddRange(Offers_of_certain_person);
                        }
                    }
                }
                InformationWithFilter overexposures_with_settings = new InformationWithFilter(Offers, f_settings);
                return Json(overexposures_with_settings);
            }
            else return Json("Not successful, id does not exist");
        }

        [HttpPost]
        public IActionResult UpdateOverexposureDataList(int user_id, [FromBody] FiltrationSettings f_settings)
        {
            if (f_settings == null)
                return Json("Not successful, settings were sent incorrectly");
            List<FullOverexposure> Offers = new List<FullOverexposure>();
            Dictionary<string, bool> possible_animals = new Dictionary<string, bool> { { "Кот/Кошка", false }, { "Собака", false }, { "Попугай", false }, { "Рыба", false }, { "Хомяк", false }, { "Морская свинка", false } };
            if (ApproveId(user_id, "user"))
            {
                User user = context.Users.Where(u => u.UserId == user_id).Include(u => u.Pets).First();
                foreach (Pet pet in user.Pets)
                {
                    if (possible_animals[pet.Animal] == false && f_settings.accessible_animals[pet.Animal] == true)
                    {
                        possible_animals[pet.Animal] = true;
                        var PeopleOffer = context.Users.Where(x => x.ReadyForOvereposure == true && x.UserId != user_id && f_settings.accessible_districts[x.District] == true).Select(x => x.UserId).ToList();
                        foreach (int id in PeopleOffer)
                        {
                            var Offers_of_certain_person = context.Overexposures.Where(x => x.UserId == id && x.Animal == pet.Animal && x.Cost >= f_settings.min_cost && x.Cost <= f_settings.max_cost).Include(x => x.User).Select(x => new FullOverexposure(x.OverexposureId, x.UserId, x.Animal, x.ONote, x.Cost, x.User.FirstName, x.User.LastName, x.User.District, x.User.Email));
                            Offers.AddRange(Offers_of_certain_person);
                        }
                    }
                }
                InformationWithFilter overexposures_with_settings = new InformationWithFilter(Offers, f_settings);
                return Json(overexposures_with_settings);
            }
            else return Json("Not successful, id does not exist");
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
