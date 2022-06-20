CREATE TABLE [Catalog].[CourseType]
(
    [Id] UNIQUEIDENTIFIER CONSTRAINT [PK_Catalog_CourseType] PRIMARY KEY,
    [Name] NVARCHAR(64) NOT NULL CONSTRAINT [UK_Catalog_CourseType_Name] UNIQUE,
    [VisualOrder] INT NOT NULL,
    [Description] NVARCHAR(256) NULL
)