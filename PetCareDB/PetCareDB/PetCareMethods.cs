using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetCareDB.EF;

namespace PetCareDB
{
    public static class PetCareMethods
    {
        public static string RegisterUser(string fname, string lname, string email, string password, string district, bool confirmation)
        {
            using (PetCareEntities context = new PetCareEntities())
            {
                try
                {
                    var users = context.Users.Where(u => u.Email == email).Select(u => new {Email = u.Email});
                    if (users.Count() == 0) //checking existance of the user
                    {
                        var user = new User
                        {
                            FirstName = fname,
                            LastName = lname,
                            Email = email,
                            Password = password,
                            District = district,
                            ReadyForOvereposure = confirmation
                        };
                        context.Users.Add(user);
                        context.SaveChanges();
                        return $"Reg_successful|{user.UserId}";
                    }
                    else return "Reg_notsuccessful|0|user already exists";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    return "Reg_notsuccessful|0";
                }
            }
        }

        public static string EnterUserProfile(string email, string password)
        {
            using (PetCareEntities context = new PetCareEntities())
            {
                try
                {
                    User user = context.Users.First(u => u.Email == email && u.Password == password);
                    context.Entry(user).Collection(c => c.Pets).Load();
                    string result = $"Ent_s|UI|{user.FirstName}|{user.LastName}|{user.District}|";
                    int n = 1;
                    foreach (Pet pet in context.Pets)
                    {
                        result += $"PI{n}|{pet.Animal}|{pet.Name}|{pet.Photo}|{pet.Gender}|{pet.Color}|{pet.DateOfBirth}|{pet.Breed}|{pet.Weight}|";
                        n++;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    return "Ent_n|0";
                }

            }
        }

        public static string RemoveUser(int user_id)
        {
            using (PetCareEntities context = new PetCareEntities())
            {
                User user_to_delete = context.Users.Find(user_id);
                if (user_to_delete != null)
                {
                    context.Users.Remove(user_to_delete);
                    context.SaveChanges();
                    return "Del_s";
                }
                else return "Del_n";
            }
        }

        public static string RegisterPet(int user_id, string animal, string name, string breed, DateTime date_of_birth, string gender, float weight, string color, byte[] image)
        {
            using (PetCareEntities context = new PetCareEntities())
            {
                try
                {
                    if (ApproveId(user_id, "user"))
                    {
                        var pets = context.Pets.Where(p => p.Animal == animal && p.Name == name).Select(p => new { Name = p.Name });
                        if (pets.Count() == 0)
                        {
                            var pet = new Pet
                            {
                                UserId = user_id,
                                Animal = animal,
                                Name = name,
                                Breed = breed,
                                DateOfBirth = date_of_birth,
                                Gender = gender,
                                Weight = weight,
                                Color = color,
                                Photo = image
                            };
                            context.Pets.Add(pet);
                            context.SaveChanges();
                            return $"Regp_s|{pet.PetId}";
                        }
                        else return "Regp_n|0|animal already exists";
                    }
                    else return "Regp_n|0|wrong user id";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    return "Regp_n|0";
                }
            }
        }

        public static string EnterPetProfile()
        {
            return "";
        }

        private static bool ApproveId(int id, string kind)
        {
            using (PetCareEntities context = new PetCareEntities())
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
}
