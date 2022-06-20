CREATE TABLE [Catalog].[CourseTrainer]
(
    [CourseId] UNIQUEIDENTIFIER
        CONSTRAINT [FK_Catalog_CourseTrainer_Catalog_Course] FOREIGN KEY
        REFERENCES [Catalog].[Course]([Id]) ON DELETE CASCADE,
    [TrainerId] UNIQUEIDENTIFIER
        CONSTRAINT [FK_Catalog_CourseTrainer_Catalog_Trainer] FOREIGN KEY
        REFERENCES [Catalog].[Trainer]([Id]) ON DELETE CASCADE,
    [VisualOrder] INT NOT NULL,
    CONSTRAINT [PK_Catalog_CourseTrainer] PRIMARY KEY ([CourseId], [TrainerId])
);
GO

CREATE INDEX [IX_Catalog_CourseTrainer_CourseId] ON [Catalog].[CourseTrainer]([CourseId]);
GO

CREATE INDEX [IX_Catalog_CourseTrainer_TrainerId] ON [Catalog].[CourseTrainer]([TrainerId]);
GO