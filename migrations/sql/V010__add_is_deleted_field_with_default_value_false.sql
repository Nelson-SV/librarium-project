BEGIN TRANSACTION;
ALTER TABLE [Books] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260306225639_AddIsDeletedFieldWithDefaultValueFalse', N'10.0.3');

COMMIT;
GO

