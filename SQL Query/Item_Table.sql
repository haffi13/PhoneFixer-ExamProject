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
--CustomerPhone	INT											NOT NULL,
--Email			NVarchar(50)								NOT NULL,
--Subscribed		Bit											NOT NULL	DEFAULT '0',
--AmountOfSales	INT											NOT NULL,
--AmountSpent		Decimal(18,2)								NOT NULL,
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

--	@barcode NVarchar(15),
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

----MOCKDATA:

----Mock data Item table:

--INSERT INTO ITEM VALUES (
--'57201760042', 'Sk�rm', 'Sk�rm til iPhone 6s hvid', '400.00', '500.00', 'Sk�rm IP', 'iPhone 6s', '03.05.2018', '2');
--INSERT INTO ITEM VALUES (
--'68302870053', 'Sk�rm', 'Sk�rm til Samsung galaxy 6', '399.96', '499.95', 'Sk�rm Smartphone', 'Samsung Gs 6', '03.05.2018', '3');
--INSERT INTO ITEM VALUES (
--'79413981164', 'Iphone 6 Cover', 'Iphone 6 cover hvid l�der', '119.96', '149.95', 'Cover', 'Iphone', '03.05.2018', '4');
--INSERT INTO ITEM VALUES (
--'80524092275', 'Iphone 6s Cover', 'Iphone 6s cover Brun l�der flip cover', '120.00', '150.00', 'Cover', 'Iphone', '03.05.2018', '1');
--INSERT INTO ITEM VALUES (
--'91635103386', 'Samsung Gs 6 Cover', 'Samsung Galaxy 6 cover bl� flip', '79.96', '99.95', 'Cover', 'Samsung', '03.05.2018', '5');
--INSERT INTO ITEM VALUES (
--'03746214497', 'Microsoft Surface 4 Cover', 'Microsoft Surface 4 Cover bl� uden tastatur', '279.96', '349.99', 'Cover', 'Surface', '03.05.2018', '1');

------Mock data for customer table.

--INSERT INTO CUSTOMER VALUES (
--'Hasse', '12345678', 'hasse@mail.dk', '0', '4', '10000');
--INSERT INTO CUSTOMER VALUES (
--'Hafstein', '23456789', 'hafstein.fan@mail.dk', '1', '1', '149.95');