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

IF SCHEMA_ID(N'Wf') IS NULL EXEC(N'CREATE SCHEMA [Wf];');
GO

CREATE TABLE [Wf].[Owner] (
    [IdOwner] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [Photo] nvarchar(max) NULL,
    [Birthday] datetime2 NULL,
    CONSTRAINT [PK_Owner] PRIMARY KEY ([IdOwner])
);
GO

CREATE TABLE [Wf].[Property] (
    [IdProperty] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [Price] decimal(18,2) NOT NULL,
    [CodeInternatal] nvarchar(max) NULL,
    [Year] int NOT NULL,
    [IdOwner] int NOT NULL,
    CONSTRAINT [PK_Property] PRIMARY KEY ([IdProperty]),
    CONSTRAINT [FK_Property_Owner_IdOwner] FOREIGN KEY ([IdOwner]) REFERENCES [Wf].[Owner] ([IdOwner]) ON DELETE CASCADE
);
GO

CREATE TABLE [Wf].[PropertyImage] (
    [IdPropertyImage] int NOT NULL IDENTITY,
    [IdProperty] int NOT NULL,
    [file] nvarchar(max) NULL,
    [Enabled] bit NOT NULL,
    CONSTRAINT [PK_PropertyImage] PRIMARY KEY ([IdPropertyImage]),
    CONSTRAINT [FK_PropertyImage_Property_IdProperty] FOREIGN KEY ([IdProperty]) REFERENCES [Wf].[Property] ([IdProperty]) ON DELETE CASCADE
);
GO

CREATE TABLE [Wf].[PropertyTrace] (
    [IdPropertyTrace] int NOT NULL IDENTITY,
    [Datesale] datetime2 NOT NULL,
    [Name] nvarchar(max) NULL,
    [Value] decimal(18,2) NOT NULL,
    [Tax] decimal(18,2) NOT NULL,
    [IdProperty] int NOT NULL,
    CONSTRAINT [PK_PropertyTrace] PRIMARY KEY ([IdPropertyTrace]),
    CONSTRAINT [FK_PropertyTrace_Property_IdProperty] FOREIGN KEY ([IdProperty]) REFERENCES [Wf].[Property] ([IdProperty]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Property_IdOwner] ON [Wf].[Property] ([IdOwner]);
GO

CREATE INDEX [IX_PropertyImage_IdProperty] ON [Wf].[PropertyImage] ([IdProperty]);
GO

CREATE INDEX [IX_PropertyTrace_IdProperty] ON [Wf].[PropertyTrace] ([IdProperty]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220131193811_initial', N'5.0.11');
GO

COMMIT;
GO

