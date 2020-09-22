DECLARE @number INT,@cmd NVARCHAR(1500), @IngredientsInRecipe NVARCHAR(150)
SET @number=0;
WHILE @number < 4
	BEGIN
		SET @IngredientsInRecipe =CONVERT(NVARCHAR,
		(SELECT Recipes.Ingredients FROM  Recipes 
		INNER JOIN Subcategories ON Recipes.SubcategoryId = Subcategories.Id
		WHERE CategoryId = 3
		Order by Recipes.Ingredients
		OFFSET @number ROWS
		FETCH FIRST 1 ROWS ONLY)
);
		
		SET @cmd = 'SELECT Recipes.Name, Ingredients.Name, CountIngredients FROM  Recipes 
		INNER JOIN Subcategories ON Recipes.SubcategoryId = Subcategories.Id, Ingredients
		INNER JOIN '+@IngredientsInRecipe+' ON IngredientsId = Ingredients.Id
		WHERE Recipes.Ingredients = '''+@IngredientsInRecipe+'''AND Subcategories.CategoryId = 3
		GROUP BY Recipes.Name, Ingredients.Name, CountIngredients;'
		EXEC(@cmd)
		SET @number = @number + 1
	END;