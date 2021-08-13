using Models;
using Service;
using Services;
using System;
using System.Collections.Generic;
using UnitOfWork.SqlServer;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestServices.TestConnection();

            var unitOFwork = new UnitOfWorkSqlServer();

            var InvoiceService = new InvoiceServices(unitOFwork);

            var result = InvoiceService.InvoiceGet(2);

            //var resultGet = InvoiceService.InvoiceGet(2);

            var invoice = new Invoice()
            {
                ClientId = 1,
                Details = new List<InvoiceDetail>()
                {
                    new InvoiceDetail()
                    {
                        ProductsId = 1,
                        Quantity = 5,
                        Price = 1500
                    },

                    new InvoiceDetail()
                    {
                        ProductsId = 8,
                        Quantity = 15,
                        Price = 125
                    },

                }
            };

            InvoiceService.Create(invoice);

            //var invoice = new Invoice()
            //{
            //    ClientId = 1,
            //    Id = 42,
            //    Details = new List<InvoiceDetail>()
            //    {
            //        new InvoiceDetail()
            //        {
            //            ProductsId = 1,
            //            Quantity = 5,
            //            Price = 1500
            //        },

            //        new InvoiceDetail()
            //        {
            //            ProductsId = 8,
            //            Quantity = 15,
            //            Price = 125
            //        },

            //    }
            //};

            //InvoiceService.Delete(40);


            Console.ReadKey();
        }
    }
}
