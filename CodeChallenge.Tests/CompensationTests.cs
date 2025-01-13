using System;
using System.Net;
using System.Net.Http;
using System.Text;
using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            // Check if compensation already exists before running the test
            var checkResponse = _httpClient.GetAsync($"api/compensation/{employeeId}").Result;
            if (checkResponse.StatusCode == HttpStatusCode.OK)
            {
                Assert.Inconclusive("Compensation already exists, skipping test to prevent duplication.");
                return;
            }

            var compensation = new Compensation
            {
                EmployeeId = employeeId,
                Salary = 90000,
                EffectiveDate = DateTime.UtcNow
            };

            var requestContent = new JsonSerialization().ToJson(compensation);
            var response = _httpClient.PostAsync("api/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json")).Result;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }






        [TestMethod]
        public void GetCompensation_Returns_Ok()
        {
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            // Ensure the compensation entry exists before running the test
            var checkResponse = _httpClient.GetAsync($"api/compensation/{employeeId}").Result;
            if (checkResponse.StatusCode == HttpStatusCode.NotFound)
            {
                // Create the Compensation to ensure it exists
                var newCompensation = new Compensation
                {
                    EmployeeId = employeeId,
                    Salary = 75000,
                    EffectiveDate = DateTime.UtcNow
                };

                var requestContent = new JsonSerialization().ToJson(newCompensation);
                var postResponse = _httpClient.PostAsync("api/compensation",
                    new StringContent(requestContent, Encoding.UTF8, "application/json")).Result;

                Assert.AreEqual(HttpStatusCode.Created, postResponse.StatusCode);
            }

            // Now retrieve Compensation
            var response = _httpClient.GetAsync($"api/compensation/{employeeId}").Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();

            Assert.IsNotNull(compensation);
            Assert.AreEqual(employeeId, compensation.EmployeeId);
        }


        [TestMethod]
        public void GetCompensation_Returns_NotFound_For_Invalid_Employee()
        {
            // Arrange
            var invalidEmployeeId = "invalid-id";

            // Act
            var response = _httpClient.GetAsync($"api/compensation/{invalidEmployeeId}").Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
