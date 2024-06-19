//using dao;
//using microsoft.entityframeworkcore;
//using model.models;
//using nunit.framework;
//using repository.users;
//using repository;
//using system.collections.generic;
//using system.linq;

//public class cartservicetests
//{
//    private diamond_dbcontext _dbcontext;
//    private icartrepository _cartservice;

//    [setup]
//    public void setup()
//    {
//        var options = new dbcontextoptionsbuilder<diamond_dbcontext>()
//                        .useinmemorydatabase(databasename: "testdatabase")
//                        .options;
//        _dbcontext = new diamond_dbcontext(options);
//        _cartservice = new cartrepository(_dbcontext);

//        add sample data to the in-memory database
//        _dbcontext.carts.add(new cart { cartid = 1, cartproducts = new list<cartproduct>() });
//        _dbcontext.products.add(new product { productid = 1 });
//        _dbcontext.savechanges();
//    }

//    public static ienumerable<testcasedata> gettestcasesfromcsv()
//    {
//        var testcases = new list<testcasedata>();
//        var filepath = @"a:\fifth-semester\swp391\swp391-dss-be\unittest\csv\carttestdata.csv";
//        var csvlines = file.readalllines(filepath).skip(1);

//        foreach (var line in csvlines)
//        {
//            var values = line.split(',');
//            var testcasename = values[0];
//            var cartid = int.parse(values[1]);
//            var productid = int.parse(values[2]);
//            var quantity = int.parse(values[3]);
//            var expectedcartquantity = int.parse(values[4]);
//            var expectedproductquantity = int.parse(values[5]);
//            var shouldthrow = bool.parse(values[6]);
//            var testcase = new testcasedata(cartid, productid, quantity, expectedcartquantity, expectedproductquantity, shouldthrow).setname(testcasename);

//            testcases.add(testcase);
//        }

//        return testcases;
//    }

//    [test, testcasesource(nameof(gettestcasesfromcsv))]
//    public void addtocartasync_shouldhandlecases(
//        int cartid, int productid, int quantity, int expectedcartquantity, int expectedproductquantity, bool shouldthrow)
//    {
//        arrange
//        if (shouldthrow)
//        {
//            if (cartid == 999)
//            {
//                _dbcontext.carts.remove(_dbcontext.carts.find(cartid));
//            }
//            if (productid == 999)
//            {
//                _dbcontext.products.remove(_dbcontext.products.find(productid));
//            }
//            _dbcontext.savechanges();
//        }

//        act & assert
//        if (shouldthrow)
//        {
//            var ex = assert.throws<exception>(() => _cartservice.addtocartasync(cartid, productid, quantity));
//            assert.istrue(ex.message == "cart not found" || ex.message == "product not found");
//        }
//        else
//        {
//            var result = _cartservice.addtocartasync(cartid, productid, quantity);
//            assert.areequal(expectedproductquantity, result.quantity);
//            var cart = _dbcontext.carts.include(c => c.cartproducts).firstordefault(c => c.cartid == cartid);
//            assert.areequal(expectedcartquantity, cart.cartquantity);
//        }
//    }
//}
