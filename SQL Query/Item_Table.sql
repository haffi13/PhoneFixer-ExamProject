------SQL Query for 

----Table creations:

----ITEM tabel

--CREATE TABLE ITEM (
--Barcode				NVarchar(15)		PRIMARY KEY		NOT NULL,
--Name				NVarchar(50)						NOT NULL,
--Description			NVarchar(150)						NULL,
--Price				Decimal(18,2)						NOT NULL,
--PriceWithTax		Decimal(18,2)						NOT NULL,
--Category			NVarchar(20)						NOT NULL,
--Model				NVarchar(30)						NOT NULL,
--LastAddDay			Datetime							NOT NULL,
--NumberAvailable		INT									NOT NULL,
--);

----CUSTOMER Tabel

--CREATE TABLE CUSTOMER (
--CustomerId		INT				PRIMARY KEY		IDENTITY	NOT NULL,
--CustomerName	NVarchar(50)								NOT NULL,
--CustomerPhone	NVarchar(15)								NOT NULL	DEFAULT 'IKKE LISTED',
--Email			NVarchar(50)								NOT NULL	DEFAULT 'IKKE LISTED',
--Subscribed		BIT											NOT NULL	DEFAULT '0',
--ItemInService	BIT											NOT NULL	DEFAULT '0',
--);

------Service table:

--CREATE TABLE SERVICE (
--ServiceNumber			INT				PRIMARY KEY		IDENTITY	NOT NULL,
--ServiceName				NVarchar(30)								NOT NULL	DEFAULT 'SERVICE',
--ServiceDescription		NVarchar(150)								NULL,
--PriceNoTax				Decimal(18,2)								NOT NULL	DEFAULT '0.00',
--PriceWithTax			Decimal(18,2)								NOT NULL	DEFAULT '0.00',
--DayServiced				Datetime									NOT NULL,
--DayUpdated				Datetime									NOT NULL,
--Repaired				BIT											NOT NULL	DEFAULT '0',
--CustomerId				INT				FOREIGN KEY 
--							REFERENCES CUSTOMER(CustomerId)
--								ON DELETE NO ACTION,
--);

----Procedures:

----Get ALL items in the item table

--CREATE PROCEDURE GetAllItems

--AS
--BEGIN
--		SELECT Barcode, Name, Description, Price, PriceWithTax, Category, Model, LastAddDay, NumberAvailable FROM ITEM
--END

----Add Item to dbo.ITEM table

--CREATE PROCEDURE AddItem
--				(@Barcode NVarchar(15), @Name NVarchar(50), @Description NVarchar(150), @Price Decimal(18,2), @PriceWithTax Decimal(18,2), @Category NVarchar(20), @Model NVarchar(30), @LastAddDay Datetime, @NumberAvailable INT)
--AS
--BEGIN
--		INSERT INTO ITEM
--		VALUES	(@Barcode, @Name, @Description, @Price, @PriceWithTax, @Category, @Model, @LastAddDay, @NumberAvailable)
--END

------Insert and update data in dbo.ITEM NB: This may make the AddItem procedure OBSOLETE!!!

--CREATE PROCEDURE UpdateItem

--	@Barcode NVarchar(15),
--	@Name NVarchar(50),
--	@Description NVarchar(150),
--	@Price Decimal(18,2),
--	@PriceWithTax Decimal(18,2),
--	@Category NVarchar(20),
--	@Model NVarchar(30),
--	@LastAddDay Datetime,
--	@NumberAvailable INT

--AS

--BEGIN
--	SET NOCOUNT ON;

--	IF (SELECT TOP (1) 1 FROM ITEM WHERE Barcode = @Barcode) IS NULL
--		INSERT INTO ITEM(Barcode, Name, Description, Price, PriceWithTax, Category, Model, LastAddDay, NumberAvailable)
--		VALUES(@Barcode, @Name, @Description, @Price, @PriceWithTax, @Category, @Model, @LastAddDay, @NumberAvailable)
--	ELSE
--		UPDATE ITEM SET Name = @Name, Description = @Description, Price = @Price, PriceWithTax = @PriceWithTax, Category = @Category, Model = @Model, LastAddDay = @LastAddDay, NumberAvailable = @NumberAvailable
--		WHERE Barcode = @Barcode
--END

----Delete Item from dbo.ITEM:

--CREATE PROCEDURE DeleteItem

--	@Barcode NVarchar(15),
--	@Name NVarchar(50),
--	@Description NVarchar(150),
--	@Price Decimal(18,2),
--	@PriceWithTax Decimal(18,2),
--	@Category NVarchar(20),
--	@Model NVarchar(30),
--	@LastAddDay Datetime,
--	@NumberAvailable INT

--AS

--BEGIN
--	SET NOCOUNT ON;
--	DELETE FROM ITEM WHERE Barcode = @Barcode
--END

------Search table ITEM for a item based on a string:

--CREATE PROCEDURE SearchItemName

--	@Barcode NVarchar(15),
--	@Name NVarchar(50),
--	@Description NVarchar(150),
--	@Price Decimal(18,2),
--	@PriceWithTax Decimal(18,2),
--	@Category NVarchar(20),
--	@Model NVarchar(30),
--	@LastAddDay Datetime,
--	@NumberAvailable INT

--AS

--BEGIN
--	SET NOCOUNT ON;
--	SELECT * FROM ITEM WHERE Name = @Name
--END

----Get all customers from CUSTOMER table

--CREATE PROCEDURE GetAllCustomers

--AS

--BEGIN
--	SELECT CustomerId, CustomerName, Email, Subscribed, ItemInService FROM CUSTOMER
--END

----Update existing customer or update exsisting customer in CUSROMER table.

--CREATE PROCEDURE UpdateCustomer

