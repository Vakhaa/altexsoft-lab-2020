CREATE TABLE [dbo].[Recipes] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (30)  UNIQUE NOT NULL,
    [Description]     NVARCHAR (300) DEFAULT ('Description is empty') NULL,
    [CategoryId]   INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id])
);