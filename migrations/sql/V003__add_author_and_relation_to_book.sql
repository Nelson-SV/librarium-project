BEGIN TRANSACTION;
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260304190309_AddAuthorAndRelationToBook', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [Author] (
    [AuthorId] nvarchar(450) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Biography] nvarchar(max) NULL,
    CONSTRAINT [PK_Author] PRIMARY KEY ([AuthorId])
);

CREATE TABLE [BookAuthor] (
    [Id] nvarchar(450) NOT NULL,
    [AuthorId] nvarchar(450) NOT NULL,
    [BookId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_BookAuthor] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BookAuthor_Author_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Author] ([AuthorId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookAuthor_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([BookId]) ON DELETE CASCADE
);

CREATE INDEX [IX_BookAuthor_AuthorId] ON [BookAuthor] ([AuthorId]);

CREATE INDEX [IX_BookAuthor_BookId] ON [BookAuthor] ([BookId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260304190856_UpdatingDbContextToCreateTables', N'10.0.3');

COMMIT;
GO

