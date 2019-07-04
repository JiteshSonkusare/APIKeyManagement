using DataLogicLayer.ApiKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model = DataModelLayer.ApiKey;

namespace API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerApiKeyRuleController : ApiController
    {
        readonly Func<ICustomerApiKeyFacade> _customerApiKeyFacade;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customerApiKeyFacade"></param>
        public CustomerApiKeyRuleController(Func<ICustomerApiKeyFacade> customerApiKeyFacade)
        {
            _customerApiKeyFacade = customerApiKeyFacade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ApiKeyRuleId</param>
        /// <returns></returns>
        [Documentation(ReturnType = typeof(Model.CustomerApiKeyRule))]
        public HttpResponseMessage Get(int id)
        {
            var customerApiKeyRule = _customerApiKeyFacade().GetCustomerApiKeyRuleById(id);

            return (customerApiKeyRule == null)
                ? Request.CreateResponse(HttpStatusCode.NotFound, $"Customer apikey rule with id: {id} not found")
                : Request.CreateResponse(HttpStatusCode.OK, customerApiKeyRule);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerApiKeyRuleModel"></param>
        /// <returns></returns>
        [Documentation(ReturnType = typeof(int))]
        public HttpResponseMessage Post(Model.CustomerApiKeyRule customerApiKeyRuleModel)
        {
            try
            {
                var id = _customerApiKeyFacade().AddCustomerApiKeyRule(customerApiKeyRuleModel);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (ArgumentException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerApiKeyRuleModel"></param>
        /// <returns></returns>
        [Documentation(ReturnType = typeof(int))]
        public HttpResponseMessage Put(Model.CustomerApiKeyRule customerApiKeyRuleModel)
        {
            try
            {
                var id = _customerApiKeyFacade().UpdateCustomerApiKeyRule(customerApiKeyRuleModel);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Documentation(ReturnType = typeof(int))]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _customerApiKeyFacade().DeleteCustomerApiKeyRule(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ArgumentException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
