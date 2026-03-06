BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Loans]') AND [c].[name] = N'Status');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Loans] DROP CONSTRAINT ' + @var + ';');
UPDATE [Loans] SET [Status] = N'' WHERE [Status] IS NULL;
ALTER TABLE [Loans] ALTER COLUMN [Status] nvarchar(max) NOT NULL;
ALTER TABLE [Loans] ADD DEFAULT N'' FOR [Status];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260306212830_ChangedStatusFieldInLoanToNonNullable', N'10.0.3');

COMMIT;
GO

