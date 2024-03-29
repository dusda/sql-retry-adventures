using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRetryAdventures
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public string Content { get; set; }
        public virtual Post Post { get; set; }
    }
}
