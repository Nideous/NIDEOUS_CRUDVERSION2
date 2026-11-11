IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'Capistrano')
BEGIN
    CREATE DATABASE Capistrano;
END;
GO

USE Capistrano;
GO

IF OBJECT_ID('StockMovements', 'U') IS NOT NULL DROP TABLE StockMovements;
IF OBJECT_ID('PurchaseOrderLines', 'U') IS NOT NULL DROP TABLE PurchaseOrderLines;
IF OBJECT_ID('PurchaseOrders', 'U') IS NOT NULL DROP TABLE PurchaseOrders;
IF OBJECT_ID('Products', 'U') IS NOT NULL DROP TABLE Products;
IF OBJECT_ID('Suppliers', 'U') IS NOT NULL DROP TABLE Suppliers;
IF OBJECT_ID('Categories', 'U') IS NOT NULL DROP TABLE Categories;
GO

CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName VARCHAR(100) NOT NULL UNIQUE
);
GO

CREATE TABLE Suppliers (
    SupplierId INT IDENTITY(1,1) PRIMARY KEY,
    SupplierCode VARCHAR(50) NOT NULL UNIQUE,
    SupplierName VARCHAR(150) NOT NULL UNIQUE,
    ContactEmail VARCHAR(150) NULL UNIQUE,
    SupplierStatus VARCHAR(20) NOT NULL CONSTRAINT CHK_Supplier_Status CHECK (SupplierStatus IN ('Active', 'Inactive'))
);
GO

CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    SKU VARCHAR(50) NOT NULL UNIQUE,
    ProductName VARCHAR(150) NOT NULL,
    CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryId),
    SupplierId INT NOT NULL FOREIGN KEY REFERENCES Suppliers(SupplierId),
    UnitCost DECIMAL(18,2) NOT NULL CONSTRAINT CHK_Product_UnitCost CHECK (UnitCost >= 0),
    ReorderLevel INT NOT NULL CONSTRAINT CHK_Product_ReorderLevel CHECK (ReorderLevel >= 0),
    StockOnHand INT NOT NULL CONSTRAINT CHK_Product_StockOnHand CHECK (StockOnHand >= 0),
    IsActive BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE PurchaseOrders (
    PurchaseOrderId INT IDENTITY(1,1) PRIMARY KEY,
    PONumber VARCHAR(50) NOT NULL UNIQUE,
    SupplierId INT NOT NULL FOREIGN KEY REFERENCES Suppliers(SupplierId),
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    POStatus VARCHAR(20) NOT NULL CONSTRAINT CHK_PO_Status CHECK (POStatus IN ('Draft', 'Approved', 'Received', 'Cancelled')),
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0.00 CONSTRAINT CHK_PO_TotalAmount CHECK (TotalAmount >= 0)
);
GO

CREATE TABLE PurchaseOrderLines (
    POLineId INT IDENTITY(1,1) PRIMARY KEY,
    PurchaseOrderId INT NOT NULL FOREIGN KEY REFERENCES PurchaseOrders(PurchaseOrderId),
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(ProductId),
    Quantity INT NOT NULL CONSTRAINT CHK_POLine_Quantity CHECK (Quantity > 0),
    UnitCost DECIMAL(18,2) NOT NULL CONSTRAINT CHK_POLine_UnitCost CHECK (UnitCost >= 0),
    CONSTRAINT UQ_PO_Product UNIQUE (PurchaseOrderId, ProductId)
);
GO

