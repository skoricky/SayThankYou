CREATE VIEW [dbo].[View_SGI_Votes]
AS
SELECT     dbo.SGI_Votes.Id, dbo.SGI_Votes.Date, dbo.SGI_Votes.AccountFrom, t_from.EmployeeName AS EmployeeNameFrom, dbo.SGI_Votes.AccountTo, t_to.EmployeeName, 
                      dbo.SGI_Votes.Value, dbo.SGI_Votes.Comment
FROM         dbo.SGI_Votes LEFT OUTER JOIN
                      dbo.View_SGI_Staff AS t_from ON dbo.SGI_Votes.AccountFrom = t_from.Account LEFT OUTER JOIN
                      dbo.View_SGI_Staff AS t_to ON dbo.SGI_Votes.AccountTo = t_to.Account