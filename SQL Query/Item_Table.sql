----Table creations:

--CREATE TABLE ITEM (
--Barcode				NVarchar(15)		PRIMARY KEY		NOT NULL,
--Name				NVarchar(50)						NOT NULL,
--Description			NVarchar(150)						NULL,
--Price				Decimal(18,2)						NOT NULL,
--Category			NVarchar(20)						NOT NULL,
--Model				NVarchar(30)						NOT NULL,
--Number_Available	INT									NOT NULL,
--);


----Procedures:


--CREATE PROCEDURE GetAllItems
--AS
--BEGIN
--		SELECT Barcode, Name, Description, Price, Category, Model, Number_Available FROM ITEM
--END

--CREATE PROCEDURE AddItem
--				(@Barcode NVarchar(15), @Name NVarchar(50), @Description NVarchar(150), @Price Decimal(18,2), @Category NVarchar(20), @Model NVarchar(30), @Number_Available INT)
--AS
--BEGIN
--		INSERT INTO ITEM
--		VALUES	(@Barcode, @Name, @Description, @Price, @Category, @Model, @Number_Available)
--END

----MOCKDATA:

--Mock data Item table:

--INSERT INTO ITEM VALUES (
--'57201760042', 'Skærm', 'Skærm til iPhone 6s hvid', '500.00', 'Skærm IP', 'iPhone 6s', '2');
--INSERT INTO ITEM VALUES (
--'68302870053', 'Skærm', 'Skærm til Samsung galaxy 6', '499.95', 'Skærm Smartphone', 'Samsung Gs 6', '3');
--INSERT INTO ITEM VALUES (
--'79413981164', 'Iphone 6 Cover', 'Iphone 6 cover hvid læder', '149.95', 'Cover', 'Iphone', '4');
--INSERT INTO ITEM VALUES (
--'80524092275', 'Iphone 6s Cover', 'Iphone 6s cover Brun læder flip cover', '150.00', 'Cover', 'Iphone', '1');
--INSERT INTO ITEM VALUES (
--'91635103386', 'Samsung Gs 6 Cover', 'Samsung Galaxy 6 cover blå flip', '99.95', 'Cover', 'Samsung', '5');
--INSERT INTO ITEM VALUES (
--'02746214497', 'Microsoft Surface 4 Cover', 'Microsoft Surface 4 Cover blå uden tastatur', '349.99', 'Cover', 'Surface', '1');