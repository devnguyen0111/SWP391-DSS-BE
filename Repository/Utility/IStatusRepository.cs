using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Model.Models;

namespace Repository.Utility
{
    public interface IStatusRepository
    {
        (bool CanChange, string Reason) CanChangeCoverMetalTypeStatus(int coverId, int metalTypeId);
        (bool CanChange, string Reason) CanChangeCoverSizeStatus(int coverId, int sizeId);
        (bool CanChange, string Reason) CanChangeCoverStatus(Cover cover);
        (bool CanChange, string Reason) CanChangeMetalTypeStatus(int metalTypeId);
        (bool CanChange, string Reason) CanChangeProductStatus(Product product);
        (bool CanChange, string Reason) CanChangeSizeStatus(int sizeId);
        string DetermineCoverMetalTypeStatus(int coverId, int metalTypeId);
        string DetermineCoverSizeStatus(int coverId, int sizeId);
        string DetermineMetalTypeStatus(int metalTypeId);
        string DetermineSizeStatus(int sizeId);
        string DetermineCoverStatus(Cover cover);
        string DetermineProductStatus(Product product);
        void UpdateCoverMetalTypeStatus(int coverId, int metalTypeId);
        void UpdateCoverSizeStatus(int coverId, int sizeId);
        void UpdateCoverStatus(Cover cover);
        void UpdateMetalTypeStatus(int metalTypeId);
        void UpdateProductStatus(Product product);
        void UpdateSizeStatus(int sizeId);
        (bool CanChange, string Reason) CanChangeDiamondStatus(Diamond diamond);
        void UpdateDiamondStatus(int diamondId);
    }
}