BEGIN TRANSACTION;
ALTER TABLE [Books] ADD [IsbnNew] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260308114219_AddIsbnNewFieldInBookAsNullable', N'10.0.3');

COMMIT;
GO

