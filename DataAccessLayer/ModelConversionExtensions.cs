using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiKeyModel = DataModelLayer.ApiKey;

namespace DataAccessLayer
{
    public static class ModelConversionExtensions
    {
        #region CustomerApiKey

        public static ApiKeyModel.CustomerApiKey MapFull(this CustomerApiKey customerApiKey)
        {
            ApiKeyModel.CustomerApiKey customerApiKeyModel = new ApiKeyModel.CustomerApiKey
            {
                Id = customerApiKey.Id,
                ApiKey = customerApiKey.ApiKey,
                Comment = customerApiKey.Comment,
                Expires = customerApiKey.Expires,
                IsPublic = customerApiKey.IsPublic,
                Locked = customerApiKey.Locked,
                LockedOn = customerApiKey.LockedOn,
                LockedUntil = customerApiKey.LockedUntil,
                Created = customerApiKey.Created,
                CreatedBy = customerApiKey.CreatedBy,
                Changed = customerApiKey.Changed,
                ChangedBy = customerApiKey.ChangedBy,
                CustomerApiKeyRules = customerApiKey.CustomerApiKeyRules.MapFull(customerApiKey.Id),
            };

            return customerApiKeyModel;
        }

        private static IList<ApiKeyModel.CustomerApiKeyRule> MapFull(this ICollection<CustomerApiKeyRule> customerApiKeyRules, int ApiKey)
        {
            IEnumerable<ApiKeyModel.CustomerApiKeyRule> returnList = customerApiKeyRules.Select(
                customerApiKeyRule => new ApiKeyModel.CustomerApiKeyRule
                {
                    Id = customerApiKeyRule.Id,
                    ApiKeyId = ApiKey,
                    Ip = customerApiKeyRule.Ip,
                    Domain = customerApiKeyRule.Domain,
                    Method = customerApiKeyRule.Method,
                    Mode = customerApiKeyRule.Mode,
                    Operation = customerApiKeyRule.Operation,
                    ApplicationName = customerApiKeyRule.ApplicationName,
                    Created = customerApiKeyRule.Created,
                    CreatedBy = customerApiKeyRule.CreatedBy,
                    Changed = customerApiKeyRule.Changed,
                    ChangedBy = customerApiKeyRule.ChangedBy,
                });

            return returnList.OrderBy(x => x.Id).ToList();
        }

        public static ApiKeyModel.CustomerApiKeyRule MapFull(this CustomerApiKeyRule customerApiKeyRule)
        {
            var customerApiKeyRuleModel = new ApiKeyModel.CustomerApiKeyRule
            {
                Id = customerApiKeyRule.Id,
                ApiKeyId = customerApiKeyRule.ApiKeyId,
                Changed = customerApiKeyRule.Changed,
                ChangedBy = customerApiKeyRule.ChangedBy,
                Created = customerApiKeyRule.Created,
                CreatedBy = customerApiKeyRule.CreatedBy,
                Domain = customerApiKeyRule.Domain,
                Ip = customerApiKeyRule.Ip,
                Method = customerApiKeyRule.Method,
                Mode = customerApiKeyRule.Mode,
                Operation = customerApiKeyRule.Operation,
                ApplicationName = customerApiKeyRule.ApplicationName
            };
            return customerApiKeyRuleModel;
        }

        public static CustomerApiKeyRule MapFull(this ApiKeyModel.CustomerApiKeyRule customerApiKeyRuleModel,
            CustomerApiKeyRule customerApiKeyRule = null)
        {
            var tmp = customerApiKeyRule ?? new CustomerApiKeyRule();

            tmp.ApiKeyId = customerApiKeyRuleModel.ApiKeyId;
            tmp.ApplicationName = customerApiKeyRuleModel.ApplicationName;
            tmp.Domain = customerApiKeyRuleModel.Domain;
            tmp.Ip = customerApiKeyRuleModel.Ip;
            tmp.Method = customerApiKeyRuleModel.Method;
            tmp.Mode = customerApiKeyRuleModel.Mode;
            tmp.Operation = customerApiKeyRuleModel.Operation;
            tmp.Created = customerApiKeyRuleModel.Created == DateTime.MinValue ? tmp.Created : customerApiKeyRuleModel.Created;
            tmp.CreatedBy = customerApiKeyRuleModel.CreatedBy ?? tmp.CreatedBy;
            tmp.Changed = customerApiKeyRuleModel.Changed;
            tmp.ChangedBy = customerApiKeyRuleModel.ChangedBy;

            return tmp;
        }

        public static CustomerApiKey MapFull(this ApiKeyModel.CustomerApiKey customerApiKeyModel, CustomerApiKey customerApiKey = null)
        {
            var tmp = customerApiKey ?? new CustomerApiKey();

            tmp.ApiKey = customerApiKeyModel.ApiKey;
            tmp.Comment = customerApiKeyModel.Comment;
            tmp.Expires = customerApiKeyModel.Expires;
            tmp.IsPublic = customerApiKeyModel.IsPublic;
            tmp.Created = customerApiKeyModel.Created == DateTime.MinValue ? tmp.Created : customerApiKeyModel.Created;
            tmp.CreatedBy = customerApiKeyModel.CreatedBy ?? tmp.CreatedBy;
            tmp.Changed = customerApiKeyModel.Changed;
            tmp.ChangedBy = customerApiKeyModel.ChangedBy;

            return tmp;
        }

        public static CustomerApiKey MapFull(this CustomerApiKey customerApiKey, bool locked, string lockedUntil)
        {
            customerApiKey.Locked = locked;

            if (locked == true)
            {
                customerApiKey.LockedOn = DateTime.Now;
                customerApiKey.LockedUntil = DateTime.Parse(lockedUntil);
            }
            else
            {
                customerApiKey.LockedOn = null;
                customerApiKey.LockedUntil = null;
            }

            return customerApiKey;
        }

        #endregion
    }
}
