USE [NTC]
GO
/****** Object:  StoredProcedure [dbo].[DashBoard]    Script Date: 13/04/2018 15:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DashBoard]
AS
BEGIN
DECLARE  @query varchar(8000),@redNoticeMembers int,@redNoticeDrivers int,@redNoticeConductors int,@highestdriverComplain int,@highestconductorComplain int,@hiestDriverPoints int,@hiestconductorPoints int
	SET NOCOUNT ON;

SELECT TOP(1) @redNoticeDrivers = count(ID)
  FROM [dbo].[Member] 
  where(ColorCodeId = 1 )
  and TypeId = 1
  Group By ColorCodeId Order By ColorCodeId;

SELECT TOP(1) @redNoticeConductors = count(ID)
  FROM [dbo].[Member] 
  where(ColorCodeId = 1 )
  and TypeId = 2
  Group By ColorCodeId Order By ColorCodeId;

  SELECT @redNoticeMembers = count(ID)
  FROM [dbo].[Member] 
  where(ColorCodeId = 1 )
  Group By ColorCodeId Order By ColorCodeId;

WITH RESULTS1 AS (SELECT top(1) count(comp.DriverId) as complainCount
  FROM [dbo].[Complains] As comp
  group by comp.DriverId
  Order By complainCount desc)
  SELECT @highestdriverComplain = complainCount from RESULTS1;

WITH RESULTS2 AS (SELECT top(1) count(comp.ConductorId) as complainCount
  FROM [dbo].[Complains] As comp
  group by comp.ConductorId
  Order By complainCount desc)
  SELECT @highestconductorComplain = complainCount from RESULTS2;


WITH RESULTS3 AS (SELECT SUM(dbo.MemberDeMerits.Point) as totalPoints
FROM     dbo.Members LEFT OUTER JOIN
                  dbo.DeMerits ON dbo.DeMerits.MemberId = dbo.Members.ID LEFT OUTER JOIN
                  dbo.MemberDeMerits ON dbo.MemberDeMerits.DeMeritId = dbo.DeMerits.ID LEFT OUTER JOIN
                  dbo.Merits ON dbo.Merits.ID = dbo.MemberDeMerits.MeritId
				  where  dbo.Members.TypeId = 1
				  GROUP BY dbo.Members.ID)
  SELECT @hiestDriverPoints = totalPoints from RESULTS3;

	SET @query = '; WITH RESULTS AS
    (
      SELECT SUM(dbo.MemberDeMerits.Point) as HighestConductorPoints
					FROM     dbo.Members LEFT OUTER JOIN
                  dbo.DeMerits ON dbo.DeMerits.MemberId = dbo.Members.ID LEFT OUTER JOIN
                  dbo.MemberDeMerits ON dbo.MemberDeMerits.DeMeritId = dbo.DeMerits.ID LEFT OUTER JOIN
                  dbo.Merits ON dbo.Merits.ID = dbo.MemberDeMerits.MeritId
				  where  dbo.Members.TypeId = 2
				  GROUP BY dbo.Members.ID

    )
    SELECT HighestConductorPoints
		, '+ CAST(@hiestDriverPoints AS varchar) + ' AS HighestDriverPoints
		, '+ CAST(@redNoticeDrivers AS varchar) + ' AS RedNoticeDrivers
		, '+ CAST(@redNoticeConductors AS varchar) + ' AS RedNoticeConductors
		, '+ CAST(@redNoticeMembers AS varchar) + ' AS RedNoticeMembers
		, '+ CAST(@highestdriverComplain AS varchar) + ' AS HighestDriverComplain
		, '+ CAST(@highestconductorComplain AS varchar) + ' AS HighestConductorComplain
    FROM RESULTS '
	EXEC (@query)
END

