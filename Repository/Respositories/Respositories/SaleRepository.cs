using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;
using Model;
using Repository.Respositories.IRespositories;

namespace Repository.Respositories.Respositories
{
    internal class SaleRespository : GenericRepository<Retail_Sale>, ISaleRepository
    {
        public SaleRespository(DataContext context) : base(context) { }
    }
}
