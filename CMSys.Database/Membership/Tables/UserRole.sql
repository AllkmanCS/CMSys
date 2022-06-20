CREATE TABLE [Membership].[UserRole]
(
    [UserId] UNIQUEIDENTIFIER
        CONSTRAINT [FK_Membership_UserRole_Membership_User] FOREIGN KEY
        REFERENCES [Membership].[User]([Id]) ON DELETE CASCADE,
    [RoleId] UNIQUEIDENTIFIER
        CONSTRAINT [FK_Membership_UserRole_Membership_Role] FOREIGN KEY
        REFERENCES [Membership].[Role]([Id]) ON DELETE CASCADE,
    CONSTRAINT [PK_Membership_UserRole] PRIMARY KEY ([UserId], [RoleId])
);
GO

CREATE INDEX [IX_Membership_UserRole_UserId] ON [Membership].[UserRole]([UserId]);
GO

CREATE INDEX [IX_Membership_UserRole_RoleId] ON [Membership].[UserRole]([RoleId]);
GO