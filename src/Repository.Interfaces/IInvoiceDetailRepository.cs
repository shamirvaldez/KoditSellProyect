using Models;
using Repository.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
   public interface IInvoiceDetailRepository : IReadRepository<InvoiceDetail, int>
   {
        public IEnumerable<InvoiceDetail> GetAllByInvoiceId(int InvoiceId);

        public void Create(IEnumerable<InvoiceDetail> model, int InvoiceId);

        public void RemoveById(int id);
   }
}
