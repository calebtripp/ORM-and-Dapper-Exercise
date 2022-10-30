using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Utilities.Collections;
using System.Runtime.CompilerServices;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _conn.Execute("INSERT INTO Products (Name, Price, CategoryID) " +
                "VALUES (@name, @price, @categoryID)",
                new { name = name, price = price, categoryID = categoryID });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM Products ORDER BY CategoryID;");
        }

        public Product GetProduct(int productID)
        {
            return _conn.QuerySingle<Product>("SELECT * FROM Products WHERE ProductID = @productID;", new { productID = productID });
        }            

        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE Products SET Name = @name, Price = @price, categoryID = @categoryID,OnSale = @onSale, Stocklevel = @stockLevel WHERE ProductID = @productID",
                new {name = product.Name,
                    price = product.Price,
                    categoryID = product.CategoryID,
                    onSale = product.OnSale,
                    stockLevel = product.StockLevel,
                    productID = product.ProductID});
        }
    }
}

