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
    //[ApiController]
    //[Route("api/[controller]")]
    public class RegisterController : Controller//Base
    {
        private PetCareEntities context;
        public RegisterController(PetCareEntities db)
        {
            context = db;
        }
        //Register/RegisterUser?fname=Дмитрий&lname=Махов&email=mahov@mail.ru&password=mmm222&district=Северный&confirmation=false
        //{"fname":"Александра","lname":"Праведник","email":"ap@mail.ru","password":"sasha1","district":"Советский","confirmation":false}
        
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

        
        
        /*
        // GET: RegisterController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RegisterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegisterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegisterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegisterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
