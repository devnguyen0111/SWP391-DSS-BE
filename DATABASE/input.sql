-- Disable foreign key constraints temporarily
EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all";

-- Delete data from leaf tables first
DELETE FROM CoverSize;
DELETE FROM CoverMetalType;
DELETE FROM CartProduct;
DELETE FROM ProductOrder;
DELETE FROM CustomerVoucher;
DELETE FROM Review;
DELETE FROM Voucher;
DELETE FROM Address;

-- Delete data from tables with foreign keys
DELETE FROM Cart;
DELETE FROM Shipping;
DELETE FROM [Order];
DELETE FROM SaleStaff;
DELETE FROM DeliveryStaff;
DELETE FROM Manager;
DELETE FROM Customer;

-- Delete data from tables without foreign keys
DELETE FROM Product;
DELETE FROM Size;
DELETE FROM MetalType;
DELETE FROM Cover;
DELETE FROM SubCategory;
DELETE FROM Category;
DELETE FROM Diamond;
DELETE FROM ShippingMethod;

-- Finally, delete data from the User table
DELETE FROM [User];

-- Re-enable foreign key constraints
EXEC sp_MSforeachtable "ALTER TABLE ? CHECK CONSTRAINT all";
