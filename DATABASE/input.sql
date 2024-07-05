

INSERT INTO ShippingMethod (methodName, cost, description)
VALUES
    ('Standard Shipping', 10, 'Delivery within 5-7 business days'),
    ('Express Shipping', 20, 'Delivery within 2-3 business days'),
    ('Free Shipping', 0.00, 'Delivery within 10-14 business days');

ALTER TABLE CoverMetaltype --add shipping method ~~
ALTER COLUMN imgUrl VARCHAR(500);
------------------------------
--Size
------------------------------
INSERT INTO Category (categoryName) VALUES ('Ring');
INSERT INTO Category (categoryName) VALUES ('Pendant');
INSERT INTO Category (categoryName) VALUES ('Earring');
INSERT INTO SubCategory (subCategoryName, categoryId) VALUES ('Engagement Ring', 1);
INSERT INTO SubCategory (subCategoryName, categoryId) VALUES ('Diamond Pendant', 2);
INSERT INTO SubCategory (subCategoryName, categoryId) VALUES ('Diamond Earring', 3);
------------------------------
Go
--*Ring Size:
DECLARE @size FLOAT = 1.0;
DECLARE @price INT = 22;
DECLARE @increment INT = 3;
DECLARE @initialPrice3 INT;
DECLARE @count INT = 0;
-- Calculate the initial price for size 3 based on the price at size 6
SET @initialPrice3 = 22 - ((6 - 3) * 4 * @increment / 4);
 
WHILE @size <= 15.0
BEGIN
    IF @size < 6.0
        SET @price = @initialPrice3 + (CAST((@size - 3.0) * 4 AS INT) * @increment / 4);
    ELSE IF @size >= 6.0 AND @size < 8.0
        SET @price = 22 + (CAST((@size - 6.0) * 4 AS INT) * @increment / 4);
    ELSE
        SET @price = 28 + (CAST((@size - 8.0) * 4 AS INT) * @increment / 4);
 
    INSERT INTO Size (sizeName, sizeValue, sizePrice)
    VALUES ('Ring Size', CAST(@size AS VARCHAR(255)), @price);
    
    SET @size = @size + 1.0; -- Increment size by 1.0
END;
go
--*Pendant
DECLARE @size INT = 15;
DECLARE @price INT = 5;
DECLARE @increment INT = 5;
DECLARE @priceIncrement INT = 5;
 
WHILE @size <= 30
BEGIN
    INSERT INTO Size (sizeName, sizeValue, sizePrice)
    VALUES ('Pendant Size', @size, @price);
    
    SET @size = @size + @increment;
    SET @price = @price + @priceIncrement;
END;
GO
--*Earring
DECLARE @size INT = 19;
DECLARE @price INT = 5;
DECLARE @increment INT = 1;
DECLARE @priceIncrement INT = 5;
 
WHILE @size <= 25
BEGIN
    INSERT INTO Size (sizeName, sizeValue, sizePrice)
    VALUES ('Earring Size', @size, @price);
    
    SET @size = @size + @increment;
    SET @price = @price + @priceIncrement;
END;
GO

go
------------------------------
--Metaltype
------------------------------
INSERT INTO Metaltype (metaltypeName, metaltypePrice) VALUES ('18k White Gold', 100);
INSERT INTO Metaltype (metaltypeName, metaltypePrice) VALUES ('18k Yellow Gold', 100);
INSERT INTO Metaltype (metaltypeName, metaltypePrice) VALUES ('18k Rose Gold', 100);
INSERT INTO Metaltype (metaltypeName, metaltypePrice) VALUES ('Platinum', 130);
INSERT INTO Metaltype (metaltypeName, metaltypePrice) VALUES ('Tungsten Carbide', 130);
INSERT INTO Metaltype (metaltypeName, metaltypePrice) VALUES ('Sterling Silver', 140);
INSERT INTO Metaltype (metaltypeName, metaltypePrice) VALUES ('Tantalum', 150);
INSERT INTO Metaltype (metaltypeName, metaltypePrice) VALUES ('Cobalt Chrome', 160);
INSERT INTO Metaltype (metaltypeName, metaltypePrice) VALUES ('Black Titanium', 200);
INSERT INTO Metaltype (metaltypeName, metaltypePrice) VALUES ('14k White Gold', 100);
INSERT INTO Metaltype (metaltypeName, metaltypePrice) VALUES ('14k Yellow Gold', 100);


