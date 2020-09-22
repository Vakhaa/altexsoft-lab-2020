CREATE PROC InsertIngredientsInRecipe 
@RecipeId INT,
@IngredientId INT,
@CountIngredient NVARCHAR(30)
AS
BEGIN
DECLARE @nameTablee NVARCHAR(100), @cmd NVARCHAR(1000)
SET @nameTablee =(SELECT Ingredients FROM Recipes WHERE Id=@RecipeId)
SET @cmd = N'INSERT INTO '+ @nameTablee +N' VALUES (
		(SELECT Id FROM Ingredients WHERE Id='+CONVERT(NVARCHAR,@IngredientId)+'),
		N'''+@CountIngredient+''')'
EXEC(@cmd);
END;