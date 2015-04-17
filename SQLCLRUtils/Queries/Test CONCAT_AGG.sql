
-- Test-run 1
SELECT Aggregates.CONCAT_AGG(a.Val) AS Result
FROM (SELECT 'a' AS Val UNION ALL
      SELECT 'b' AS Val UNION ALL
	  SELECT 'c' AS Val) a
go
SELECT Aggregates.CONCAT_AGG_Optimized(a.Val,a.x) AS Result
FROM (SELECT 'a' AS Val,0 x UNION ALL
      SELECT 'b' AS Val,1 x UNION ALL
	  SELECT 'c' AS Val,0 x) a


