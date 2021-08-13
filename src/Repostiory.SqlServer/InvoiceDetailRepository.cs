using Models;
using Repository.Interfaces;
using Repository.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repostiory.SqlServer
{
    public class InvoiceDetailRepository : Repository , IInvoiceDetailRepository
    {

        public InvoiceDetailRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;

        }

        public void Create(IEnumerable<InvoiceDetail> model, int InvoiceId)
        {
            var query = "insert into invoiceDetail (InvoiceId, ProductId, Quantity, Price, Iva, SubTotal, Total)  values (@InvoiceId, @ProductId, @Quantity, @Price, @Iva, @SubTotal, @Total)";
            var comand = CreateCommand(query);

            foreach (var item in model)
            {

                comand.Parameters.AddWithValue("@InvoiceId", InvoiceId);
                comand.Parameters.AddWithValue("@ProductId", item.ProductsId);
                comand.Parameters.AddWithValue("@Quantity", item.Quantity);
                comand.Parameters.AddWithValue("@Price", item.Price);
                comand.Parameters.AddWithValue("@Iva", item.Iva);
                comand.Parameters.AddWithValue("@SubTotal", item.SubTotal);
                comand.Parameters.AddWithValue("@Total", item.Total);

                comand.ExecuteNonQuery();
                comand.Parameters.Clear();

            }
        }
        public IEnumerable<InvoiceDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveById(int id)
        {
            var query = "Delete from  invoiceDetail where InvoiceId = @InvoiceId";
            var comand = CreateCommand(query);

            comand.Parameters.AddWithValue("@InvoiceId", id);

            comand.ExecuteNonQuery();
        }

        public IEnumerable<InvoiceDetail> GetAllByInvoiceId(int InvoiceId)
       {
            var result = new List<InvoiceDetail>();

            var comand = CreateCommand("SELECT * FROM InvoiceDetail WITH(NOLOCK) WHERE InvoiceId = @InvoiceId");

            comand.Parameters.AddWithValue("@InvoiceId", InvoiceId);

            using (var reader = comand.ExecuteReader())
            {
                while (reader.Read())
                {

                    result.Add(new InvoiceDetail
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        ProductsId = Convert.ToInt32(reader["ProductId"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Iva = Convert.ToDecimal(reader["Iva"]),
                        SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                        Total = Convert.ToDecimal(reader["Total"]),
             

                    });
                }
            }

            return result;

        }

       

        InvoiceDetail IReadRepository<InvoiceDetail, int>.Get(int id)
        {
            throw new NotImplementedException();
        }

      
    }
}
