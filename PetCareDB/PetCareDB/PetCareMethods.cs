using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetCareDB.EF;
using System.Text.Json;

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
                        return $"Reg_u_successful|{user.UserId}";
                    }
                    else return "Reg_u_notsuccessful|0|user already exists";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    return "Reg_u_notsuccessful|0";
                }
            }
        }

        public static string EnterUserProfile(string email, string password)
        {
            using (PetCareEntities context = new PetCareEntities())
            {
                try
                {
                    List<string> Result = new List<string>();
                    User user = context.Users.First(u => u.Email == email && u.Password == password);
                    //context.Entry(user).Collection(c => c.Pets).Load();
                    UserInformation u_inf = new UserInformation
                    {
                        fname = user.FirstName,
                        lname = user.LastName,
                        email = null,
                        password = null,
                        district = user.District,
                        confirmation = user.ReadyForOvereposure
                    };

                    Result.Add(JsonSerializer.Serialize(u_inf));
                    foreach (Pet pet in user.Pets)
                    {
                        PetInformation p_inf = new PetInformation
                        {
                            animal = pet.Animal,
                            name = pet.Name,
                            date_of_birth = pet.DateOfBirth,
                            gender = pet.Gender,
                            weight = pet.Weight,
                            color = pet.Color,
                            photo = pet.Photo
                        };
                        Result.Add(JsonSerializer.Serialize(p_inf));

                        foreach (Illness illness in pet.Illnesses)
                        {
                            IllnessInformation i_inf = new IllnessInformation
                            {
                                type = illness.Type,
                                date_of_begining = illness.DateOfBegining,
                                date_of_ending = illness.DateOfEnding
                            };
                            Result.Add(JsonSerializer.Serialize(i_inf));
                        }
                    }

                    foreach (Note n in user.Notes)
                    {
                        NoteInformation n_inf = new NoteInformation
                        {
                            date = n.Date,
                            n_text = n.TextOfNote
                        };
                        Result.Add(JsonSerializer.Serialize(n_inf));
                    }

                    foreach (Mention m in user.Mentions)
                    {
                        MentionInformation m_inf = new MentionInformation
                        {
                            date = m.Date,
                            m_text = m.TextOfMention
                        };
                        Result.Add(JsonSerializer.Serialize(m_inf));
                    }

                    SendQuery query = new SendQuery
                    {
                        result = "Ent_p_successful",
                        data = Result
                    };
                    return JsonSerializer.Serialize(query);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    SendQuery query = new SendQuery
                    {
                        result = "Ent_p_notsuccessful",
                        data = null
                    };
                    return JsonSerializer.Serialize(query);
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

        //addition of new data
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
                            return $"Reg_p_successful|{pet.PetId}";
                        }
                        else return "Reg_p_notsuccessful|0|animal already exists";
                    }
                    else return "Reg_p_notsuccessful|0|wrong user id";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    return "Reg_p_notsuccessful|0";
                }
            }
        }

        public static string RegisterIllness(int pet_id, string type, DateTime date_of_begining, DateTime date_of_ending)
        {
            using (PetCareEntities context = new PetCareEntities())
            {
                try
                {
                    if (ApproveId(pet_id, "pet"))
                    {
                        var illnesses = context.Illnesses.Where(i => i.Type == type && i.DateOfBegining == date_of_begining && i.DateOfEnding == date_of_ending).Select(i => new { Type = i.Type });
                        if (illnesses.Count() == 0)
                        {
                            var illness = new Illness
                            {
                                PetId = pet_id,
                                Type = type,
                                DateOfBegining = date_of_begining,
                                DateOfEnding = date_of_ending
                            };
                            context.Illnesses.Add(illness);
                            context.SaveChanges();
                            return $"Reg_i_successful|{illness.IllnessId}";
                        }
                        else return "Reg_i_notsuccessful|0|illness already exists";
                    }
                    else return "Reg_i_notsuccessful|0|wrong pet id";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    return "Reg_i_notsuccessful|0";
                }
            }
        }

        public static string RegisterNote(int user_id, string text, DateTime date)
        {
            using (PetCareEntities context = new PetCareEntities())
            {
                try
                {
                    if (ApproveId(user_id, "user"))
                    {
                        var notes = context.Notes.Where(n => n.TextOfNote == text && n.Date == date).Select(n => new { TextOfNote = n.TextOfNote });
                        if (notes.Count() == 0)
                        {
                            var note = new Note
                            {
                                UserId = user_id,
                                TextOfNote = text,
                                Date = date
                            };
                            context.Notes.Add(note);
                            context.SaveChanges();
                            return $"Reg_n_successful|{note.NoteId}";
                        }
                        else return "Reg_n_notsuccessful|0|note already exists";
                    }
                    else return "Reg_n_notsuccessful|0|wrong user id";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    return "Reg_n_notsuccessful|0";
                }
            }
        }

        public static string RegisterMention(int user_id, string text, DateTime date, TimeSpan time)
        {
            using (PetCareEntities context = new PetCareEntities())
            {
                try
                {
                    if (ApproveId(user_id, "user"))
                    {
                        var mentions = context.Mentions.Where(m => m.TextOfMention == text && m.Date == date && m.Time == time).Select(m => new { TextOfMention = m.TextOfMention });
                        if (mentions.Count() == 0)
                        {
                            var mention = new Mention
                            {
                                UserId = user_id,
                                TextOfMention = text,
                                Date = date,
                                Time = time
                            };
                            context.Mentions.Add(mention);
                            context.SaveChanges();
                            return $"Reg_m_successful|{mention.MentionId}";
                        }
                        else return "Reg_m_notsuccessful|0|mention already exists";
                    }
                    else return "Reg_m_notsuccessful|0|wrong user id";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    return "Reg_m_notsuccessful|0";
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
}
