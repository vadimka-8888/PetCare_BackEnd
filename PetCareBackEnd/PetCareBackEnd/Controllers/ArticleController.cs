using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PetCareBackEnd.Models;

namespace PetCareBackEnd.Controllers
{
    public class ArticleController : Controller
    {
        private PetCareEntities context;
        public ArticleController(PetCareEntities db)
        {
            context = db;
        }

        [HttpGet]
        public IActionResult LoadArticles(int user_id)
        {
            List<Article> NecessaryArticles = new List<Article>();
            Dictionary<string, bool> possible_animals = new Dictionary<string, bool> { { "Кот/Кошка", false }, { "Собака", false }, { "Попугай", false }, { "Рыба", false }, { "Хомяк", false }, { "Морская свинка", false } };
            if (ApproveId(user_id, "user"))
            {
                User user = context.Users.Find(user_id);
                foreach (Pet pet in user.Pets)
                {
                    if (possible_animals[pet.Animal] == false)
                    {
                        possible_animals[pet.Animal] = true;

                        var articles = context.Articles.Where(a => a.Animal == pet.Animal || a.Animal == "Любое" || ((pet.Animal == "Кот/Кошка" || pet.Animal == "Собака") && a.Animal.Contains('-')));
                        NecessaryArticles.AddRange(articles);
                    }
                }
                return Json(NecessaryArticles);
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
