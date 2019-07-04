using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Context : IContext
    {
        private readonly CustomerDBEntities _customerDBEntities;

        public Context(CustomerDBEntities customerDBEntities)
        {
            _customerDBEntities = customerDBEntities;
        }

        public Context()
        {
            _customerDBEntities = new CustomerDBEntities();
        }

        public DbEntityEntry Entry(object entry)
        {
            return _customerDBEntities.Entry(entry);
        }

        public void Save()
        {
            try
            {
                _customerDBEntities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        #region CustomerApiKey module

        public IDbSet<CustomerApiKey> CustomerApiKey
        {
            get { return _customerDBEntities.CustomerApiKeys; }
            set { }
        }

        public IDbSet<CustomerApiKeyRule> CustomerApiKeyRule
        {
            get { return _customerDBEntities.CustomerApiKeyRules; }
            set { }
        }

        #endregion
    }
}
