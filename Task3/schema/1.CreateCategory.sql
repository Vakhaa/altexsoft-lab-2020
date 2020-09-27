CREATE TABLE [dbo].[Categories] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (30) NOT NULL,
    [ParentId] INT NULL REFERENCES Categories(Id),
    PRIMARY KEY CLUSTERED ([Id] ASC)
);