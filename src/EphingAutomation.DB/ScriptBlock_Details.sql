CREATE TABLE [dbo].[ScriptBlock_Details]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[ScriptBlock_Id] INT NOT NULL, 
    [Name] NVARCHAR(150) NOT NULL, 
    [Description] NVARCHAR(200) NULL, 
    [Last_Modified_By] INT NOT NULL, 
    [Last_Modified] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [Code_Id] INT NOT NULL, 
    [Latest] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_ScriptBlock_Details_User] FOREIGN KEY ([Last_Modified_By]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_ScriptBlock_Details_ScriptBlock_Code] FOREIGN KEY ([Code_Id]) REFERENCES [ScriptBlock_Code]([Id]), 
    CONSTRAINT [FK_ScriptBlock_Details_ScriptBlock] FOREIGN KEY ([ScriptBlock_Id]) REFERENCES [ScriptBlock]([Id]) 
)
