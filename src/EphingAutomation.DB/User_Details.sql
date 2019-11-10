CREATE TABLE [dbo].[User_Details]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [Domain] NVARCHAR(100) NOT NULL, 
    [Last_Modified] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [Last_Modified_By] INT NOT NULL, 
    [Latest] BIT NOT NULL DEFAULT 1, 
    [Email] NVARCHAR(151) NULL, 
    [User_Id] INT NOT NULL, 
    CONSTRAINT [FK_User_Details_User] FOREIGN KEY ([User_Id]) REFERENCES [User]([Id])
)
