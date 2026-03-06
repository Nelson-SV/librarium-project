BEGIN TRANSACTION;
ALTER TABLE [Members] ADD [PhoneNumber] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260306140349_AddMemberPhoneNumberAsNullable', N'10.0.3');

COMMIT;
GO

