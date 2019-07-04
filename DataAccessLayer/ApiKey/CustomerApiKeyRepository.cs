using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DataModelLayer.ApiKey;

namespace DataAccessLayer.ApiKey
{
    public class CustomerApiKeyRepository : ICustomerApiKeyRepository
    {
        private readonly IContext _context;

        public CustomerApiKeyRepository(IContext context)
        {
            _context = context;
        }

        #region ApiKey

        public IList<Model.CustomerApiKey> GetCustomerApiKeys()
        {
            try
            {
                var contacts = _context.CustomerApiKey.AsQueryable();
                var result = contacts.ToList().Select(x => x.MapFull()).ToList();

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Model.CustomerApiKey GetCustomerApiKeyById(int id)
        {
            CustomerApiKey customerApiKey = _context.CustomerApiKey.SingleOrDefault(x => id.Equals(x.Id));
            Model.CustomerApiKey customerApiKeyModel = customerApiKey?.MapFull();

            return customerApiKeyModel;
        }

        public int AddCustomerApiKey(Model.CustomerApiKey customerApiKeyModel)
        {
            if (string.IsNullOrEmpty(customerApiKeyModel.ApiKey))
                throw new ArgumentException($"Apikey value is null or empty");

            CustomerApiKey customerApiKey = customerApiKeyModel.MapFull();

            _context.CustomerApiKey.Add(customerApiKey);
            _context.Save();

            return customerApiKey.Id;
        }

        public int UpdateCustomerApiKey(Model.CustomerApiKey customerApiKeyModel)
        {
            if (customerApiKeyModel.Id == 0)
                throw new ArgumentException($"Id is not valid");

            CustomerApiKey customerApiKey = _context.CustomerApiKey.SingleOrDefault(x => x.Id.Equals(customerApiKeyModel.Id));
            if (customerApiKey == null)
                throw new ArgumentException($"No data found with id: {customerApiKeyModel.Id}");

            customerApiKeyModel.MapFull(customerApiKey);

            _context.Save();
            return customerApiKey.Id;
        }

        public void DeleteCustomerApiKey(int id)
        {
            CustomerApiKey customerApiKey = _context.CustomerApiKey.SingleOrDefault(x => x.Id.Equals(id));
            if (customerApiKey == null)
                throw new ArgumentException($"Id: {id} not exists");
            _context.CustomerApiKey.Remove(customerApiKey);
            _context.Save();
        }

        public int ToggleLock(int id, bool isLocked, string lockedUntil)
        {
            CustomerApiKey customerApiKey = _context.CustomerApiKey.SingleOrDefault(x => x.Id.Equals(id));

            if (customerApiKey == null)
                throw new ArgumentException($"No data found with id: {id}");

            customerApiKey?.MapFull(isLocked, lockedUntil);

            _context.Save();
            return customerApiKey.Id;
        }

        #endregion ApiKey

        #region ApiKey Rule

        public IList<Model.CustomerApiKeyRule> GetCustomerApiKeyRulesByApiKey(int id)
        {
            var customerApiKeyRules = _context.CustomerApiKeyRule.Where(x => id.Equals(x.ApiKeyId)).ToList();
            var result  = customerApiKeyRules.ToList().Select(x => x.MapFull()).ToList();

            return result;
        }

        public Model.CustomerApiKeyRule GetCustomerApiKeyRuleById(int id)
        {
            CustomerApiKeyRule customerApiKeyRule = _context.CustomerApiKeyRule.SingleOrDefault(x => id.Equals(x.Id));
            Model.CustomerApiKeyRule customerApiKeyRuleModel = customerApiKeyRule?.MapFull();
            return customerApiKeyRuleModel;
        }

        public int AddCustomerApiKeyRule(Model.CustomerApiKeyRule customerApiKeyRuleModel)
        {
            CustomerApiKeyRule customerApiKeyRule = customerApiKeyRuleModel.MapFull();

            if (string.IsNullOrEmpty(customerApiKeyRule.ApplicationName) || customerApiKeyRule.ApplicationName.Contains("*"))
                throw new ArgumentException($"This apikey is public and does not allow System to be wildcard.");

            _context.CustomerApiKeyRule.Add(customerApiKeyRule);
            _context.Save();

            return customerApiKeyRule.Id;
        }

        public int UpdateCustomerApiKeyRule(Model.CustomerApiKeyRule customerApiKeyRuleModel)
        {
            CustomerApiKeyRule customerApiKeyRule = _context.CustomerApiKeyRule.SingleOrDefault(x => x.Id.Equals(customerApiKeyRuleModel.Id));
            if (customerApiKeyRule == null)
                throw new ArgumentException($"No data found with id: {customerApiKeyRule.Id}");

            customerApiKeyRuleModel.MapFull(customerApiKeyRule);
            if (string.IsNullOrEmpty(customerApiKeyRule.ApplicationName) || customerApiKeyRule.ApplicationName.Contains("*"))
                throw new ArgumentException($"This Apikey is public and does not allow System to be wildcard.");

            _context.Save();
            return customerApiKeyRule.Id;
        }

        public void DeleteCustomerApiKeyRule(int id)
        {
            CustomerApiKeyRule customerApiKeyRule = _context.CustomerApiKeyRule.SingleOrDefault(x => x.Id.Equals(id));
            if (customerApiKeyRule == null)
                throw new ArgumentException($"Apikey rule with id: {id} not exists");
            _context.CustomerApiKeyRule.Remove(customerApiKeyRule);
            _context.Save();
        }

        #endregion ApiKey Rule
    }
}
