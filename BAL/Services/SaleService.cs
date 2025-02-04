using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.IServices;
using Model.DTO;
using Model.Entities;
using Repository.UnitOfWork;

namespace BAL.Services
{
    internal class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SaleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddSale(AddSaleDTO inputModel)
        {
            try
            {
                var saleproduct = (await _unitOfWork.Products.GetByCondition(p => p.ProductId == inputModel.ProductId)).FirstOrDefault();
                if (saleproduct != null)
                {
                    saleproduct.RemainingStock -= inputModel.Qty;
                   
                    var salereport = new Retail_Sale()
                    {
                        ProductId = inputModel.ProductId,
                        Name = inputModel.Name,
                        Qty = inputModel.Qty,
                        TotalPrice = Convert.ToDecimal(inputModel.Qty * saleproduct.SellingPrice),
                        TotalProfit = Convert.ToDecimal(inputModel.Qty*saleproduct.Profit),
                        Created_by = inputModel.Created_by,
                    };
                    await _unitOfWork.Sales.Add(salereport);
                    await _unitOfWork.SaveChangesAsync();
                }
               
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task AddSaleMultiple(IEnumerable <AddSaleDTO> inputModel)
        {
            try
            {
                foreach (var item in inputModel)
                {
                    var saleproduct = (await _unitOfWork.Products.GetByCondition(p => p.ProductId == item.ProductId && p.ActiveFlag==true)).FirstOrDefault();
                    if(saleproduct != null)
                    {
                        if(saleproduct.RemainingStock<=item.Qty){
                            throw new Exception("Product Quantity is not Enought"); 
                       }
                        if (saleproduct.RemainingStock == 0)
                        {
                            throw new Exception("Product Quantity is not be 0");

                        }
                        saleproduct.RemainingStock -= item.Qty;
                        _unitOfWork.Products.Update(saleproduct);
                        var salereport = new Retail_Sale()
                        {
                            ProductId = item.ProductId,
                            Name = item.Name,
                            Qty = item.Qty,
                            TotalPrice = Convert.ToDecimal(item.Qty * saleproduct.SellingPrice),
                            TotalProfit = Convert.ToDecimal(item.Qty * saleproduct.Profit),
                            Created_by = item.Created_by,
                        };
                        await _unitOfWork.Sales.Add(salereport);
                    }
                }
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<Show> ShowReport()
        {
            var salereport = await _unitOfWork.Sales.GetAll();
            if(salereport != null)
            {
                var report = new Show();
                foreach( var sreport in salereport)
                {

                    report.Total +=Convert.ToDecimal (sreport.TotalPrice);
                    report.TotalProfit += Convert.ToDecimal(sreport.TotalProfit);
                }
                return report;

            }

            else
            {
                throw new Exception("No Sale Record");
            }
        }
 
        public async Task DeleteReport(DeleteReportDTO inputModel)
        {
            try
            {
                var deletesale = (await _unitOfWork.Sales.GetByCondition(p => p.SaleId == inputModel.SaleId)).FirstOrDefault();
                if (deletesale != null)
                {
                    deletesale.ActiveFlag = false;
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Sale not found.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
