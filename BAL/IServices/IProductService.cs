using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;

namespace BAL.IService
{
    public interface IProductService
    {
        Task AddProduct(AddProductDTO inputModel);
        Task UpdateProduct(UpdateProductDTO inputModel);
        Task DeleteProduct(DeleteProductDTO inputModel);
        


    }
}
