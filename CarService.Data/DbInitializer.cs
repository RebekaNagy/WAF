using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.Data
{
    public static class DbInitializer
    {
        public static async void Initialize(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var userManager = provider.GetRequiredService<UserManager<CarServiceUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            bool alreadyCreated = await roleManager.RoleExistsAsync("Mechanic");

            if (!alreadyCreated)
            {
                await roleManager.CreateAsync(new IdentityRole("Mechanic"));
                await roleManager.CreateAsync(new IdentityRole("Client"));

                for (int i = 0; i < 3; i++)
                {
                    var mechanic = new CarServiceUser
                    {
                        UserName = $"mech{i}@mech.com",
                        Name = $"Mechanic{i}"
                    };
                    var mechanicPWD = "Mec!23";

                    var result = await userManager.CreateAsync(mechanic, mechanicPWD);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(mechanic, "Mechanic");
                    }

                    var material = new Cost
                    {
                        Name = $"Material{i}",
                        Amount = (i + 1) * 100
                    };
                    context.Costs.Add(material);

                    var hour = new Cost
                    {
                        Name = $"Hourly wage{i}",
                        Amount = (i + 1) * 100
                    };
                    context.Costs.Add(hour);

                }
                context.SaveChanges();
                for (int i = 0; i < 3; i++)
                {
                    var client = new CarServiceUser
                    {
                        UserName = $"test{i}@test.com",
                        Name = $"Tester{i}"
                    };
                    var clientPWD = "Tes!23";

                    var result = await userManager.CreateAsync(client, clientPWD);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(client, "Client");
                    }

                    var materiall = new Cost
                    {
                        Name = $"Material{i+3}",
                        Amount = (i+3) * 100
                    };
                    context.Costs.Add(materiall);

                    var part = new Cost
                    {
                        Name = $"Part{i}",
                        Amount = (i + 1) * 100
                    };
                    context.Costs.Add(part);

                }
            }
            context.SaveChanges();
        }
    }
}
