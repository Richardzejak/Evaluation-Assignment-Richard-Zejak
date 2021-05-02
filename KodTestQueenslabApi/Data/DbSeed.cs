using KodTestQueenslabApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KodTestQueenslabApi.Data
{
    public class DbSeed
    {
        public static void Seeder(Context context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();

            // Seeds in some sample departmens
            var departments = new Department[]
            {
                new Department { Name = "Provisions", Details = "All sorts of food." },
                new Department { Name = "Hobby", Details = "Outdoor activity stuff, sports and camping." },
                new Department { Name = "Electronics", Details = "Computer-related things, including phones and entertainement media." },
                new Department { Name = "Office", Details = "Office accessories, also books and arts." },
                new Department { Name = "Clothing", Details = "All four seasons clothing, for her, him and youngsters." }
            };

            context.Departments.AddRange(departments);
            context.SaveChanges();
        }
    }
}
