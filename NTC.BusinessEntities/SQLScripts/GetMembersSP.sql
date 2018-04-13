USE [NTC]
GO
/****** Object:  StoredProcedure [dbo].[GetMembers]    Script Date: 13/04/2018 15:00:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMembers]  
 @colorCode int,
 @createdDateFrom varchar(100),
 @createdDateTo varchar(100),
 @typeId int
AS
BEGIN

	DECLARE @query varchar(8000)
	SET NOCOUNT ON;

	if @createdDateFrom IS null OR @createdDateFrom = ''
	SET @createdDateFrom = ''
	if @createdDateTo IS null OR @createdDateTo = ''
	SET @createdDateTo = ''

	SELECT ID, FullName ,TrainingCenter,TrainingCertificateNo, count(Point) as Points
  FROM [dbo].[Member] 
  where(@colorCode = 0 OR ColorCodeId = @colorCode )
  and (@createdDateFrom = '' OR CreatedDate between @createdDateFrom and @createdDateTo)
  and (@typeId = 0 OR TypeId = @typeId)
  group By FullName,TrainingCenter,TrainingCertificateNo,ID

END
