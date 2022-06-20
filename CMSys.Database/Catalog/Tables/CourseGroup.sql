CREATE TABLE [Catalog].[CourseGroup]
(
    [Id] UNIQUEIDENTIFIER CONSTRAINT [PK_Catalog_CourseGroup] PRIMARY KEY,
    [Name] NVARCHAR(64) NOT NULL CONSTRAINT [UK_Catalog_CourseGroup_Name] UNIQUE,
    [VisualOrder] INT NOT NULL,
    [Description] NVARCHAR(256) NULL
)