using System;
using Microsoft.EntityFrameworkCore;

namespace Server_DataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User Daria = new User { Name = "Дарья", Lastname = "Чёрная", District = "Ленинский", Mail = "xxx@xxx.ru", CanOverexpose = false, Password = "XXX", Price = 0 };
                db.Users.Add(Daria);

                Animal Repka = new Animal { Species = "Собака", Name = "Репка", Breed = "Чихуахуа", DateOfBirth = DateTime.Parse("02.08.17"), Gender = "Ж", Weight = 3, Color = "Рыжий", User = Daria };
                db.Animals.Add(Repka);

                Disease Cold = new Disease { DiseaseName = "", StartDate = DateTime.Parse("12.10.18"), EndDate = DateTime.Parse("20.10.18"), Animal = Repka };
                db.Diseases.Add(Cold);

                db.SaveChanges();

                var animals = db.Animals.Include(x => x.User);
                foreach (Animal a in animals)
                {
                    Console.WriteLine($"{a.AnimalId} {a.Species} {a.Name} - {a.UserId}, {a.User.Name}");
                }
            }
        }
    }
}
