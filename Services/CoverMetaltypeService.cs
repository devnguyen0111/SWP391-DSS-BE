using Model.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CoverMetaltypeService : ICoverMetaltypeService
    {
        private readonly ICoverMetaltypeRepository _coverSizeRepository;

        public CoverMetaltypeService(ICoverMetaltypeRepository coverSizeRepository)
        {
            _coverSizeRepository = coverSizeRepository;
        }

        public CoverMetaltype GetCoverMetaltype(int coverId, int metaltypeId)
        {
            return _coverSizeRepository.GetCoverMetaltype(coverId, metaltypeId);
        }
    }
}