--	@CustomerId INT,
--	@CustomerName NVarchar(50),
--	@CustomerPhone NVarchar(15),
--	@Email NVarchar(50),
--	@Subscribed BIT,
--	@ItemInService BIT

--AS

--BEGIN
--	SET NOCOUNT ON;

--	IF (SELECT TOP (1) 1 FROM CUSTOMER WHERE CustomerId = @CustomerId) IS NULL
--		INSERT INTO CUSTOMER(CustomerId, CustomerName, CustomerPhone, Email, Subscribed, ItemInService)
--		VALUES(@CustomerId, @CustomerName, @CustomerPhone, @Email, @Subscribed, @ItemInService)
--	ELSE
--		UPDATE CUSTOMER SET CustomerName = @CustomerName, CustomerPhone = @CustomerPhone, Email = @Email, Subscribed = @Subscribed, ItemInService = @ItemInService
--		WHERE CustomerId = @CustomerId
--END

----Delete Customer from CUSTOMER table

--CREATE PROCEDURE DeleteCustomer

--	@CustomerId INT,
--	@CustomerName NVarchar(50),
--	@CustomerPhone NVarchar(15),
--	@Email NVarchar(50),
--	@Subscribed BIT,
--	@ItemInService BIT

--AS

--BEGIN
--	SET NOCOUNT ON;
--	DELETE FROM CUSTOMER WHERE CustomerId = @CustomerId
--END

----Get all services from SERVICE table

--CREATE PROCEDURE GetAllServices

--AS
--BEGIN
--	SELECT ServiceNumber, ServiceName, ServiceDescription, PriceNoTax, PriceWithTax, DayServiced, DayUpdated, Repaired, CustomerId FROM SERVICE
--END

----Delete a service from SERVICE

--CREATE PROCEDURE DeleteService

--	@ServiceNumber INT,
--	@ServiceName NVarchar(30),
--	@ServiceDescription NVarchar(150),
--	@PriceNoTax Decimal(18,2),
--	@PriceWithTax Decimal(8,2),
--	@DayServiced Datetime,
--	@DayUpdated Datetime,
--	@Repaired BIT,
--	@CustomerId INT

--AS

--BEGIN
--	SET NOCOUNT ON;
--	DELETE FROM SERVICE WHERE ServiceNumber = @ServiceNumber
--END

----Update or add new service to SERVICE table.

--CREATE PROCEDURE UpdateService

--	@ServiceNumber INT,
--	@ServiceName NVarchar(30),
--	@ServiceDescription NVarchar(150),
--	@PriceNoTax Decimal(18,2),
--	@PriceWithTax Decimal(18,2),
--	@DayServiced Datetime,
--	@DayUpdated Datetime,
--	@Repaired BIT,
--	@CustomerId INT

--AS

--BEGIN
--	SET NOCOUNT ON;

--	IF (SELECT TOP (1) 1 FROM SERVICE WHERE ServiceNumber = @ServiceNumber) IS NULL
--		INSERT INTO SERVICE(ServiceNumber, ServiceName, ServiceDescription, PriceNoTax, PriceWithTax, DayServiced, DayUpdated, Repaired, CustomerId)
--		VALUES(@ServiceNumber, @ServiceName, @ServiceDescription, @PriceNoTax, @PriceWithTax, @DayServiced, @DayUpdated, @Repaired, @CustomerId)
--	ELSE
--		UPDATE SERVICE SET ServiceName = @ServiceName, PriceNoTax = @PriceNoTax, PriceWithTax = @PriceWithTax, DayServiced = @DayServiced, DayUpdated = @DayUpdated, Repaired = @Repaired, CustomerId = @CustomerId
--		WHERE ServiceNumber = @ServiceNumber
--END

----MOCKDATA:

----Mock data Item table:

--INSERT INTO ITEM VALUES (
--'57201760042', 'Skærm', 'Skærm til iPhone 6s hvid', '400.00', '500.00', 'Skærm IP', 'iPhone 6s', '03.05.2018', '2');
--INSERT INTO ITEM VALUES (
--'68302870053', 'Skærm', 'Skærm til Samsung galaxy 6', '399.96', '499.95', 'Skærm Smartphone', 'Samsung Gs 6', '03.05.2018', '3');
--INSERT INTO ITEM VALUES (
--'79413981164', 'Iphone 6 Cover', 'Iphone 6 cover hvid læder', '119.96', '149.95', 'Cover', 'Iphone', '03.05.2018', '4');
--INSERT INTO ITEM VALUES (
--'80524092275', 'Iphone 6s Cover', 'Iphone 6s cover Brun læder flip cover', '120.00', '150.00', 'Cover', 'Iphone', '03.05.2018', '1');
--INSERT INTO ITEM VALUES (
--'91635103386', 'Samsung Gs 6 Cover', 'Samsung Galaxy 6 cover blå flip', '79.96', '99.95', 'Cover', 'Samsung', '03.05.2018', '5');
--INSERT INTO ITEM VALUES (
--'03746214497', 'Microsoft Surface 4 Cover', 'Microsoft Surface 4 Cover blå uden tastatur', '279.96', '349.99', 'Cover', 'Surface', '03.05.2018', '1');

------Mock data for customer table.

--INSERT INTO CUSTOMER VALUES (
--'Hasse', '12345678', 'hasse@mail.dk', '0', '0');
--INSERT INTO CUSTOMER VALUES (
--'Hafstein', '23456789', 'hafstein.fan@mail.dk', '1', '1');
--INSERT INTO CUSTOMER VALUES (
--'Faizan', '34567890', 'Faizan@mail.dk', '0', '1');
--INSERT INTO CUSTOMER VALUES (
--'JJ', '45678901', 'jonas@mail.dk', '1', '0');