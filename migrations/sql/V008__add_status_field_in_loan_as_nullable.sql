BEGIN TRANSACTION;
ALTER TABLE [Loans] ADD [Status] nvarchar(max) NULL;

UPDATE Loans SET Status = 'Returned' WHERE ReturnDate IS NOT NULL

UPDATE Loans SET Status = 'Active' WHERE ReturnDate IS NULL

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260306204756_AddStatusFieldInLoanAsNullable', N'10.0.3');

COMMIT;
GO

