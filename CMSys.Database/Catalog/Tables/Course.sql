CREATE TABLE [Catalog].[Course]
(
    [Id] UNIQUEIDENTIFIER CONSTRAINT [PK_Catalog_Course] PRIMARY KEY,
    [Name] NVARCHAR(64) NOT NULL,
    [VisualOrder] INT NOT NULL,
    [IsNew] BIT NOT NULL,
    [CourseTypeId] UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT [FK_Catalog_Course_Catalog_CourseType] FOREIGN KEY
        REFERENCES [Catalog].[CourseType] ([Id]),
    [CourseGroupId] UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT [FK_Catalog_Course_Catalog_CourseGroup] FOREIGN KEY
        REFERENCES [Catalog].[CourseGroup] ([Id]),
    [Description] NVARCHAR(4000) NULL
);
GO

CREATE INDEX [IX_Catalog_Course_CourseTypeId] ON [Catalog].[Course]([CourseTypeId]);
GO

CREATE INDEX [IX_Catalog_Course_CourseGroupId] ON [Catalog].[Course]([CourseGroupId]);
GO