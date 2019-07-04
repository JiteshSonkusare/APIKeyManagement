using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace API
{
    /// <summary>
    /// Attribute to set properties used for documentation.
    /// </summary>
    public class DocumentationAttribute : FilterAttribute
    {
        /// <summary>
        /// The Type that your method returns.
        /// </summary>
        public Type ReturnType { get; set; }
    }
}