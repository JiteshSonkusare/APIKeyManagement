using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IContext : IUnitOfWork
    {
        DbEntityEntry Entry(Object entry);

        #region CustomerApiKey module

        IDbSet<CustomerApiKey> CustomerApiKey { get; set; }

        IDbSet<CustomerApiKeyRule> CustomerApiKeyRule { get; set; }

        #endregion
    }
}
