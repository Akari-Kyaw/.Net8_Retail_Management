using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ApplicationConfig;

namespace Model.Entities
{
    public class Products : Common
    {
        [Key]
        public Guid ProductId { get; set; } = new Guid();
        public string? Name { get; set; }    
        public int? RemainingStock {  get; set; }    
        public decimal? SellingPrice {  get; set; }  
        public decimal? Profit { get; set; }
    }
}
