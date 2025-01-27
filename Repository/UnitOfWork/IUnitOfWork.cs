using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Respositories.IRespositories;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IProductRepository Products {  get; }
        ISaleRepository Sales { get; }
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();

    }
}
