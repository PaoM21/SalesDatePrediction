--Sales Date Prediction
WITH CTE0 AS (
    SELECT MAX(orderdate) AS LastOrderDate, custid, orderdate
    FROM Sales.Orders
    GROUP BY custid, orderdate
), 
CTE1 AS (
    SELECT
        custid,
        LastOrderDate,
        LAG(orderdate) OVER (PARTITION BY custid ORDER BY orderdate) AS FechaAnterior,
        ROW_NUMBER() OVER (PARTITION BY custid ORDER BY LastOrderDate DESC) AS RN
    FROM CTE0
), 
CTE2 AS (
    SELECT
        c.custid,
        AVG(DATEDIFF(DAY, c.FechaAnterior, c.LastOrderDate)) AS PromedioDias
    FROM CTE1 c
    WHERE c.FechaAnterior IS NOT NULL
    GROUP BY c.custid
)
SELECT 
    Cu.companyname,
    c1.LastOrderDate,
    DATEADD(DAY, c2.PromedioDias, c1.LastOrderDate) AS NextPredictedOrder
FROM CTE1 c1
JOIN CTE2 c2 ON c1.custid = c2.custid
JOIN Sales.Customers AS Cu ON c1.custid = Cu.custid
WHERE c1.RN = 1
ORDER BY companyname;

--Get Client Orders
SELECT [orderid]
      ,[requireddate]
      ,[shippeddate]
      ,[shipname]
      ,[shipaddress]
      ,[shipcity]
FROM [StoreSample].[Sales].[Orders]

--Get employees
SELECT [empid]
	   ,CONCAT([firstname], [lastname]) AS FullName
FROM [StoreSample].[HR].[Employees]

--Get Shippers
SELECT [shipperid]
      ,[companyname]
FROM [StoreSample].[Sales].[Shippers]

--Get Products
SELECT [productid]
      ,[productname]
FROM [StoreSample].[Production].[Products]

--Add New Order
CREATE ALTER PROCEDURE AddNewOrder
    @Empid INT,
    @Shipperid INT,
    @Shipname NVARCHAR(100),
    @Shipaddress NVARCHAR(255),
    @Shipcity NVARCHAR(100),
    @Orderdate DATETIME,
    @Requireddate DATETIME,
    @Shippeddate DATETIME,
    @Freight DECIMAL(18, 2),
    @Shipcountry NVARCHAR(100),
    @Productid INT,
    @Unitprice DECIMAL(18, 2),
    @Qty INT,
    @Discount DECIMAL(5, 2)
AS
BEGIN
    DECLARE @Orderid INT;
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO [Sales].[Orders] ([empid], [shipperid], [shipname], [shipaddress], [shipcity], [orderdate], [requireddate], [shippeddate], [freight], [shipcountry])
        VALUES (@Empid, @Shipperid, @Shipname, @Shipaddress, @Shipcity, @Orderdate, @Requireddate, @Shippeddate, @Freight, @Shipcountry);

        SET @Orderid = SCOPE_IDENTITY();

		INSERT INTO [Sales].[OrderDetails] ([orderid], [productid], [unitprice], [qty], [discount])
		VALUES (@Orderid, @Productid, @Unitprice, @Qty, @Discount)

        COMMIT TRANSACTION;

        SELECT @Orderid AS OrderId;

    END TRY
    BEGIN CATCH
        -- Revertir la transacción en caso de error
        ROLLBACK TRANSACTION;

        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
