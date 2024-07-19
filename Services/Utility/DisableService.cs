using Model.Models;
using Repository.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utility
{
    public class DisableService : IDisableService
    {
        private readonly IStatusRepository _status;
        public DisableService(IStatusRepository statusRepository)
        {
            _status = statusRepository;
        }
        public string DetermineCoverStatus(Cover cover)
        {
            return _status.DetermineCoverStatus(cover);
        }
        public string DetermineProductStatus(Product product)
        {
            return _status.DetermineProductStatus(product);
        }
        public string DetermineCoverSizeStatus(int coverId, int sizeId)
        {
            return _status.DetermineCoverSizeStatus(coverId, sizeId);
        }

        public string DetermineCoverMetalTypeStatus(int coverId, int metalTypeId)
        {
            return _status.DetermineCoverMetalTypeStatus(coverId, metalTypeId);
        }

        public string DetermineSizeStatus(int sizeId)
        {
            return _status.DetermineSizeStatus(sizeId);
        }
        public string DetermineMetalTypeStatus(int metalTypeId)
        {
            return _status.DetermineMetalTypeStatus(metalTypeId);
        }
        public (bool CanChange, string Reason) CanChangeCoverMetalTypeStatus(int coverId, int metalTypeId)
        {
            return _status.CanChangeCoverMetalTypeStatus(coverId, metalTypeId);
        }
        public (bool CanChange, string Reason) CanChangeCoverSizeStatus(int coverId, int sizeId)
        {
            return _status.CanChangeCoverSizeStatus(coverId, sizeId);
        }
        public (bool CanChange, string Reason) CanChangeCoverStatus(Cover cover)
        {
            return _status.CanChangeCoverStatus(cover);
        }
        public (bool CanChange, string Reason) CanChangeMetalTypeStatus(int metalTypeId)
        {
            return _status.CanChangeMetalTypeStatus(metalTypeId);
        }
        public (bool CanChange, string Reason) CanChangeProductStatus(Product product)
        {
            return _status.CanChangeProductStatus(product);
        }
        public (bool CanChange, string Reason) CanChangeSizeStatus(int sizeId)
        {
            return _status.CanChangeSizeStatus(sizeId);
        }
        public void UpdateCoverMetalTypeStatus(int coverId, int metalTypeId, string newStatus)
        {
            _status.UpdateCoverMetalTypeStatus(coverId, metalTypeId, newStatus);
        }
        public void UpdateCoverSizeStatus(int coverId, int sizeId, string newStatus)
        {
            _status.UpdateCoverSizeStatus(coverId, sizeId, newStatus);
        }
        public void UpdateCoverStatus(Cover cover)
        {
            _status.UpdateCoverStatus(cover);
        }
        public void UpdateMetalTypeStatus(int metalTypeId, string newStatus)
        {
            _status.UpdateMetalTypeStatus(metalTypeId, newStatus);
        }
        public void UpdateProductStatus(Product product)
        {
            _status.UpdateProductStatus(product);
        }
        public void UpdateSizeStatus(int sizeId, string newStatus)
        {
            _status.UpdateSizeStatus(sizeId, newStatus);
        }
        public (bool CanChange, string Reason) CanChangeDiamondStatus(Diamond diamond)
        {
            return _status.CanChangeDiamondStatus(diamond);
        }
        public void UpdateDiamondStatus(int diamondId, string newStatus)
        {
            _status.UpdateDiamondStatus(diamondId, newStatus);
        }
    }
}
