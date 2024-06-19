using Model.Models;
using Moq;
using Repository.Products;
using Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestFixture]
    public class ProductBrowseTest
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private IProductService _productService;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        private List<Product> GetSampleProducts()
        {
            return new List<Product>
        {
            new Product { ProductName="product1", Cover = new Cover { CategoryId = 1, SubCategoryId = 1 }, Metaltype = new Metaltype { MetaltypeId = 1 }, Size = new Size { SizeId = 1 }, UnitPrice = 100 },
            new Product {ProductName="product2", Cover = new Cover { CategoryId = 1, SubCategoryId = 2 }, Metaltype = new Metaltype { MetaltypeId = 2 }, Size = new Size { SizeId = 2 }, UnitPrice = 200 },
            new Product {ProductName="product3", Cover = new Cover { CategoryId = 2, SubCategoryId = 1 }, Metaltype = new Metaltype { MetaltypeId = 1 }, Size = new Size { SizeId = 3 }, UnitPrice = 300 },
            new Product {ProductName="product4", Cover = new Cover { CategoryId = 2, SubCategoryId = 2 }, Metaltype = new Metaltype { MetaltypeId = 2 }, Size = new Size { SizeId = 4 }, UnitPrice = 400 },
        };
        }

        public static IEnumerable<TestCaseData> GetTestCasesFromCsv()
        {
            var testCases = new List<TestCaseData>();
            var filePath = @"A:\FIfth-semester\SWP391\SWP391-DSS-BE\UnitTest\csv\ProductTestData.csv";
            var csvLines = File.ReadAllLines(filePath).Skip(1);

            foreach (var line in csvLines)
            {
                var values = line.Split(',');
                var testCaseName = values[0];
                var categoryId = string.IsNullOrWhiteSpace(values[1]) ? (int?)null : int.Parse(values[1]);
                var subCategoryId = string.IsNullOrWhiteSpace(values[2]) ? (int?)null : int.Parse(values[2]);
                var metaltypeId = string.IsNullOrWhiteSpace(values[3]) ? (int?)null : int.Parse(values[3]);
                var sizeId = string.IsNullOrWhiteSpace(values[4]) ? (int?)null : int.Parse(values[4]);
                var minPrice = string.IsNullOrWhiteSpace(values[5]) ? (decimal?)null : decimal.Parse(values[5]);
                var maxPrice = string.IsNullOrWhiteSpace(values[6]) ? (decimal?)null : decimal.Parse(values[6]);
                var expectedProductCount = string.IsNullOrWhiteSpace(values[7]) ? (int?)null : int.Parse(values[7]);
                var testCase = new TestCaseData(categoryId, subCategoryId, metaltypeId, sizeId, minPrice, maxPrice, expectedProductCount)
                    .SetName(testCaseName);

                testCases.Add(testCase);
            }

            return testCases;
        }

        [Test, TestCaseSource(nameof(GetTestCasesFromCsv))]
        public void FilterProducts_ShouldReturnCorrectProducts(
            int? categoryId, int? subCategoryId, int? metaltypeId, int? sizeId,
            decimal? minPrice, decimal? maxPrice, int expectedProductCount)
        {
            var sampleProducts = GetSampleProducts();
            _productRepositoryMock.Setup(repo => repo.GetAllProducts()).Returns(sampleProducts);

            var result = _productService.FilterProducts(categoryId, subCategoryId, metaltypeId, sizeId, minPrice, maxPrice);

            Assert.AreEqual(expectedProductCount, result.Count);
            Console.WriteLine("Filtered Products: " + string.Join(", ", result.Select(p => p.ProductName.ToString())));
        }
    }
}
