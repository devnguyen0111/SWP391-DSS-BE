//using Model.Models;
//using DAO;

//namespace Repository.Users
//{
//    public class WishlistRepository : IWishlistRepository
//    {
//        private readonly DIAMOND_DBContext _context;

//        public WishlistRepository(DIAMOND_DBContext context)
//        {
//            _context = context;
//        }

//        public List<Wishlist> GetAllWishlist(int userId)
//        {
//            return _context.Wishlists.Where(w => w.UserId == userId).ToList();
//        }

//        public Wishlist AddWishlist(Wishlist wishlist)
//        {
//            _context.Wishlists.Add(wishlist);
//            _context.SaveChanges();
//            return wishlist;
//        }

//        public Wishlist RemoveWishlist(int wishlistId)
//        {
//            var wishlist = _context.Wishlists.Where(w => w.WishlistId == wishlistId).FirstOrDefault();
//            _context.Wishlists.Remove(wishlist);
//            _context.SaveChanges();
//            return wishlist;
//        }


//    }
//}
