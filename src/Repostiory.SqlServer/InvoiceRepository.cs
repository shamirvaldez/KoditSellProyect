using Models;
using Repository.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repostiory.SqlServer
{
    public class InvoiceRepository : Repository , IInvoiceRepository
    {

        public InvoiceRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;

        }

        

        public void Create(Invoice model)
        {
            var query = "insert into invoices(ClientId, Iva, SubTotal, Total) output INSERTED.ID values (@ClientId, @Iva, @SubTotal, @Total)";
            var comand = CreateCommand(query);

            comand.Parameters.AddWithValue("@ClientId", model.ClientId);
            comand.Parameters.AddWithValue("@Iva", model.Iva);
            comand.Parameters.AddWithValue("@SubTotal", model.SubTotal);
            comand.Parameters.AddWithValue("@Total", model.Total);

            model.Id = Convert.ToInt32(comand.ExecuteScalar());
        }

        public Invoice Get(int id)
        {
            var result = new Invoice();
            try
            {
               

                var comand = CreateCommand("SELECT * FROM INVOICES WHERE id = @Id");
                    comand.Parameters.AddWithValue("@Id", id);

                    using (var reader = comand.ExecuteReader())
                    {
                        reader.Read();


                        result.Id = Convert.ToInt32(reader["id"]);
                        result.ClientId = Convert.ToInt32(reader["CLientId"]);
                        result.Total = Convert.ToDecimal(reader["Total"]);
                        result.Iva = Convert.ToDecimal(reader["Iva"]);
                        result.SubTotal = Convert.ToDecimal(reader["SubTotal"]);

                    }
                //client
                //  SetCLient(result, context);

                //SetDetails(result, context);

                return result;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public IEnumerable<Invoice> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(Invoice id)
        {
     

            var command = CreateCommand("DELETE FROM invoices WHERE id = @Id");
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        public void Update(Invoice model)
        {
            var query = "update invoices set ClientId = @ClientId, Iva = @Iva, SubTotal =  @SubTotal, Total = @Total where id = @Id";
            var comand = CreateCommand(query);

            comand.Parameters.AddWithValue("@ClientId", model.ClientId);
            comand.Parameters.AddWithValue("@Iva", model.Iva);
            comand.Parameters.AddWithValue("@SubTotal", model.SubTotal);
            comand.Parameters.AddWithValue("@Total", model.Total);
            comand.Parameters.AddWithValue("@id", model.Id);

            comand.ExecuteNonQuery();
        }
    }
}
