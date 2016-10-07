using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRetryAdventures
{
    public class TestContext : DbContext
    {
        public TestContext() : base("Name=SqlContext")
        {
            Database.SetInitializer(new NullDatabaseInitializer<TestContext>());
        }
    }
}
