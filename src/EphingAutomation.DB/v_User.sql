CREATE VIEW [dbo].[v_User]
	AS
	SELECT u.Id
			,u.Active
			,u.Created
			,u.Created_By
			,ud.UserName
			,ud.Domain
			,ud.Email
			,ud.Last_Modified
			,ud.Last_Modified_By
	FROM [User] AS u
	JOIN [User_Details] AS ud ON ud.User_Id = u.Id AND ud.Latest = 1
