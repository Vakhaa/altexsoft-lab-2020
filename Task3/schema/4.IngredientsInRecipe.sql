CREATE TABLE [dbo].[IngredientsInRecipe] (
    [RecipeId]   INT   NOT NULL REFERENCES Recipes (Id),
    [IngredientId] INT NOT NULL REFERENCES Ingredients (Id),
    [CountIngredient] NVARCHAR (30) NOT NULL DEFAULT N'по вкусу',
    PRIMARY KEY ([RecipeId],[IngredientId])
);