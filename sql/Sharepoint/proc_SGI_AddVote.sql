-- =============================================
-- Author:		Denis Wilson
-- Create date: 2017-01-23
-- Description:	Add vote to log
-- =============================================
CREATE PROCEDURE [dbo].[proc_SGI_AddVote]
	@AccountFrom varchar(255),   
    @AccountTo varchar(255),
	@Value varchar(255),
	@Comment nvarchar(4000)
AS
BEGIN
	INSERT INTO [dbo].[SGI_Votes]
           ([Date]
           ,[AccountFrom]
           ,[AccountTo]
           ,[Value]
           ,[Comment])
     VALUES
			(GetDate(),
			@AccountFrom,   
			@AccountTo,
			@Value,
			@Comment)

END