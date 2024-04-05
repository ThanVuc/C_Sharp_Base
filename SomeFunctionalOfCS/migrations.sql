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
GO

CREATE TABLE [article] (
    [ArticleId] int NOT NULL IDENTITY,
    [Title] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_article] PRIMARY KEY ([ArticleId])
);
GO

CREATE TABLE [tags] (
    [TagId] nvarchar(20) NOT NULL,
    [Content] ntext NOT NULL,
    CONSTRAINT [PK_tags] PRIMARY KEY ([TagId])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240404151536_initWeb', N'9.0.0-preview.2.24128.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [article] ADD [Description] ntext NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240404152428_update2', N'9.0.0-preview.2.24128.4');
GO

COMMIT;
GO

