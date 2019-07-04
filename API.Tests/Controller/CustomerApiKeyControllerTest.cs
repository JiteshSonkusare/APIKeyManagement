using API.Controllers;
using DataLogicLayer.ApiKey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net.Http;
using System.Web.Http;
using Model = DataModelLayer.ApiKey;

namespace API.Tests.Controller
{
    [TestClass]
    public class CustomerApiKeyControllerTest
    {
        private static Mock<ICustomerApiKeyFacade> CreateMock()
        {
            var mock = new Mock<ICustomerApiKeyFacade>();
            return mock;
        }

        [TestMethod]
        public void Get_ExistingValidApikey_ResultsInOK()
        {
            var apikey = new Model.CustomerApiKey
            {
                Id = 1,
                ApiKey = "XXX",
                Comment = "comment1",
                
                ChangedBy = "Name1",
                Changed = DateTime.Now,
                CreatedBy = "Name1",
                Created = DateTime.Now,
                Expires = DateTime.Now.AddYears(1),
                IsPublic = true,
                Locked = false,
                LockedOn = null,
                LockedUntil = null,
            };
            var mock = CreateMock();
            mock.Setup(x => x.GetCustomerApiKeyById(1)).Returns(apikey);
            var controller = new CustomerApiKeyController(() => mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var ret = controller.Get(1);
            Assert.AreEqual(Newtonsoft.Json.JsonConvert.SerializeObject(apikey), ret.Content.ReadAsStringAsync().Result);
        }

        [TestMethod]
        public void Get_NonExistingApikey_ResultsIn404()
        {
            var mock = CreateMock();
            mock.Setup(x => x.GetCustomerApiKeyById(1)).Returns((Model.CustomerApiKey)null);
            var controller = new CustomerApiKeyController(() => mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var ret = controller.Get(1);
            Assert.AreEqual(404, (int)ret.StatusCode);
        }

        [TestMethod]
        public void Delete_NonExsistingApikey_ResultIn400()
        {
            var mock = CreateMock();
            mock.Setup(x => x.DeleteCustomerApiKey(1)).Throws<ArgumentException>();
            var controller = new CustomerApiKeyController(() => mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var ret = controller.Delete(1);
            Assert.AreEqual(400, (int)ret.StatusCode);
        }

        [TestMethod]
        public void Delete_Exception_ResultIn500()
        {
            var mock = CreateMock();
            mock.Setup(x => x.DeleteCustomerApiKey(1)).Throws<Exception>();
            var controller = new CustomerApiKeyController(() => mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var ret = controller.Delete(1);
            Assert.AreEqual(500, (int)ret.StatusCode);
        }

        [TestMethod]
        public void Delete_ValidApikey_ResultIn200()
        {
            var mock = CreateMock();
            var controller = new CustomerApiKeyController(() => mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var ret = controller.Delete(1);
            Assert.AreEqual(200, (int)ret.StatusCode);
        }

        [TestMethod]
        public void Post_ValidObjectPassed_ReturnsIsOk()
        {
            var mock = CreateMock();
            var controller = new CustomerApiKeyController(() => mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var customerApikeyTestModel = new Model.CustomerApiKey
            {
                ApiKey = "XXX",
                Comment = "comment1",
                
                ChangedBy = "Name1",
                Changed = DateTime.Now,
                CreatedBy = "Name1",
                Created = DateTime.Now,
                Expires = DateTime.Now.AddYears(1),
                IsPublic = true,
                Locked = false,
                LockedOn = null,
                LockedUntil = null,
            };

            var result = controller.Post(customerApikeyTestModel);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, (int)result.StatusCode);
        }

        [TestMethod]
        public void Post_InvalidObjectPassed_ReturnsBadRequest()
        {
            var apikeyMissingModel = new Model.CustomerApiKey
            {
                Comment = "comment1",
                
                ChangedBy = "Name1",
                Changed = DateTime.Now,
                CreatedBy = "Name1",
                Created = DateTime.Now,
                Expires = DateTime.Now.AddYears(1),
                IsPublic = true,
                Locked = false,
                LockedOn = null,
                LockedUntil = null,
            };

            var mock = CreateMock();
            var controller = new CustomerApiKeyController(() => mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            controller.ModelState.AddModelError("Apikey", "Required");
            var badResponse = controller.Post(apikeyMissingModel);

            Assert.AreEqual(500, (int)badResponse.StatusCode);
        }

        [TestMethod]
        public void Put_ValidObjectPassed_ReturnsIsOk()
        {
            var existingObj = new Model.CustomerApiKey
            {
                ApiKey = "XXX",
                Comment = "comment1",
                
                ChangedBy = "Name1",
                Changed = DateTime.Now,
                CreatedBy = "Name1",
                Created = DateTime.Now,
                Expires = DateTime.Now.AddYears(1),
                IsPublic = true,
                Locked = false,
                LockedOn = null,
                LockedUntil = null,
            };

            var mock = CreateMock();
            mock.Setup(x => x.UpdateCustomerApiKey(existingObj)).Returns(1);
            var controller = new CustomerApiKeyController(() => mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var updatedObj = new Model.CustomerApiKey { ApiKey = "XXX", Comment = "comment2",  ChangedBy = "Name2", IsPublic = false };
            var ret = controller.Put(updatedObj);

            Assert.AreEqual(Newtonsoft.Json.JsonConvert.SerializeObject(existingObj.ApiKey), ret.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(200, (int)ret.StatusCode);
        }
    }
}
