BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'Isbn');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Books] DROP COLUMN [Isbn];

EXEC sp_rename N'[Books].[IsbnNew]', N'Isbn', 'COLUMN';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260308131003_RemovedIsbnFieldAndRenamedIsbnnewInBook', N'10.0.3');

COMMIT;
GO

