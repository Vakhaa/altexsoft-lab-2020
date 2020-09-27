CREATE TABLE [dbo].[StepsInRecipe] (
    [RecipeId]   INT   NOT NULL REFERENCES Recipes (Id),    
    [Description] NVARCHAR (500) NOT NULL
);