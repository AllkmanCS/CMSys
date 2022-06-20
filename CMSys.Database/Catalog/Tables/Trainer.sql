CREATE TABLE [Catalog].[Trainer]
(
    [Id] UNIQUEIDENTIFIER
        CONSTRAINT [PK_Catalog_Trainer] PRIMARY KEY
        CONSTRAINT [FK_Catalog_Trainer_Id_Membership_User] FOREIGN KEY
        REFERENCES [Membership].[User] ([Id]) ON DELETE CASCADE,
    [VisualOrder] INT NOT NULL,
    [TrainerGroupId] UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT [FK_Catalog_Trainer_Catalog_TrainerGroup] FOREIGN KEY
        REFERENCES [Catalog].[TrainerGroup] ([Id]),
    [Description] NVARCHAR(4000) NULL,
);
GO

CREATE INDEX [IX_Catalog_Trainer_TrainerGroupId] ON [Catalog].[Trainer]([TrainerGroupId]);
GO