------------------------------
--Cover
------------------------------
INSERT INTO Cover (coverName, status, unitPrice, subCategoryId, categoryId)
VALUES
('Petite Solitaire Engagement Ring', 'Available', 500, 1, 1),
('French Pave Diamond Engagement Ring', 'Available', 900, 1, 1),
('Petite Twist Diamond Engagement Ring', 'Available', 610, 1, 1),
('Classic Six-Prong Solitaire Engagement Ring', 'Available', 620, 1, 1),
('Petite Micropave Diamond Engagement Ring', 'Available', 1030, 1, 1),
('Classic Four-Prong Solitaire Engagement Ring', 'Available', 740, 1, 1),
('Petite Nouveau Six-Prong Solitaire Engagement Ring', 'Available', 850, 1, 1),
('Riviera Pave Diamond Engagement Ring', 'Available', 960, 1, 1),
('Classic Simple Solitaire Engagement Ring', 'Available', 770, 1, 1),
('Petite Hidden Halo Solitaire Plus Diamond Engagement Ring', 'Available', 1080, 1, 1),
('Petite Cathedral Solitaire Engagement Ring', 'Available', 690, 1, 1),
('Scalloped Pave Diamond Engagement Ring', 'Available', 1500, 1, 1),
('Six-Prong Low Dome Comfort Fit Solitaire Engagement Ring', 'Available', 610, 1, 1),
('Solitaire Plus Hidden Halo Diamond Engagement Ring', 'Available', 560, 1, 1),
('Pear Sidestone Diamond Engagement Ring', 'Available', 780, 1, 1),
('Petite Diamond Engagement Ring', 'Available', 690, 1, 1),
('Classic Comfort Fit Solitaire Engagement Ring', 'Available', 620, 1, 1),
('Delicate Twist Petite Diamond Engagement Ring', 'Available', 600, 1, 1),
('Twisted Halo Diamond Engagement Ring', 'Available', 1500, 1, 1),
('Tapered Cathedral Solitaire Engagement Ring', 'Available', 800, 1, 1);
------
INSERT INTO Cover (coverName, status, unitPrice, subCategoryId, categoryId)
VALUES ('Triple Row Diamond Hoop Earrings (3/4 Ct. Tw.)', 'Available', 630, 3, 3),
       ('Diamond Graduated Hoop Earrings (1 Ct. Tw.)', 'Available', 530, 3, 3),
       ('Diamond Graduated Hoop Earrings (1 1/4 Ct. Tw.)', 'Available', 540, 3, 3),
       ('Diamond Graduated Hoop Earrings (1 1/2 Ct. Tw.)', 'Available', 550, 3, 3),
       ('Diamond Line Drop Earrings (1 1/4 Ct. Tw.)', 'Available', 640, 3, 3),
       ('Diamond Line Drop Earrings (1 1/2 Ct. Tw.)', 'Available', 580, 3, 3),
       ('Petite Diamond Milgrain Hoop Earrings (1/4 Ct. Tw.)', 'Available', 730, 3, 3),
       ('Petite Diamond Milgrain Hoop Earrings (1/3 Ct. Tw.)', 'Available', 720, 3, 3),
       ('Petite Diamond Milgrain Hoop Earrings (1/5 Ct. Tw.)', 'Available', 710, 3, 3),
       ('Floating Diamond Hoop Earrings (1/2 Ct. Tw.)', 'Available', 550, 3, 3),
       ('Floating Diamond Eternity Hoop Earrings (1/2 Ct. Tw.)', 'Available', 630, 3, 3),
       ('Floating Diamond Eternity Hoop Earrings (1 Ct. Tw.)', 'Available', 830, 3, 3),
       ('Halo Diamond Earring Setting (1/3 Ct. Tw.)', 'Available', 650, 3, 3),
       ('Halo Diamond Earring Setting (1/2 Ct. Tw.)', 'Available', 930, 3, 3),
       ('Sunburst Diamond Stud Earrings (1 Ct. Tw.)', 'Available', 760, 3, 3),
       ('Sunburst Diamond Stud Earrings (3/4 Ct. Tw.)', 'Available', 710, 3, 3),
       ('Sunburst Diamond Stud Earrings (1 1/4 Ct. Tw.)', 'Available', 720, 3, 3);
