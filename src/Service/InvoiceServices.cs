using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Transactions;
using UnitOfWork.Interfaces;

namespace Services
{
    public  class InvoiceServices
    {
        private IUnitOfWork _unitOfWork;

        public InvoiceServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Invoice> GetAll()
        {
 
         

                using (var context = _unitOfWork.Create())
                {
                   var  result = context.Repositories.InvoiceRepository.GetAll();

                    foreach(var item in result)
                    {
                        item.Client = context.Repositories.ClientRepository.Get(item.ClientId);
                        item.Details = context.Repositories.InvoiceDetailRepository.GetAllByInvoiceId(item.Id);

                        foreach(var value in item.Details)
                        {
                            value.product = context.Repositories.ProductRepository.Get(value.ProductsId);
                        }
                    }

                    return result;
                }
               

        }

        public Invoice InvoiceGet(int Id)
        {
            var result = new Invoice();
            try
            {
               

                using(var context = _unitOfWork.Create())
                {

                    result = context.Repositories.InvoiceRepository.Get(Id);
                    result.Client = context.Repositories.ClientRepository.Get(result.ClientId);
                    result.Details = context.Repositories.InvoiceDetailRepository.GetAllByInvoiceId(result.Id);

                    foreach(var item in result.Details)
                    {
                        item.product = context.Repositories.ProductRepository.Get(item.ProductsId);
                    }

                    return result;
                }
            
                      
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public void Create(Invoice model)
        {
            PrepareOrder(model);

            using(var context = _unitOfWork.Create())
            {
                context.Repositories.InvoiceRepository.Create(model);
                   

                context.Repositories.InvoiceDetailRepository.Create(model.Details, model.Id);

                context.SaveChanges();
                
            }

               
        }


        public void Update(Invoice model)
        {
            PrepareOrder(model);

            using(var context = _unitOfWork.Create())
            {
                context.Repositories.InvoiceRepository.Update(model);

                context.Repositories.InvoiceDetailRepository.RemoveById(model.Id);
                context.Repositories.InvoiceDetailRepository.Create(model.Details, model.Id);

                context.Repositories.InvoiceDetailRepository.Create(model.Details, model.Id);

                context.SaveChanges();


            }
            

        }

        public void Delete(Invoice model)
        {
            using(var context = _unitOfWork.Create())
            {
                context.Repositories.InvoiceRepository.Remove(model);
            }
              

        }

      
        

        private void PrepareOrder(Invoice model)
        {
            foreach (var item in model.Details)
            {
                item.Total = item.Quantity * item.Price;
                item.Iva = item.Total * Parametro.IvaRate;
                item.SubTotal = item.Total - item.Iva;
            }

            model.Total = model.Details.Sum(x => x.Total);
            model.Iva = model.Details.Sum(x => x.Iva);
            model.SubTotal = model.Details.Sum(x => x.SubTotal);
        }


  
    }
}
