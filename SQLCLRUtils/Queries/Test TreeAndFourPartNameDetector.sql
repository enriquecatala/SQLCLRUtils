select * 
from (values('select * from bbdd.dbo.table'),('select * from dbo.table'),('select * from server.bbdd.dbo.table')) x(txt)
cross apply dbo.TreeAndFourPartNameDetector(x.txt) result
where result.[Group] <> '0'