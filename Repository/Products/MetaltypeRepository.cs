using DAO;
using Model.Models;

namespace Repository.Products
{
    public class MetaltypeRepository : IMetaltypeRepository
    {
        private readonly DIAMOND_DBContext _context;

        public MetaltypeRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public List<Metaltype> GetAllMetaltypes()
        {
            return _context.Metaltypes.ToList();
        }

        public Metaltype GetMetaltypeById(int id)
        {
            return _context.Metaltypes.Find(id);
        }

        /*public void Add(Metaltype metaltype)
        {
            _context.Metaltypes.Add(metaltype);
            _context.SaveChanges();
        }

        public void Update(Metaltype metaltype)
        {
            _context.Metaltypes.Update(metaltype);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var metaltype = _context.Metaltypes.Find(id);
            if (metaltype != null)
            {
                _context.Metaltypes.Remove(metaltype);
                _context.SaveChanges();
            }
        }*/
    }
}
