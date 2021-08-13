using Models;
using Repository.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repostiory.SqlServer
{
    public class ProductRepository : Repository , IProductRepository
    {

        public ProductRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;

        }

        public void Create(Invoice t)
        {
           
        }

        public Product Get(int id)
        {
           
                var comand = CreateCommand("SELECT * FROM Products WHERE id = @ProductId");
                comand.Parameters.AddWithValue("@ProductId",id);

                using (var reader = comand.ExecuteReader())
                {
                    reader.Read();

                    return  new Product
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = Convert.ToString(reader["Name"]),
                        Price = Convert.ToDecimal(reader["Price"])
                    };
                }           
       
        }

        public IEnumerable<Invoice> GetAll()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Product> IReadRepository<Product, int>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
