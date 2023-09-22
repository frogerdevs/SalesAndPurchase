-- Store procedure dengan parameter tanggal
CREATE PROCEDURE GetTransactionByDate
    @StartDate DATE,
    @EndDate DATE,
    @ProductId NVARCHAR(255)
AS
BEGIN

    WITH PurchaseSummary AS (
    SELECT CAST( p.PurchaseDate as Date) as Tanggal, p.SkuCode as NoTransaksi
    ,pd.Quantity AS QtyMasuk, pd.Price  AS HargaMasuk, 0 AS QtyKeluar, 0 AS HargaKeluar, pd.Quantity  AS QtySaldo
    FROM sample.Purchase p left join sample.PurchaseDetail pd on p.Id =pd.PurchaseId
    WHERE CAST( p.PurchaseDate as Date)  BETWEEN @StartDate AND @EndDate AND pd.ProductId  = @ProductId

    UNION ALL

    SELECT CAST( s.SalesDate as Date) as Tanggal, s.SkuCode as NoTransaksi
    , 0 AS QtyMasuk, 0 AS HargaMasuk, sd.Quantity AS QtyKeluar, sd.Price AS HargaKeluar, -sd.Quantity AS QtySaldo
    FROM SalesAndPurchaseDB.sample.Sell s left join sample.SellDetail sd on s.Id = sd.SellId 
    WHERE CAST( s.SalesDate as Date)  BETWEEN @StartDate AND @EndDate AND sd.ProductId  = @ProductId
    )
    select * into #tempdata from PurchaseSummary

    select DISTINCT tanggal into #temptanggal
    from #tempdata 

    select * from #tempdata

    SELECT tmp.tanggal, SUM(ps.QtyMasuk) as QtyMasuk, SUM(ps.HargaMasuk) as HargaMasuk
    , SUM(ps.QtyKeluar) as QtyKeluar , SUM(ps.HargaKeluar) as HargaKeluar , SUM(ps.QtySaldo) as QtySaldo
    from #temptanggal tmp left join #tempdata ps on ps.tanggal = tmp.tanggal 
    GROUP BY tmp.tanggal
    ORDER BY tmp.tanggal

    drop table #temptanggal
    drop table #tempdata

END
