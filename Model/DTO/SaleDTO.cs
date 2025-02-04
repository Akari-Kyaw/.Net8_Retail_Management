using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ApplicationConfig;

namespace Model.DTO
{
    public class AddSaleDTO
    {
        public Guid ? ProductId { get; set; }
        public string Name { get; set; }
        public int? Qty { get; set; }
        public string ? Created_by {  get; set; }

    }
    public  class Show
    {
        public decimal? Total { get; set; } = 0;
        public decimal? TotalProfit { get; set; } = 0;


    }
    public class DeleteReportDTO
    {
        public Guid? SaleId { get; set; }
    }
}
