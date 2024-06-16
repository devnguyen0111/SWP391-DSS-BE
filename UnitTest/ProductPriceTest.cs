//using Model.Models;
//using Moq;
//using Repository.Diamonds;
//using Repository.Products;
//using Services.Products;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace UnitTest
//{
//    public class ProductPriceTest
//    {

//        private Mock<IProductRepository> _productRepositoryMock;
//        private Mock<ICoverRepository> _coverRepositoryMock;
//        private Mock<IMetaltypeRepository> _metaltypeRepositoryMock;
//        private Mock<ISizeRepository> _sizeRepositoryMock;
//        private Mock<IDiamondRepository> _diamondRepositoryMock;
//        private IProductService _productService;

//        [SetUp]
//        public void Setup()
//        {
//            _productRepositoryMock = new Mock<IProductRepository>();
//            _coverRepositoryMock = new Mock<ICoverRepository>();
//            _metaltypeRepositoryMock = new Mock<IMetaltypeRepository>();
//            _sizeRepositoryMock = new Mock<ISizeRepository>();
//            _diamondRepositoryMock = new Mock<IDiamondRepository>();

//            _productService = new ProductService(
//                _productRepositoryMock.Object,
//                _coverRepositoryMock.Object,
//                _metaltypeRepositoryMock.Object,
//                _sizeRepositoryMock.Object,
//                _diamondRepositoryMock.Object);
//        }
//        public static IEnumerable<TestCaseData> GetTestCasesFromCsv()
//        {
//            var testCases = new List<TestCaseData>();
//            var filePath = @"C:\Users\caotr\OneDrive\Documents\SWP391\SWP391-DSS-BE\UnitTest\csv\ProductPriceTest.csv";
//            var csvLines = File.ReadAllLines(filePath).Skip(1);

//            foreach (var line in csvLines)
//            {
//                var values = line.Split(',');
//                var testCaseName = values[0];
//                var productId = string.IsNullOrWhiteSpace(values[1]) ? (int?)null : int.Parse(values[1]);
//                var expectedPrice = string.IsNullOrWhiteSpace(values[1]) ? (decimal?)null : decimal.Parse(values[1]); ;

//                var testCase = new TestCaseData(productId, expectedPrice)
//                    .SetName(testCaseName);

//                testCases.Add(testCase);
//            }

//            return testCases;
//        }
//        [Test, TestCaseSource(nameof(GetTestCasesFromCsv))]
//        public void GetProductTotal_ShouldReturnCorrectTotalPrice(int productId, decimal expectedTotalPrice)
//        {
//            var product = new Product
//            {
//                ProductId = productId,
//                SizeId = 1, 
//                MetaltypeId = 1,
//                CoverId = 1,
//                DiamondId = 1,
//                UnitPrice = 100 
//            };

//            Size size = new Size { SizeValue = "test", SizePrice = 500 }; 
//            var metaltype = new Metaltype { MetaltypeName = "Gold", MetaltypePrice = 1000 };
//            var cover = new Cover { CoverName = "Leather", UnitPrice = 300 };
//            Diamond diamond = new Diamond(2000); 

//            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).Returns(product);
//            _sizeRepositoryMock.Setup(repo => repo.GetSizeById(1)).Returns(size);
//            _metaltypeRepositoryMock.Setup(repo => repo.GetMetaltypeById(1)).Returns(metaltype);
//            _coverRepositoryMock.Setup(repo => repo.GetCoverById(1)).Returns(cover);
//            _diamondRepositoryMock.Setup(repo => repo.getDiamondById(1)).Returns(diamond);

//            // Act
//            var actualTotalPrice = _productService.GetProductTotal(productId);

//            // Assert
//            Assert.AreEqual(expectedTotalPrice, actualTotalPrice);
//        }

//    }
//}
