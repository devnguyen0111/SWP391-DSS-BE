using Model.Models;

namespace Services.Utility
{
    public interface IDisableService
    {
        (bool CanChange, string Reason) CanChangeCoverMetalTypeStatus(int coverId, int metalTypeId);
        (bool CanChange, string Reason) CanChangeCoverSizeStatus(int coverId, int sizeId);
        (bool CanChange, string Reason) CanChangeCoverStatus(Cover cover);
        (bool CanChange, string Reason) CanChangeDiamondStatus(Diamond diamond);
        (bool CanChange, string Reason) CanChangeMetalTypeStatus(int metalTypeId);
        (bool CanChange, string Reason) CanChangeProductStatus(Product product);
        (bool CanChange, string Reason) CanChangeSizeStatus(int sizeId);
        string DetermineCoverMetalTypeStatus(int coverId, int metalTypeId);
        string DetermineCoverSizeStatus(int coverId, int sizeId);
        string DetermineCoverStatus(Cover cover);
        string DetermineMetalTypeStatus(int metalTypeId);
        string DetermineProductStatus(Product product);
        string DetermineSizeStatus(int sizeId);
        void UpdateCoverMetalTypeStatus(int coverId, int metalTypeId, string newStatus);
        void UpdateCoverSizeStatus(int coverId, int sizeId, string newStatus);
        void UpdateCoverStatus(Cover cover);
        void UpdateDiamondStatus(int diamondId, string newStatus);
        void UpdateMetalTypeStatus(int metalTypeId, string newStatus);
        void UpdateProductStatus(Product product);
        void UpdateSizeStatus(int sizeId,string newStatus);
    }
}