USE master

--Use this to create the database
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'FarmerManagement_DB')
DROP DATABASE FarmerManagement_DB;
CREATE DATABASE FarmerManagement_DB;--Run this part only to create the Student_DB database.
USE FarmerManagement_DB;

CREATE TABLE Users (
    [UserID]       VARCHAR (10) NOT NULL,
    [UserType]     VARCHAR (60) NOT NULL,
    [EmailAddress] VARCHAR (60) NOT NULL,
    [UserPassword] BINARY (64)  NOT NULL,
    PRIMARY KEY (UserID)
);

CREATE TABLE Employee (
    [EmployeeID]      INT        IDENTITY(10000,1)   NOT NULL,
	[UserID]          VARCHAR(10)   NOT NULL,
    [EmployeeName]    VARCHAR (100) NOT NULL,
    [EmployeeSurname] VARCHAR (100) NOT NULL,
    PRIMARY KEY  ([EmployeeId]),
	FOREIGN KEY ([UserID]) REFERENCES Users([UserID])
);

CREATE TABLE Farmer (
    [FarmerID]      INT         IDENTITY(10000,1)  NOT NULL,
    [UserID]        VARCHAR(10)   NOT NULL,
    [FarmerName]    VARCHAR (100) NOT NULL,
    [FarmerSurname] VARCHAR (100) NOT NULL,
    PRIMARY KEY  ([FarmerID] ASC),
	FOREIGN KEY ([UserID]) REFERENCES Users([UserID])
);

CREATE TABLE Product (
    [ProductID]          INT         IDENTITY(100,1)  NOT NULL,
    [ProductName]        VARCHAR (100) NOT NULL,
    [ProductDescription] VARCHAR (100) NOT NULL,
    [ProductType]        VARCHAR (100) NOT NULL,
	[ProuductAmount]      INT NOT NULL,
    [ProductPrice]       DECIMAL(10,2) NOT NULL,
    PRIMARY KEY  ([ProductID])
);

CREATE TABLE EmployeeFarmer (
    [FarmerID]   INT  NOT NULL,
    [EmployeeID] INT  NOT NULL,
    [DateAdded]  DATE NOT NULL,
    PRIMARY KEY ([FarmerID] ASC, [EmployeeID] ASC),
    FOREIGN KEY ([FarmerID]) REFERENCES Farmer ([FarmerID]),
    FOREIGN KEY ([EmployeeID]) REFERENCES Employee ([EmployeeID])
);

CREATE TABLE FarmerProduct (
    [ProductID]        INT  NOT NULL,
    [FarmerID]         INT  NOT NULL,
    [DateAddedProduct] DATE NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductID] ASC),
    FOREIGN KEY ([ProductID]) REFERENCES Product ([ProductID]),
    FOREIGN KEY ([FarmerID]) REFERENCES Farmer ([FarmerID])
);

CREATE TABLE ProductImage(
    [ProductID]        INT  NOT NULL,
	[ProductImageName] VARCHAR(100) NOT NULL,
    [ProductImage]    VARBINARY(max) NOT NULL
    PRIMARY KEY CLUSTERED ([ProductID] ASC),
    FOREIGN KEY ([ProductID]) REFERENCES Product ([ProductID]),
);

DROP TABLE ProductImage

CREATE PROCEDURE AddEmployee(
    @UserID VARCHAR(60), 
    @UserPassword VARCHAR(60),
	@EmailAddress VARCHAR(60),
    @FirstName VARCHAR(60),
    @Surname VARCHAR(60)
 )   
AS
BEGIN
        INSERT INTO Users(UserID, EmailAddress, UserType ,UserPassword)
        VALUES(@UserID,@EmailAddress,'Employee', HASHBYTES('SHA2_512', @UserPassword))

		INSERT INTO Employee(UserID,EmployeeName,EmployeeSurname)
		VALUES(@UserID,@FirstName,@Surname)
END;

EXEC AddEmployee @UserID ='emp1000', @UserPassword = 'Ye67',@EmailAddress = 'ntokozomweli@gmail.com', @FirstName = 'ntokozo', @Surname = 'mweli'

CREATE PROCEDURE AddFarmer(
    @UserID VARCHAR(60), 
    @UserPassword VARCHAR(60),
	@EmailAddress VARCHAR(60),
    @FirstName VARCHAR(60),
    @Surname VARCHAR(60),
	@EmployeeID INT,
	@DateAdded DATE
 )   
AS
BEGIN

        DECLARE @FarmerID INT

        INSERT INTO Users(UserID, EmailAddress, UserType ,UserPassword)
        VALUES(@UserID,@EmailAddress,'Farmer', HASHBYTES('SHA2_512', @UserPassword))

		INSERT INTO Farmer(UserID,FarmerName,FarmerSurname)
		VALUES(@UserID,@FirstName,@Surname)

		SELECT @FarmerID = FarmerId FROM Farmer WHERE @UserID = UserID

		INSERT INTO EmployeeFarmer(FarmerId,EmployeeId,DateAdded)
		VALUES(@FarmerID,@EmployeeID,@DateAdded)
END;


EXEC AddFarmer @UserID ='fam1000', @UserPassword = 'Ye68',@EmailAddress = 'ntokozomweli002@gmail.com', @FirstName = 'ntokozo', @Surname = 'mweli',@EmployeeID = 10000,@DateAdded = '2022-05-17'


CREATE PROCEDURE AddProduct(
    @ProductName VARCHAR(100),
	@ProductDescription VARCHAR(100),
	@ProductType VARCHAR(100),
    @ProductPrice DECIMAL(10,2),
	@ProuductAmount INT,
    @FarmerId INT,
	@DateAddedProduct DATE,
	@ProductImage VARBINARY(MAX)

 )   
AS
BEGIN
		DECLARE @ProductID INT


        INSERT INTO Product( ProductName, ProductDescription ,ProductType, ProuductAmount, ProductPrice)
        VALUES(@ProductName,@ProductDescription,@ProductType,@ProuductAmount,@ProductPrice)

		SELECT @ProductID = ProductID FROM Product WHERE @ProductName = ProductName AND @ProductDescription = ProductDescription

		INSERT INTO FarmerProduct(FarmerId,ProductId,DateAddedProduct)
		VALUES(@FarmerID,@ProductID,@DateAddedProduct)

		INSERT INTO ProductImage(ProductID,ProductImageName,ProductImage)
		VALUES(@ProductID,@ProductName,@ProductImage)
END;



