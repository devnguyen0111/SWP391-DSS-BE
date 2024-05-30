-- Size
INSERT INTO Size (sizeName, sizeValue, sizePrice) VALUES
('Small', 'S', 10.00),
('Medium', 'M', 20.00),
('Large', 'L', 30.00);

-- MetalType
INSERT INTO Metaltype (metaltypeName, metaltypePrice) VALUES
('Gold', 100.00),
('Silver', 50.00),
('Platinum', 150.00);

-- Category
INSERT INTO Category (categoryName) VALUES
('Rings'),
('Necklaces'),
('Bracelets');

-- SubCategory
INSERT INTO SubCategory (subCategoryName, categoryId) VALUES
('Wedding Rings', 1),
('Engagement Rings', 1),
('Chokers', 2),
('Pendants', 2),
('Cuffs', 3),
('Bangles', 3);

-- Cover
INSERT INTO Cover (coverName, status, unitPrice, subCategoryId, categoryId) VALUES
('Diamond Cover', 'Available', 200.00, 1, 1),
('Gold Cover', 'Available', 150.00, 2, 1),
('Silver Cover', 'Out of Stock', 100.00, 3, 2);

-- CoverSize
INSERT INTO CoverSize (sizeId, coverId, status) VALUES
(1, 1, 'Available'),
(2, 2, 'Available'),
(3, 3, 'Out of Stock');

-- CoverMetalType
INSERT INTO CoverMetaltype (metaltypeId, coverId, status) VALUES
(1, 1, 'Available'),
(2, 2, 'Available'),
(3, 3, 'Out of Stock');

-- Diamond
INSERT INTO Diamond (diamondName, caratWeight, color, clarity, cut, shape, price) VALUES
('Round Brilliant', 1.0, 'D', 'VVS1', 'Excellent', 'Round', 5000.00),
('Princess Cut', 0.8, 'E', 'VS1', 'Very Good', 'Square', 4000.00),
('Oval Cut', 1.2, 'F', 'SI1', 'Good', 'Oval', 4500.00);

-- Product
INSERT INTO Product (productName, unitPrice, diamondId, coverId, metaltypeId, sizeId, PP) VALUES
('Diamond Ring', 6000.00, 1, 1, 1, 1, 'Yes'),
('Gold Necklace', 3000.00, NULL, 2, 2, 2, 'No'),
('Silver Bracelet', 1500.00, NULL, 3, 3, 3, 'No');

-- User
INSERT INTO [User] (email, password, status, role) VALUES
('john.doe@example.com', 'password123', 'Active', 'Customer'),
('jane.smith@example.com', 'password123', 'Active', 'Manager'),
('admin@example.com', 'adminpassword', 'Active', 'Admin');

-- Customer
INSERT INTO Customer (cusId, cusFirstName, cusLastName, cusPhoneNum) VALUES
(1, 'John', 'Doe', '123456789'),
(2, 'Alice', 'Johnson', '987654321');

-- Manager
INSERT INTO Manager (manId, manName, manPhone) VALUES
(1, 'Jane Smith', '555123456');

-- SaleStaff
INSERT INTO SaleStaff (sStaffId, name, phone, email, managerId) VALUES
(1, 'Bob Brown', '555987654', 'bob.brown@example.com', 1);

-- DeliveryStaff
INSERT INTO DeliveryStaff (dStaffId, name, phone, managerId) VALUES
(1, 'Charlie Davis', '555654321', 1);

-- Cart
INSERT INTO Cart (cartId, cartQuantity) VALUES
(1, 2),
(2, 3);

-- CartProduct
INSERT INTO CartProduct (cartId, productId, quantity) VALUES
(1, 1, 1),
(1, 2, 2),
(2, 3, 1);

-- favorite
INSERT INTO favorite (favoriteId, quantity) VALUES
(1, 1),
(2, 2);

-- favoriteProduct
INSERT INTO favoriteProduct (favoriteId, productId, quantity) VALUES
(1, 1, 1),
(2, 2, 2);

-- Address
INSERT INTO Address (street, state, city, zipCode, country, addressId) VALUES
('123 Main St', 'CA', 'Los Angeles', '90001', 'USA', 1),
('456 Oak St', 'NY', 'New York', '10001', 'USA', 2);

-- ShippingMethod
INSERT INTO ShippingMethod (methodName, cost, description, date) VALUES
('Standard Shipping', 5.00, '5-7 business days', '2024-05-30'),
('Express Shipping', 15.00, '1-2 business days', '2024-05-30');

-- Order
INSERT INTO [Order] (orderDate, totalAmount, status, cusId, shippingMethodId) VALUES
('2024-05-30', 7000.00, 'Processing', 1, 1),
('2024-05-31', 4000.00, 'Shipped', 2, 2);

-- ProductOrder
INSERT INTO ProductOrder (productId, orderId, quantity) VALUES
(1, 1, 1),
(2, 1, 1),
(3, 2, 2);

-- Shipping
INSERT INTO Shipping (status, orderId, saleStaffId, deliveryStaffId) VALUES
('Dispatched', 1, 1, 1),
('In Transit', 2, 1, 1);

-- Review
INSERT INTO Review (review, rating, reviewDate, cusId) VALUES
('Great product!', 5, '2024-05-30', 1),
('Good quality.', 4, '2024-05-31', 2);

-- Voucher
INSERT INTO Voucher (name, description, expDate, quantity, rate, cusId) VALUES
('Summer Sale', '10% off', '2024-12-31', 100, 10, 1),
('Winter Sale', '20% off', '2025-01-31', 50, 20, 2);

-- CustomerVoucher
INSERT INTO CustomerVoucher (cusId, voucherId) VALUES
(1, 1),
(2, 2);
