using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Repository.Diamonds
{
    public class DiamondRepository : IDiamondRepository
    {
        private readonly DIAMOND_DBContext _context;

        public DiamondRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public Diamond createDiamond(Diamond diamond)
        {
            _context.Diamonds.Add(diamond);
            _context.SaveChanges();
            return diamond;
        }

        public void deleteDiamond(int Id)
        {
            Diamond diamond = getDiamondById(Id);
            if (diamond != null)
            {
                _context.Diamonds.Remove(diamond);
                _context.SaveChanges();
            }
        }

        public List<Diamond> getAllDiamonds()
        {
            return _context.Diamonds.ToList();
        }

        public Diamond getDiamondById(int Id)
        {
            return _context.Diamonds.FirstOrDefault(c => c.DiamondId == Id);
        }

        public Diamond updateDiamond(Diamond diamond)
        {
            _context.Entry(diamond).State = EntityState.Modified;
            _context.SaveChanges();
            return diamond;
        }
    }
}
