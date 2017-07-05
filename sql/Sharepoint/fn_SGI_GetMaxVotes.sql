-- =============================================
-- Author:		Denis Wilson
-- Create date: 2017-05-15
-- Description:	Returns max votes for account
-- =============================================
CREATE FUNCTION [dbo].[fn_SGI_GetMaxVotes] 
(
	@AccountName varchar(255)
)
RETURNS int
AS	
BEGIN
	DECLARE @MaxVotes int
	
	DECLARE @PositionLevel varchar(255)

	SELECT @PositionLevel = Value
	FROM 
	(
		SELECT pref.value('(text())[1]', 'nvarchar(255)') AS Account,
			pref2.value('(text())[1]', 'nvarchar(255)') AS Value
		FROM
			[dbo].[UserData] 
				CROSS APPLY
			tp_ColumnSet.nodes('/nvarchar21') AS Account(pref)
				CROSS APPLY
			tp_ColumnSet.nodes('/nvarchar31') AS Value(pref2)
		WHERE tp_ListId = 'F239CEB0-A683-411B-9378-D2EF15A85D54'
	) t
	WHERE Account = @AccountName


	IF @PositionLevel = 'Director'
		SET @MaxVotes = 4
	ELSE IF @PositionLevel = 'Head of Department'
		SET @MaxVotes = 4
	ELSE IF @PositionLevel = 'Head of Unit'
		SET @MaxVotes = 4
	ELSE
		SET @MaxVotes = 4;
	
	RETURN @MaxVotes
END