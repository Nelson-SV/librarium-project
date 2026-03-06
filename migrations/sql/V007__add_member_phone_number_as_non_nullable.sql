BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Members]') AND [c].[name] = N'PhoneNumber');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Members] DROP CONSTRAINT ' + @var + ';');
UPDATE [Members] SET [PhoneNumber] = N'' WHERE [PhoneNumber] IS NULL;
ALTER TABLE [Members] ALTER COLUMN [PhoneNumber] nvarchar(max) NOT NULL;
ALTER TABLE [Members] ADD DEFAULT N'' FOR [PhoneNumber];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260306141021_AddMemberPhoneNumberAsNonNullable', N'10.0.3');

COMMIT;
GO

