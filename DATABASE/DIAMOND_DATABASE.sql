CREATE DATABASE DIAMOND_DB

USE DIAMOND_DB

DROP DATABASE DIAMOND_DB

-- Table: Size
CREATE TABLE Size (
    sizeId INT IDENTITY PRIMARY KEY,
    sizeName VARCHAR(255) NOT NULL,
    sizeValue VARCHAR(255) NOT NULL
);

-- Table: Category
CREATE TABLE Category (
    categoryId INT IDENTITY PRIMARY KEY,
    categoryName VARCHAR(255) NOT NULL
);

-- Table: SubCategory
CREATE TABLE SubCategory (
    subCategoryId INT IDENTITY PRIMARY KEY,
    subCategoryName VARCHAR(255) NOT NULL,
    categoryId INT,
    FOREIGN KEY (categoryId) REFERENCES Category(categoryId)
);

-- Table: Cover
CREATE TABLE Cover (
    coverId INT IDENTITY PRIMARY KEY,
    coverName VARCHAR(255) NOT NULL,
    status VARCHAR(50),
    unitPrice DECIMAL(10, 2) NOT NULL,
    subCategoryId INT,
    categoryId INT,
    FOREIGN KEY (subCategoryId) REFERENCES SubCategory(subCategoryId),
    FOREIGN KEY (categoryId) REFERENCES Category(categoryId)
);

-- Table: MetalType
CREATE TABLE MetalType (
    metalTypeId INT IDENTITY PRIMARY KEY,
    metalTypeName VARCHAR(255) NOT NULL,
    metalTypeValue VARCHAR(255) NOT NULL
);

-- Table: CoverSize
CREATE TABLE CoverSize (
    sizeId INT,
    coverId INT,
    status VARCHAR(50),
    PRIMARY KEY (sizeId, coverId),
    FOREIGN KEY (sizeId) REFERENCES Size(sizeId),
    FOREIGN KEY (coverId) REFERENCES Cover(coverId)
);

-- Table: CoverMetalType
CREATE TABLE CoverMetalType (
    metalTypeId INT,
    coverId INT,
    status VARCHAR(50),
    PRIMARY KEY (metalTypeId, coverId),
    FOREIGN KEY (metalTypeId) REFERENCES MetalType(metalTypeId),
    FOREIGN KEY (coverId) REFERENCES Cover(coverId)
);

-- Table: Diamond
CREATE TABLE Diamond (
    diamondId INT IDENTITY PRIMARY KEY,
    diamondName VARCHAR(255) NOT NULL,
    caratWeight DECIMAL(5, 2) NOT NULL,
    color VARCHAR(50),
    clarity VARCHAR(50),
    cut VARCHAR(50),
    shape VARCHAR(50),
    price DECIMAL(10, 2) NOT NULL
);

-- Table: Product
CREATE TABLE Product (
    productId INT IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    unitPrice DECIMAL(10, 2) NOT NULL,
    diamondId INT,
    coverId INT,
    FOREIGN KEY (diamondId) REFERENCES Diamond(diamondId),
    FOREIGN KEY (coverId) REFERENCES Cover(coverId)
);

-- Table: User
CREATE TABLE [User] (
    userId INT IDENTITY PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    status VARCHAR(50),
    role VARCHAR(50)
);

-- Table: Customer
CREATE TABLE Customer (
    cusId INT IDENTITY PRIMARY KEY,
    cusFirstName VARCHAR(255) NOT NULL,
    cusLastName VARCHAR(255) NOT NULL,
    cusPhoneNum VARCHAR(20) NOT NULL,
    userId INT,
    FOREIGN KEY (userId) REFERENCES [User](userId)
);

-- Table: Manager
CREATE TABLE Manager (
    managerId INT IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    userId INT,
    FOREIGN KEY (userId) REFERENCES [User](userId)
);

-- Table: SaleStaff
CREATE TABLE SaleStaff (
    saleStaffId INT IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    email VARCHAR(255),
    userId INT,
    managerId INT,
    FOREIGN KEY (userId) REFERENCES [User](userId),
    FOREIGN KEY (managerId) REFERENCES Manager(managerId)
);

-- Table: DeliveryStaff
CREATE TABLE DeliveryStaff (
    deliveryStaffId INT IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    managerId INT,
    userId INT,
    FOREIGN KEY (userId) REFERENCES [User](userId),
    FOREIGN KEY (managerId) REFERENCES Manager(managerId)
);

