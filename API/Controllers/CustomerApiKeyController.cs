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
    /// CustomerApiKey
    /// </summary>
    public class CustomerApiKeyController : ApiController
    {
        readonly Func<ICustomerApiKeyFacade> _customerApiKeyFacade;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customerApiKeyFacade"></param>
        public CustomerApiKeyController(Func<ICustomerApiKeyFacade> customerApiKeyFacade)
        {
            _customerApiKeyFacade = customerApiKeyFacade;
        }

        #region API key

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Apikey</param> 
        /// <returns></returns>
        [Documentation(ReturnType = typeof(Model.CustomerApiKey))]
        public HttpResponseMessage Get(int id)
        {
            var customerApiKey = _customerApiKeyFacade().GetCustomerApiKeyById(id);

            return (customerApiKey == null)
               ? Request.CreateResponse(HttpStatusCode.NotFound, $"Customer api key with id: {id} not found")
               : Request.CreateResponse(HttpStatusCode.OK, customerApiKey);
        }

        /// <summary>
        /// Add new api key 
        /// </summary>
        /// <param name="customerApiKeyModel"></param> 
        /// <returns></returns>
        [Documentation(ReturnType = typeof(int))]
        public HttpResponseMessage Post(Model.CustomerApiKey customerApiKeyModel)
        {
            try
            {
                if (customerApiKeyModel == null)
                    throw new HttpRequestException("customer apikey model is null");
                if (!ModelState.IsValid)
                    throw new HttpRequestException("model is invalid");

                return Request.CreateResponse(HttpStatusCode.OK, _customerApiKeyFacade().AddCustomerApiKey(customerApiKeyModel));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// update api key details by api key
        /// </summary>
        /// <param name="customerApiKeyModel"></param> 
        /// <returns></returns>
        [Documentation(ReturnType = typeof(int))]
        public HttpResponseMessage Put(Model.CustomerApiKey customerApiKeyModel)
        {
            try
            {
                if (customerApiKeyModel == null)
                    throw new HttpRequestException("customer apikey model is null");
                if (!ModelState.IsValid)
                    throw new HttpRequestException("customer apikey model is invalid");

                _customerApiKeyFacade().UpdateCustomerApiKey(customerApiKeyModel);
                return Request.CreateResponse(HttpStatusCode.OK, customerApiKeyModel.ApiKey);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Delete apikey details and apikey rules by apikey
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Documentation(ReturnType = typeof(int))]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id == 0)
                    throw new HttpRequestException("model id is null or empty");

                _customerApiKeyFacade().DeleteCustomerApiKey(id);
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

        #endregion API key
    }
}
