CREATE TABLE [Catalog].[TrainerGroup]
(
    [Id] UNIQUEIDENTIFIER CONSTRAINT [PK_Catalog_TrainerGroup] PRIMARY KEY,
    [Name] NVARCHAR(64) NOT NULL CONSTRAINT [UK_Catalog_TrainerGroup_Name] UNIQUE,
    [VisualOrder] INT NOT NULL,
    [Description] NVARCHAR(256) NULL
)