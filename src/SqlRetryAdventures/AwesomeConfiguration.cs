using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRetryAdventures
{
    public class AwesomeConfiguration : DbConfiguration
    {
        public AwesomeConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () =>
            {
                return new ActualSqlExecutionStrategy(3, TimeSpan.FromSeconds(2));
            });
        }
    }

    /// <summary>
    /// This extends the SqlAzureExecutionStrategy to support more SqlExceptions.
    /// See https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlerror.number(v=vs.110).aspx .
    /// </summary>
    public class ActualSqlExecutionStrategy : SqlAzureExecutionStrategy
    {
        //
        int[] sqlExceptionNumbers = new int[] 
        {
            -2, //connection timeout
            0   //a bunch of others, for some reason
        };

        public ActualSqlExecutionStrategy() : base() { }
        public ActualSqlExecutionStrategy(int maxRetryCount, TimeSpan maxDelay)
            : base(maxRetryCount, maxDelay)
        {

        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            var should = base.ShouldRetryOn(exception);
            if (exception is SqlException)
                if (sqlExceptionNumbers.Contains(((SqlException)exception).Number))
                    return true;

            return base.ShouldRetryOn(exception);
        }
    }

}
