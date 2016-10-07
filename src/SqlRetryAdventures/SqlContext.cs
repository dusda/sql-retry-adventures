using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace SqlRetryAdventures
{
    [DbConfigurationType(typeof(AwesomeConfiguration))]
    public partial class SqlContext : DbContext
    {
        public SqlContext()
            : base("Name=SqlContext")
        {
            Database.Log = s => Console.WriteLine(s);
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
