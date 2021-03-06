USE [NTC]
GO
SET IDENTITY_INSERT [dbo].[MemberTypes] ON 

INSERT [dbo].[MemberTypes] ([ID], [Code]) VALUES (1, N'Driver')
INSERT [dbo].[MemberTypes] ([ID], [Code]) VALUES (2, N'Conductor')
SET IDENTITY_INSERT [dbo].[MemberTypes] OFF
SET IDENTITY_INSERT [dbo].[Members] ON 

INSERT [dbo].[Members] ([ID], [UserID], [TypeId], [TrainingCertificateNo], [LicenceNo], [TrainingCenter], [HighestEducation], [JoinDate], [IssuedDate], [ExpireDate], [ImagePath], [FullName], [ShortName], [DOB], [PermanetAddress], [CurrentAddress], [TelNo], [NIC]) VALUES (16, NULL, 1, NULL, NULL, NULL, NULL, CAST(N'2017-02-19 00:00:00.000' AS DateTime), CAST(N'2017-03-08 00:00:00.000' AS DateTime), CAST(N'2017-03-08 00:00:00.000' AS DateTime), NULL, N'Test1', N'A Test', CAST(N'2017-03-08 00:00:00.000' AS DateTime), NULL, NULL, NULL, N'933020115V')
SET IDENTITY_INSERT [dbo].[Members] OFF
SET IDENTITY_INSERT [dbo].[Routes] ON 

INSERT [dbo].[Routes] ([ID], [From], [To], [RouteNo]) VALUES (1, N'Dehiwala', N'Fort', N'159')
SET IDENTITY_INSERT [dbo].[Routes] OFF
SET IDENTITY_INSERT [dbo].[Buses] ON 

INSERT [dbo].[Buses] ([ID], [LicenceNo], [Type], [DriverId], [ConductorId], [RouteId]) VALUES (2, N'NC1234', N'Layland', 16, 16, 1)
SET IDENTITY_INSERT [dbo].[Buses] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (1, N'C1', N'Charging extra fare/ Not returning the balance  ')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (2, N'C2', N'Not issuing tickets properly')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (3, N'C3', N'Being discourteous to passengers')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (4, N'C4', N'Overcrowding the bus surpassing the approved no. of passengers')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (5, N'C5', N'Taking too much time to reach the destination')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (6, N'C6', N'Collecting passengers in midway points by halting the bus in an unnecessary manner')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (7, N'C7', N'Malfunctions in the air conditioners')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (8, N'C8', N'Unfavorable travelling conditions of the bus')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (9, N'C9', N'Reckless driving/Competitive driving/Using cell phone while driving')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (10, N'C10', N'Playing cassettes/radio in unbearable volume')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (11, N'C11', N'Driving without NTC  license')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (12, N'C12', N'Not beginning from the assigned place and not travelling up to the destination')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (13, N'C13', N'Violations of the assigned timetable')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (14, N'C14', N'Not displaying the destination boards in the bus')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (15, N'C15', N'Conflicts between buses')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (16, N'C16', N'Highway issues')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (17, N'C17', N'Not running in the assigned rout')
INSERT [dbo].[Categories] ([ID], [CategoryNo], [Description]) VALUES (18, N'C18', N'Other')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Merits] ON 

INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (2, N'1', N'Not having a valid licenses to run on the particular rout ', 1)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (3, N'2', N'Running from expired licenses ', 1)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (4, N'3', N'Not having valid competency certificate', 1)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (5, N'4', N'Not running in the appointed rout ', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (6, N'5', N'Not having relevant documents to run on the road', 1)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (7, N'6', N'Not having valid license ', 1)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (9, N'7', N'Journey starting and ending should be according to the permit ', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (11, N'8', N'All the passengers should have a ticket ', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (12, N'9', N'Should issue tickets at the beginning of the ride', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (13, N'10', N'Not charging correct fair ', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (14, N'11', N'Every passenger should receive a ticket', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (15, N'12', N'Not displaying the bus fare notice', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (16, N'13', N'Not running according to the assigned timetable', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (17, N'14', N'Overcrowding the bus surpassing the approved no. of passengers', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (18, N'15', N'Collecting passengers in midway points', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (19, N'16', N'Number of standing passengers should not exceeded the allowed number ', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (20, N'17', N'Front and back of the bus should not have unnecessary stickers ', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (21, N'18', N'Windows should not be tinted, painted or covered with stickers', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (22, N'19', N'Approved destination sticker should be there with the rout number', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (23, N'20', N'Front and back of the bus should have approved  NTC/SLTB sticker ', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (24, N'21', N'Bus should be in good travelling conditions', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (25, N'22', N'Malfunctions in the air conditioners', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (26, N'23', N'Not having Assigned number of seats', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (27, N'24', N'Front and back door of the bus should be there and those should be closed when bus is running ', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (28, N'25', N'Unapproved notices should not be displayed ', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (29, N'26', N'Not having Easily used, working and reachable bell ', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (30, N'27', N'Smoking is strictly prohibited inside the bus', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (31, N'28', N'Seat allocations should be displayed', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (32, N'29', N'Start and arrival time should be displayed inside the bus', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (33, N'30', N'Bus registration number should display inside the bus', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (34, N'31', N'Driver ID and conductor ID should display inside the bus', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (35, N'32', N'Should give support to flying squad when they checking the bus', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (36, N'33', N'Registered conductor should be on the ride and conductor must wear his ID card', 1)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (37, N'34', N'Driver and conductor should be in their uniforms', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (38, N'35', N'Registered driver should be on the ride and driver must wear his ID card', 1)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (39, N'36', N'Having valid driving license ', 1)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (40, N'37', N'Having valid driving medical certificate ', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (41, N'38', N'Only a radio is allowed to play inside the bus', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (42, N'39', N'First-aid box should be inside the bus', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (43, N'40', N'Bus should have a Fire alarm and extinguisher ', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (44, N'41', N'Bus owners name and address should be displayed ', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (45, N'42', N'Selling and begging inside the bus is not allowed', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (46, N'43', N'Should not pump fuel on the ride', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (47, N'44', N'Well maintained log book should be available', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (48, N'45', N'Private  trips are not allowed without a special permission', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (49, N'46', N'Should be polite to passengers', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (50, N'47', N'Charging extra fare/ Not returning the balance  ', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (51, N'48', N'Not issuing tickets properly', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (52, N'49', N'Being discourteous to passengers', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (53, N'50', N'Overcrowding the bus surpassing the approved no. of passengers', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (54, N'51', N'Taking too much time to reach the destination', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (55, N'52', N'Collecting passengers in midway points by halting the bus in an unnecessary manner', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (56, N'53', N'Malfunctions in the air conditioners', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (57, N'54', N'Unfavorable travelling conditions of the bus', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (58, N'55', N'Reckless driving/Competitive driving/Using cell phone while driving', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (59, N'56', N'Playing cassettes/radio in unbearable volume', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (61, N'57', N'Driving without NTC  license', 1)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (62, N'58', N'Not beginning from the assigned place and not travelling up to the destination', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (63, N'59', N'Violations of the assigned timetable', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (64, N'60', N'Not displaying the destination boards in the bus', 2)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (65, N'61', N'Conflicts between buses', 4)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (66, N'62', N'Highway issues', 3)
INSERT [dbo].[Merits] ([ID], [Code], [Description], [ColorCodeId]) VALUES (67, N'63', N'Not running in the assigned rout', 2)
SET IDENTITY_INSERT [dbo].[Merits] OFF
SET IDENTITY_INSERT [dbo].[Officers] ON 

INSERT [dbo].[Officers] ([ID], [Name], [TelNo], [NIC]) VALUES (1, N'TestA', N'1224554554', N'934584215V')
SET IDENTITY_INSERT [dbo].[Officers] OFF
