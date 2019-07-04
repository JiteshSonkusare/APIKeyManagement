using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Model = DataModelLayer.ApiKey;
using DataLogicLayer.ApiKey;

namespace API.Tests.Controller
{
    [TestClass]
    public class CustomerApiKeyRuleControllerTest
    {
        private static Mock<ICustomerApiKeyFacade> CreateMock()
        {
            var mock = new Mock<ICustomerApiKeyFacade>();
            return mock;
        }

        [TestMethod]
        public void NonExistantIdResultsIn404()
        {
            var mock = CreateMock();
            mock.Setup(x => x.GetCustomerApiKeyRuleById(1)).Returns((Model.CustomerApiKeyRule)null);
            var controller = new CustomerApiKeyRuleController(() => mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var ret = controller.Get(1);
            Assert.AreEqual(404, (int)ret.StatusCode);
        }

        [TestMethod]
        public void GetRule()
        {
            var obj = new Model.CustomerApiKeyRule
            {
                Id = 1,
                ApiKeyId = 11,
                Changed = DateTime.Now,
                ChangedBy = "Jitesh"
            };
            var mock = CreateMock();
            mock.Setup(x => x.GetCustomerApiKeyRuleById(1)).Returns(obj);
            var controller = new CustomerApiKeyRuleController(() => mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var ret = controller.Get(1);
            Assert.AreEqual(Newtonsoft.Json.JsonConvert.SerializeObject(obj), ret.Content.ReadAsStringAsync().Result);
        }

        [TestMethod]
        public void DeleteNonExsistantGives400()
        {
            var mock = CreateMock();
            mock.Setup(x => x.DeleteCustomerApiKeyRule(1)).Throws<ArgumentException>();
            var controller = new CustomerApiKeyRuleController(() => mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var ret = controller.Delete(1);
            Assert.AreEqual(400, (int)ret.StatusCode);
        }

        [TestMethod]
        public void DeleteExceptionGives500()
        {
            var mock = CreateMock();
            mock.Setup(x => x.DeleteCustomerApiKeyRule(1)).Throws<Exception>();
            var controller = new CustomerApiKeyRuleController(() => mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var ret = controller.Delete(1);
            Assert.AreEqual(500, (int)ret.StatusCode);
        }
    }
}
