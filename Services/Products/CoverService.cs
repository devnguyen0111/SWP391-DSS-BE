using Model.Models;
using Repository.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products
{
    public class CoverService : ICoverService
    {
        private readonly ICoverRepository _coverRepository;

        public CoverService(ICoverRepository coverRepository)
        {
            _coverRepository = coverRepository;
        }

        public IEnumerable<Cover> GetAllCovers()
        {
            return _coverRepository.GetAllCovers();
        }

        public Cover GetCoverById(int coverId)
        {
            return _coverRepository.GetCoverById(coverId);
        }

        public void AddCover(Cover cover)
        {
            _coverRepository.AddCover(cover);
            _coverRepository.Save();
        }

        public void UpdateCover(Cover cover)
        {
            _coverRepository.UpdateCover(cover);
            _coverRepository.Save();
        }

        public void DeleteCover(int coverId)
        {
            _coverRepository.DeleteCover(coverId);
            _coverRepository.Save();
        }
    }

}
