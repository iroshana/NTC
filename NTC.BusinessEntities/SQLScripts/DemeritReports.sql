USE [NTC]
GO
/****** Object:  StoredProcedure [dbo].[DeMeritReports]    Script Date: 17/04/2018 21:28:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeMeritReports]
 @colorCode int,
 @createdDateFrom varchar(100),
 @createdDateTo varchar(100),
 @typeId int,
 @orderBy varchar(20)

AS
BEGIN
DECLARE @orderByASC varchar(50), @query varchar(8000)
	SET NOCOUNT ON;

	IF @orderBy IS NULL OR @orderBy = ''
		SET @orderBy = 'ASC'

	SET @query = '; WITH RESULTS AS
    (
SELECT me.ID
	, me.FullName
	,m.Description
	,de.InqueryDate
	,ROW_NUMBER() OVER (ORDER BY me.ID ' + @orderBy + ' ) AS rn
  FROM [dbo].Member me
  inner join dbo.DeMerits de ON  me.ID = de.MemberId
  inner join dbo.[MemberDeMerits] mer ON mer.DeMeritId = de.ID
  inner join dbo.Merits m ON  m.ID = mer.MeritId
  where ('''+CAST((@typeId) AS varchar)+''' = 0 OR me.TypeId = '''+CAST((@typeId) AS varchar)+''') 
  and ('''+CAST((@colorCode) AS varchar)+''' = 0 OR m.ColorCodeId = '''+CAST((@colorCode) AS varchar)+''')
  and mer.Point != 0
  and de.CreatedDate between '''+@createdDateFrom+''' and '''+@createdDateTo+'''
  group by me.ID, me.FullName,m.Description,de.InqueryDate
  )
  SELECT ID
      ,FullName
      ,Description
      ,InqueryDate
    FROM RESULTS a
	ORDER BY rn ASC'
	EXEC (@query)

END
