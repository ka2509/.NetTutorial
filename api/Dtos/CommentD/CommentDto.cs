using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.CommentD
{
    public class CommentDto
    {
        public int Id {get; set; }
        public string Content {get; set; } = string.Empty;
        public string Title {get; set; } = string.Empty;
        public DateTime CreatedAt {get; set; } = DateTime.Now;
        //Mapping Many To One
        public int? StockId {get; set; }
    }
}