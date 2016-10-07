using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRetryAdventures
{
    public class User
    {
        public User()
        {
            Posts = new List<Post>();
            CreateDate = DateTime.UtcNow;
        }

        public int UserId { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), StringLength(250)]
        public string Password { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
