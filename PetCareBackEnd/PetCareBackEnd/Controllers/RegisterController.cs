using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using PetCareBackEnd.Models;
using PetCareBackEnd.DataForPost;

namespace PetCareBackEnd.Controllers
{
    public class RegisterController : Controller
    {
        private PetCareEntities context;
        public RegisterController(PetCareEntities db)
        {
            context = db;
        }
        
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserPost ep)
        {
            if (ep != null)
            {
                var users = context.Users.Where(u => u.Email == ep.email).Select(u => new { Email = u.Email });
                if (users.Count() == 0)
                {
                    var user0 = new User
                    {
                        FirstName = ep.fname,
                        LastName = ep.lname,
                        Email = ep.email,
                        Password = ep.password,
                        District = ep.district,
                        ReadyForOvereposure = ep.confirmation
                    };
                    context.Users.Add(user0);
                    await context.SaveChangesAsync();
                    return Json($"Successful, user_id = {user0.UserId}");
                }
                else return Json("Not successful, such user already exists");
            }
            else return Json("Not successful: possibly json is incorrect");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPet([FromBody] PetPost pet)
        {
            if (pet != null)
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
                        return Json($"Successful, pet_id = {pet0.PetId}");
                    }
                    else return Json("Not successful, such pet already exists");
                }
                else return Json("Not successful, id does not exist");
            }
            else return Json("Not successful: possibly json is incorrect");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNote([FromBody] NotePost note)
        {
            if (note != null)
            {
                if (ApproveId(note.user_id, "user"))
                {
                    var notes = context.Notes.Where(n => n.TextOfNote == note.text && n.Date == note.date).Select(n => new { TextOfNote = n.TextOfNote });
                    if (notes.Count() == 0)
                    {
                        var note0 = new Note
                        {
                            UserId = note.user_id,
                            TextOfNote = note.text,
                            Date = note.date
                        };
                        context.Notes.Add(note0);
                        await context.SaveChangesAsync();
                        return Json($"Successful, note_id = {note0.NoteId}");
                    }
                    else return Json("Not successful, such note already exists");
                }
                else return Json("Not successful, id does not exist");
            }
            else return Json("Not successful: possibly json is incorrect");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMention([FromBody] MentionPost mention)
        {
            if (mention != null)
            {
                if (ApproveId(mention.user_id, "user"))
                {
                    var mentions = context.Mentions.Where(m => m.TextOfMention == mention.text && m.Date == mention.date && m.Time == mention.time).Select(m => new { TextOfMention = m.TextOfMention });
                    if (mentions.Count() == 0)
                    {
                        var mention0 = new Mention
                        {
                            UserId = mention.user_id,
                            TextOfMention = mention.text,
                            Date = mention.date,
                            Time = mention.time
                        };
                        context.Mentions.Add(mention0);
                        await context.SaveChangesAsync();
                        return Json($"Successful, mention_id = {mention0.MentionId}");
                    }
                    else return Json("Not successful, such mention already exists");
                }
                else return Json("Not successful, id does not exist");
            }
            else return Json("Not successful: possibly json is incorrect");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterOverexposure([FromBody] OverexposurePost overexposure)
        {
            if (overexposure != null)
            {
                if (ApproveId(overexposure.user_id, "user"))
                {
                    var overexposures = context.Overexposures.Where(o => o.Animal == overexposure.animal && o.Cost == overexposure.cost && o.ONote == overexposure.overexposure_note).Select(o => new { Animal = o.Animal });
                    if (overexposures.Count() == 0)
                    {
                        var overexposure0 = new Overexposure
                        {
                            UserId = overexposure.user_id,
                            Animal = overexposure.animal,
                            ONote = overexposure.overexposure_note,
                            Cost = overexposure.cost
                        };
                        context.Overexposures.Add(overexposure0);
                        await context.SaveChangesAsync();
                        return Json($"Successful, overexposure_id = {overexposure0.OverexposureId}");
                    }
                    else return Json("Not successful, such overexposure already exists");
                }
                else return Json("Not successful, id does not exist");
            }
            else return Json("Not successful: possibly json is incorrect");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterIllness([FromBody] IllnessPost illness)
        {
            if (illness != null)
            {
                if (ApproveId(illness.pet_id, "pet"))
                {
                    var illnesses = context.Illnesses.Where(i => i.Type == illness.type && i.DateOfBegining == illness.date_of_begining && i.DateOfEnding == illness.date_of_ending).Select(i => new { Type = i.Type });
                    if (illnesses.Count() == 0)
                    {
                        var illness0 = new Illness
                        {
                            PetId = illness.pet_id,
                            Type = illness.type,
                            DateOfBegining = illness.date_of_begining,
                            DateOfEnding = illness.date_of_ending
                        };
                        context.Illnesses.Add(illness0);
                        await context.SaveChangesAsync();
                        return Json($"Successful, illness_id = {illness0.IllnessId}");
                    }
                    else return Json("Not successful, such illness already exists");
                }
                else return Json("Not successful, id does not exist");
            }
            else return Json("Not successful: possibly json is incorrect");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterVaccination([FromBody] VaccinationPost vaccination)
        {
            if (vaccination != null)
            {
                if (ApproveId(vaccination.pet_id, "pet"))
                {
                    var vaccinations = context.Vaccinations.Where(v => v.Type == vaccination.type && v.Date == vaccination.date).Select(v => new { Type = v.Type });
                    if (vaccinations.Count() == 0)
                    {
                        var vaccination0 = new Vaccination
                        {
                            PetId = vaccination.pet_id,
                            Type = vaccination.type,
                            Date = vaccination.date,
                            OfficialDocument = vaccination.document,
                            NecessityOfRevaccination = vaccination.necessety_of_revaccination
                        };
                        context.Vaccinations.Add(vaccination0);
                        await context.SaveChangesAsync();
                        return Json($"Successful, vaccination_id = {vaccination0.VaccinationId}");
                    }
                    else return Json("Not successful, such vaccination already exists");
                }
                else return Json("Not successful, id does not exist");
            }
            else return Json("Not successful: possibly json is incorrect");
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
