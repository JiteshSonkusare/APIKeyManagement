using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DataModelLayer.ApiKey;

namespace DataLogicLayer.ApiKey
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerApiKeyFacade
    {
        #region ApiKey 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IList<Model.CustomerApiKey> GetCustomerApiKeys();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Model.CustomerApiKey GetCustomerApiKeyById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerApiKeyModel"></param>
        /// <returns></returns>
        int AddCustomerApiKey(Model.CustomerApiKey customerApiKeyModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerApiKeyModel"></param>
        /// <returns></returns>
        int UpdateCustomerApiKey(Model.CustomerApiKey customerApiKeyModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void DeleteCustomerApiKey(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="isLocked"></param>
        /// <param name="lockedUntil"></param>
        int ToggleLock(int id, bool isLocked, string lockedUntil);

        #endregion ApiKey

        #region ApiKeyRule

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IList<Model.CustomerApiKeyRule> GetCustomerApiKeyRulesByApiKey(int id); 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id of the CustomerApiKeyRule</param>
        /// <returns></returns>
        Model.CustomerApiKeyRule GetCustomerApiKeyRuleById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerApiKeyRuleModel"></param>
        /// <returns></returns>
        int AddCustomerApiKeyRule(Model.CustomerApiKeyRule customerApiKeyRuleModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerApiKeyRuleModel"></param>
        /// <returns></returns>
        int UpdateCustomerApiKeyRule(Model.CustomerApiKeyRule customerApiKeyRuleModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void DeleteCustomerApiKeyRule(int id);

        #endregion
    }
}
