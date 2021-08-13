using Models;
using Repository.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces.Actions
{
    public interface IInvoiceRepository : IReadRepository<Invoice, int>, ICreateRepository<Invoice>, IRemoveRepository<Invoice>, IUpdateRepository<Invoice>
    {
    }
}
