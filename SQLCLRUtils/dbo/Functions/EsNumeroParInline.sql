CREATE FUNCTION [dbo].[EsNumeroParInline]
(
	@num int
)
RETURNS table
AS
RETURN (
	SELECT CASE WHEN @num %2 = 0 THEN 1 ELSE 0 END AS result
)
