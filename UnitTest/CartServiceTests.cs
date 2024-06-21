using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Model.Models;
using Moq;
using NUnit.Framework;
using Repository.Users;
using Services.Users;
namespace UnitTest
{

    [TestFixture]
    public class CartServiceTests
    {
        private ICartService _cartService;
        private Mock<ICartRepository> _repositoryMock;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<ICartRepository>();

            // Set up mock responses for repository methods
            var mockCart = new Cart
            {
                CartId = 1,
                CartQuantity = 10,
                CartNavigation = new Customer { /* Initialize customer properties */ },
                CartProducts = new List<CartProduct>
            {
                new CartProduct { ProductId = 101, Quantity = 1 },
                new CartProduct { ProductId = 102, Quantity = 2 },
                new CartProduct { ProductId = 103, Quantity = 3 }
            }
            };

            var mockCartProduct = new CartProduct { ProductId = 101, Quantity = 1 };

            _repositoryMock.Setup(r => r.getCartFromCustomer(It.IsAny<int>())).Returns(mockCart);
            _repositoryMock.Setup(r => r.AddToCartAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(mockCartProduct);
            _repositoryMock.Setup(r => r.UpdateCartAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(mockCartProduct);

            _cartService = new CartService(_repositoryMock.Object);
        }

        [TestCaseSource(nameof(GetTestCases))]
        public void TestCartServiceMethods(TestCase testCase)
        {
            switch (testCase.MethodName)
            {
                case "GetCartFromCus":
                    var cart = _cartService.GetCartFromCus(testCase.CustomerId);
                    Assert.AreEqual(ExpectedCartObject.CartId, cart.CartId);
                    Assert.AreEqual(ExpectedCartObject.CartQuantity, cart.CartQuantity);
                    Assert.AreEqual(ExpectedCartObject.CartProducts.Count, cart.CartProducts.Count);

                    // Check individual products
                    for (int i = 0; i < ExpectedCartObject.CartProducts.Count; i++)
                    {
                        Assert.AreEqual(ExpectedCartObject.CartProducts.ElementAt(i).ProductId, cart.CartProducts.ElementAt(i).ProductId);
                        Assert.AreEqual(ExpectedCartObject.CartProducts.ElementAt(i).Quantity, cart.CartProducts.ElementAt(i).Quantity);
                    }
                    break;

                case "AddToCart":
                    if (testCase.ProductId.HasValue)
                    {
                        var cartProduct = _cartService.AddToCart(testCase.CustomerId, testCase.ProductId.Value);
                        Assert.AreEqual(ExpectedCartProductObject.ProductId, cartProduct.ProductId);
                        Assert.AreEqual(ExpectedCartProductObject.Quantity, cartProduct.Quantity);
                    }
                    else
                    {
                        Assert.Throws<ArgumentNullException>(() => _cartService.AddToCart(testCase.CustomerId, testCase.ProductId.Value));
                    }
                    break;

                case "RemoveFromCart":
                    if (testCase.ProductId.HasValue)
                    {
                        Assert.DoesNotThrow(() => _cartService.RemoveFromCart(testCase.CustomerId, testCase.ProductId.Value));
                    }
                    else
                    {
                        Assert.Throws<ArgumentNullException>(() => _cartService.RemoveFromCart(testCase.CustomerId, testCase.ProductId.Value));
                    }
                    break;

                case "EmptyCart":
                    Assert.DoesNotThrow(() => _cartService.emptyCart(testCase.CustomerId));
                    break;

                case "UpdateCart":
                    if (testCase.ProductId.HasValue)
                    {
                        if (testCase.ExpectedResult == "Exception")
                        {
                            Assert.Throws<Exception>(() => _cartService.updateCart(testCase.CustomerId, testCase.ProductId.Value, testCase.Quantity));
                        }
                        else
                        {
                            var updatedCartProduct = _cartService.updateCart(testCase.CustomerId, testCase.ProductId.Value, testCase.Quantity);
                            Assert.AreEqual(ExpectedUpdatedCartProductObject.ProductId, updatedCartProduct.ProductId);
                            Assert.AreEqual(ExpectedUpdatedCartProductObject.Quantity, updatedCartProduct.Quantity);
                        }
                    }
                    else
                    {
                        Assert.Throws<ArgumentNullException>(() => _cartService.updateCart(testCase.CustomerId, testCase.ProductId.Value, testCase.Quantity));
                    }
                    break;

                default:
                    Assert.Fail("Unknown method name");
                    break;
            }
        }


        public static IEnumerable<TestCase> GetTestCases()
        {
            
            using (var reader = new StreamReader(@"A:\FIfth-semester\SWP391\SWP391-DSS-BE\UnitTest\csv\CartTestData.csv"))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                return csv.GetRecords<TestCase>().ToList();
            }
        }
        public static Cart ExpectedCartObject = new Cart
        {
            CartId = 1,
            CartQuantity = 10,
            CartNavigation = new Customer { /* Initialize customer properties */ },
            CartProducts = new List<CartProduct>
    {
        new CartProduct { ProductId = 101, Quantity = 1 },
        new CartProduct { ProductId = 102, Quantity = 2 },
        new CartProduct { ProductId = 103, Quantity = 3 }
    }
        };

        public static CartProduct ExpectedCartProductObject = new CartProduct
        {
            ProductId = 101,
            Quantity = 1
        };

        public static CartProduct ExpectedUpdatedCartProductObject = new CartProduct
        {
            ProductId = 101,
            Quantity = 5
        };
    }
    public class TestCase
    {
        public int TestCaseId { get; set; }
        public string MethodName { get; set; }
        public int CustomerId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public string ExpectedResult { get; set; }
    }


}