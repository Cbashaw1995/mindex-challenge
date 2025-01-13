using System.Net;
using System.Net.Http;
using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureTests
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
        public void GetReportingStructure_Returns_Ok()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f"; // John Lennon

            // Act
            var response = _httpClient.GetAsync($"api/reportingstructure/{employeeId}").Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var structure = response.DeserializeContent<ReportingStructure>();

            Assert.IsNotNull(structure);
            Assert.IsNotNull(structure.Employee);
            Assert.AreEqual(employeeId, structure.Employee.EmployeeId);
            Assert.IsTrue(structure.NumberOfReports > 0);
        }

        [TestMethod]
        public void GetReportingStructure_Returns_NotFound_For_Invalid_Employee()
        {
            // Arrange
            var invalidEmployeeId = "invalid-id";

            // Act
            var response = _httpClient.GetAsync($"api/reportingstructure/{invalidEmployeeId}").Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
