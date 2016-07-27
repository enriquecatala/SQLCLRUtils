
-- Please, DO NOT USE THIS...IT´S ONLY TO EDUCATIONAL PURPOSES
-- BAD PERFORMANCE CODE
--
CREATE FUNCTION dbo.OldSplitString
    (
      @String NVARCHAR(MAX) ,
      @Delim NCHAR(1)
    )
RETURNS @Values TABLE ( [Value] NVARCHAR(MAX) )
AS
    BEGIN
        DECLARE @Pos INT;
        SET @Pos = CHARINDEX(@Delim, @String);
        WHILE @Pos > 0
            BEGIN
                INSERT  INTO @Values
                        ( [Value]
                        )
                        SELECT  LTRIM(RTRIM(LEFT(@String, @Pos - 1)));
                SET @String = SUBSTRING(@String, @Pos + 1, LEN(@String));
                SET @Pos = CHARINDEX(@Delim, @String);
            END;
        IF LEN(LTRIM(RTRIM(@String))) > 0
            INSERT  INTO @Values
                    ( [Value] )
                    SELECT  @String;
        RETURN;
    END;