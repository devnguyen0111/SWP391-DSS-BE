using Repository.Products;
using Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products
{
    public class CoverInventoryService
    {
        private readonly ICoverInventoryRepository coverInventoryRepository;
        private readonly IDisableService disableService;

        public CoverInventoryService(ICoverInventoryRepository coverInventoryRepository, IDisableService disableService)
        {
            this.coverInventoryRepository = coverInventoryRepository;
            this.disableService = disableService;
        }

    }
}
