﻿using Microsoft.Extensions.Configuration;
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
            var departmentRepo = new DapperDepartmentRepository(conn);
            var departments = departmentRepo.GetAllDepartments();

            departmentRepo.InsertDepartment("More Fun Stuff");

            foreach (var department in departments)
            {
                Console.WriteLine($"{department.Name} {department.DepartmentID}\n");
            }
        }
    }
}