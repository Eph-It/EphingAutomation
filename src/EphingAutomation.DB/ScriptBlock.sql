CREATE TABLE [dbo].[ScriptBlock]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Created] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [Created_By] INT NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_ScriptBlock_User] FOREIGN KEY ([Created_By]) REFERENCES [User]([Id]) 
)
