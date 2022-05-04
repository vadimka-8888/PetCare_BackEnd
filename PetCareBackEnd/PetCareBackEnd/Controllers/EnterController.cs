using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetCareBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PetCareBackEnd.Controllers
{
    public class EnterController : Controller
    {
        private PetCareEntities context;
        public EnterController(PetCareEntities db)
        {
            context = db;
        }

        public IActionResult EnterUserProfile(string email, string password)
        {
            User user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            context.Pets.Where(p => p.UserId == user.UserId).Load();
            context.Notes.Where(n => n.UserId == user.UserId).Load();
            context.Mentions.Where(m => m.UserId == user.UserId).Load();
            foreach (Pet p in user.Pets)
            {
                context.Illnesses.Where(i => i.PetId == p.PetId).Load();
                context.Vaccinations.Where(v => v.PetId == p.PetId).Load();
            }
            if (user != null)
                return Json(user);
            else return Json("");
        }
    }
}