------------
INSERT INTO Cover (coverName, status, unitPrice, subCategoryId, categoryId) 
VALUES ('Double-Bail Solitaire Pendant Setting', 'Available', 300, 2, 2 )
GO
INSERT INTO Cover (coverName, status, unitPrice, subCategoryId, categoryId) 
VALUES ('The Gallery Collection™ Diamond Halo Pavé Pendant Setting', 'Available', 310, 2, 2 )
GO
INSERT INTO Cover (coverName, status, unitPrice, subCategoryId, categoryId) 
VALUES ('Halo Diamond Pendant Setting', 'Available', 330, 2, 2 )
GO
INSERT INTO Cover (coverName, status, unitPrice, subCategoryId, categoryId) 
VALUES ('Bezel Solitaire Pendant Setting', 'Available', 320, 2, 2 )
GO
INSERT INTO Cover (coverName, status, unitPrice, subCategoryId, categoryId) 
VALUES ('Petite Bail Pendant', 'Available', 300, 2, 2 )
GO
INSERT INTO Cover (coverName, status, unitPrice, subCategoryId, categoryId) 
VALUES ('Bezel Set Solitaire Pendant Setting', 'Available', 350, 2, 2 )
GO
------------------- 
--CoverSize
-------------------
--Ring
DECLARE @coverId INT;
DECLARE @sizeCount INT;
DECLARE @sizeId INT;
DECLARE @startSizeId INT;
DECLARE @endSizeId INT;

SET @coverId = 1;

WHILE @coverId <= 20
BEGIN
    -- Determine a random number of sizes (between 3 and 6) for each cover
    SET @sizeCount = 3 + (ABS(CHECKSUM(NEWID())) % 4);
    
    -- Determine the starting size ID for this cover
    SET @startSizeId = 1 + (ABS(CHECKSUM(NEWID())) % (17 - @sizeCount));
    
    -- Determine the ending size ID for this cover
    SET @endSizeId = @startSizeId + @sizeCount - 1;

    -- Insert size entries for this cover
    SET @sizeId = @startSizeId;
    WHILE @sizeId <= @endSizeId
    BEGIN
        INSERT INTO CoverSize (sizeId, coverId, status)
        VALUES (@sizeId, @coverId, 'Available');
        SET @sizeId = @sizeId + 1;
    END
    
    SET @coverId = @coverId + 1;
END;
go
-----
--Earring
DECLARE @coverId INT;
DECLARE @sizeCount INT;
DECLARE @sizeId INT;
DECLARE @startSizeId INT;
DECLARE @endSizeId INT;
SET @coverId = 21;
WHILE @coverId <= 37
BEGIN
    -- Determine a random number of sizes (between 3 and 6) for each cover
    SET @sizeCount = 3 + (ABS(CHECKSUM(NEWID())) % 4);
    
    -- Determine the starting size ID for this cover
    SET @startSizeId = 21 + (ABS(CHECKSUM(NEWID())) % (7 - @sizeCount));
    
    -- Determine the ending size ID for this cover
    SET @endSizeId = @startSizeId + @sizeCount - 1;

    -- Insert size entries for this cover
    SET @sizeId = @startSizeId;
    WHILE @sizeId <= @endSizeId
    BEGIN
        INSERT INTO CoverSize (sizeId, coverId, status)
        VALUES (@sizeId, @coverId, 'Available');
        SET @sizeId = @sizeId + 1;
    END
    
    SET @coverId = @coverId + 1;
END;
----
--Pendant
SET @coverId = 38;

WHILE @coverId <= 43
BEGIN
    -- Determine a random number of sizes (between 3 and 4) for each cover
    SET @sizeCount = 3 + (ABS(CHECKSUM(NEWID())) % 2);
    
    -- Determine the starting size ID for this cover
    SET @startSizeId = 17 + (ABS(CHECKSUM(NEWID())) % (5 - @sizeCount));
    
    -- Determine the ending size ID for this cover
    SET @endSizeId = @startSizeId + @sizeCount - 1;

    -- Insert size entries for this cover
    SET @sizeId = @startSizeId;
    WHILE @sizeId <= @endSizeId
    BEGIN
        INSERT INTO CoverSize (sizeId, coverId, status)
        VALUES (@sizeId, @coverId, 'Available');
        SET @sizeId = @sizeId + 1;
    END
    
    SET @coverId = @coverId + 1;
