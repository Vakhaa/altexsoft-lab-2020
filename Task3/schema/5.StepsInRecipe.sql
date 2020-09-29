CREATE TABLE [dbo].[StepsInRecipe] (
    Id INT IDENTITY PRIMARY KEY,
    [RecipeId]   INT   NOT NULL REFERENCES Recipes (Id),    
    [Description] NVARCHAR (500) NOT NULL
);