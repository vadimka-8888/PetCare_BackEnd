using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using PetCareBackEnd.AuxiliaryClasses;

namespace PetCareBackEnd.Controllers
{
    public class LoadController : Controller
    {
        private PetCareEntities context;
        public LoadController(PetCareEntities db)
        {
            context = db;
        }

        [HttpGet]
        public IActionResult LoadNotes(int user_id)
        {
            if (ApproveId(user_id, "user"))
            {
                User user = context.Users.Find(user_id);
                var result = context.Notes.Where(n => n.User == user).Select(n => new { n.NoteId, n.TextOfNote, n.Date }).ToList();
                return Json(result);
            }
            else return Json("Not successful, id does not exist");
        }

        [HttpGet]
        public IActionResult LoadMentions(int user_id)
        {
            if (ApproveId(user_id, "user"))
            {
                User user = context.Users.Find(user_id);
                var result = context.Mentions.Where(m => m.User == user).Select(m => new { m.MentionId, m.TextOfMention, m.Date, m.Time }).ToList();
                return Json(result);
            }
            else return Json("Not successful, id does not exist");
        }

        [HttpGet]
        public IActionResult LoadIllnesses(int pet_id)
        {
            if (ApproveId(pet_id, "pet"))
            {
                Pet pet = context.Pets.Find(pet_id);
                var result = context.Illnesses.Where(i => i.Pet == pet).Select(i => new { i.IllnessId, i.Type, i.DateOfBegining, i.DateOfEnding }).ToList();
                return Json(result);
            }
            else return Json("Not successful, id does not exist");
        }

        [HttpGet]
        public IActionResult LoadVaccinations(int pet_id)
        {
            if (ApproveId(pet_id, "pet"))
            {
                Pet pet = context.Pets.Find(pet_id);
                var result = context.Vaccinations.Where(v => v.Pet == pet).Select(v => new { v.VaccinationId, v.Type, v.Date, v.OfficialDocument, v.NecessityOfRevaccination }).ToList();
                return Json(result);
            }
            else return Json("Not successful, id does not exist");
        }

        [HttpGet]
        public IActionResult LoadIllnessesUserIdDefault(int user_id)
        {
            if (ApproveId(user_id, "user"))
            {
                var pets = context.Pets.Where(p => p.UserId == user_id);
                int pet_id = pets.Count() > 0 ? pets.First().PetId : 0;
                return LoadIllnesses(pet_id);
            }
            else return Json("Not successful, id does not exist");
        }

        [HttpGet]
        public IActionResult LoadVaccinationsUserIdDefault(int user_id)
        {
            if (ApproveId(user_id, "user"))
            {
                var pets = context.Pets.Where(p => p.UserId == user_id);
                int pet_id = pets.Count() > 0 ? pets.First().PetId : 0;
                return LoadVaccinations(pet_id);
            }
            else return Json("Not successful, id does not exist");
        }

        [HttpGet]
        public IActionResult LoadIllnessesUserIdAll(int user_id)
        {
            if (ApproveId(user_id, "user"))
            {
                List<Pet> pets = context.Pets.Where(p => p.UserId == user_id).Include(p => p.Illnesses).ToList();
                List<Illness> result0 = new List<Illness>();
                foreach (Pet p in pets)
                {
                    List<Illness> ilnesses_for_certain_pet = p.Illnesses.ToList();
                    result0.AddRange(ilnesses_for_certain_pet);
                }
                var result = result0.Select(i => new { i.IllnessId, i.Type, i.DateOfBegining, i.DateOfEnding }).ToList();
                return Json(result);
            }
            else return Json("Not successful, id does not exist");
        }

        [HttpGet]
        public IActionResult LoadVaccinationsUserIdAll(int user_id)
        {
            if (ApproveId(user_id, "user"))
            {
                List<Pet> pets = context.Pets.Where(p => p.UserId == user_id).Include(p => p.Vaccinations).ToList();
                List<Vaccination> result0 = new List<Vaccination>();
                foreach (Pet p in pets)
                {
                    List<Vaccination> vaccinations_for_certain_pet = p.Vaccinations.ToList();
                    result0.AddRange(vaccinations_for_certain_pet);
                }
                var result = result0.Select(v => new { v.VaccinationId, v.Type, v.Date, v.OfficialDocument, v.NecessityOfRevaccination }).ToList();
                return Json(result);
            }
            else return Json("Not successful, id does not exist");
        }

        [HttpGet]
        public IActionResult LoadFavouriteArticles(int user_id)
        {
            if (ApproveId(user_id, "user"))
            {
                List<Favourite> favourites = context.Favourites.Where(f => f.UserId == user_id).ToList();
                List<OnlyArticle> result = new List<OnlyArticle>();
                foreach (Favourite f in favourites)
                {
                    OnlyArticle a = context.Articles.Where(a => a.ArticleId == f.ArticleId).Select(a => new OnlyArticle(a.ArticleId, a.Animal, a.Image, a.ImageAdress, a.IsFavourite, a.TextOfArticle, a.Title)).First();
                    result.Add(a);
                }
                return Json(result);
            }
            else return Json("Not successful, id does not exist");
        }

        [HttpGet]
        public IActionResult LoadOverexposures(int user_id)
        {
            if (ApproveId(user_id, "user"))
            {
                var overexposures = context.Overexposures.Where(o => o.UserId == user_id).Select(o => new { o.OverexposureId, o.Animal, o.ONote, o.Cost });
                return Json(overexposures);
            }
            else return Json("Not successful, id does not exist");
        }

        [HttpGet]
        public IActionResult LoadPets(int user_id)
        {
            if (ApproveId(user_id, "user"))
            {
                var pets = context.Pets.Where(p => p.UserId == user_id).Select(p => new { p.PetId, p.Animal, p.Name, p.Gender, p.Breed, p.DateOfBirth, p.Weight, p.Color, p.Photo });
                if (pets.Count() > 0)
                {
                    return Json(pets);
                }
                else return Json("Not successful, there are no pets");
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
                    case "pet":
                        res = context.Pets.Any(p => p.PetId == id);
                        break;
                    default:
                        break;
                }
            }
            return res;
        }
    }
}
