using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestEfCoreManyToMany.Data;

namespace TestEfCoreManyToMany
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MyContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var hobbies = new List<Hobbie>()
                {
                    new Hobbie()
                    {
                        LastUpdate = DateTime.Now,
                        Name = "SSSS"
                    }
                };

                context.AddRange(hobbies);
                context.SaveChanges();

                hobbies = context.Set<Hobbie>().ToList();

                var fb = new FacebookUser()
                {
                    UserName = "xxzezx",
                    FacebookId = "1958472731", //Fake id,
                    Hobbies = hobbies.Select(h => new FacebookUsersHobbies()
                    {
                        HobbieId = h.Id
                    }).ToList()
                };

                context.Attach(fb).State = EntityState.Added;
                var entries = context.ChangeTracker.Entries().ToList();

                context.SaveChanges();


            }

        }
    }
}
