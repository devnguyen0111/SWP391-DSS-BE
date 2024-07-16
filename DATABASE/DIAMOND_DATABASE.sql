
CREATE DATABASE DIAMOND_DB
GO
USE DIAMOND_DB
GO
-- Table: Size
CREATE TABLE Size (
    sizeId INT IDENTITY PRIMARY KEY,
    sizeName VARCHAR(255) NOT NULL,
    sizeValue VARCHAR(255) NOT NULL,
	sizePrice Money,
	--status varchar(50)
);
GO
-- Table: MetalType
CREATE TABLE Metaltype (
    metaltypeId INT IDENTITY PRIMARY KEY,
    metaltypeName VARCHAR(255) NOT NULL,
    metaltypePrice Money
    --status varchar(50)
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
-- Table: CoverMetalType --add img:Cover with MetalType will create an img
CREATE TABLE CoverMetaltype (
    metaltypeId INT,
    coverId INT,
    status VARCHAR(50),
	imgUrl varchar(200),
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
    diamondId INT NOT NULL,
    coverId INT NOT NULL,
	metaltypeId INT NOT NULL,
	sizeId INT NOT NULL,
	PP VARCHAR(50),
    FOREIGN KEY (diamondId) REFERENCES Diamond(diamondId) on delete cascade,
    FOREIGN KEY (coverId) REFERENCES Cover(coverId) on delete cascade,
	FOREIGN KEY (metaltypeId) REFERENCES Metaltype(metaltypeId)on delete cascade,
    FOREIGN KEY (sizeId) REFERENCES Size(sizeId)on delete cascade
);
GO
-- Table: User
CREATE TABLE [User] (
    userId INT IDENTITY PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    status VARCHAR(50) NOT NULL,
    role VARCHAR(50) NOT NULL,
	RefreshToken VARCHAR(200),
	RefreshTokenExpiryTime Datetime,
);
GO
-- Table: Customer

CREATE TABLE Customer (
    cusId INT PRIMARY KEY,
    cusFirstName VARCHAR(255) NOT NULL,
    cusLastName VARCHAR(255) NOT NULL,
    cusPhoneNum VARCHAR(20),
    FOREIGN KEY (cusId) REFERENCES [User](userId) on delete cascade
);
GO
-- Table: Manager
CREATE TABLE Manager (
    manId INT  PRIMARY KEY,
    manName VARCHAR(255) NOT NULL,
    manPhone VARCHAR(20), 
    FOREIGN KEY (manId) REFERENCES [User](userId) on delete cascade
);
GO
-- Table: SaleStaff
CREATE TABLE SaleStaff (
    sStaffId INT  PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    email VARCHAR(255),
    managerId INT,
    FOREIGN KEY (sStaffId) REFERENCES [User](userId) on delete cascade,
    FOREIGN KEY (managerId) REFERENCES Manager(manId)
);
GO
-- Table: DeliveryStaff
CREATE TABLE DeliveryStaff (
    dStaffId INT  PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    managerId INT,
    FOREIGN KEY (dStaffId) REFERENCES [User](userId) on delete cascade,
    FOREIGN KEY (managerId) REFERENCES Manager(manId)
);
GO
-- Table: Cart
CREATE TABLE Cart (
    cartId INT PRIMARY KEY,
    cartQuantity INT NOT NULL,
    FOREIGN KEY (cartId) REFERENCES Customer(cusId) on delete cascade
);
GO
-- Table: CartProduct
CREATE TABLE CartProduct (
    cartId INT,
    productId INT,
    quantity INT NOT NULL,
    PRIMARY KEY (cartId, productId),
    FOREIGN KEY (cartId) REFERENCES Cart(cartId) on delete cascade,
    FOREIGN KEY (productId) REFERENCES Product(productId)
);
GO
CREATE TABLE favorite (
    favoriteId INT PRIMARY KEY,
    quantity INT NOT NULL,
    FOREIGN KEY (favoriteId) REFERENCES Customer(cusId) on delete cascade
);

CREATE TABLE favoriteProduct (
    favoriteId INT,
    productId INT,
    PRIMARY KEY (favoriteId, productId),
    FOREIGN KEY (favoriteId) REFERENCES favorite(favoriteId),
    FOREIGN KEY (productId) REFERENCES Product(productId)
);
GO
-- Table: Address
CREATE TABLE [Address] (
    addressId INT PRIMARY KEY,
    street VARCHAR(500),
    state VARCHAR(50),
    city VARCHAR(50),
    zipCode VARCHAR(20),
    country VARCHAR(50),
    FOREIGN KEY (addressId) REFERENCES Customer(cusId) on delete cascade
);
GO
-- Table: ShippingMethod --remove Date
CREATE TABLE ShippingMethod (
    shippingMethodId INT IDENTITY PRIMARY KEY,
    methodName VARCHAR(255) NOT NULL,
    cost DECIMAL(10, 2) NOT NULL,
    description VARCHAR(255),
);
GO
-- Table: Order --Add deliveryAddress and contactNumber
CREATE TABLE [Order] (
    orderId INT IDENTITY PRIMARY KEY,
    orderDate DATETIME NOT NULL,
    totalAmount DECIMAL(10, 2) NOT NULL,
    status VARCHAR(50),
    cusId INT NOT NULL,
	deliveryAddress varchar(200),
	contactNumber varchar(20),
    shippingMethodId INT,
    FOREIGN KEY (cusId) REFERENCES Customer(cusId) on delete cascade,
    FOREIGN KEY (shippingMethodId) REFERENCES ShippingMethod(shippingMethodId)
);
GO
-- Table: ProductOrder
CREATE TABLE ProductOrder (
    productId INT not null,
    orderId INT not null,
    quantity INT NOT NULL,
    PRIMARY KEY (productId, orderId),
    FOREIGN KEY (productId) REFERENCES Product(productId)on delete cascade,
    FOREIGN KEY (orderId) REFERENCES [Order](orderId) on delete cascade
);
GO
-- Table: Shipping --add and expectedFinishDate
CREATE TABLE Shipping (
    shippingId INT IDENTITY PRIMARY KEY,
    status VARCHAR(50),
    orderId INT,
	expectedFinishDate Date,
    saleStaffId INT NOT NULL,
    deliveryStaffId INT,
    FOREIGN KEY (orderId) REFERENCES [Order](orderId)on delete cascade,
    FOREIGN KEY (saleStaffId) REFERENCES SaleStaff(sStaffId),
    FOREIGN KEY (deliveryStaffId) REFERENCES DeliveryStaff(dStaffId)
);
GO
-- Table: Review
CREATE TABLE Review (
    reviewId INT IDENTITY PRIMARY KEY,
    review TEXT,
    rating DECIMAL,
    reviewDate DATE,
    cusId INT NOT NULL,
	productId INT NOT NULL,
    FOREIGN KEY (cusId) REFERENCES Customer(cusId)on delete cascade,
	FOREIGN KEY (productId) REFERENCES Product(productId) on delete cascade
);
GO
-- Table: Voucher --remove userid --change rate to int --add top and bottom price
CREATE TABLE Voucher (
    voucherId INT IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
	topPrice money ,
	bottomPrice money ,
    description TEXT,
    expDate DATE NOT NULL,
	quantity INT,
	rate INT,
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

-- Table: Wishlist
CREATE TABLE Wishlist (
	wishlistId INT IDENTITY PRIMARY KEY,
    userId INT NOT NULL,
    productId INT NOT NULL,
    dateAdded DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (userId) REFERENCES [User](userId) ON DELETE CASCADE,
    FOREIGN KEY (productId) REFERENCES Product(productId) ON DELETE CASCADE
);
GO

--Table: Request
CREATE TABLE Request (
    requestId INT IDENTITY PRIMARY KEY,
    requestedDate DATETIME DEFAULT GETDATE(),
    context VARCHAR(255) NOT NULL,
    status VARCHAR(255) NOT NULL,
    sStaffId INT NOT NULL,
    manId INT NOT NULL,
    orderId INT NOT NULL,
    FOREIGN KEY (sStaffId) REFERENCES SaleStaff(sStaffId) ON DELETE NO ACTION ON UPDATE NO ACTION,
    FOREIGN KEY (manId) REFERENCES Manager(manId) ON DELETE NO ACTION ON UPDATE NO ACTION,
    FOREIGN KEY (orderId) REFERENCES [Order](orderId) ON DELETE NO ACTION ON UPDATE NO ACTION
);
