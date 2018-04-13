USE [NTC]
GO

/****** Object:  View [dbo].[Member]    Script Date: 13/04/2018 17:57:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Member]
AS
SELECT dbo.Members.ID, dbo.Members.FullName, dbo.Members.TrainingCenter, dbo.Members.TrainingCertificateNo, dbo.Merits.ColorCodeId, dbo.DeMerits.CreatedDate, dbo.Members.TypeId, dbo.MemberDeMerits.Point, dbo.Members.NTCNo, 
                  dbo.Members.NIC, dbo.MemberTypes.Code
FROM     dbo.Members INNER JOIN
                  dbo.MemberTypes ON dbo.Members.TypeId = dbo.MemberTypes.ID LEFT OUTER JOIN
                  dbo.DeMerits ON dbo.DeMerits.MemberId = dbo.Members.ID LEFT OUTER JOIN
                  dbo.MemberDeMerits ON dbo.MemberDeMerits.DeMeritId = dbo.DeMerits.ID LEFT OUTER JOIN
                  dbo.Merits ON dbo.Merits.ID = dbo.MemberDeMerits.MeritId

GO


