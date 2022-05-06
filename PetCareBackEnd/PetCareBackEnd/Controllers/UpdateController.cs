using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareBackEnd.DataForPost;
using PetCareBackEnd.Models;

namespace PetCareBackEnd.Controllers
{
    public class UpdateController : Controller
    {
        private PetCareEntities context;
        public UpdateController(PetCareEntities db)
        {
            context = db;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInformation([FromBody] FieldInformation ep)
        {
            object value_to_change = null;
            if (ep is FieldInformationStr)
            {
                value_to_change = ((FieldInformationStr)ep).new_value;
            }
            else if (ep is FieldInformationFl)
            {
                value_to_change = ((FieldInformationFl)ep).new_value;
            }
            else if (ep is FieldInformationByte)
            {
                value_to_change = ((FieldInformationByte)ep).new_value;
            }
            else if (ep is FieldInformationInt)
            {
                value_to_change = ((FieldInformationInt)ep).new_value;
            }
            else if (ep is FieldInformationBool)
            {
                value_to_change = ((FieldInformationBool)ep).new_value;
            }

            switch (ep.what.ToLower())
            {
                case "email":
                    if (ApproveId(ep.id, "user"))
                    {
                        User user = context.Users.Find(ep.id);
                        user.Email = (string)value_to_change;
                        await context.SaveChangesAsync();
                        return Json($"Successful");
                    }
                    else return Json("Not successful");
                case "district":
                    if (ApproveId(ep.id, "user"))
                    {
                        User user = context.Users.Find(ep.id);
                        user.District = (string)value_to_change;
                        await context.SaveChangesAsync();
                        return Json($"Successful");
                    }
                    else return Json("Not successful");
                case "name":
                    if (ApproveId(ep.id, "user"))
                    {
                        User user = context.Users.Find(ep.id);
                        user.FirstName = (string)value_to_change;
                        await context.SaveChangesAsync();
                        return Json($"Successful");
                    }
                    else return Json("Not successful");
                case "lastname":
                    if (ApproveId(ep.id, "user"))
                    {
                        User user = context.Users.Find(ep.id);
                        user.LastName = (string)value_to_change;
                        await context.SaveChangesAsync();
                        return Json($"Successful");
                    }
                    else return Json("Not successful");
                case "pet_name":
                    if (ApproveId(ep.id, "pet"))
                    {
                        Pet pet = context.Pets.Find(ep.id);
                        pet.Name = (string)value_to_change;
                        await context.SaveChangesAsync();
                        return Json($"Successful");
                    }
                    else return Json("Not successful");
                case "weight":
                    if (ApproveId(ep.id, "pet"))
                    {
                        Pet pet = context.Pets.Find(ep.id);
                        pet.Weight = (float)value_to_change;
                        await context.SaveChangesAsync();
                        return Json($"Successful");
                    }
                    else return Json("Not successful");
                case "pet_photo":
                    if (ApproveId(ep.id, "pet"))
                    {
                        Pet pet = context.Pets.Find(ep.id);
                        pet.Photo = (byte[])value_to_change;
                        await context.SaveChangesAsync();
                        return Json($"Successful");
                    }
                    else return Json("Not successful");
                case "overexposure_note":
                    if (ApproveId(ep.id, "overexposure"))
                    {
                        Overexposure overexposure = context.Overexposures.Find(ep.id);
                        overexposure.ONote = (string)value_to_change;
                        await context.SaveChangesAsync();
                        return Json($"Successful");
                    }
                    else return Json("Not successful");
                case "overexposure_cost":
                    if (ApproveId(ep.id, "overexposure"))
                    {
                        Overexposure overexposure = context.Overexposures.Find(ep.id);
                        overexposure.Cost = (int)value_to_change;
                        await context.SaveChangesAsync();
                        return Json($"Successful");
                    }
                    else return Json("Not successful");
                case "state":
                    if (ApproveId(ep.id, "user"))
                    {
                        User user = context.Users.Find(ep.id);
                        user.ReadyForOvereposure = (bool)value_to_change;
                        await context.SaveChangesAsync();
                        return Json($"Successful");
                    }
                    else return Json("Not successful");
                case "note":
                    if (ApproveId(ep.id, "note"))
                    {
                        Note note = context.Notes.Find(ep.id);
                        note.TextOfNote = (string)value_to_change;
                        note.Date = DateTime.Now;
                        await context.SaveChangesAsync();
                        return Json($"Successful");
                    }
                    else return Json("Not successful");
                default:
                    return Json("Not successful");
            }
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
