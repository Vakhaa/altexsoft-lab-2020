WITH CTE_Categories(Id, ParentId, Name, LevelLayer) AS   
(  
    SELECT Id, ParentId, Name, 0 AS LevelLayer  
    FROM Categories
		WHERE ParentId IS NULL  
    UNION ALL  
    SELECT c1.Id, c1.ParentId, c1.Name, LevelLayer + 1  
    FROM Categories AS c1  
        INNER JOIN CTE_Categories AS c2  
        ON c1.ParentId = c2.Id   
)

SELECT Recipe, Ingredients.Name As Ingredient, CountIngredient , Iterator FROM (
        SELECT Recipes.Id AS Id, Recipes.Name As Recipe , ROW_NUMBER() OVER(ORDER BY Recipes.Name ) AS Iterator FROM	
		Recipes INNER JOIN CTE_Categories AS c1 ON Recipes.CategoryId = c1.Id
		WHERE c1.ParentId = 3
) AS t ,Ingredients INNER JOIN IngredientsInRecipe AS IR1 ON IR1.IngredientId = Ingredients.Id
WHERE Iterator<=3 AND IR1.RecipeId = t.Id