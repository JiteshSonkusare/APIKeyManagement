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
    public class CustomerApiKeyRule
    {
        public int Id { get; set; }

        public int ApiKeyId { get; set; }

        public string Mode { get; set; }

        public string Method { get; set; }

        public string Domain { get; set; }

        public string Operation { get; set; }

        public string Ip { get; set; }

        public string ApplicationName { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Changed { get; set; }

        public string ChangedBy { get; set; }
    }
}
