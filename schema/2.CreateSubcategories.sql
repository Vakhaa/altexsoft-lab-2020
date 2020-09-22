CREATE TABLE [dbo].[Subcategories] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (30) NOT NULL,
    [CategoryId] INT           NOT NULL  REFERENCES [dbo].[Categories] ([Id]),
    PRIMARY KEY CLUSTERED ([Id] ASC), 
); 