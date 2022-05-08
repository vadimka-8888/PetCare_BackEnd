using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using PetCareBackEnd.Models;

namespace PetCareBackEnd.Controllers
{
    public class DeleteController : Controller
    {
        private PetCareEntities context;
        public DeleteController(PetCareEntities db)
        {
            context = db;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNote(int note_id)
        {
            if (ApproveId(note_id, "note"))
            {
                Note note = context.Notes.Find(note_id);
                context.Notes.Remove(note);
                await context.SaveChangesAsync();
                return Json($"Successful");
            }
            else return Json("Not successful");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMention(int mention_id)
        {
            if (ApproveId(mention_id, "mention"))
            {
                Mention mention = context.Mentions.Find(mention_id);
                context.Mentions.Remove(mention);
                await context.SaveChangesAsync();
                return Json($"Successful");
            }
            else return Json("Not successful");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOverexposure(int overexposure_id)
        {
            if (ApproveId(overexposure_id, "mention"))
            {
                Overexposure overexposure = context.Overexposures.Find(overexposure_id);
                context.Overexposures.Remove(overexposure);
                await context.SaveChangesAsync();
                return Json($"Successful");
            }
            else return Json("Not successful");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteIllness(int illness_id)
        {
            if (ApproveId(illness_id, "illness"))
            {
                Illness illness = context.Illnesses.Find(illness_id);
                context.Illnesses.Remove(illness);
                await context.SaveChangesAsync();
                return Json($"Successful");
            }
            else return Json("Not successful");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVaccination(int vaccination_id)
        {
            if (ApproveId(vaccination_id, "vaccination"))
            {
                Vaccination vaccination = context.Vaccinations.Find(vaccination_id);
                context.Vaccinations.Remove(vaccination);
                await context.SaveChangesAsync();
                return Json($"Successful");
            }
            else return Json("Not successful");
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
                    case "vaccination":
                        res = context.Vaccinations.Any(v => v.VaccinationId == id);
                        break;
                    default:
                        break;
                }
            }
            return res;
        }
    }
}
