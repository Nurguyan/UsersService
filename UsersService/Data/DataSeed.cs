using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Models;

namespace UsersService.Data
{
    public static class DataSeed
    {
        public static void PreparePopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<UsersDbContext>());
        }

        private static void SeedData(UsersDbContext context)
        {
            if (!context.Users.Any())
            {
                Console.WriteLine("--> Seeding data...");

                User firstUser = new() { Surname = "Kornilov", Age = 25, Sex = ESex.Male, IsActive = true };
                Phone phone1 = new() { UserId = firstUser.Id, Number = "+79008007070", User = firstUser };
                Phone phone2 = new() { UserId = firstUser.Id, Number = "+79008006060", User = firstUser };
                firstUser.Phones.Append(phone1);
                firstUser.Phones.Append(phone2);

                User secondUser = new() { Surname = "Ivanov", Age = 20, Sex = ESex.Male, IsActive = false };
                Phone phone3 = new() { UserId = secondUser.Id, Number = "+79008005050", User = secondUser };
                secondUser.Phones.Append(phone3);
                
                User thirdUser = new() { Surname = "Petrova", Age = 35, Sex = ESex.Female, IsActive = true };
                Phone phone4 = new() { UserId = thirdUser.Id, Number = "+79008004040", User = thirdUser };
                thirdUser.Phones.Append(phone4);

                context.Users.AddRange(firstUser, secondUser, thirdUser);
                context.Phones.AddRange(phone1, phone2, phone3, phone4);

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
