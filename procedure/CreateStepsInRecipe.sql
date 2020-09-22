CREATE PROC TabelStepsInRecipe
    @stepsTable NVARCHAR(50) OUTPUT
AS
BEGIN
    SET @stepsTable =CONCAT('StepsInRecipe_',(SELECT IIF( MAX(Id)+1 >1, MAX(Id)+1, 1) FROM Recipes))
    DECLARE @cmd nvarchar(1000)
    SET @cmd = 'CREATE TABLE dbo.'+@stepsTable+'(Id INT IDENTITY PRIMARY KEY, Description NVARCHAR(500))'
    EXEC(@cmd);
END;