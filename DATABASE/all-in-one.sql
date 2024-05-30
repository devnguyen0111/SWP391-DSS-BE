CREATE DATABASE DIAMOND_DB
GO
USE DIAMOND_DB

GO
-- Table: Size
CREATE TABLE Size (
    sizeId INT IDENTITY PRIMARY KEY,
    sizeName VARCHAR(255) NOT NULL,
    sizeValue VARCHAR(255) NOT NULL,
	sizePrice Money
);
GO
-- Table: MetalType
CREATE TABLE Metaltype (
    metaltypeId INT IDENTITY PRIMARY KEY,
    metaltypeName VARCHAR(255) NOT NULL,
    metaltypePrice Money
);
GO
-- Table: Category
CREATE TABLE Category (
    categoryId INT IDENTITY PRIMARY KEY,
    categoryName VARCHAR(255) NOT NULL
);
GO
-- Table: SubCategory
CREATE TABLE SubCategory (
    subCategoryId INT IDENTITY PRIMARY KEY,
    subCategoryName VARCHAR(255) NOT NULL,
    categoryId INT,
    FOREIGN KEY (categoryId) REFERENCES Category(categoryId)
);
GO
-- Table: Cover
CREATE TABLE Cover (
    coverId INT IDENTITY PRIMARY KEY,
    coverName VARCHAR(255) NOT NULL,
    status VARCHAR(50),
    unitPrice Money,
    subCategoryId INT NOT NULL,
    categoryId INT NOT NULL,
    FOREIGN KEY (subCategoryId) REFERENCES SubCategory(subCategoryId),
    FOREIGN KEY (categoryId) REFERENCES Category(categoryId)
);
GO
-- Table: CoverSize
CREATE TABLE CoverSize (
    sizeId INT,
    coverId INT,
    status VARCHAR(50),
    PRIMARY KEY (sizeId, coverId),
    FOREIGN KEY (sizeId) REFERENCES Size(sizeId),
    FOREIGN KEY (coverId) REFERENCES Cover(coverId)
);
GO
-- Table: CoverMetalType
CREATE TABLE CoverMetaltype (
    metaltypeId INT,
    coverId INT,
    status VARCHAR(50),
    PRIMARY KEY (metaltypeId, coverId),
    FOREIGN KEY (metaltypeId) REFERENCES Metaltype(metaltypeId),
    FOREIGN KEY (coverId) REFERENCES Cover(coverId)
);
GO
-- Table: Diamond
CREATE TABLE Diamond (
    diamondId INT IDENTITY PRIMARY KEY,
    diamondName VARCHAR(255) NOT NULL,
    caratWeight DECIMAL(5, 2) NOT NULL,
    color VARCHAR(50) NOT NULL,
    clarity VARCHAR(50) NOT NULL,
    cut VARCHAR(50) NOT NULL,
    shape VARCHAR(50) NOT NULL,
    price Money NOT NULL
);
GO
-- Table: Product
CREATE TABLE Product (
    productId INT IDENTITY PRIMARY KEY,
    productName NVARCHAR(255) NOT NULL,
    unitPrice Money,
    diamondId INT,
    coverId INT,
	metaltypeId INT,
	sizeId INT,
	PP VARCHAR(50),
    FOREIGN KEY (diamondId) REFERENCES Diamond(diamondId),
    FOREIGN KEY (coverId) REFERENCES Cover(coverId),
	FOREIGN KEY (metaltypeId) REFERENCES Metaltype(metaltypeId),
    FOREIGN KEY (sizeId) REFERENCES Size(sizeId)
);
GO
-- Table: User
CREATE TABLE [User] (
    userId INT IDENTITY PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    status VARCHAR(50) NOT NULL,
    role VARCHAR(50) NOT NULL
);
GO
-- Table: Customer

CREATE TABLE Customer (
    cusId INT PRIMARY KEY,
    cusFirstName VARCHAR(255) NOT NULL,
    cusLastName VARCHAR(255) NOT NULL,
    cusPhoneNum VARCHAR(20) NOT NULL,
    FOREIGN KEY (cusId) REFERENCES [User](userId)
);
GO
-- Table: Manager
CREATE TABLE Manager (
    manId INT  PRIMARY KEY,
    manName VARCHAR(255) NOT NULL,
    manPhone VARCHAR(20),
    FOREIGN KEY (manId) REFERENCES [User](userId)
);
GO
-- Table: SaleStaff
CREATE TABLE SaleStaff (
    sStaffId INT  PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    email VARCHAR(255),
    managerId INT,
    FOREIGN KEY (sStaffId) REFERENCES [User](userId),
    FOREIGN KEY (managerId) REFERENCES Manager(manId)
);
GO
-- Table: DeliveryStaff
CREATE TABLE DeliveryStaff (
    dStaffId INT  PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    managerId INT,
    FOREIGN KEY (dStaffId) REFERENCES [User](userId),
    FOREIGN KEY (managerId) REFERENCES Manager(manId)
);
GO
-- Table: Cart
CREATE TABLE Cart (
    cartId INT PRIMARY KEY,
    cartQuantity INT NOT NULL,
    FOREIGN KEY (cartId) REFERENCES Customer(cusId)
);
GO
-- Table: CartProduct
CREATE TABLE CartProduct (
    cartId INT,
    productId INT,
    quantity INT NOT NULL,
    PRIMARY KEY (cartId, productId),
    FOREIGN KEY (cartId) REFERENCES Cart(cartId),
    FOREIGN KEY (productId) REFERENCES Product(productId)
);
GO
CREATE TABLE favorite (
    favoriteId INT PRIMARY KEY,
    quantity INT NOT NULL,
    FOREIGN KEY (favoriteId) REFERENCES Customer(cusId)
);

