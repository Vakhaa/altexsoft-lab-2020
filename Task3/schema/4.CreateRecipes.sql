CREATE TABLE [dbo].[Recipes] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (30)  UNIQUE NOT NULL,
    [Description]     NVARCHAR (300) DEFAULT ('Description is empty') NULL,
    [StepsHowCooking] NVARCHAR (25)  NOT NULL,
    [SubcategoryId]   INT            NOT NULL,
    [Ingredients]     NVARCHAR (25)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([SubcategoryId]) REFERENCES [dbo].[Subcategories] ([Id])
);