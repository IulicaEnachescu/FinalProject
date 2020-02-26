USE [SchoolApp]
GO

INSERT INTO [dbo].[Course]
           ([NumberOfLessons]
           ,[Description]
           ,[Category]
           ,[Language]
           ,[Level]
           ,[StatusActive])
     VALUES
           (6,'Learning Time with Timmy (Children 3-5 Years)','Children','English','A1Beginer',0),
		   (6,'English Primary Infants (Children 6-7 Years)','Children','English','A1Beginer',0),
		   (9,'English Primary Plus (Children 8-10 Years)','Children','English','A1Beginer',0),
		   (9,'English Primary Plus (Children 8-10 Years)','Children','English','A2Elementary',0),
		    (11,'English Lower Secondary B1 (Children 11-14 Years)','Children','English','B1Intermediate',0),
			(11,'English Lower Secondary B2 (Children 11-14 Years)','Children','English','B1Intermediate',0),
			(11,'English Lower Secondary B2 (Children 11-14 Years)','Children','English','B2UpperIntermediate',0),
		   (11,'English Lower Secondary A2 (Teens)','Teens','English','A2Elementary',0),
		   (11,'English Lower Secondary B1 (Teens)','Teens','English','B1Intermediate',0),
		   (11,'English Lower Secondary B2 (Teens)','Teens','English','B2UpperIntermediate',0),
		   (11,'English Lower Secondary C1 (Teens)','Teens','English','B2PreAdvance',0),
		   (11,'General English- Elementary  for A2 Level','Adults','English','A2Elementary',0),
		   (11,'General English- Intermediate  for B1 Level','Adults','English','B1Intermediate',0),
		   (11,'General English- Upper-intermediate  for B2 Level','Adults','English','B1Intermediate',0),
		   (11,'General English- Pre-Advanced for B2 Level','Adults','English','B2PreAdvance',0),
		   (11,'General English- Advanced for C1 Level','Adults','English','C1Advance',0),
		   (11,'Business English- Proficiency for C2 Level','Adults','English','C2Profiency',0)
GO


