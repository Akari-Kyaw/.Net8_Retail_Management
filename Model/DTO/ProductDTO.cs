using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class AddProductDTO
    {

        public string? Name { get; set; }
        public int? RemainingStock { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? Pofit {  get; set; }
        public string? CreatedBy { get; set; }

    }
    public class  UpdateProductDTO
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public int? RemainingStock { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? Pofit { get; set; }
        public string? Update_by { get; set; }

    }
    public class DeleteProductDTO
    {
        public Guid ProductId { get; set; }

    }
}
