using Commands.AddProducts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;
using ProductCatalog;
using Queries;
using System;
using System.Collections.Generic;

namespace GenerateTestData
{
    public class Program
    {
        public static string[] Descriptors = new string[] { "Big", "Small", "Broken", "Large", "Huge", "Tiny", "Wrecking" };
        public static string[] Ability = new string[] { "functional", "disfunctional", "smashing", "precise"};
        public static string[] Color = new string[] { "red", "green", "blue", "purple", "yellow", "black"};
        public static string[] Items = new string[] { "hammer", "nails", "screwdriver", "jigsaw", "saw", "screw", "sledgehammer", "router" };

        static void Main(string[] args)
        {
            var products = GenerateTestProducts(100);

            var connectionstring = "Server=(localdb)\\mssqllocaldb;Database=Products;Trusted_Connection=True;";
            var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
            optionsBuilder.UseSqlServer(connectionstring);
            ProductDbContext dbContext = new ProductDbContext(optionsBuilder.Options);
            var pr = new ProductRepository(dbContext);
            var er = new EventRepository(dbContext);

            foreach (AddProductDTO product in products) {
                var addProductCommand = new AddProductCommand(pr,er);
                addProductCommand.ProductDTO = product;
                addProductCommand.Execute();
            }

        }


        private static List<AddProductDTO> GenerateTestProducts(int amount) {
            Random random = new Random();
            List<AddProductDTO> products = new List<AddProductDTO>();
            for (int i = 0; i < amount; i++) {
                string descriptor = Descriptors[random.Next(0, Descriptors.Length)];
                string ability = Ability[random.Next(0, Ability.Length)];
                string color = Color[random.Next(0, Color.Length)];
                string items = Items[random.Next(0, Items.Length)];

                var product = new AddProductDTO()
                {
                    Title = descriptor+" "+ability+" "+color +" "+items+".",
                    Description = descriptor + " " + ability + " " + color + " " + items + "."
                };

                products.Add(product);
            }
            return products;
        }
    }
}
