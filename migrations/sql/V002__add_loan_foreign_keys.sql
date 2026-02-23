BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Loans]') AND [c].[name] = N'MemberId');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Loans] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Loans] ALTER COLUMN [MemberId] nvarchar(450) NOT NULL;

DECLARE @var1 nvarchar(max);
SELECT @var1 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Loans]') AND [c].[name] = N'BookId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Loans] DROP CONSTRAINT ' + @var1 + ';');
ALTER TABLE [Loans] ALTER COLUMN [BookId] nvarchar(450) NOT NULL;

CREATE INDEX [IX_Loans_BookId] ON [Loans] ([BookId]);

CREATE INDEX [IX_Loans_MemberId] ON [Loans] ([MemberId]);

ALTER TABLE [Loans] ADD CONSTRAINT [FK_Loans_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([BookId]) ON DELETE CASCADE;

ALTER TABLE [Loans] ADD CONSTRAINT [FK_Loans_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([MemberId]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260223212100_AddLoanForeignKeys', N'10.0.3');

COMMIT;
GO

