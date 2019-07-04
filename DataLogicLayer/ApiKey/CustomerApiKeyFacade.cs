using DataAccessLayer;
using DataAccessLayer.ApiKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLayer.ApiKey
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerApiKeyFacade : CustomerApiKeyRepository, ICustomerApiKeyFacade
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CustomerApiKeyFacade(IContext context) : base(context)
        {

        }
    }
}
