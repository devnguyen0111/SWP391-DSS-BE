﻿using Model.Models;

namespace Services.Products
{
    public interface ICoverService
    {
        IEnumerable<Cover> GetAllCovers();
        Cover GetCoverById(int coverId);
        void AddCover(Cover cover);
        void UpdateCover(Cover cover);
        void DeleteCover(int coverId);
        bool CombinationExists(int coverId, int sizeId, int metaltypeId);
        string DetermineCoverStatus(int coverId);
        string DetermineCoverStatus1(Cover cover);
        void EmptyCover(int id);
    }

}
