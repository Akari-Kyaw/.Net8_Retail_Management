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
