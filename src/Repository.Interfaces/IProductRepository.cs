using Models;
using Repository.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces.Actions
{
    public interface IProductRepository : IReadRepository<Product, int>
    {
    }
}
