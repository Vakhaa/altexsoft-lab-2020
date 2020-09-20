CREATE PROC InsertIntoRecipe
    @nameRecipe NVARCHAR(50),
    @Description NVARCHAR (500),
    @SubcategoryId INT
AS
BEGIN
    DECLARE @stepsTable NVARCHAR(50), @ingredientTable NVARCHAR(50)
    EXEC TabelStepsInRecipe @stepsTable OUTPUT;
    EXEC TabelIngredientsInRecipe @ingredientTable OUTPUT;
    INSERT INTO Recipes VALUES
    (@nameRecipe, @Description,CONVERT(NVARCHAR,@stepsTable), @SubcategoryId ,CONVERT(NVARCHAR,@ingredientTable))
END;