CREATE TABLE favoriteProduct (
    favoriteId INT,
    productId INT,
    quantity INT NOT NULL,
    PRIMARY KEY (favoriteId, productId),
    FOREIGN KEY (favoriteId) REFERENCES favorite(favoriteId),
    FOREIGN KEY (productId) REFERENCES Product(productId)
);
GO
-- Table: Address
CREATE TABLE Address (
    addressId INT IDENTITY PRIMARY KEY,
    street VARCHAR(255),
    state VARCHAR(50),
    city VARCHAR(50),
    zipCode VARCHAR(20),
    country VARCHAR(50),
    cusId INT,
    FOREIGN KEY (cusId) REFERENCES Customer(cusId)
);
GO
-- Table: ShippingMethod
CREATE TABLE ShippingMethod (
    shippingMethodId INT IDENTITY PRIMARY KEY,
    methodName VARCHAR(255) NOT NULL,
    cost DECIMAL(10, 2) NOT NULL,
    description VARCHAR(255),
    date DATE
);
GO
-- Table: Order
CREATE TABLE [Order] (
    orderId INT IDENTITY PRIMARY KEY,
    orderDate DATETIME NOT NULL,
    totalAmount DECIMAL(10, 2) NOT NULL,
    status VARCHAR(50),
    cusId INT,
    shippingMethodId INT,
    FOREIGN KEY (cusId) REFERENCES Customer(cusId),
    FOREIGN KEY (shippingMethodId) REFERENCES ShippingMethod(shippingMethodId)
);
GO
-- Table: ProductOrder
CREATE TABLE ProductOrder (
    productId INT,
    orderId INT,
    quantity INT NOT NULL,
    PRIMARY KEY (productId, orderId),
    FOREIGN KEY (productId) REFERENCES Product(productId),
    FOREIGN KEY (orderId) REFERENCES [Order](orderId)
);
GO
-- Table: Shipping
CREATE TABLE Shipping (
    shippingId INT IDENTITY PRIMARY KEY,
    status VARCHAR(50),
    orderId INT,
    saleStaffId INT,
    deliveryStaffId INT,
    FOREIGN KEY (orderId) REFERENCES [Order](orderId),
    FOREIGN KEY (saleStaffId) REFERENCES SaleStaff(sStaffId),
    FOREIGN KEY (deliveryStaffId) REFERENCES DeliveryStaff(dStaffId)
);
GO
-- Table: Review
CREATE TABLE Review (
    reviewId INT IDENTITY PRIMARY KEY,
    review TEXT,
    rating INT,
    reviewDate DATE,
    cusId INT,
    FOREIGN KEY (cusId) REFERENCES Customer(cusId)
);
GO
-- Table: Voucher
CREATE TABLE Voucher (
    voucherId INT IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    expDate DATE NOT NULL,
	quantity INT,
	rate INT,
    cusId INT,
    FOREIGN KEY (cusId) REFERENCES Customer(cusId)
);
GO
-- Table: CustomerVoucher
CREATE TABLE CustomerVoucher (
    cusId INT,
    voucherId INT,
    PRIMARY KEY (cusId, voucherId),
    FOREIGN KEY (cusId) REFERENCES Customer(cusId),
    FOREIGN KEY (voucherId) REFERENCES Voucher(voucherId)
);	

-- Disable foreign key constraints temporarily
EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all";

-- Insert data into tables
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
('alice.johnson@example.com', 'password123', 'Active', 'Customer'),
('jane.smith@example.com', 'password123', 'Active', 'Manager'),
('admin@example.com', 'adminpassword', 'Active', 'Admin'),
('bob.brown@example.com', 'password123', 'Active', 'SaleStaff'),
('charlie.davis@example.com', 'password123', 'Active', 'DeliveryStaff');

-- Customer
INSERT INTO Customer (cusId, cusFirstName, cusLastName, cusPhoneNum) VALUES
(1, 'John', 'Doe', '123456789'),
(2, 'Alice', 'Johnson', '987654321');

-- Manager
INSERT INTO Manager (manId, manName, manPhone) VALUES
(3, 'Jane Smith', '555123456');

-- SaleStaff
INSERT INTO SaleStaff (sStaffId, name, phone, email, managerId) VALUES
(5, 'Bob Brown', '555987654', 'bob.brown@example.com', 3);

-- DeliveryStaff
INSERT INTO DeliveryStaff (dStaffId, name, phone, managerId) VALUES
(6, 'Charlie Davis', '555654321', 3);

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

-- Temporarily enable IDENTITY_INSERT for Address table
SET IDENTITY_INSERT Address ON;

-- Insert data into Address table
INSERT INTO Address (addressId, street, state, city, zipCode, country, cusId) VALUES
(1, '123 Main St', 'CA', 'Los Angeles', '90001', 'USA', 1),
(2, '456 Oak St', 'NY', 'New York', '10001', 'USA', 2);

-- Disable IDENTITY_INSERT for Address table
SET IDENTITY_INSERT Address OFF;

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
('Dispatched', 1, 5, 6),
('In Transit', 2, 5, 6);

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

-- Enable foreign key constraints
EXEC sp_MSforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all";
