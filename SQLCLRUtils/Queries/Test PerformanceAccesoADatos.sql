
SET STATISTICS TIME Off
SET STATISTICS IO OFF

PRINT 'EJECUCION T-SQL *******************************************************************************'
SET STATISTICS TIME ON
SET STATISTICS IO ON
EXEC dbo.MyStoredProcedure @producto = 'CORSA WIND 1.0 2P BF9CE862'
GO
SET STATISTICS TIME Off
SET STATISTICS IO OFF
PRINT 'EJECUCION CLR *******************************************************************************'
SET STATISTICS TIME ON
SET STATISTICS IO ON
EXEC dbo.MyStoredProcedureCLR @producto = 'CORSA WIND 1.0 2P BF9CE862'
GO
-- AHORA SQLSTRESS PARA TESTEAR PERFORMANCE DE SPs