using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Bogus;
using TallerWeb.Src.Models;

namespace TallerWeb.Src.Data
{
    public class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();

                if(!context.Roles.Any()){
                    context.Roles.AddRange(
                        new Role { Nombre = "Admin"},
                        new Role { Nombre = "Cliente"}
                     );
                }
               
                if(!context.Gender.Any())
                {
                    context.Gender.AddRange(
                        new Gender { Genero = "Masculino"},
                        new Gender { Genero = "Femenino"},
                        new Gender { Genero = "Prefiero no decirlo"},
                        new Gender { Genero = "Otro"}
                    );
                }
                context.SaveChanges();
                
                if(!context.Users.Any())
                {
                    
                    var user = new User {
                         
                        Nombre = "Ignacio Mancilla",
                        FechaNacimiento = new DateTime(2000, 10, 25),
                        Email = "ignacio.mancilla@gmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("P4ssw0rd"),
                        RoleId = 1,
                        GenderId = 1,
                        Rut = "20416699-4",
                        IsActive = true
                        
                    };
                    context.Users.Add(user);

                    var user2 = new User {
                         
                        Nombre = "Pablo Perez",
                        FechaNacimiento = new DateTime(1982, 03, 15),
                        Email = "pablo.perez@gmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("Ab05wkf"),
                        RoleId = 2,
                        GenderId = 1,
                        Rut = "15936652-k",
                        IsActive = true
                        
                    };
                    context.Users.Add(user2);
                    
                    var user3 = new User {
                         
                        Nombre = "Marcos Alberto",
                        FechaNacimiento = new DateTime(2000, 10, 26),
                        Email = "marcos.alberto@gmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("Albertito1"),
                        RoleId = 2,
                        GenderId = 1,
                        Rut = "20743101-k",
                        IsActive = true
                        
                    };
                    context.Users.Add(user3);
                    var user4 = new User {
                         
                        Nombre = "Melany Martinez",
                        FechaNacimiento = new DateTime(2002, 12, 01),
                        Email = "melany.martinez@gmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("DotnetLoMejor24"),
                        RoleId = 2,
                        GenderId = 2,
                        Rut = "21019691-1",
                        IsActive = true
                        
                    };
                    context.Users.Add(user4);

                    
                }
                

                context.SaveChanges();
            }
        }
    }
}