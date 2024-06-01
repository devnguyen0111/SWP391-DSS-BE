﻿using Model.Models;
namespace Services
{
    public interface IVoucherService
    {
        void createVoucher( string name, string description, DateOnly expdate, int quantity, int rate, int cusId);
        void deleteVoucher(int Id);
        List<Voucher> GetAllVouchers();
        Voucher getVoucherById(int Id);
        Voucher updateVoucher(Voucher voucher);
    }
}
