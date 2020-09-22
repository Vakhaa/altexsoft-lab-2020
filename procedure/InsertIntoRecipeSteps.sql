CREATE PROC InsertStepsInRecipe 
@RecipeId INT,
@Description NVARCHAR(1000)
AS
BEGIN
DECLARE @nameTablee NVARCHAR(100), @cmd NVARCHAR(1000)
SET @nameTablee =(SELECT StepsHowCooking FROM Recipes WHERE Id=@RecipeId)
SET @cmd = N'INSERT INTO '+ @nameTablee +N' VALUES (N'''+@Description+''')'
EXEC(@cmd);
END;