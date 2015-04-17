
SELECT *
FROM sys.databases
WHERE dbo.RegExIsMatch(name,'db') = 1
GO

SELECT name, dbo.RegExReplace(name,'db','XXXXXXXXXXXX')
FROM sys.databases
go