using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Services.Users;
using Model.Models;

namespace UnitTest
{
    [TestFixture]
    public class RegisterTests
    {
        private Mock<ICustomerService> _mockCustomerService;
        private Mock<ICartService> _mockCartService;

        [SetUp]
        public void Setup()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _mockCartService = new Mock<ICartService>();
        }

        public static IEnumerable<TestCaseData> GetTestCasesFromCsv()
        {
            var csvLines = File.ReadAllLines("A:\\FIfth-semester\\SWP391\\SWP391-DSS-BE\\UnitTest\\csv\\RegisterTestData.csv").Skip(1);
            foreach (var line in csvLines)
            {
                var values = line.Split(',');
                yield return new TestCaseData(
                    values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7])
                    .SetName($"{values[0]}_{values[1]}_{values[3]}");
            }
        }

        [Test, TestCaseSource(nameof(GetTestCasesFromCsv))]
        public void Register_ValidRequest_ShouldCreateCustomerAndCart(
            string firstName, string lastName, string phoneNumber, string email, string password, string expectedEmail, string expectedStatus, string expectedRole)
        {
            // Arrange
            var request = new registerRequest
            {
                firstName = firstName,
                lastName = lastName,
                phoneNumber = phoneNumber,
                email = email,
                password = password
            };

            var customer = new Customer
            {
                CusFirstName = request.firstName,
                CusLastName = request.lastName,
                CusPhoneNum = request.phoneNumber,
                Cus = new User
                {
                    Email = request.email,
                    Password = GetHashString(request.password),
                    Status = expectedStatus,
                    Role = expectedRole
                },
                Address = new Address
                {
                    Country = "VietNam"
                }
            };

            _mockCustomerService.Setup(service => service.addCustomer(It.IsAny<Customer>())).Returns(customer);

            // Act
            _mockCustomerService.Object.addCustomer(customer);
            _mockCartService.Object.createCart(customer.CusId);

            // Assert
            _mockCustomerService.Verify(service => service.addCustomer(It.IsAny<Customer>()), Times.Once);
            _mockCartService.Verify(service => service.createCart(It.IsAny<int>()), Times.Once);

            Assert.IsNotNull(customer);
            Assert.AreEqual(expectedEmail, customer.Cus.Email);
            Assert.AreEqual(expectedStatus, customer.Cus.Status);
            Assert.AreEqual(expectedRole, customer.Cus.Role);
        }

        private string GetHashString(string inputString)
        {
            using (var hashAlgorithm = System.Security.Cryptography.SHA256.Create())
            {
                byte[] data = hashAlgorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputString));
                var sBuilder = new System.Text.StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
        public class registerRequest
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string phoneNumber { get; set; }
            public string email { get; set; }
            public string password { get; set; }
        }
    }
}