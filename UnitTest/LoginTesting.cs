using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using NUnit.Framework;
using Repository.Users;
using Repository;
using System.Collections.Generic;
using System.Linq;

public class CartServiceTests
{
    private DIAMOND_DBContext _DBContext;
    private ICartRepository _cartService;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DIAMOND_DBContext>()
                        .UseInMemoryDatabase(databaseName: "TestDatabase")
                        .Options;
        _DBContext = new DIAMOND_DBContext(options);
        _cartService = new CartRepository(_DBContext);

        // Add sample data to the in-memory database
        _DBContext.Carts.Add(new Cart { CartId = 1, CartProducts = new List<CartProduct>() });
        _DBContext.Products.Add(new Product { ProductId = 1 });
        _DBContext.SaveChanges();
    }

    public static IEnumerable<TestCaseData> GetTestCasesFromCsv()
    {
        var testCases = new List<TestCaseData>();
        var filePath = @"A:\FIfth-semester\SWP391\SWP391-DSS-BE\UnitTest\csv\CartTestData.csv";
        var csvLines = File.ReadAllLines(filePath).Skip(1);

        foreach (var line in csvLines)
        {
            var values = line.Split(',');
            var testCaseName = values[0];
            var cartId = int.Parse(values[1]);
            var productId = int.Parse(values[2]);
            var quantity = int.Parse(values[3]);
            var expectedCartQuantity = int.Parse(values[4]);
            var expectedProductQuantity = int.Parse(values[5]);
            var shouldThrow = bool.Parse(values[6]);
            var testCase = new TestCaseData(cartId, productId, quantity, expectedCartQuantity, expectedProductQuantity, shouldThrow).SetName(testCaseName);

            testCases.Add(testCase);
        }

        return testCases;
    }

    [Test, TestCaseSource(nameof(GetTestCasesFromCsv))]
    public void AddToCartAsync_ShouldHandleCases(
        int cartId, int productId, int quantity, int expectedCartQuantity, int expectedProductQuantity, bool shouldThrow)
    {
        // Arrange
        if (shouldThrow)
        {
            if (cartId == 999)
            {
                _DBContext.Carts.Remove(_DBContext.Carts.Find(cartId));
            }
            if (productId == 999)
            {
                _DBContext.Products.Remove(_DBContext.Products.Find(productId));
            }
            _DBContext.SaveChanges();
        }

        // Act & Assert
        if (shouldThrow)
        {
            var ex = Assert.Throws<Exception>(() => _cartService.AddToCartAsync(cartId, productId, quantity));
            Assert.IsTrue(ex.Message == "Cart not found" || ex.Message == "Product not found");
        }
        else
        {
            var result = _cartService.AddToCartAsync(cartId, productId, quantity);
            Assert.AreEqual(expectedProductQuantity, result.Quantity);
            var cart = _DBContext.Carts.Include(c => c.CartProducts).FirstOrDefault(c => c.CartId == cartId);
            Assert.AreEqual(expectedCartQuantity, cart.CartQuantity);
        }
    }
}
