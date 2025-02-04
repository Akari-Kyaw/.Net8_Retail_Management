using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ApplicationConfig;

namespace Model.Entities
{
    [Table("Retail_Sale")]
    public class Retail_Sale : Common
    {
        [Key]
        public Guid SaleId { get; set; } = new Guid();
        public Guid ? ProductId { get; set; }
        public string? Name { get; set; }
        public int ? Qty { get; set; }
        public decimal ? TotalPrice { get; set; }
        public decimal ? TotalProfit { get; set; }
    }
}
