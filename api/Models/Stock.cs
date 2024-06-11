using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Stock
    {
        public int Id {get; set; }
        public string Symbol {get; set; } = string.Empty;
        public string CompanyName {get; set; } = string.Empty;
        // define the format of the value in this column
        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase {get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv {get; set; }
        public string Industry {get; set; } = string.Empty;
        public long MarketCap {get; set; }

        //Mapping
        //One To Many
        public List<Comment> Comments {get; set; } = new List<Comment>();
    }
}   