BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Members]') AND [c].[name] = N'Email');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Members] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Members] ALTER COLUMN [Email] nvarchar(450) NOT NULL;

CREATE UNIQUE INDEX [IX_Members_Email] ON [Members] ([Email]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260306135127_AddMemberEmailUniqueIndex', N'10.0.3');

COMMIT;
GO

