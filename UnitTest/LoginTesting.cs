using Moq;
using NUnit.Framework;
using Services.Users;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Repository.Users;
using Model.Models;

namespace UnitTest
{
    [TestFixture]
    public class AuthenticateServiceTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IConfiguration> _mockConfiguration;
        private AuthenticateService _authenticateService;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockConfiguration = new Mock<IConfiguration>();

            // Mock configuration for JWT
            var jwtSection = new Mock<IConfigurationSection>();
            jwtSection.Setup(x => x["Key"]).Returns("my_secret_key_12345");
            jwtSection.Setup(x => x["Issuer"]).Returns("myIssuer");
            jwtSection.Setup(x => x["Audience"]).Returns("myAudience");
            _mockConfiguration.Setup(x => x.GetSection("Jwt")).Returns(jwtSection.Object);

            _authenticateService = new AuthenticateService(_mockUserRepository.Object, _mockConfiguration.Object);
        }

        private static IEnumerable<TestCaseData> GetTestCasesFromCsv()
        {
            var testCases = new List<TestCaseData>();
            var filePath = @"C:\Users\caotr\OneDrive\Documents\SWP391\SWP391-DSS-BE\UnitTest\csv\ProductTestData.csv";  // Adjust the path to your CSV file

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The CSV file was not found.", filePath);
            }

            var csvLines = File.ReadAllLines(filePath).Skip(1);

            foreach (var line in csvLines)
            {
                var values = line.Split(',');
                var testCaseName = values[0];
                var email = values[1];
                var password = values[2];
                var expectedResult = values[3];

                var testCase = new TestCaseData(email, password, expectedResult).SetName(testCaseName);
                testCases.Add(testCase);
            }

            return testCases;
        }

        [Test, TestCaseSource(nameof(GetTestCasesFromCsv))]
        public void AuthenticateTest(string email, string password, string expectedResult)
        {
            // Arrange
            var hashedPassword = GetHashString(password); // Reuse the hashing method
            User user = null;

            if (expectedResult == "Success")
            {
                user = new User
                {
                    Email = email,
                    Password = hashedPassword
                };
            }

            _mockUserRepository.Setup(repo => repo.GetAll())
                .Returns((Delegate)new List<User> { user }.Where(u => u != null));

            // Act
            var result = _authenticateService.Authenticate(email, hashedPassword);

            // Assert
            if (expectedResult == "Success")
            {
                Assert.IsNotNull(result);
            }
            else
            {
                Assert.IsNull(result);
            }
        }

        private static byte[] GetHash(string inputString)
        {
            using (var algorithm = System.Security.Cryptography.SHA256.Create())
                return algorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputString));
        }

        private static string GetHashString(string inputString)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
