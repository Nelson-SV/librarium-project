IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Books] (
    [BookId] nvarchar(450) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Isbn] bigint NOT NULL,
    [PublicationYear] int NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([BookId])
);

CREATE TABLE [Loans] (
    [LoanId] nvarchar(450) NOT NULL,
    [MemberId] nvarchar(max) NOT NULL,
    [BookId] nvarchar(max) NOT NULL,
    [LoanDate] datetime2 NOT NULL,
    [ReturnDate] datetime2 NULL,
    CONSTRAINT [PK_Loans] PRIMARY KEY ([LoanId])
);

CREATE TABLE [Members] (
    [MemberId] nvarchar(450) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY ([MemberId])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260223210149_InitialSchema', N'10.0.3');

COMMIT;
GO

