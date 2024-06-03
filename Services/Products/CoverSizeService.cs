using Model.Models;
using Repository.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products
{
    public class CoverSizeService : ICoverSizeService
    {
        private readonly ICoverSizeRepository _coverSizeRepository;

        public CoverSizeService(ICoverSizeRepository coverSizeRepository)
        {
            _coverSizeRepository = coverSizeRepository;
        }

        public CoverSize GetCoverSize(int coverId, int sizeId)
        {
            return _coverSizeRepository.GetCoverSize(coverId, sizeId);
        }
    }
}
