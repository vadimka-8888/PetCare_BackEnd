﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetCareDB.EF;

namespace PetCareDB
{
    public static class PetCareMethods
    {
        public static int AddNewUser(string fname, string lname, string email, string password, string district, bool confirmation)
        {
            using (PetCareEntities context = new PetCareEntities())
            {
                try
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
                    return user.UserId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    return 0;
                }
            }
        }

        public static void RemoveUser(int user_id)
        {
            using (PetCareEntities context = new PetCareEntities())
            {
                User user_to_delete = context.Users.Find(user_id);
                if (user_to_delete != null)
                {
                    context.Users.Remove(user_to_delete);
                    context.SaveChanges();
                }
            }
        }
    }
}