CREATE PROC TabelIngredientsInRecipe
    @ingredientsTable NVARCHAR(50) OUTPUT
AS
BEGIN
    SET @ingredientsTable =CONCAT('IngredientsInRecipe_',(SELECT MAX(Id)+1 FROM Recipes))
    DECLARE @cmd nvarchar(1000)
    SET @cmd = 'CREATE TABLE dbo.'+@ingredientsTable+'(Id INT IDENTITY PRIMARY KEY, IngredientsId INT REFERENCES Ingredients(Id),CountIngredients NVARCHAR(30))'
    EXEC(@cmd);
END;
    