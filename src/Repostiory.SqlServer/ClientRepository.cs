using Models;
using Repository.Interfaces;
using Repository.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repostiory.SqlServer
{
    public class ClientRepository : Repository , IClientRepository
    {

        public ClientRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;

        }

 
        public Client Get(int id)
        {
            var comand = CreateCommand("SELECT * FROM Clients WITH(NOLOCK) WHERE id = @ClientId");
            comand.Parameters.AddWithValue("@ClientId", id);

            using (var reader = comand.ExecuteReader())
            {
                reader.Read();

                return new Client()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = Convert.ToString(reader["name"])
                };
            }
        }

        public IEnumerable<Client> GetAll()
        {
            throw new NotImplementedException();
        }

      
    }
}
