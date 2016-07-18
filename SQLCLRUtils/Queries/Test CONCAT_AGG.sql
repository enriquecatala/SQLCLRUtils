
-- Test-run 1
SELECT  Aggregates.CONCAT_AGG_Optimized(a.Val, a.x) AS Result
FROM    ( SELECT    'a' AS Val ,
                    0 x
          UNION ALL
          SELECT    'b' AS Val ,
                    1 x
          UNION ALL
          SELECT    'c' AS Val ,
                    0 x
        ) a;
GO
SELECT  Aggregates.CONCAT_AGG(a.Val) AS Result
FROM    ( SELECT    'a' AS Val ,
                    0 x
          UNION ALL
          SELECT    'b' AS Val ,
                    1 x
          UNION ALL
          SELECT    'c' AS Val ,
                    0 x
        ) a;
GO

-- Test-run 2
-- old fashion
--
SELECT  STUFF((SELECT   ';' + CONVERT(NVARCHAR(20), a.object_id)
               FROM     sys.objects a
                        CROSS JOIN sys.objects
        FOR   XML PATH('') ,
                  TYPE).value('.', 'nvarchar(max)'), 1, 1, '');
-- new fashion
--
SELECT  Aggregates.CONCAT_AGG(a.object_id)
FROM    sys.objects a
        CROSS JOIN sys.objects;
