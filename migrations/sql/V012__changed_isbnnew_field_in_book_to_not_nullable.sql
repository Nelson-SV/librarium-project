BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'IsbnNew');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT ' + @var + ';');
UPDATE [Books] SET [IsbnNew] = N'' WHERE [IsbnNew] IS NULL;
ALTER TABLE [Books] ALTER COLUMN [IsbnNew] nvarchar(max) NOT NULL;
ALTER TABLE [Books] ADD DEFAULT N'' FOR [IsbnNew];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260308130545_ChangedIsbnNewFieldInBookToNotNullable', N'10.0.3');

COMMIT;
GO

