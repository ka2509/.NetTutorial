using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.CommentD
{
    public class CreateCommentDto
    {
        public string Content {get; set; } = string.Empty;
        public string Title {get; set; } = string.Empty;
    }
}