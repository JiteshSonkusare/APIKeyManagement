using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLayer.ApiKey
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerApiKey
    {
        public CustomerApiKey()
        {
            CustomerApiKeyRules = new List<CustomerApiKeyRule>();
        }

        public int Id { get; set; }

        public string ApiKey { get; set; }

        public DateTime? Expires { get; set; }

        public bool Locked { get; set; }

        public DateTime? LockedUntil { get; set; }

        public DateTime? LockedOn { get; set; }

        public string Comment { get; set; }

        public bool? IsPublic { get; set; }

        public DateTime Changed { get; set; }

        public string ChangedBy { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public IList<CustomerApiKeyRule> CustomerApiKeyRules { get; set; }
    }
}
