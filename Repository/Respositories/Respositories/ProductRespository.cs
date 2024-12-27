using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Entities;
using Repository.Respositories.IRespositories;

namespace Repository.Respositories.Respositories
{
    internal class ProductRespository:GenericRepository<Products>,IProductRepository
    {
        public ProductRespository(DataContext context):base(context) { }
    }
}
