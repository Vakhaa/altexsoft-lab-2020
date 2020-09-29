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
),
FirthLayerCategories (Id, InheritorId, Name)AS
(
    SELECT c1.Id AS Id, c.Id AS InheritorId, c1.Name AS Name FROM CTE_Categories AS c INNER JOIN CTE_Categories AS c1 ON c.ParentId = c1.Id
    WHERE c.LevelLayer<2
)

SELECT Recipes.Name As Recipe , Ingredients.Name As Ingredient, CountIngredient FROM	
		Recipes INNER JOIN FirthLayerCategories AS c ON Recipes.CategoryId = c.InheritorId,
		Ingredients INNER JOIN IngredientsInRecipe AS IR1 ON IR1.IngredientId = Ingredients.Id
		WHERE IR1.RecipeId =Recipes.Id AND c.Id = 3
		GROUP BY Recipes.Name , Ingredients.Name , CountIngredient;

