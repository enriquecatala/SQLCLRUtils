/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/*
 WORKAROUND PARA DESPLEGAR EL ESQUEMA CUSTOM QUE QUERAMOS
*/
if object_id('dbo.CONCAT_AGG') is not NULL begin
    alter schema Aggregates transfer dbo.CONCAT_AGG
END

if object_id('dbo.CONCAT_AGG_Optimized') is not NULL begin
    alter schema Aggregates transfer dbo.CONCAT_AGG_Optimized
END
