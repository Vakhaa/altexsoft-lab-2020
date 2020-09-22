CREATE TABLE [dbo].[Ingredients] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (30) UNIQUE NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);