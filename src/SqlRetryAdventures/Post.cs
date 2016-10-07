using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRetryAdventures
{
    public class Post
    {
        public Post()
        {
            Comments = new List<Comment>();
        }

        public int PostId { get; set; }
        public int UserId { get; set; }

        [Required, StringLength(250)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
