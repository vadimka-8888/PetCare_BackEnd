using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PetCareBackEnd.Models;
using Microsoft.EntityFrameworkCore;

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
                User user = context.Users.Include(u => u.Pets).Where(u => u.UserId == user_id).First();
                foreach (Pet pet in user.Pets)
                {
                    if (possible_animals[pet.Animal] == false)
                    {
                        possible_animals[pet.Animal] = true;

                        var articles = context.Articles.Where(a => a.Animal == pet.Animal || a.Animal == "Любое" || ((pet.Animal == "Кот/Кошка" || pet.Animal == "Собака") && a.Animal == "Кошка-Собака"));
                        NecessaryArticles.AddRange(articles);
                    }
                }
                return Json(NecessaryArticles);
            }
            else return Json("Not successful, id does not exist");
        }

        [HttpGet]
        public IActionResult IsArticleFavourite(int user_id, int article_id)
        {
            if (ApproveIdsInTable(user_id, article_id, "favourites"))
            {
                return Json(true);
            }
            else return Json(false);
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

        private bool ApproveIdsInTable(int id1, int id2, string where)
        {
            bool res = false;
            if (id1 > 0 && id2 > 0)
            {
                switch (where)
                {
                    case "favourites":
                        res = context.Favourites.Any(f => f.UserId == id1 && f.ArticleId == id2);
                        break;
                    default:
                        break;
                }
            }
            return res;
        }
    }
}
