using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetCareBackEnd.Models;
using Newtonsoft.Json;
using PetCareBackEnd.DataForPost;

namespace PetCareBackEnd.Controllers
{
    //Content-Type: application/json
    //Conten-Encoding: charset=utf-8
    public class RegisterController : Controller//Base
    {
        private PetCareEntities context;
        public RegisterController(PetCareEntities db)
        {
            context = db;
        }
        
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserPost ep)
        {
            var users = context.Users.Where(u => u.Email == ep.email).Select(u => new { Email = u.Email });
            if (users.Count() == 0)
            {
                var user = new User
                {
                    FirstName = ep.fname,
                    LastName = ep.lname,
                    Email = ep.email,
                    Password = ep.password,
                    District = ep.district,
                    ReadyForOvereposure = ep.confirmation
                };
                context.Users.Add(user);
                await context.SaveChangesAsync();
                return Ok(user);//Json($"Successful, user_id = {user.UserId}");
            }
            else return BadRequest();//Json("Not successful");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPet([FromBody] PetPost pet)
        {
            if (ApproveId(pet.user_id, "user"))
            {
                var pets = context.Pets.Where(p => p.Animal == pet.animal && p.Name == pet.name).Select(p => new { Name = p.Name });
                if (pets.Count() == 0)
                {
                    Pet pet0 = new Pet
                    {
                        UserId = pet.user_id,
                        Animal = pet.animal,
                        Name = pet.name,
                        Breed = pet.breed,
                        DateOfBirth = pet.date_of_birth,
                        Gender = pet.gender,
                        Weight = pet.weight,
                        Color = pet.color,
                        Photo = pet.photo
                    };
                    context.Pets.Add(pet0);
                    await context.SaveChangesAsync();
                    return Ok(pet0);
                }
                else return BadRequest(pet.user_id);
            }
            else return BadRequest(pet.animal);
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
                    case "illness":
                        res = context.Illnesses.Any(i => i.IllnessId == id);
                        break;
                    case "note":
                        res = context.Notes.Any(n => n.NoteId == id);
                        break;
                    case "overexposure":
                        res = context.Overexposures.Any(o => o.OverexposureId == id);
                        break;
                    case "mention":
                        res = context.Mentions.Any(m => m.MentionId == id);
                        break;
                    default:
                        break;
                }
            }
            return res;
        }
    }
}
