using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BAL.IService;
using Microsoft.Identity.Client;
using Model.DTO;
using Model.Entities;
using Repository.UnitOfWork;

namespace BAL.Services
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddProduct(AddProductDTO inputModel)
        {
            try
            {
                var addproduct = new Products()
                {
                    Name=inputModel.Name,
                    RemainingStock=inputModel.RemainingStock,
                    SellingPrice=inputModel.SellingPrice,
                    Profit=inputModel.Profit,
                    Created_by=inputModel.CreatedBy,
                };
                await _unitOfWork.Products.Add(addproduct);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task UpdateProduct(UpdateProductDTO inputModel)
        {
            try
            {
                var updateproduct = (await _unitOfWork.Products.GetByCondition(p=>p.ProductId==inputModel.ProductId && p.ActiveFlag==true)).FirstOrDefault();
                if (updateproduct != null)
                {
                    updateproduct.Name = inputModel.Name;
                    updateproduct.RemainingStock = inputModel.RemainingStock;
                    updateproduct.SellingPrice = inputModel.SellingPrice;
                    updateproduct.Profit = inputModel.Profit;
                    updateproduct.Updated_by = inputModel.Update_by;
                    _unitOfWork.Products.Update(updateproduct);
                }
                
                    await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception) 
            {
                throw;
            }

        }
        public async Task DeleteProduct(DeleteProductDTO inputModel)
        {
            try
            {
                var deleteproduct = (await _unitOfWork.Products.GetByCondition(p => p.ProductId == inputModel.ProductId && p.ActiveFlag == true)).FirstOrDefault();
                if (deleteproduct != null)
                {
                    deleteproduct.ActiveFlag = false;
                   _unitOfWork.Products.Update(deleteproduct);
                    await _unitOfWork.SaveChangesAsync();                   
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    } 
}
