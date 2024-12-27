using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;

namespace BAL.IServices
{
    public interface ISaleService
    {
        Task AddSale(AddSaleDTO inputModel);
        Task<Show> ShowReport();
        Task DeleteReport(DeleteReportDTO inputModel);

    }
}
