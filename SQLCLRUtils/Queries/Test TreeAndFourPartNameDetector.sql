SELECT *
FROM
(
    VALUES
        ('select * from bbdd.dbo.table'),
        ('select * from rf.UNOx.dbo.MerchTerminalDetail'),
        ('select * from dbo.table'),
        ('select * from server.bbdd.dbo.table'),
		('select * from server2.bbdd2.dbo.table2')
) x (txt)
    CROSS APPLY dbo.TreeAndFourPartNameDetector(x.txt) result
WHERE result.[Group] <> '0';