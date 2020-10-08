WITH CTE_Categories(Id, ParentId, Name, LevelLayer) AS   
(  
    SELECT Id, ParentId, Name, 0 AS LevelLayer  
    FROM Categories
		WHERE ParentId =3
    UNION ALL  
    SELECT c1.Id, c1.ParentId, c1.Name, LevelLayer + 1  
    FROM Categories AS c1  
        INNER JOIN CTE_Categories AS c2  
        ON c1.ParentId = c2.Id   
)

SELECT Recipe,I.Name As Ingredient, CountIngredient , Iterator FROM 
(
        SELECT  R.Id AS Id, R.Name As Recipe ,ROW_NUMBER() OVER(ORDER BY R.Name ) AS Iterator FROM
        Recipes as R INNER JOIN CTE_Categories AS c1 ON R.CategoryId = c1.Id
) 
AS t INNER JOIN IngredientsInRecipe AS IR1 ON IR1.RecipeId = t.Id 
INNER JOIN Ingredients as I ON IR1.IngredientId = I.Id
WHERE Iterator<=3