CREATE TABLE StockMovements (
    MovementId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(ProductId),
    MovementType VARCHAR(20) NOT NULL CONSTRAINT CHK_Stock_MovementType CHECK (MovementType IN ('IN', 'OUT', 'ADJUSTMENT')),
    Quantity INT NOT NULL CONSTRAINT CHK_Stock_Quantity CHECK (Quantity > 0),
    ReferenceNo VARCHAR(50) NULL,
    MovementDate DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('usp_Product_CRUD', 'P') IS NOT NULL DROP PROCEDURE usp_Product_CRUD;
IF OBJECT_ID('usp_Supplier_CRUD', 'P') IS NOT NULL DROP PROCEDURE usp_Supplier_CRUD;
IF OBJECT_ID('usp_PurchaseOrder_CreateDraft', 'P') IS NOT NULL DROP PROCEDURE usp_PurchaseOrder_CreateDraft;
IF OBJECT_ID('usp_PurchaseOrder_AddLine', 'P') IS NOT NULL DROP PROCEDURE usp_PurchaseOrder_AddLine;
IF OBJECT_ID('usp_PurchaseOrder_Receive', 'P') IS NOT NULL DROP PROCEDURE usp_PurchaseOrder_Receive;
IF OBJECT_ID('usp_Inventory_LowStockReport', 'P') IS NOT NULL DROP PROCEDURE usp_Inventory_LowStockReport;
GO

CREATE PROCEDURE usp_Product_CRUD
    @Action VARCHAR(10),
    @ProductId INT = NULL,
    @SKU VARCHAR(50) = NULL,
    @ProductName VARCHAR(150) = NULL,
    @CategoryId INT = NULL,
    @SupplierId INT = NULL,
    @UnitCost DECIMAL(18,2) = NULL,
    @ReorderLevel INT = NULL,
    @StockOnHand INT = 0,
    @IsActive BIT = 1,
    @SearchKeyword VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Action = 'CREATE'
    BEGIN
        INSERT INTO Products (SKU, ProductName, CategoryId, SupplierId, UnitCost, ReorderLevel, StockOnHand, IsActive)
        VALUES (@SKU, @ProductName, @CategoryId, @SupplierId, @UnitCost, @ReorderLevel, @StockOnHand, @IsActive);
        
        SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewProductId;
    END
    ELSE IF @Action = 'READ'
    BEGIN
        SELECT * FROM Products WHERE ProductId = @ProductId;
    END
    ELSE IF @Action = 'UPDATE'
    BEGIN
        UPDATE Products
        SET SKU = ISNULL(@SKU, SKU),
            ProductName = ISNULL(@ProductName, ProductName),
            CategoryId = ISNULL(@CategoryId, CategoryId),
            SupplierId = ISNULL(@SupplierId, SupplierId),
            UnitCost = ISNULL(@UnitCost, UnitCost),
            ReorderLevel = ISNULL(@ReorderLevel, ReorderLevel),
            StockOnHand = ISNULL(@StockOnHand, StockOnHand),
            IsActive = ISNULL(@IsActive, IsActive)
        WHERE ProductId = @ProductId;
    END
    ELSE IF @Action = 'DELETE'
    BEGIN
        DELETE FROM Products WHERE ProductId = @ProductId;
    END
    ELSE IF @Action = 'SEARCH'
    BEGIN
        SELECT p.*, c.CategoryName, s.SupplierName 
        FROM Products p
        JOIN Categories c ON p.CategoryId = c.CategoryId
        JOIN Suppliers s ON p.SupplierId = s.SupplierId
        WHERE (@SearchKeyword IS NULL 
            OR p.ProductName LIKE '%' + @SearchKeyword + '%' 
            OR p.SKU LIKE '%' + @SearchKeyword + '%');
    END
END;
GO

CREATE PROCEDURE usp_Supplier_CRUD
    @Action VARCHAR(10),
    @SupplierId INT = NULL,
    @SupplierCode VARCHAR(50) = NULL,
    @SupplierName VARCHAR(150) = NULL,
    @ContactEmail VARCHAR(150) = NULL,
    @SupplierStatus VARCHAR(20) = 'Active'
AS
BEGIN
    SET NOCOUNT ON;

    IF @Action = 'CREATE'
    BEGIN
        INSERT INTO Suppliers (SupplierCode, SupplierName, ContactEmail, SupplierStatus)
        VALUES (@SupplierCode, @SupplierName, @ContactEmail, @SupplierStatus);
    END
    ELSE IF @Action = 'READ'
    BEGIN
        SELECT * FROM Suppliers WHERE SupplierId = @SupplierId;
    END
    ELSE IF @Action = 'UPDATE'
    BEGIN
        UPDATE Suppliers
        SET SupplierCode = ISNULL(@SupplierCode, SupplierCode),
            SupplierName = ISNULL(@SupplierName, SupplierName),
            ContactEmail = ISNULL(@ContactEmail, ContactEmail),
            SupplierStatus = ISNULL(@SupplierStatus, SupplierStatus)
        WHERE SupplierId = @SupplierId;
    END
    ELSE IF @Action = 'DELETE'
    BEGIN
        DELETE FROM Suppliers WHERE SupplierId = @SupplierId;
    END
    ELSE IF @Action = 'GETALL'
    BEGIN
        SELECT * FROM Suppliers;
    END
END;
GO

CREATE PROCEDURE usp_PurchaseOrder_CreateDraft
    @PONumber VARCHAR(50),
    @SupplierId INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 
        FROM Suppliers 
        WHERE SupplierId = @SupplierId AND SupplierStatus = 'Active'
    )
    BEGIN
        RAISERROR('Cannot create purchase order. Supplier does not exist or is inactive.', 16, 1);
        RETURN;
    END

    INSERT INTO PurchaseOrders (PONumber, SupplierId, OrderDate, POStatus, TotalAmount)
    VALUES (@PONumber, @SupplierId, GETDATE(), 'Draft', 0.00);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS PurchaseOrderId;
END;
GO

