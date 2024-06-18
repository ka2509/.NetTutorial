using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id {get; set; }
        public string Content {get; set; } = string.Empty;
        public string Title {get; set; } = string.Empty;
        public DateTime CreatedAt {get; set; } = DateTime.Now;
        //Mapping Many To One
        public int? StockId {get; set; }
        // Navigation 
        public Stock? Stock {get; set; }
    }
}