END;
---------------------
--CoverMetalType
---------------------
--Ring
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (1,1,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0.jpg?alt=media&token=2a5cfbe4-e291-4040-af96-501a3f239e2b');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (2,1,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(1).jpg?alt=media&token=e90bf300-397e-460e-8d79-da906f6e505f');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (3,1,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(2).jpg?alt=media&token=34df5ae1-a389-4de8-abda-41914aa36a8e');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (1,2,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(3).jpg?alt=media&token=1f1c9ce1-986d-4ce6-80ed-a22f30283f1a');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (2,2,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(4).jpg?alt=media&token=27776ed7-0d42-411e-853e-3f6632ae4823');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (3,2,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(5).jpg?alt=media&token=fac690bf-6194-4b95-8d8e-8769b1b3c975');
--------------------------------
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (1,3,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(6).jpg?alt=media&token=e816419e-7c2b-4b71-832a-20d98db0b138');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (2,3,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(7).jpg?alt=media&token=370094a3-fb12-43c9-8f40-46bfd7df116b');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (3,3,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(8).jpg?alt=media&token=268dd0f3-0a31-4849-993d-33b205bf9a6b');
---------------------------------------
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (1,4,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(9).jpg?alt=media&token=9549fc11-7c8b-47df-a527-c762f291fcf0');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (2,4,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(10).jpg?alt=media&token=d08c9427-53e7-4f28-9c76-482b3e4cc5ed');
--------------------------------------
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (1,5,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(11).jpg?alt=media&token=654dc35f-4085-43ac-8cdb-38eef724f670');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (2,5,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(12).jpg?alt=media&token=5edf40bc-74c8-4503-8ad4-1084e51e807e');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (3,5,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(13).jpg?alt=media&token=bb36e0af-1d53-4964-a213-cb9e99cd6c4d');
--------------------------------------------
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (1,6,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(14).jpg?alt=media&token=f1809024-a0a0-4bbb-9191-412b421a6407');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (2,6,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(15).jpg?alt=media&token=f61df6c8-1b95-4197-bc16-33f28f635daa');
---------------------------------------------------
-- Records for coverId 7
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 7, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(16).jpg?alt=media&token=d7cd8a53-d3c8-4e21-917d-77aa583f1488'),
    (4, 7, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(16).jpg?alt=media&token=d7cd8a53-d3c8-4e21-917d-77aa583f1488');

-- Records for coverId 8
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 8, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(17).jpg?alt=media&token=4be4f204-20f5-4f12-8e9a-5229101cbbdf'),
    (3, 8, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(18).jpg?alt=media&token=ef5f42e5-a546-4723-9ecb-74b85965e834'),
    (2, 8, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(19).jpg?alt=media&token=be095fdc-5d38-46bf-a24f-05f7d02e6780');

-- Records for coverId 9
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (4, 9, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(20).jpg?alt=media&token=d924d9e2-7cc4-48ed-a1d5-3fb8c4cb2054');

-- Records for coverId 10
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 10, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(21).jpg?alt=media&token=7d367ade-93ac-4336-982b-ac67c34c3ef3'),
    (2, 10, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(22).jpg?alt=media&token=d32cc702-0ccb-4d5a-b018-adb8d562c7fc'),
    (3, 10, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(23).jpg?alt=media&token=88e6d683-e592-4315-b10a-03d0ad57a8dc');

-- Records for coverId 11
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 11, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(24).jpg?alt=media&token=4af41769-d9be-4b5e-9d31-a2d0b95a7718'),
    (2, 11, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(25).jpg?alt=media&token=3a85af2f-0d01-42a0-81e3-5dcb6c0096b2'),
    (3, 11, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(26).jpg?alt=media&token=cf6ead99-f308-4a94-b2b0-9d6db34da6ad');

-- Records for coverId 12
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 12, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(27).jpg?alt=media&token=3e6ed3cb-6810-42fe-92db-2d15c789ce0d'),
    (2, 12, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(28).jpg?alt=media&token=56716a6b-4777-40c5-a17a-4fbc1e5fdebf'),
    (4, 12, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(29).jpg?alt=media&token=d8ddd6d4-481d-4d86-b0b5-ab7a0491c956');

-- Records for coverId 13
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 13, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(30).jpg?alt=media&token=689aa4b3-a6b8-4781-baab-64d2ec6ba471'),
    (2, 13, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(31).jpg?alt=media&token=8ea2796e-b7c2-47e6-9cf9-647c7af18395'),
    (4, 13, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(32).jpg?alt=media&token=be2508bd-4e17-44fa-b75d-ca54b4d49424');

-- Records for coverId 14
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 14, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(33).jpg?alt=media&token=2dbbd922-d0be-4dc4-814f-58ff21de40e2'),
    (2, 14, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(34).jpg?alt=media&token=f3c35bef-31f0-49c6-89bb-764ab2b61142'),
    (3, 14, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(35).jpg?alt=media&token=9545910d-522b-4298-b205-3d2fdd10c26f');

-- Records for coverId 15
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 15, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(36).jpg?alt=media&token=a1427e29-5592-4ef4-b178-7be6b37012b3'),
    (2, 15, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(37).jpg?alt=media&token=37f50724-24f8-4bb1-9751-e7d918fbf3ed'),
    (3, 15, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(38).jpg?alt=media&token=d6c9c1f5-4ef0-4b48-8705-cbeeb59240cc');

-- Records for coverId 16
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 16, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(39).jpg?alt=media&token=eb9a14c6-75bc-4a77-8443-7ff3c019647a'),
    (2, 16, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(40).jpg?alt=media&token=01306f66-00e0-48f2-9c32-22983cf4b73e'),
    (3, 16, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(41).jpg?alt=media&token=9533d62e-43ac-4f58-8237-147190a7d2ad');

-- Records for coverId 17
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 17, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(42).jpg?alt=media&token=79f793ed-f52b-4047-99a7-b136abdfebf5'),
    (2, 17, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(43).jpg?alt=media&token=7d4d93ae-047f-42fa-96fa-f54da0590a1a'),
    (3, 17, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(44).jpg?alt=media&token=ea88e2fc-bb29-4a3c-bc5c-647c21d25033');

-- Records for coverId 18
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 18, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(45).jpg?alt=media&token=6b31550e-6ed6-40ef-8d26-c4c2f89a7ef3'),
    (2, 18, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(46).jpg?alt=media&token=5c86ad4d-78e1-43f7-9e96-f0b8ecfc1b7a'),
    (3, 18, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(47).jpg?alt=media&token=8d17a71a-497b-4676-83dc-9c49fdd0b260'),
    (4, 18, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(48).jpg?alt=media&token=908e14a5-1b02-4a4d-8318-e2e810d69a9b');

-- Records for coverId 19
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 19, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(49).jpg?alt=media&token=1d453f7b-3c08-48e7-bd91-87f67129de73'),
    (2, 19, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(50).jpg?alt=media&token=dd3d1648-4fd5-4d48-8bbd-9d20c6c5455d'),
    (3, 19, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(51).jpg?alt=media&token=7873b95b-4a25-4c03-b1b8-0a4a78080715'),
    (4, 19, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(52).jpg?alt=media&token=ffad48d6-52f1-4538-ae37-d7f10af56498');

-- Records for coverId 20
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES 
    (1, 20, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(53).jpg?alt=media&token=15c01ec1-c7bb-4ed2-89d5-109002d3af00'),
    (2, 20, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(54).jpg?alt=media&token=7e56b77e-c812-42ff-b6dc-f033c72fc701'),
    (3, 20, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(55).jpg?alt=media&token=87d49be2-57a5-401d-83b0-ff64f29a4ae7'),
    (4, 20, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/LS_stage_0%20(56).jpg?alt=media&token=1a07a30d-52f6-4935-97c2-203abacb6476');
-----------
--Earring
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl)
VALUES (2,21,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er01.jpg?alt=media&token=53ec8d13-197b-4bfc-9040-7c503e9944b1'),
(1,22,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er02-1.jpg?alt=media&token=7a98aaa0-e214-44db-adab-7235594e332b'),
(3,23,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er02-2.jpg?alt=media&token=68863905-b6b8-48af-899d-cb23b2f1d639'),
(2,24,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er02-3.jpg?alt=media&token=864c8dc7-ea5a-4154-8c57-cbbbec153754'),
(1,25,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er03.jpg?alt=media&token=ab3847d2-ebfc-42f1-b84e-04e166863b99'),
(1,27,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er04-1.jpg?alt=media&token=5b6d5578-b1b8-455c-98d1-7ab913fb5f8e'),
(2,28,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er04-2.jpg?alt=media&token=dc6f74e0-305b-48df-82c6-7eb8b2d71fc0'),
(3,29,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er04-3.jpg?alt=media&token=46883b01-725a-4e99-88b7-0114d38ac23c'),
(1,30,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er05.jpg?alt=media&token=2a394edb-7882-416c-ae83-0ea96fcf6d46'),
(1,31,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er06.jpg?alt=media&token=3f47a8a6-5c83-47f3-996e-95c2edf97414'),
(1,32,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er06.jpg?alt=media&token=3f47a8a6-5c83-47f3-996e-95c2edf97414'),
(1,33,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er07-1.jpg?alt=media&token=fcfbf00e-de67-4ecd-87cd-02c82557594b'),
(4,34,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er07-2.jpg?alt=media&token=66ddd5a2-6fb7-46fb-8ec2-2455def44bf1'),
(1,35,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er08-1.jpg?alt=media&token=bcfe28c8-289e-4934-add9-8f239932a161'),
(2,36,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er08-2.jpg?alt=media&token=08fba4c3-2a2d-47b7-8cc0-f6cca37040b6'),
(3,37,'Available','https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/er08-3.jpg?alt=media&token=a9a83f4b-2194-4616-9b35-792b3f7186e8');
-------------
--Pendant
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (1, 38, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Double-Bail%20Solitaire%20Pendant%20Setting%20In%2018k%20White%20Gold.jpg?alt=media&token=bdd10add-4287-45c5-a1a7-879c4991f06c');

INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (2, 38, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Double-Bail%20Solitaire%20Pendant%20Setting%20In%2018k%20Yellow%20Gold.jpg?alt=media&token=7652810e-8f1d-45e8-b098-9c1f03bb8411');

INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (4, 38, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Double-Bail%20Solitaire%20Pendant%20Setting%20In%20Platinum.jpg?alt=media&token=3312f93c-035c-44e3-89df-b4adaacbee36');

INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (11, 38, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Double%20Bail%20Solitaire%20Pendant%20Setting%20In%2014k%20White%20Gold.jpg?alt=media&token=920c6e01-09de-46eb-8a5d-4a7066d1a6d4');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (4, 39, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/The%20Gallery%20Collection%E2%84%A2%20Diamond%20Halo%20Pav%C3%A9%20Pendant%20Setting%20In%20Platinum.jpg?alt=media&token=e70fd3ac-4040-4272-86a9-317102402d98');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (4, 40, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Halo%20Diamond%20Pendant%20Setting%20In%20Platinum.jpg?alt=media&token=0548eedd-3ad0-4f42-9602-3c3a223c5bb1');

INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (10, 40, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Halo%20Diamond%20Pendant%20Setting%20In%2014k%20White%20Gold.jpg?alt=media&token=d8a6dad3-d5f4-49d1-b0eb-71e55fec2e57');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (10, 41, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Bezel%20Solitaire%20Pendant%20Setting%20In%2014k%20White%20Gold.jpg?alt=media&token=e2047961-f5d6-4bea-b0ab-597d76a09df2');

INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (11, 41, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Bezel%20Solitaire%20Pendant%20Setting%20In%2014k%20Yellow%20Gold.jpg?alt=media&token=e287304f-03d6-4002-b428-4be0223c7644');

INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (4, 41, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Bezel%20Solitaire%20Pendant%20Setting%20In%20Platinum.jpg?alt=media&token=a3daf3a8-0341-4d24-934c-1df12d7b6e02');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (1, 42, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Petite%20Bail%20Pendant%20In%2018k%20White%20Gold.jpg?alt=media&token=7537ba11-eeda-4929-a3fa-09c152c06f19');

INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (2, 42, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Petite%20Bail%20Pendant%20In%2018k%20Yellow%20Gold.jpg?alt=media&token=a7e31ad4-d043-43b5-b4bc-686bdf6f3268');

INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (4, 42, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Petite%20Bail%20Pendant%20In%20Platinum.jpg?alt=media&token=c97e8cf1-acce-4d42-b15e-3f9f4aa8acd0');

INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (10, 42, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Petite%20Bail%20Pendant%20In%2014k%20White%20Gold.jpg?alt=media&token=4335a407-d6f4-44cc-8f2c-a675cbb09ffa');
INSERT INTO CoverMetaltype (metaltypeId, coverId, status, imgUrl) 
VALUES (10, 43, 'Available', 'https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Bezel%20Set%20Solitaire%20Pendant%20Setting%20In%2014k%20White%20Gold.jpg?alt=media&token=95fe918c-0198-4206-a381-ac28ca2f53d7');

go
-- Create temporary tables to hold existing IDs
CREATE TABLE #CoverIds (coverId INT);
CREATE TABLE #MetaltypeIds (metaltypeId INT);
CREATE TABLE #SizeIds (sizeId INT);
CREATE TABLE #DiamondIds (diamondId INT);

-- Populate the temporary tables with existing IDs
INSERT INTO #CoverIds SELECT coverId FROM Cover;
INSERT INTO #MetaltypeIds SELECT metaltypeId FROM Metaltype;
INSERT INTO #SizeIds SELECT sizeId FROM Size;
INSERT INTO #DiamondIds SELECT diamondId FROM Diamond;

DECLARE @coverId INT;
DECLARE @metaltypeId INT;
DECLARE @sizeId INT;
DECLARE @diamondId INT;
DECLARE @coverName NVARCHAR(255);
DECLARE @diamondName NVARCHAR(255);
DECLARE @productName NVARCHAR(255);
DECLARE @unitPrice MONEY = 100.00; -- Default price for example
DECLARE @PP VARCHAR(50) = 'premade'; -- Default PP for example

DECLARE @i INT = 1;

-- Loop to insert 1000 products
WHILE @i <= 1000
BEGIN
    -- Get random IDs from temporary tables
    SELECT TOP 1 @coverId = coverId FROM #CoverIds ORDER BY NEWID();
    SELECT TOP 1 @metaltypeId = metaltypeId FROM #MetaltypeIds ORDER BY NEWID();
    SELECT TOP 1 @sizeId = sizeId FROM #SizeIds ORDER BY NEWID();
    SELECT TOP 1 @diamondId = diamondId FROM #DiamondIds ORDER BY NEWID();

    -- Check if the metaltype and size are associated with the cover
    IF EXISTS (SELECT 1 FROM CoverMetaltype WHERE coverId = @coverId AND metaltypeId = @metaltypeId)
    AND EXISTS (SELECT 1 FROM CoverSize WHERE coverId = @coverId AND sizeId = @sizeId)
    BEGIN
        -- Get coverName and diamondName
        SELECT @coverName = coverName FROM Cover WHERE coverId = @coverId;
        SELECT @diamondName = diamondName FROM Diamond WHERE diamondId = @diamondId;

        -- Create the product name as combination of coverName and diamondName
        SET @productName = @coverName + ' ' + @diamondName;

        -- Insert the new product
        INSERT INTO Product (productName, unitPrice, diamondId, coverId, metaltypeId, sizeId, PP)
        VALUES (@productName, @unitPrice, @diamondId, @coverId, @metaltypeId, @sizeId, @PP);
        
        -- Increment the counter only if the product is inserted
        SET @i = @i + 1;
    END
END

-- Clean up temporary tables
DROP TABLE #CoverIds;
DROP TABLE #MetaltypeIds;
DROP TABLE #SizeIds;
DROP TABLE #DiamondIds;

PRINT 'Inserted 1000 products successfully.';

INSERT INTO Voucher (name, topPrice, bottomPrice, description, expDate, quantity, rate) VALUES
('Voucher 1', 10000.00, 500.00, 'Description for Voucher 1', '2024-12-31', 100, 10),
('Voucher 2', 15000.00, 0, 'Description for Voucher 2', '2024-11-30', 100, 15),
('Voucher 3', 20000.00, 10000.00, 'Description for Voucher 3', '2024-10-31', 100, 30),
('Voucher 4', 25000.00, 12500.00, 'Description for Voucher 4', '2024-09-30', 100, 35)
