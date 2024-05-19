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
                
                if(!context.TypeProducts.Any()){
                    context.TypeProducts.AddRange(
                        new TypeProduct { NameTypeProduct = "Tecnología"},
                        new TypeProduct { NameTypeProduct = "Electrhogar"},
                        new TypeProduct { NameTypeProduct = "Juguetería"},
                        new TypeProduct { NameTypeProduct = "Ropa"},
                        new TypeProduct { NameTypeProduct = "Muebles"},
                        new TypeProduct { NameTypeProduct = "Comida"},
                        new TypeProduct { NameTypeProduct = "Libros"}
                    );
                }

                context.SaveChanges();
                
                if(!context.Products.Any()){
                    var product = new Product {

                        Name = "Televisora",
                        Type = "Tecnología",
                        Price = 1000000,
                        QuantityStock = 10,
                        Image = "https://res.cloudinary.com/dslme7hxd/image/upload/v1716149765/TallerWeb/televisor.png",
                        IdImage = "TallerWeb/televisor"

                    };
                    context.Products.Add(product);

                    var product2 = new Product {

                        Name = "El rey sin corona",
                        Type = "Libros",
                        Price = 100000,
                        QuantityStock = 50,
                        Image = "https://res.cloudinary.com/dslme7hxd/image/upload/v1716149766/TallerWeb/El%20rey%20sin%20corona.png",
                        IdImage = "TallerWeb/El rey sin corona"

                    };
                    context.Products.Add(product2);

                    var product3 = new Product {

                        Name = "Lechugacon",
                        Type = "Tecnología",
                        Price = 1000000,
                        QuantityStock = 10,
                        Image = "https://res.cloudinary.com/dslme7hxd/image/upload/v1716149766/TallerWeb/Lechuga.png",
                        IdImage = "TallerWeb/Lechuga"

                    };
                    context.Products.Add(product3);

                    context.SaveChanges();
                }
            }
        }
    }
}