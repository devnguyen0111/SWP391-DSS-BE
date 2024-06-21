using CsvHelper;
using CsvHelper.Configuration;
using Model.Models;
using Moq;
using NUnit.Framework;
using Repository.Diamonds;
using Repository.Products;
using Services.Products;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public class ProductTestCase
{
    public int TestCaseId { get; set; }
    public int ProductId { get; set; }
    public int? SizeId { get; set; }
    public int? MetaltypeId { get; set; }
    public int? CoverId { get; set; }
    public int? DiamondId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? ExpectedTotal { get; set; }
    public bool? ExpectedException { get; set; }
}

[TestFixture]
public class ProductServiceTests
{
    private Mock<IProductRepository> _productRepositoryMock;
    private Mock<ISizeRepository> _sizeRepositoryMock;
    private Mock<IMetaltypeRepository> _metaltypeRepositoryMock;
    private Mock<ICoverRepository> _coverRepositoryMock;
    private Mock<IDiamondRepository> _diamondRepositoryMock;
    private IProductService _productService;

    [SetUp]
    public void SetUp()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _sizeRepositoryMock = new Mock<ISizeRepository>();
        _metaltypeRepositoryMock = new Mock<IMetaltypeRepository>();
        _coverRepositoryMock = new Mock<ICoverRepository>();
        _diamondRepositoryMock = new Mock<IDiamondRepository>();

        _productService = new ProductService(_productRepositoryMock.Object, _coverRepositoryMock.Object,
                                             _metaltypeRepositoryMock.Object, _sizeRepositoryMock.Object,
                                             _diamondRepositoryMock.Object);
    }
    //_coverRepositoryMock.Object

    [TestCaseSource(nameof(GetTestCases))]
    public void TestGetProductTotal(ProductTestCase testCase)
    {
        // Arrange
        var product = new Product
        {
            ProductId = testCase.ProductId,
            SizeId = (int)testCase.SizeId,
            MetaltypeId = (int)testCase.MetaltypeId,
            CoverId = (int)testCase.CoverId,
            DiamondId = (int)testCase.DiamondId,
            UnitPrice = testCase.UnitPrice
        };

        _productRepositoryMock.Setup(r => r.GetProductById(testCase.ProductId)).Returns(product);
        if (testCase.SizeId.HasValue)
            _sizeRepositoryMock.Setup(r => r.GetSizeById(testCase.SizeId.Value)).Returns(new Size { SizeId = testCase.SizeId.Value, SizePrice = 50 });
        if (testCase.MetaltypeId.HasValue)
            _metaltypeRepositoryMock.Setup(r => r.GetMetaltypeById(testCase.MetaltypeId.Value)).Returns(new Metaltype { MetaltypeId = testCase.MetaltypeId.Value, MetaltypePrice = 200 });
        if (testCase.CoverId.HasValue)
            _coverRepositoryMock.Setup(r => r.GetCoverById(testCase.CoverId.Value)).Returns(new Cover { CoverId = testCase.CoverId.Value, UnitPrice = testCase.CoverId.Value == 2 ? -75 : 75 });
        if (testCase.DiamondId.HasValue)
            _diamondRepositoryMock.Setup(r => r.getDiamondById(testCase.DiamondId.Value)).Returns(new Diamond { DiamondId = testCase.DiamondId.Value, Price = testCase.DiamondId.Value == 2 ? -300 : 300 });

        // Act and Assert
        if (testCase.ExpectedException.HasValue && testCase.ExpectedException.Value)
        {
            Assert.Throws<Exception>(() => _productService.GetProductTotal(testCase.ProductId));
        }
        else
        {
            var result = _productService.GetProductTotal(testCase.ProductId);
            Assert.AreEqual(testCase.ExpectedTotal, result);
        }
    }

    public static IEnumerable<ProductTestCase> GetTestCases()
    {
        using (var reader = new StreamReader(@"A:\FIfth-semester\SWP391\SWP391-DSS-BE\UnitTest\csv\ProductTestCases.csv"))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            return csv.GetRecords<ProductTestCase>().ToList();
        }
    }

}

