CREATE VIEW [dbo].[View_SGI_Staff]
AS
SELECT     tp_ColumnSet, tp_ColumnSet.value('/nvarchar1[1]', 'nvarchar(max)') AS EmployeeName, tp_ColumnSet.value('/nvarchar9[1]', 'nvarchar(max)') AS Department, 
                      tp_ColumnSet.value('/nvarchar21[1]', 'nvarchar(max)') AS Account
FROM         dbo.UserData
WHERE     (tp_ListId = 'F239CEB0-A683-411B-9378-D2EF15A85D54')