-- Table: Cart
CREATE TABLE Cart (
    cartId INT IDENTITY PRIMARY KEY,
    quantity INT NOT NULL,
    cusId INT,
    FOREIGN KEY (cusId) REFERENCES Customer(cusId)
);

-- Table: CartProduct
CREATE TABLE CartProduct (
    cartId INT,
    productId INT,
    quantity INT NOT NULL,
    PRIMARY KEY (cartId, productId),
    FOREIGN KEY (cartId) REFERENCES Cart(cartId),
    FOREIGN KEY (productId) REFERENCES Product(productId)
);
CREATE TABLE favorite (
    favoriteId INT IDENTITY PRIMARY KEY,
    quantity INT NOT NULL,
    cusId INT,
    FOREIGN KEY (cusId) REFERENCES Customer(cusId)
);

CREATE TABLE favoriteProduct (
    favoriteId INT,
    productId INT,
    quantity INT NOT NULL,
    PRIMARY KEY (favoriteId, productId),
    FOREIGN KEY (favoriteId) REFERENCES favorite(favoriteId),
    FOREIGN KEY (productId) REFERENCES Product(productId)
);

-- Table: Address
CREATE TABLE Address (
    addressId INT IDENTITY PRIMARY KEY,
    street VARCHAR(255) NOT NULL,
    state VARCHAR(50),
    city VARCHAR(50),
    zipCode VARCHAR(20),
    country VARCHAR(50),
    cusId INT,
    FOREIGN KEY (cusId) REFERENCES Customer(cusId)
);

-- Table: ShippingMethod
CREATE TABLE ShippingMethod (
    shippingMethodId INT IDENTITY PRIMARY KEY,
    methodName VARCHAR(255) NOT NULL,
    cost DECIMAL(10, 2) NOT NULL,
    description VARCHAR(255),
    date DATE
);

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

-- Table: ProductOrder
CREATE TABLE ProductOrder (
    productId INT,
    orderId INT,
    quantity INT NOT NULL,
    PRIMARY KEY (productId, orderId),
    FOREIGN KEY (productId) REFERENCES Product(productId),
    FOREIGN KEY (orderId) REFERENCES [Order](orderId)
);

-- Table: Shipping
CREATE TABLE Shipping (
    shippingId INT IDENTITY PRIMARY KEY,
    status VARCHAR(50),
    orderId INT,
    saleStaffId INT,
    deliveryStaffId INT,
    FOREIGN KEY (orderId) REFERENCES [Order](orderId),
    FOREIGN KEY (saleStaffId) REFERENCES SaleStaff(saleStaffId),
    FOREIGN KEY (deliveryStaffId) REFERENCES DeliveryStaff(deliveryStaffId)
);


-- Table: Review
CREATE TABLE Review (
    reviewId INT IDENTITY PRIMARY KEY,
    review TEXT,
    rating INT,
    reviewDate DATE,
    cusId INT,
    FOREIGN KEY (cusId) REFERENCES Customer(cusId)
);

-- Table: Voucher
CREATE TABLE Voucher (
    voucherId INT IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    expDate DATE,
    cusId INT,
    FOREIGN KEY (cusId) REFERENCES Customer(cusId)
);

-- Table: CustomerVoucher
CREATE TABLE CustomerVoucher (
    cusId INT,
    voucherId INT,
    PRIMARY KEY (cusId, voucherId),
    FOREIGN KEY (cusId) REFERENCES Customer(cusId),
    FOREIGN KEY (voucherId) REFERENCES Voucher(voucherId)
);	
-- Add ColorId column to Product table
ALTER TABLE Product
ADD metalTypeId INT;

-- Add foreign key constraint for ColorId column
ALTER TABLE Product
ADD CONSTRAINT metalTypeId
FOREIGN KEY (metalTypeId) REFERENCES MetalType(metalTypeId);

-- Add SizeId column to Product table
ALTER TABLE Product
ADD SizeId INT;

-- Add foreign key constraint for SizeId column
ALTER TABLE Product
ADD CONSTRAINT sizeId
FOREIGN KEY (SizeId) REFERENCES Size(SizeId);

ALTER TABLE Product
ADD PP VARCHAR(50);

select * from diamond

