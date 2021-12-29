-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE TRIGGER dbo.InsertBlogIfNotExists 
   ON  dbo.Blogs 
   INSTEAD OF INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO Blogs (Name, User_Id) 
	SELECT inserted.Name, Inserted.User_Id
	From inserted
	WHERE NOT EXISTS (SELECT * FROM Blogs WITH(UPDLOCK) WHERE Inserted.Name=Blogs.Name)	
	IF ROWCOUNT_BIG()=0
	BEGIN
		ROLLBACK TRANSACTION; 
		THROW 51001, 'Blog name must be uniquie', 1
	END
	ELSE
	BEGIN
	 SELECT [Id], [Name], [User_Id] FROM Blogs WHERE Id=SCOPE_IDENTITY();
	END;
END
GO
