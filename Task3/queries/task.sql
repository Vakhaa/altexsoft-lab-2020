	SELECT Recipes.Name As Recipe , Ingredients.Name As Ingredient, CountIngredient FROM	
		Recipes INNER JOIN Categories AS c ON Recipes.CategoryId = c.Id
		INNER JOIN Categories AS c1 ON c.ParentId = c1.Id,
		Ingredients INNER JOIN IngredientsInRecipe AS IR1 ON IR1.IngredientId = Ingredients.Id
		WHERE IR1.RecipeId =Recipes.Id AND c.ParentId = 3
		GROUP BY Recipes.Name , Ingredients.Name , CountIngredient;