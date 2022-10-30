using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;


namespace ORM_Dapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            var dRepo = new DapperDepartmentRepository(conn);
            dRepo.InsertDepartment("EVEN MORE Fun Stuff");
            var departments = dRepo.GetAllDepartments();
            foreach (var department in departments)
            {
                Console.WriteLine($"{department.DepartmentID} {department.Name} \n");
            }

            Console.WriteLine("-------------------------------\n");

            var pRepo = new DapperProductRepository(conn);
            pRepo.CreateProduct("A BETTER Newwwwww Car!", 99.99, 10);
            var products = pRepo.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.CategoryID} {product.Price} {product.Name} \n");
            }

            Console.WriteLine("-------------------------------\n");

            var productUpdate = pRepo.GetProduct(2);
            productUpdate.Name = "Lenovo Yoga";
            productUpdate.Price = 1;
            productUpdate.CategoryID = 1;
            productUpdate.StockLevel = 1;
            productUpdate.OnSale = true;   
            pRepo.UpdateProduct(productUpdate);

            Console.ReadKey();
        }
    }
}