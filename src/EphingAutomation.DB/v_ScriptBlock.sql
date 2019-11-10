CREATE VIEW [dbo].[v_ScriptBlock]
	AS 
	SELECT SB.Id
			,SB.Active
			,SB.Created
			,SB.Created_By
			,SBD.Name
			,SBD.Description
			,SBD.Last_Modified
			,SBD.Last_Modified_By
			,SBC.Code
	FROM [ScriptBlock] AS SB
	JOIN [ScriptBlock_Details] AS SBD ON SBD.ScriptBlock_Id = SB.Id AND SBD.Latest = 1
	JOIN [ScriptBlock_Code] AS SBC ON SBC.Id = SBD.Code_Id
