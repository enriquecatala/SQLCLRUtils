USE Training
GO
SET STATISTICS TIME ON
/*
SELECT *
FROM dbo.fn_Nums(200000)
*/
PRINT 'EsNumeroParClr *********************************************************'
SELECT COUNT(*)
FROM dbo.fn_Nums(200000) 
WHERE dbo.EsNumeroParClr(n) = 1

PRINT 'EsNumeroParClrDataAccess *********************************************************'
SELECT COUNT(*)
FROM dbo.fn_Nums(200000) 
WHERE dbo.EsNumeroParClrDataAccess(n) = 1

PRINT 'EsNumeroPar *********************************************************'
SELECT COUNT(*)
FROM dbo.fn_Nums(200000) 
WHERE dbo.EsNumeroPar(n) = 1

PRINT 'EsNumeroParInline *********************************************************'
SELECT COUNT(*)
FROM dbo.fn_Nums(200000) f
	CROSS APPLY dbo.EsNumeroParInline(n) e
WHERE e.Result= 1