CREATE PROCEDURE usp_PurchaseOrder_AddLine
    @PurchaseOrderId INT,
    @ProductId INT,
    @Quantity INT,
    @UnitCost DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM PurchaseOrders WHERE PurchaseOrderId = @PurchaseOrderId AND POStatus = 'Draft')
        BEGIN
            RAISERROR('Cannot add lines to a purchase order that is not in Draft status.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM PurchaseOrderLines WHERE PurchaseOrderId = @PurchaseOrderId AND ProductId = @ProductId)
        BEGIN
            RAISERROR('Duplicate product detected. This product is already added to the Purchase Order.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        INSERT INTO PurchaseOrderLines (PurchaseOrderId, ProductId, Quantity, UnitCost)
        VALUES (@PurchaseOrderId, @ProductId, @Quantity, @UnitCost);

        UPDATE PurchaseOrders
        SET TotalAmount = (
            SELECT SUM(Quantity * UnitCost) 
            FROM PurchaseOrderLines 
            WHERE PurchaseOrderId = @PurchaseOrderId
        )
        WHERE PurchaseOrderId = @PurchaseOrderId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE usp_PurchaseOrder_Receive
    @PurchaseOrderId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @CurrentStatus VARCHAR(20);
        DECLARE @PONumber VARCHAR(50);

        SELECT @CurrentStatus = POStatus, @PONumber = PONumber 
        FROM PurchaseOrders 
        WHERE PurchaseOrderId = @PurchaseOrderId;

        IF @CurrentStatus = 'Received'
        BEGIN
            RAISERROR('Purchase Order has already been received.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF @CurrentStatus = 'Cancelled'
        BEGIN
            RAISERROR('Cannot receive a cancelled Purchase Order.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        UPDATE p
        SET p.StockOnHand = p.StockOnHand + pol.Quantity
        FROM Products p
        INNER JOIN PurchaseOrderLines pol ON p.ProductId = pol.ProductId
        WHERE pol.PurchaseOrderId = @PurchaseOrderId;

        INSERT INTO StockMovements (ProductId, MovementType, Quantity, ReferenceNo, MovementDate)
        SELECT ProductId, 'IN', Quantity, @PONumber, GETDATE()
        FROM PurchaseOrderLines
        WHERE PurchaseOrderId = @PurchaseOrderId;

        UPDATE PurchaseOrders
        SET POStatus = 'Received'
        WHERE PurchaseOrderId = @PurchaseOrderId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE usp_Inventory_LowStockReport
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.ProductId,
        p.SKU,
        p.ProductName,
        c.CategoryName,
        s.SupplierName,
        p.StockOnHand,
        p.ReorderLevel,
        (p.ReorderLevel - p.StockOnHand) AS SuggestedReorderQty
    FROM Products p
    JOIN Categories c ON p.CategoryId = c.CategoryId
    JOIN Suppliers s ON p.SupplierId = s.SupplierId
    WHERE p.StockOnHand <= p.ReorderLevel
      AND p.IsActive = 1;
END;
GO

INSERT INTO Categories (CategoryName) VALUES ('Electronics');
INSERT INTO Suppliers (SupplierCode, SupplierName, ContactEmail, SupplierStatus) 
VALUES ('SUP01', 'TechCorp', 'contact@techcorp.com', 'Active');

EXEC usp_Product_CRUD 
    @Action = 'CREATE', 
    @SKU = 'PROD-MOUSE', 
    @ProductName = 'Wireless Mouse', 
    @CategoryId = 1, 
    @SupplierId = 1, 
    @UnitCost = 15.00, 
    @ReorderLevel = 10, 
    @StockOnHand = 5;

EXEC usp_PurchaseOrder_CreateDraft @PONumber = 'PO-1001', @SupplierId = 1;

EXEC usp_PurchaseOrder_AddLine @PurchaseOrderId = 1, @ProductId = 1, @Quantity = 20, @UnitCost = 15.00;

BEGIN TRY
    EXEC usp_PurchaseOrder_AddLine @PurchaseOrderId = 1, @ProductId = 1, @Quantity = 5, @UnitCost = 15.00;
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS ErrorMessage;
END CATCH;

EXEC usp_PurchaseOrder_Receive @PurchaseOrderId = 1;

SELECT ProductId, SKU, StockOnHand FROM Products WHERE ProductId = 1;

BEGIN TRY
    EXEC usp_PurchaseOrder_Receive @PurchaseOrderId = 1;
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS ErrorMessage;
END CATCH;

SELECT ProductId, SKU, StockOnHand FROM Products WHERE ProductId = 1;

SELECT * FROM StockMovements WHERE ReferenceNo = 'PO-1001';

EXEC usp_Supplier_CRUD @Action = 'GETALL';