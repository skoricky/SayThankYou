-- =============================================
-- Author:		Denis Wilson
-- Create date: 2017-01-23
-- Update date:	2017-03-17
-- Description:	Returns vote count by Account in current month
-- =============================================
CREATE FUNCTION [dbo].[fn_SGI_GetVotesInMonth] 
(
	@AccountName varchar(255),
	@Value varchar(255)
)
RETURNS int
AS	
BEGIN
	DECLARE @VoteCount int;

	SELECT @VoteCount = COUNT(*) 
	FROM SGI_Votes
	WHERE AccountFrom = @AccountName AND Value = @Value AND MONTH(Date) = MONTH(GETDATE());

	RETURN @VoteCount
END