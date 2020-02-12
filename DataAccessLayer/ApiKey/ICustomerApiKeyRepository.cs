using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DataModelLayer.ApiKey;

namespace DataAccessLayer.ApiKey
{
    public interface ICustomerApiKeyRepository 
    {
        #region ApiKey

        IList<Model.CustomerApiKey> GetCustomerApiKeys();

        Model.CustomerApiKey GetCustomerApiKeyById(int id);

        int AddCustomerApiKey(Model.CustomerApiKey customerApiKeyeModel);

        int UpdateCustomerApiKey(Model.CustomerApiKey customerApiKeyModel);

        void DeleteCustomerApiKey(int id);

        int ToggleLock(int id, bool isLocked, string lockedUntil);

        #endregion ApiKey 

        #region ApiKey Rule 

        IList<Model.CustomerApiKeyRule> GetCustomerApiKeyRulesByApiKey(int id);

        Model.CustomerApiKeyRule GetCustomerApiKeyRuleById(int id);

        int AddCustomerApiKeyRule(Model.CustomerApiKeyRule customerApiKeyRuleModel);

        int UpdateCustomerApiKeyRule(Model.CustomerApiKeyRule customerApiKeyRuleModel);

        void DeleteCustomerApiKeyRule(int id);

        #endregion ApiKey Rule
    }
}
