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

CREATE TABLE [Cliente] (
    [Id] int NOT NULL IDENTITY,
    [Nome] VARCHAR(80) NOT NULL,
    [Telefone] CHAR(11) NULL,
    [CEP] CHAR(8) NOT NULL,
    [Estado] CHAR(2) NOT NULL,
    [Cidade] nvarchar(60) NOT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Produto] (
    [Id] int NOT NULL IDENTITY,
    [CodigoBarras] VARCHAR(14) NOT NULL,
    [Descricao] VARCHAR(60) NULL,
    [valor] decimal(18,2) NOT NULL,
    [TipoProduto] nvarchar(max) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Produto] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pedido] (
    [Id] int NOT NULL IDENTITY,
    [ClienteID] int NOT NULL,
    [IniciadoEm] datetime2 NOT NULL DEFAULT (GETDATE()),
    [FinalizadoEm] datetime2 NOT NULL,
    [TipoFrete] int NOT NULL,
    [StatusPedido] nvarchar(max) NOT NULL,
    [Observacao] VARCHAR(512) NULL,
    CONSTRAINT [PK_Pedido] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pedido_Cliente_ClienteID] FOREIGN KEY ([ClienteID]) REFERENCES [Cliente] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PedidoItem] (
    [Id] int NOT NULL IDENTITY,
    [PedidoId] int NOT NULL,
    [ProdutoId] int NOT NULL,
    [Quantidade] int NOT NULL DEFAULT 1,
    [Valor] decimal(18,2) NOT NULL,
    [Desconto] decimal(18,2) NOT NULL,
    [PedidoItemId] int NULL,
    CONSTRAINT [PK_PedidoItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PedidoItem_Pedido_PedidoId] FOREIGN KEY ([PedidoId]) REFERENCES [Pedido] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PedidoItem_PedidoItem_PedidoItemId] FOREIGN KEY ([PedidoItemId]) REFERENCES [PedidoItem] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PedidoItem_Produto_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produto] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [idx_cliente_telefone] ON [Cliente] ([Telefone]);
GO

CREATE INDEX [IX_Pedido_ClienteID] ON [Pedido] ([ClienteID]);
GO

CREATE INDEX [IX_PedidoItem_PedidoId] ON [PedidoItem] ([PedidoId]);
GO

CREATE INDEX [IX_PedidoItem_PedidoItemId] ON [PedidoItem] ([PedidoItemId]);
GO

CREATE INDEX [IX_PedidoItem_ProdutoId] ON [PedidoItem] ([ProdutoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210323013325_PrimeiraMigracao', N'5.0.4');
GO

COMMIT;
GO

