/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
-- Solo si no es Azure
IF(EXISTS ( SELECT 1 FROM (SELECT SERVERPROPERTY('Edition') AS v) x WHERE v <> 'SQL Azure'))
begin
EXEC sp_configure 'clr enabled',1;
RECONFIGURE WITH OVERRIDE;
end

if object_id('aggregates.CONCAT_AGG') is NOT NULL BEGIN
	drop aggregate Aggregates.CONCAT_AGG
END

if object_id('aggregates.CONCAT_AGG_Optimized') is not NULL BEGIN
	DROP AGGREGATE aggregates.CONCAT_AGG_Optimized
END

IF EXISTS ( SELECT  *
            FROM    sys.assemblies
            WHERE   name = 'CustomFunctions' )
   BEGIN
         DROP FUNCTION dbo.BuscarTexto
        -- DROP FUNCTION dbo.BuscarTextoCI
         DROP ASSEMBLY CustomFunctions
   END