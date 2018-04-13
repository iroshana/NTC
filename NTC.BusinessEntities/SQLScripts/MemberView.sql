USE [NTC]
GO

/****** Object:  View [dbo].[Member]    Script Date: 13/04/2018 15:00:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Member]
AS
SELECT dbo.Members.ID, dbo.Members.FullName, dbo.Members.TrainingCenter, dbo.Members.TrainingCertificateNo, dbo.Merits.ColorCodeId, dbo.DeMerits.CreatedDate, dbo.Members.TypeId, dbo.MemberDeMerits.Point
FROM     dbo.Members LEFT OUTER JOIN
                  dbo.DeMerits ON dbo.DeMerits.MemberId = dbo.Members.ID LEFT OUTER JOIN
                  dbo.MemberDeMerits ON dbo.MemberDeMerits.DeMeritId = dbo.DeMerits.ID LEFT OUTER JOIN
                  dbo.Merits ON dbo.Merits.ID = dbo.MemberDeMerits.MeritId

GO


